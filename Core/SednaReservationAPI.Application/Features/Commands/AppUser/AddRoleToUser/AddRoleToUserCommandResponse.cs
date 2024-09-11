using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SednaReservationAPI.Application.Features.Commands.AppUser.AddRoleToUser
{
    public class AddRoleToUserCommandResponse
    {
        public bool Succeed { get; set; }
        public string Message { get; set; }
    }
}
