using MediatR;
using SednaReservationAPI.Application.Abstractions.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SednaReservationAPI.Application.Features.Queries.Role.GetRolesByUserId
{
    public class GetRolesByUserIdQueryHandler : IRequestHandler<GetRolesByUserIdQueryRequest, GetRolesByUserIdQueryResponse>
    {
        readonly IUserService _userService;

        public GetRolesByUserIdQueryHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<GetRolesByUserIdQueryResponse> Handle(GetRolesByUserIdQueryRequest request, CancellationToken cancellationToken)
        {

            var roles = await _userService.GetRolesToUserAsync(request.userId);

            return new()
            {
                Roles = roles
            };
        }
    }
}
