using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MoviesApp.Filters
{
    public class AgeFilter : Attribute, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {

        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var age = DateTime.Parse(context.HttpContext.Request.Form["Birthdate"]).Year;
            if (DateTime.Now.Year - age > 7 && DateTime.Now.Year - age < 99) context.Result = new BadRequestResult();
        }
    }
}