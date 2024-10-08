﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SednaReservationAPI.Application.Features.Queries.Role.GetRolesByUserId
{
    public class GetRolesByUserIdQueryRequest:IRequest<GetRolesByUserIdQueryResponse>
    {
        public string userId { get; set; }
    }
}
