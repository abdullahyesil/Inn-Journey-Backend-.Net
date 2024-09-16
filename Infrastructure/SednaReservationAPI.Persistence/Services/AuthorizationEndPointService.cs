using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SednaReservationAPI.Application.Abstractions.Services;
using SednaReservationAPI.Application.Abstractions.Services.Configurations;
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
    public class AuthorizationEndPointService : IAuthorizationEndPointService
    {
        readonly IApplicationService _applicationService;
        readonly IEndPointReadRepository _endPointReadRepository;
        readonly IEndPointWriteRepository _endPointWriteRepository;
        readonly IMenuReadRepository _menuReadRepository;
        readonly IMenuWriteRepository _menuWriteRepository;
        readonly RoleManager<AppRole> _roleManager;
        public AuthorizationEndPointService(IApplicationService applicationService, IEndPointReadRepository endPointReadRepository, IEndPointWriteRepository endPointWriteRepository, IMenuReadRepository menuReadRepository, IMenuWriteRepository menuWriteRepository, RoleManager<AppRole> roleManager)
        {
            _applicationService = applicationService;
            _endPointReadRepository = endPointReadRepository;
            _endPointWriteRepository = endPointWriteRepository;
            _menuReadRepository = menuReadRepository;
            _menuWriteRepository = menuWriteRepository;
            _roleManager = roleManager;
        }

        public async Task AssingRoleEndPointAsync(string[] roles, string menu,string code, Type type)
        {



            Menu _menu= await _menuReadRepository.GetSingleAsync(m=> m.Name==menu);
            if (_menu == null) {

                _menu = new()
                {
                    Id = Guid.NewGuid(),
                    Name = menu,
                };
                await _menuWriteRepository.AddAsync(_menu);

                await _menuWriteRepository.SaveAsync();
            }

        


          Endpoint? endpoint= await _endPointReadRepository.Table.Include(e=> e.Menu).Include(e => e.Roles).FirstOrDefaultAsync(e=> e.Code == code && e.Menu.Name == menu);

            if (endpoint == null) {
               var action = _applicationService.GetAuthorizeDefiniationEndPoints(type).FirstOrDefault(m => m.MenuName == menu)
                    ?.Actions.FirstOrDefault(e => e.Code == code);
                

                endpoint = new()
                {
                    Code = action.Code,
                    ActionType = action.ActionType,
                    HttpType = action.HttpType,
                    Definiation = action.Definition,
                    Id = Guid.NewGuid(),
                    Menu = _menu,
                };

                await _endPointWriteRepository.AddAsync(endpoint);
                await _endPointWriteRepository.SaveAsync();
            }
            foreach (var role in endpoint.Roles)
            {
                endpoint.Roles.Remove(role);
            }
            var appRoles = await _roleManager.Roles.Where(r => roles.Contains(r.Name)).ToListAsync();

            foreach (var role in appRoles)
            {
                 
                endpoint.Roles.Add(role);
            }

            await _endPointWriteRepository.SaveAsync();


        }

        public async Task<List<string>> GetRolesToEndPointAsync(string code, string menu)
        {
          Endpoint? endpoint= await _endPointReadRepository.Table.Include(e => e.Roles).Include(e=> e.Menu).FirstOrDefaultAsync(e=> e.Code == code && e.Menu.Name == menu);
        
            if(endpoint == null)
            {
                return null;
            }
           return endpoint.Roles.Select(r=> r.Name).ToList();
        }
    }
}
