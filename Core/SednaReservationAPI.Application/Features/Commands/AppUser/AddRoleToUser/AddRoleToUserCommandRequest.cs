using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SednaReservationAPI.Application.Features.Commands.AppUser.AddRoleToUser
{
    public class AddRoleToUserCommandRequest:IRequest<AddRoleToUserCommandResponse>
    {
        public string userId { get; set; }
        public List<string> Roles{ get; set; }
    }
}
