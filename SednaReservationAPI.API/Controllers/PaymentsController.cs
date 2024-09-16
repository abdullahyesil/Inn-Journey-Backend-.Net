using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SednaReservationAPI.Application.CustomAttributes;
using SednaReservationAPI.Application.Enum;
using SednaReservationAPI.Application.Features.Commands.Payment.CreatePayment;
using SednaReservationAPI.Application.Features.Commands.Payment.DeletePayment;
using SednaReservationAPI.Application.Features.Commands.Payment.UpdatePayment;
using SednaReservationAPI.Application.Features.Queries.Payment.GetAllPayment;
using SednaReservationAPI.Application.Features.Queries.Payment.GetByIdPayment;
using SednaReservationAPI.Application.Features.Queries.Payment.GetPaymentByHotelId;
using SednaReservationAPI.Application.Features.Queries.Payment.GetPaymentByUserId;
using SednaReservationAPI.Application.Repositories;

namespace SednaReservationAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private readonly IPaymentReadRepository _paymentReadRepository;
        private readonly IPaymentWriteRepository _paymentWriteRepository;
        private readonly IMediator _mediator;

        public PaymentsController(IPaymentReadRepository paymentReadRepository, IPaymentWriteRepository paymentWriteRepository, IMediator mediator)
        {
            _paymentReadRepository = paymentReadRepository;
            _paymentWriteRepository = paymentWriteRepository;
            _mediator = mediator;
        }

        [HttpGet]
        [AuthorizeDefinition(ActionType = ActionType.Reading, Definition = "Get All Payments", Menu = "Payment")]
        public async Task<IActionResult> Get([FromQuery] GetAllPaymentQueryRequest getAllPaymentQueryRequest)
        {
            List<GetAllPaymentQueryResponse> response = await _mediator.Send(getAllPaymentQueryRequest);
            return Ok(response);
        }
        [HttpGet("{Id}")]
        [AuthorizeDefinition(ActionType = ActionType.Reading, Definition = "Get By Id Payments", Menu = "Payment")]
        public async Task<IActionResult> GetByID([FromRoute] GetByIdPaymentQueryRequest getByIdPaymentQueryRequest)
        {
            GetByIdPaymentQueryResponse response = await _mediator.Send(getByIdPaymentQueryRequest);
            return Ok(response);
        }

        [HttpPost]
        [AuthorizeDefinition(ActionType = ActionType.Writing, Definition = "Add Payment", Menu = "Payment")]
        public async Task<IActionResult> add(CreatePaymentCommandRequest createPaymentCommandRequest)
        {
            CreatePaymentCommandResponse response = await _mediator.Send(createPaymentCommandRequest);
            return Ok(response);
        }

        [HttpDelete("{Id}")]
        [AuthorizeDefinition(ActionType = ActionType.Deleting, Definition = "Delete Payments", Menu = "Payment")]
        public async Task<IActionResult> delete([FromRoute] DeletePaymentCommandRequest deletePaymentCommandRequest)
        {
            DeletePaymentCommandResponse response = await _mediator.Send(deletePaymentCommandRequest);
            return Ok(response);
        }
        [HttpPut]
        [AuthorizeDefinition(ActionType = ActionType.Updating, Definition = "Update Payment", Menu = "Payment")]
        public async Task<IActionResult> update([FromBody] UpdatePaymentCommandRequest updatePaymentCommandRequest)
        {
            UpdatePaymentCommandResponse response = await _mediator.Send(updatePaymentCommandRequest);
            return Ok(response);
        }

        [HttpGet("hotel")]
        [AuthorizeDefinition(ActionType = ActionType.Reading, Definition = "Get Payment By Otel Id", Menu = "Payment")]
        public async Task<IActionResult> getPaymentHotelId([FromQuery] GetPaymentByHotelIdRequest getPaymentByHotelIdRequest)
        {
            List<GetPaymentByHotelIdResponse> response = await _mediator.Send(getPaymentByHotelIdRequest);
            return Ok(response);
        }
        [HttpGet("user/")]
        [AuthorizeDefinition(ActionType = ActionType.Reading, Definition = "Get Payment By UserId", Menu = "Payment")]
        public async Task<IActionResult> getUserId([FromQuery] GetPaymentByUserIdRequest getPaymentByUserlIdRequest)
        {
            GetPaymentByUserIdResponse response = await _mediator.Send(getPaymentByUserlIdRequest);
            return Ok(response);
        }
    }
}
