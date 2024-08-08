﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SednaReservationAPI.Application.Features.Queries.Room.GetRoomByHotelId
{
    public class GetRoomByHotelIdQueryResponse
    {
        public string? Id { get; set; }
        public string HotelId { get; set; }
        public string RoomTypeId { get; set; }
        public decimal? AdultPrice { get; set; }
        public decimal? ChildPrice { get; set; }
        public int Capacity { get; set; }
        public string? Status { get; set; }
    }
}
