using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CoinChallenge.Api.CSharp.Domain;

namespace CoinChallenge.Api.CSharp.Controllers
{
    public class ControllerBase : Microsoft.AspNetCore.Mvc.ControllerBase
    {
        protected ObjectResult StandardResponse<T>(T result) where T : ModelBase
        {
            if (result == default) return ServerErrorResponse(nameof(result));

            var response = result.OperationResult switch
            {
                OperationResult.Ok => OkResponse(result, result.Message),
                OperationResult.Fail => BadRequestResponse(result.Message),
                OperationResult.Error => ServerErrorResponse(result.Message),
                _ => ServerErrorResponse("A server error occurred")
            };

            return response;
        }

        private ObjectResult OkResponse<T>(T data, string message = "Operation Successful.") where T : ModelBase 
            => Ok(new StandardResponse { Status = "Ok", StatusCode =StatusCodes.Status200OK,  Message = message, Data = data });

        private ObjectResult BadRequestResponse(string message = "Operation failed.") 
            => BadRequest(new StandardResponse { Status = "Bad Request", StatusCode = StatusCodes.Status400BadRequest, Message = message, Data = null });

        private ObjectResult ServerErrorResponse(string message = "Operation had a fatal error.")
        {
            var response = new StandardResponse {Status = "Server Error", StatusCode = StatusCodes.Status500InternalServerError, Message = message, Data = null};
            return StatusCode(StatusCodes.Status500InternalServerError, response);
        }

    }
}
