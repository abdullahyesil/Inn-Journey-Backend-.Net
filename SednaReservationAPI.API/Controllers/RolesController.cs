﻿using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SednaReservationAPI.API.Filters;
using SednaReservationAPI.Application.CustomAttributes;
using SednaReservationAPI.Application.Enum;
using SednaReservationAPI.Application.Features.Commands.AppUser.AddRoleToUser;
using SednaReservationAPI.Application.Features.Commands.Roles.CreateRole;
using SednaReservationAPI.Application.Features.Commands.Roles.DeleteRole;
using SednaReservationAPI.Application.Features.Commands.Roles.UpdateRole;
using SednaReservationAPI.Application.Features.Queries.Role.GetRoleById;
using SednaReservationAPI.Application.Features.Queries.Role.GetRoles;
using SednaReservationAPI.Application.Features.Queries.Role.GetRolesByUserId;

namespace SednaReservationAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(AuthenticationSchemes ="Admin")]

    public class RolesController : ControllerBase
    {

        readonly IMediator _mediator;

        public RolesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [AuthorizeDefinition(ActionType = ActionType.Reading,Definition = "Get All Roles", Menu = "Roles")]
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetRoles([FromQuery] GetRolesQueryRequest getRolesQueryRequest)
        {
            GetRolesQueryResponse response = await _mediator.Send(getRolesQueryRequest);
            return Ok(response);
           
        }
        [AuthorizeDefinition(ActionType = ActionType.Reading, Definition = "Get Role ById", Menu = "Roles")]
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetRoleById([FromRoute] GetRoleByIdQueryRequest getRoleByIdQueryRequest)
        {
            GetRoleByIdQueryResponse response = await _mediator.Send(getRoleByIdQueryRequest);
            return Ok(response);
        }
        [AuthorizeDefinition(ActionType = ActionType.Writing, Definition = "Add Role", Menu = "Roles")]

        [HttpPost]
        public async Task<IActionResult> CreateRole([FromBody] CreateRoleCommandRequest createRoleCommandRequest)
        {
            CreateRoleCommandResponse response = await _mediator.Send(createRoleCommandRequest);
            return Ok(response);
        }
        [ServiceFilter(typeof(RolePermissionFilter))]
        [AuthorizeDefinition(ActionType = ActionType.Updating, Definition = "Updating Role", Menu = "Roles")]

        [HttpPut("{Id}")]
        public async Task<IActionResult> UpdateRole([FromBody]UpdateRoleCommandRequest updateRoleCommandRequest)
        {
            UpdateRoleCommandResponse response = await _mediator.Send(updateRoleCommandRequest);

            return Ok(response);
        }
        [AuthorizeDefinition(ActionType = ActionType.Deleting, Definition = "Delete Role", Menu = "Roles")]
        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteRole([FromRoute] DeleteRoleCommandRequest deleteRoleCommandRequest)
        {
            DeleteRoleCommandResponse response = await _mediator.Send(deleteRoleCommandRequest);
            return Ok(response);
        }

        [HttpPost("AddRolesToUser")]
        [AuthorizeDefinition(ActionType = ActionType.Deleting, Definition = "Add Roles to User", Menu = "Roles")]
        public async Task<IActionResult> AddRolestToUser(AddRoleToUserCommandRequest addRoleToUserCommandRequest) 
        {

            AddRoleToUserCommandResponse response = await _mediator.Send(addRoleToUserCommandRequest);
            return Ok(response);
        }

        [AuthorizeDefinition(ActionType = ActionType.Reading, Definition = "Get Role By UserId", Menu = "Roles")]
        [HttpGet("User/")]
        public async Task<IActionResult> GetRoleByUserId([FromQuery] GetRolesByUserIdQueryRequest getRolesByUserId)
        {
            GetRolesByUserIdQueryResponse response = await _mediator.Send(getRolesByUserId);
            return Ok(response);
        }
    }
}
