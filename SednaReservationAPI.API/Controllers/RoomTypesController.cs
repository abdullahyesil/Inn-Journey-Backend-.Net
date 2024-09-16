using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SednaReservationAPI.API.Filters;
using SednaReservationAPI.Application.Consts;
using SednaReservationAPI.Application.CustomAttributes;
using SednaReservationAPI.Application.Enum;
using SednaReservationAPI.Application.Features.Commands.RoomType.CreateRoomType;
using SednaReservationAPI.Application.Features.Commands.RoomType.DeleteRoomType;
using SednaReservationAPI.Application.Features.Commands.RoomType.UpdateRoomType;
using SednaReservationAPI.Application.Features.Queries.RoomType.GetAllRoomType;
using SednaReservationAPI.Application.Features.Queries.RoomType.GetByIdRoomType;
using SednaReservationAPI.Application.Repositories;

namespace SednaReservationAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomTypesController : ControllerBase
    {
        private readonly IRoomTypeReadRepository _roomTypeReadRepository;
        private readonly IRoomTypeWriteRepository _roomTypeWriteRepository;
        private readonly IMediator _mediator;

        public RoomTypesController(IRoomTypeReadRepository roomTypeReadRepository, IRoomTypeWriteRepository roomTypeWriteRepository, IMediator mediator)
        {
            _roomTypeReadRepository = roomTypeReadRepository;
            _roomTypeWriteRepository = roomTypeWriteRepository;
            _mediator = mediator;
        }

        [HttpGet]
        [ServiceFilter(typeof(RolePermissionFilter))]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.RoomType, ActionType = ActionType.Reading, Definition = "Get All Room Type")]
        public async Task<IActionResult> Get([FromQuery] GetAllRoomTypesQueryRequest getAllRoomTypesQueryRequest)
        {
            List<GetAllRoomTypesQueryResponse> response = await _mediator.Send(getAllRoomTypesQueryRequest);
            return Ok(response);
        }
        [HttpGet("{Id}")]
        [ServiceFilter(typeof(RolePermissionFilter))]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.RoomType, ActionType = ActionType.Reading, Definition = "Get Room Type By Id")]
        public async Task<IActionResult> GetById([FromRoute] GetByIdRoomTypeQueryRequest getByIdRoomTypeQueryRequest)
        {
            GetByIdRoomTypeQueryResponse response = await _mediator.Send(getByIdRoomTypeQueryRequest);
            return Ok(response);
        }
        [HttpPost]
        [ServiceFilter(typeof(RolePermissionFilter))]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.RoomType, ActionType = ActionType.Writing, Definition = "Add Room Type")]
        public async Task<IActionResult> addRoomType(CreateRoomTypeCommandRequest createRoomTypeCommandRequest)
        {
            CreateRoomTypeCommandResponse response = await _mediator.Send(createRoomTypeCommandRequest);
            return Ok(response);
        }

        [HttpDelete("{Id}")]
        [ServiceFilter(typeof(RolePermissionFilter))]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.RoomType, ActionType = ActionType.Deleting, Definition = "Delete Room Type")]
        public async Task<IActionResult> deleteRoomType([FromRoute] DeleteRoomTypeCommandRequest deleteRoomTypeCommandRequest)
        {
            DeleteRoomTypeCommandResponse response = await _mediator.Send(deleteRoomTypeCommandRequest);
            return Ok(response);
        }
        [HttpPut]
        [ServiceFilter(typeof(RolePermissionFilter))]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.RoomType, ActionType = ActionType.Updating, Definition = "Updating Room Type")]
        public async Task<IActionResult> updateRoom([FromBody] UpdateRoomTypeCommandRequest updateRoomTypeCommandRequest)
        {
            UpdateRoomTypeCommandResponse response = await _mediator.Send(updateRoomTypeCommandRequest);
            return Ok(response);
        }
    }
}
