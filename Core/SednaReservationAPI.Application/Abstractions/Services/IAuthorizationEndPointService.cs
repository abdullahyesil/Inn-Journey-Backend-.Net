using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SednaReservationAPI.Application.Abstractions.Services
{
    public interface IAuthorizationEndPointService
    {
        public Task AssingRoleEndPointAsync(string[] roles ,string menu, string code, Type type);
        public Task<List<string>> GetRolesToEndPointAsync(string code, string menu);
    }
}
