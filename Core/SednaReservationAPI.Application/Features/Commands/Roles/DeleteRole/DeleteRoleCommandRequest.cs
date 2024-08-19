using MediatR;

namespace SednaReservationAPI.Application.Features.Commands.Roles.DeleteRole
{
    public class DeleteRoleCommandRequest:IRequest<DeleteRoleCommandResponse>
    {
        public string Id { get; set; }
    }
}