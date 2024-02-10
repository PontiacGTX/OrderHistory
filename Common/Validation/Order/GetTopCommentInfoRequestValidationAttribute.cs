
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Common.Validation.Order
{
    public class GetTopCommentInfoRequestValidationAttribute: ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var descriptor = context.ActionDescriptor as ControllerActionDescriptor;

            var parameters = descriptor.MethodInfo.GetParameters();
            Dictionary<string, string> values = new Dictionary<string, string>();
            foreach (ParameterInfo parameter in parameters) // Iterate over all parameters of method
            {
                string parameterName = parameter.Name;
                string value = context.HttpContext.Request.Query[parameterName].ToString();
                values.Add(parameterName, value);
            }

            if (int.TryParse(values["count"].ToString(), out int count))
            {
                if (count <= 0)
                {
                    context.ModelState.TryAddModelError("count", $"Minimum value count is 1");
                }
            }

            base.OnActionExecuting(context);
        }
    }
}
