using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SednaReservationAPI.Application.CustomAttributes;
using SednaReservationAPI.Application.Enum;
using SednaReservationAPI.Application.Features.Commands.Review.CreateReview;
using SednaReservationAPI.Application.Features.Commands.Review.DeleteReview;
using SednaReservationAPI.Application.Features.Commands.Review.UpdateReview;
using SednaReservationAPI.Application.Features.Queries.Review.GetAllReview;
using SednaReservationAPI.Application.Features.Queries.Review.GetByIdReview;
using SednaReservationAPI.Application.Features.Queries.Review.GetReviewHotelById;
using SednaReservationAPI.Application.Features.Queries.Review.GetReviewUserById;
using SednaReservationAPI.Application.Repositories;

namespace SednaReservationAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        private readonly IReservationReadRepository _reservationReadRepository;
        private readonly IReservationWriteRepository _reservationWriteRepository;
        private readonly IMediator _mediator;

        public ReviewsController(IReservationReadRepository reservationReadRepository, IReservationWriteRepository reservationWriteRepository, IMediator mediator)
        {
            _reservationReadRepository = reservationReadRepository;
            _reservationWriteRepository = reservationWriteRepository;
            _mediator = mediator;
        }
        [HttpGet]
        [AuthorizeDefinition(ActionType = ActionType.Reading, Definition = "Get All Reviews", Menu = "Review")]
        public async Task<IActionResult> getReview([FromQuery] GetAllReviewQueryRequest getAllReviewQueryRequest)
        {
            List<GetAllReviewQueryResponse> response = await _mediator.Send(getAllReviewQueryRequest);
            return Ok(response);
        }

        [HttpGet("{Id}")]
        [AuthorizeDefinition(ActionType = ActionType.Reading, Definition = "Get Reviews to By Id", Menu = "Review")]
        public async Task<IActionResult> getReviewById([FromRoute] GetByIdReviewQueryRequest getByIdReviewQueryRequest)
        {
            GetByIdReviewQueryResponse response = await _mediator.Send(getByIdReviewQueryRequest);
            return Ok(response);
        }
        [HttpPost]
        [AuthorizeDefinition(ActionType = ActionType.Writing, Definition = "Add Review", Menu = "Review")]
        public async Task<IActionResult> getAddReview(CreateReviewCommandRequest createReviewCommandRequest)
        {
            CreateReviewCommandResponse response = await _mediator.Send(createReviewCommandRequest);
            return Ok(response);
        }
        [HttpDelete("{Id}")]
        [AuthorizeDefinition(ActionType = ActionType.Deleting, Definition = "Delete Review", Menu = "Review")]
        public async Task<IActionResult> deleteReview([FromRoute] DeleteReviewCommandRequest deleteReviewCommandRequest)
        {
            DeleteReviewCommandResponse response = await _mediator.Send(deleteReviewCommandRequest);
            return Ok(response);
        }
        [HttpPut]
        [AuthorizeDefinition(ActionType = ActionType.Updating, Definition = "Update Review", Menu = "Review")]
        public async Task<IActionResult> updateReview([FromBody] UpdateReviewCommandRequest updateReviewCommandRequest)
        {
            UpdateReviewCommandResponse response = await _mediator.Send(updateReviewCommandRequest);
            return Ok(response);
        }
        [HttpGet("comments/")]
      
        public async Task<IActionResult> getReviewHotelById([FromQuery] GetReviewHotelByIdQueryRequest getReviewHotelByIdQueryRequest)
        {
            GetReviewHotelByIdQueryResponse response = await _mediator.Send(getReviewHotelByIdQueryRequest);
            return Ok(response);

        }
        [HttpGet("user/comments")]
        [AuthorizeDefinition(ActionType = ActionType.Reading, Definition = "Get Review By UserId", Menu = "Review")]
        public async Task<IActionResult> getReviewUserById([FromQuery] GetReviewUserByIdQueryRequest getReviewUserByIdQueryRequest)
        {
            GetReviewUserByIdQueryResponse response = await _mediator.Send(getReviewUserByIdQueryRequest);
            return Ok(response);
        }

    }
}
