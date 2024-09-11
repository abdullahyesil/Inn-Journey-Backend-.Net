using SednaReservationAPI.Application.DTOs.User;
using SednaReservationAPI.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SednaReservationAPI.Application.Abstractions.Services
{
    public interface IUserService
    {
        Task<CreateUserResponse> CreateAsync(CreateUser user);
        Task UpdateRefreshToken(string refreshToken, AppUser user, DateTime accessTokenDate, int addOnAccessToken);
        Task<List<ListUser>> GetAllUserAsync();
        Task<AppUser> getByIdUser(string id);
        Task<List<AppUser>> getByIdUsers(IEnumerable<string> ids);
        Task<AppUser> UpdateUser(UpdateUser user);
        Task<bool> AddRoleToUser(string id, List<string> roles);
        Task<bool> RemoveRoleFromUser(string id, List<AppRole> roles);



    }
}
