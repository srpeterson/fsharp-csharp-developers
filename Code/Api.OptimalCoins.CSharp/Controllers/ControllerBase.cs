using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Challenge.Api.CSharp.Domain;

namespace Challenge.Api.CSharp.Controllers;

public class ControllerBase : Microsoft.AspNetCore.Mvc.ControllerBase
{
    protected ObjectResult OkResponse<T>(T result, string message = "Operation Successful.")
        => Ok(new StandardResponse { Status = "Ok", StatusCode =StatusCodes.Status200OK,  Message = message, Result = result });

    protected ObjectResult BadRequestResponse(string message = "Operation failed.") 
        => BadRequest(new StandardResponse { Status = "Bad Request", StatusCode = StatusCodes.Status400BadRequest, Message = message, Result = null });

    protected ObjectResult ServerErrorResponse(string message = "Operation had a fatal error.")
    {
        var response = new StandardResponse {Status = "Server Error", StatusCode = StatusCodes.Status500InternalServerError, Message = message, Result = null};
        return StatusCode(StatusCodes.Status500InternalServerError, response);
    }

}
