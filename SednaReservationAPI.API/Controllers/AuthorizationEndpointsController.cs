using MediatR;
using Microsoft.AspNetCore.Mvc;
using SednaReservationAPI.Application.Features.Commands.AuthorizationEndPoint.AssingRoleEndPoint;
using SednaReservationAPI.Application.Features.Queries.AuthorizationEndPoint.GetRolesToEndPoint;

namespace SednaReservationAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizationEndpointsController : ControllerBase
    {
        IMediator _mediator;

        public AuthorizationEndpointsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> AssingRole(AssingRoleEndPointCommandRequest assingRoleEndPointCommandRequest)
        {
            assingRoleEndPointCommandRequest.Type = typeof(Program);
            AssingRoleEndPointCommandResponse resp = await _mediator.Send(assingRoleEndPointCommandRequest);
            return Ok(resp);
        }

        [HttpPost("get-roles-to-endpoint")]
        public async Task<IActionResult> getRolestoEndpoint(GetRolesToEndPointQueryRequest endPointQueryRequest)
        { 
            GetRolesToEndPointQueryResponse response= await _mediator.Send(endPointQueryRequest);
            return Ok(response);
        }
       
    }
}
