using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SednaReservationAPI.Application.Abstractions.Services;
using SednaReservationAPI.Application.DTOs.Role;
using SednaReservationAPI.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SednaReservationAPI.Persistence.Services
{
    public class RoleServices : IRoleServices
    {
        readonly RoleManager<AppRole> _roleManager;

        public RoleServices(RoleManager<AppRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<bool> CreateRole(string name)
        {
          IdentityResult result= await _roleManager.CreateAsync(new() { Id= Guid.NewGuid().ToString(), Name = name });
            return result.Succeeded;
          
        }

        public async Task<bool> DeleteRole(string Id)
        {
            AppRole role= await _roleManager.FindByIdAsync(Id);

          IdentityResult result = await _roleManager.DeleteAsync(role);
            return result.Succeeded;

        }

        public async  Task<List<RoleDto>> GetAllRoles()
        {
            // Rolleri al
            var roles = await _roleManager.Roles.ToListAsync(); // IQueryable to List

            // RoleDto listesine dönüştür
            var roleDtos = roles.Select(role => new RoleDto
            {
                Id = role.Id,
                Name = role.Name
                // Diğer gerekli özellikleri ekleyin
            }).ToList();

            return roleDtos;
        }

        public async Task<RoleDto> GetRoleById(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            return new() { Id = role.Id, Name= role.Name };

        }

        public async Task<bool> UpdateRole(string id)
        {
            AppRole appRole = await _roleManager.FindByIdAsync(id);
           IdentityResult result= await _roleManager.UpdateAsync(appRole);
            return result.Succeeded;
        }
    }
}
