using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Email.Application.Wrappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Email.API.Filters
{
    public class CustomValidatorResponse : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var errors = context.ModelState.Values.Where(v => v.Errors.Count > 0)
                    .SelectMany(v => v.Errors)
                    .Select(v => v.ErrorMessage)
                    .ToList();

                var responseObj = new Error()
                {
                    StatusCode = 400,
                    Errors = errors
                }.ToString();

                context.Result = new BadRequestObjectResult(responseObj);
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
        }
    }
}
