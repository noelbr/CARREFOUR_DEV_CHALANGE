using System;
using Microsoft.AspNetCore.Mvc;

namespace service_account.CustomResults
{
    public class ErrorResult : IActionResult
    {
        private readonly BaseObjectResult _result;

        public ErrorResult(object value, params string[] errors)
        {
            _result = new BaseObjectResult
            {
                StatusCode = StatusCodes.Status400BadRequest,
                Result = value,
                Errors = errors.ToList()
            };
        }

        public ErrorResult(params string[] errors)
        {
            _result = new BaseObjectResult
            {
                StatusCode = StatusCodes.Status400BadRequest,
                Errors = errors.ToList()
            };
        }

        public ErrorResult(Exception ex)
        {

            _result = new BaseObjectResult
            {
                StatusCode = StatusCodes.Status500InternalServerError,
                Errors = new List<String>()
            };

            //{ String.Concat(ex.Message, " - ", ex.InnerException) }
            Exception loopException = ex;

            while (loopException != null)
            {
                _result.Errors.Add(loopException.Message);
                loopException = loopException.InnerException;
            }
        }

        public async Task ExecuteResultAsync(ActionContext context)
        {
            var objectResult = new ObjectResult(_result)
            {
                StatusCode = StatusCodes.Status400BadRequest

            };

            await objectResult.ExecuteResultAsync(context);
        }
    }
}

