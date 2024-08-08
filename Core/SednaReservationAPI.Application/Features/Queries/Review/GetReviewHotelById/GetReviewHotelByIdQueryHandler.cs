using MediatR;
using SednaReservationAPI.Application.Features.Queries.Reservation.GetHotelByIdReservation;
using SednaReservationAPI.Application.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SednaReservationAPI.Application.Features.Queries.Review.GetReviewHotelById
{
    public class GetReviewHotelByIdQueryHandler:IRequestHandler<GetReviewHotelByIdQueryRequest, List<GetReviewHotelByIdQueryResponse>>
    {
        IReviewReadRepository _reviewReadRepository;

        public GetReviewHotelByIdQueryHandler(IReviewReadRepository reviewReadRepository)
        {
            _reviewReadRepository = reviewReadRepository;
        }

        public async Task<List<GetReviewHotelByIdQueryResponse>> Handle(GetReviewHotelByIdQueryRequest request, CancellationToken cancellation)
        {
            var review = _reviewReadRepository.GetWhere(r => r.HotelId == request.HotelId)
                .OrderByDescending(p => p.CreatedDate) // En son eklenenleri önce al
                .Skip(request.Page * request.Size) 
                .Take(request.Size)
                .Select(rev => new GetReviewHotelByIdQueryResponse
            {
                Id = rev.Id.ToString(),
                UserId = rev.UserId,
                HotelId = rev.HotelId,
                Rating = rev.Rating,    
                Comment = rev.Comment,
                CreatedDate = rev.CreatedDate ?? DateTime.MinValue,
            }).ToList();
           
          
            return await Task.FromResult(review);

        }
    }
}
