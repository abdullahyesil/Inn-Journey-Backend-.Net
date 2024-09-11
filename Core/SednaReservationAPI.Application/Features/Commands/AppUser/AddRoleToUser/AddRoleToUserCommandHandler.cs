using MediatR;
using SednaReservationAPI.Application.Abstractions.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SednaReservationAPI.Application.Features.Commands.AppUser.AddRoleToUser
{
    public class AddRoleToUserCommandHandler : IRequestHandler<AddRoleToUserCommandRequest, AddRoleToUserCommandResponse>
    {
        readonly IUserService _userService;

        public AddRoleToUserCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<AddRoleToUserCommandResponse> Handle(AddRoleToUserCommandRequest request, CancellationToken cancellationToken)
        {
         bool resp=await _userService.AddRoleToUser(request.userId, request.Roles);

            if (resp == true) {

                return new AddRoleToUserCommandResponse(){ Succeed = true, Message = "Role başarıyla eklendi" };
            }
            else {
                return new AddRoleToUserCommandResponse() { Succeed = false, Message = "Role ekleme başarısız" };
            }
        }
    }
}
