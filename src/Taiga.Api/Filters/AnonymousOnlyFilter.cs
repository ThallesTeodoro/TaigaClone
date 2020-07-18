using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Taiga.Api.Filters
{
    public class AnonymousOnlyFilter : ActionFilterAttribute
    {
        private readonly string _action;
        private readonly string _controller;

        public AnonymousOnlyFilter(string action, string controller)
        {
            _action = action;
            _controller = controller;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.HttpContext.User.Identity.IsAuthenticated)
            {
                context.Result = new RedirectToActionResult(_action, _controller, null);
            }
        }
    }
}
