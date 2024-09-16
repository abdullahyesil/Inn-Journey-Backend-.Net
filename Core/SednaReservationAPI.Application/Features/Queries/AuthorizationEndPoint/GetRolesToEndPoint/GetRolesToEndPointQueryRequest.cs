using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SednaReservationAPI.Application.Features.Queries.AuthorizationEndPoint.GetRolesToEndPoint
{
    public class GetRolesToEndPointQueryRequest:IRequest<GetRolesToEndPointQueryResponse>
    {
        public string Code { get; set; }
        public string Menu { get; set; }


    }
}
