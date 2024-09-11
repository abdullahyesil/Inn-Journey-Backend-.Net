using MediatR;
using SednaReservationAPI.Application.Abstractions.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SednaReservationAPI.Application.Features.Commands.AuthorizationEndPoint.AssingRoleEndPoint
{
    public class AssingRoleEndPointCommandHandler : IRequestHandler<AssingRoleEndPointCommandRequest, AssingRoleEndPointCommandResponse>
    {
        readonly IAuthorizationEndPointService _authorizationEndPointService;

        public AssingRoleEndPointCommandHandler(IAuthorizationEndPointService authorizationEndPointService)
        {
            _authorizationEndPointService = authorizationEndPointService;
        }

        public async Task<AssingRoleEndPointCommandResponse> Handle(AssingRoleEndPointCommandRequest request, CancellationToken cancellationToken)
        {
          await  _authorizationEndPointService.AssingRoleEndPointAsync(request.Roles, request.Menu, request.Code, request.Type);

            return new()
            {
            };
        }
    }
}
