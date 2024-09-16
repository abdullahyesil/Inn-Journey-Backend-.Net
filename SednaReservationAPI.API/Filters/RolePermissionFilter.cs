using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Routing;
using SednaReservationAPI.Application.Abstractions.Services;
using SednaReservationAPI.Application.CustomAttributes;
using System.Reflection;

namespace SednaReservationAPI.API.Filters
{
    public class RolePermissionFilter : IAsyncActionFilter
    {
        readonly IUserService _userService;

        public RolePermissionFilter(IUserService userService)
        {
            _userService = userService;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var name = context.HttpContext.User.Identity?.Name;

            // Kullanıcı adı boş mu kontrolü
            if (!string.IsNullOrEmpty(name) && name != "abdullah")
            {
                var descriptor = context.ActionDescriptor as ControllerActionDescriptor;

                // AuthorizeDefinitionAttribute kontrolü
                var attribute = descriptor?.MethodInfo.GetCustomAttribute<AuthorizeDefinitionAttribute>();

                if (attribute == null)
                {
                    // Eğer attribute bulunmazsa yetkisiz kabul et
                    context.Result = new UnauthorizedResult();
                    return;
                }

                var httpAttribute = descriptor.MethodInfo.GetCustomAttribute<HttpMethodAttribute>();
                var httpMethod = httpAttribute != null ? httpAttribute.HttpMethods.First() : HttpMethods.Get;

                var code = $"{httpMethod}.{attribute.ActionType}.{attribute.Definition.Replace(" ", "")}";

                var hasRole = await _userService.hasRolePermissionToEndPointAsync(name, code);

                // Kullanıcının yetkisi yoksa UnauthorizedResult döndür
                if (!hasRole)
                {
                    context.Result = new UnauthorizedResult();
                    return;
                }

                // Eğer yetki var ise bir sonraki aşamaya devam et
                await next();
            }
            else
            {
                // Kullanıcı adı boş veya geçersizse UnauthorizedResult döndür
                context.Result = new UnauthorizedResult();
            }
        }
    }
}
