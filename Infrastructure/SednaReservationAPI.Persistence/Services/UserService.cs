﻿using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SednaReservationAPI.Application.Abstractions.Services;
using SednaReservationAPI.Application.DTOs.User;
using SednaReservationAPI.Application.Exceptions;
using SednaReservationAPI.Application.Features.Commands.AppUser.CreateAppUser;
using SednaReservationAPI.Application.Repositories;
using SednaReservationAPI.Domain.Entities;
using SednaReservationAPI.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SednaReservationAPI.Persistence.Services
{
    public class UserService : IUserService
    {
        readonly UserManager<Domain.Entities.Identity.AppUser> _userManager;
        readonly IEndPointReadRepository _endPointReadRepository;



        public UserService(UserManager<AppUser> userManager, IEndPointReadRepository endPointReadRepository)
        {
            _userManager = userManager;
            _endPointReadRepository = endPointReadRepository;
        }

        public async Task<CreateUserResponse> CreateAsync(CreateUser user)
        {
            IdentityResult result = await _userManager.CreateAsync(new()
            {
                Id = Guid.NewGuid().ToString(),
                UserName = user.UserName,
                Name = user.Name,
                Email = user.Email,
                PhoneNumber = user.Phone,
                Age = user.Age,
                Gender = user.Gender
            }, user.Password);

            CreateUserResponse response = new() { Success = result.Succeeded };

            if (result.Succeeded)
                response.Message = "User Created Successfully!";
            else
                foreach (var error in result.Errors)
                    response.Message += $"{error.Code} - {error.Description}\n";

            return response;
        }

        public async Task UpdateRefreshToken(string refreshToken, AppUser user, DateTime accessTokenDate, int accessTokenAddOn)
        {
            if (user != null)
            {
                user.RefreshToken = refreshToken;
                user.RefreshTokenExpirationDate = accessTokenDate.AddMinutes(accessTokenAddOn);
                await _userManager.UpdateAsync(user);
            }
            else
                throw new NotFoundUserException();
        }

        public async Task<List<GetUser>> GetAsync()
        {
            var users = await _userManager.Users.ToListAsync();
            var userDtos = new List<GetUser>();

            foreach (var user in users)
            {
                userDtos.Add(new GetUser
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Name = user.Name,
                    Email = user.Email,
                    Phone = user.PhoneNumber,
                    Age = user.Age,
                    Gender = user.Gender
                });
            }

            return userDtos;
        }

        public async Task<List<ListUser>> GetAllUserAsync(int page, int size, string? value)
        {
            var query = _userManager.Users.AsQueryable();  // Sorgu başlangıcı

            // Eğer arama değeri varsa, filtre uygula
            if (!string.IsNullOrEmpty(value))
            {
                var lowValue = value.ToLower();
                query = query.Where(i => (i.UserName != null && i.UserName.ToLower().Contains(lowValue)) ||
                                         (i.Name != null && i.Name.ToLower().Contains(lowValue)));
            }

            // Toplam kullanıcı sayısını hesapla (filtreli veya filtresiz)
            var totalCount = await query.CountAsync();

            // Sayfalama uygula ve kullanıcıları çek
            var users = await query.Skip(page * size)
                                   .Take(size)
                                   .ToListAsync();

            // Kullanıcıları DTO'ya (ListUser) dönüştür
            var listUsers = users.Select(user => new ListUser
            {
                Id = user.Id,
                Email = user.Email,
                Name = user.Name,
                UserName = user.UserName,
                Phone = user.PhoneNumber,
                Age = user.Age,
                Gender = user.Gender.HasValue ? user.Gender.Value : false, // Nullable kontrolü
                TotalCount = totalCount
            }).ToList();

            return listUsers;
        }


        public Task<AppUser> getByIdUser(string id)
        {
            var user = _userManager.FindByIdAsync(id);

            return user;
        }

        public async Task<List<AppUser>> getByIdUsers(IEnumerable<string> ids)
        {
            var users = new List<AppUser>();

            foreach (var id in ids)
            {
                var user = await _userManager.FindByIdAsync(id);
                if (user != null)
                {
                    users.Add(user);
                }
            }

            return users;
        }



        public async Task<AppUser> UpdateUser(UpdateUser user)
        {
            // Kullanıcıyı Id'ye göre bul
            AppUser existingUser = await _userManager.FindByIdAsync(user.Id);
            if (existingUser != null)
            {
                existingUser.Name = user.Name;
                existingUser.UserName = user.UserName;
                existingUser.Email = user.Email;
                existingUser.PhoneNumber = user.Phone;
                existingUser.Gender = user.Gender;
                existingUser.Age = user.Age;
            }
            var result = await _userManager.UpdateAsync(existingUser);

            if (result.Succeeded && !string.IsNullOrEmpty(user.Password) && !string.IsNullOrEmpty(user.PasswordConfirm))
            {
                await _userManager.RemovePasswordAsync(existingUser);  //kullanıcınını parolasını sildik
                await _userManager.AddPasswordAsync(existingUser, user.Password); //modelden gelen passwordu user daki passworda aktardık
            }

            
            if (result.Succeeded)
            {
                return existingUser;
            }
            else
            {
                // Handle update failure
                throw new Exception($"Update failed for user {user.Id}: {string.Join("\n", result.Errors.Select(e => $"{e.Code} - {e.Description}"))}");
            }
        }

        public async Task<bool> AddRoleToUser(string id, List<string> roles)
        {
            AppUser? user = await _userManager.FindByIdAsync(id);
            if (user == null) {
                return await Task.FromResult(false);
            }
            await _userManager.RemoveFromRolesAsync(user, await _userManager.GetRolesAsync(user));
            if (roles != null)
            {
                await _userManager.AddToRolesAsync(user, roles);
                
                return await Task.FromResult(true);
            }
            return await Task.FromResult(false);


        }

        public Task<bool> RemoveRoleFromUser(string id, List<AppRole> roles)
        {
            return Task.FromResult(true);
        }

        public async Task<bool> hasRolePermissionToEndPointAsync(string name, string code)
        {
             var userRoles= await GetRolesToUserAsync(name);
            if (!userRoles.Any())
                return false;

            Endpoint? endpoint = await _endPointReadRepository.Table.Include(e => e.Roles).FirstOrDefaultAsync(e => e.Code == code);
            if (endpoint == null)
                return false;
            var hasRole = false;
            var endpointRoles = endpoint.Roles.Select(r => r.Name);
  

            foreach (var role in userRoles) {

                foreach (var endoint in endpointRoles)
                {
                    if (userRoles == endpointRoles)
                        return true;
                }
            }
            return false;

        }

        public async Task<string[]> GetRolesToUserAsync(string userIdOrName)
        {

            AppUser user = await _userManager.FindByIdAsync(userIdOrName);
            if (user == null) {
                user = await _userManager.FindByNameAsync(userIdOrName); 
            }

            if (user != null) {
            var userRoles = await _userManager.GetRolesAsync(user);
                return userRoles.ToArray();
            }

            return new string[]{};
        }


    }
    
}
