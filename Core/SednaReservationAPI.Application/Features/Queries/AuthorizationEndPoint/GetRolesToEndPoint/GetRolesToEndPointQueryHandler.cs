using MediatR;
using SednaReservationAPI.Application.Abstractions.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SednaReservationAPI.Application.Features.Queries.AuthorizationEndPoint.GetRolesToEndPoint
{
    public class GetRolesToEndPointQueryHandler : IRequestHandler<GetRolesToEndPointQueryRequest, GetRolesToEndPointQueryResponse>
    { IAuthorizationEndPointService _authorizationEndPointService;

        public GetRolesToEndPointQueryHandler(IAuthorizationEndPointService authorizationEndPointService)
        {
            _authorizationEndPointService = authorizationEndPointService;
        }

        public async Task<GetRolesToEndPointQueryResponse> Handle(GetRolesToEndPointQueryRequest request, CancellationToken cancellationToken)
        {
            var data = await _authorizationEndPointService.GetRolesToEndPointAsync(request.Code, request.Menu);
            return new() {
                Roles =data
            };
        }
    }
}
