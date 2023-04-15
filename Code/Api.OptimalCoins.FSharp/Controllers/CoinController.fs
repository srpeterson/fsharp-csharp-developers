namespace Api.OptimalCoins.FSharp.Controllers

open Microsoft.AspNetCore.Mvc
open Microsoft.AspNetCore.Http
open Api.OptimalCoins.FSharp.CalculateCoins
open Api.OptimalCoins.FSharp.Http.Responses

[<ApiController>]
[<Produces("application/json")>]
[<Route("api/optimalcoins")>]
type OptimalCoinsController () =
    inherit ControllerBase()
    
    /// <summary>
    /// Calculates the optimal coins for a given non negative monetary amount up to $2,147,483,647.00
    /// </summary>
    /// <remarks>
    /// Sample response for $1.56:
    ///
    ///     {
    ///        "status": "Ok",
    ///        "statusCode": 200,
    ///        "message": "Calculate optimal coins operation successful",
    ///        "result": {
    ///                "silverDollar": 1
    ///                "halfDollar": 1
    ///                "quarter": 0
    ///                "dime": 0
    ///                "nickel": 1
    ///                "penny": 1
    ///         }
    ///     }
    ///
    /// </remarks>
    /// <param name="amount">The amount to calculate coins by</param> 
    /// <returns>The optimal coins broken down by coin value</returns>
    /// <response code="200">Success</response>
    /// <response code="400">Invalid amount. Example: -3.45</response>
    [<HttpGet("{amount:decimal}")>]
    [<ProducesResponseType(typeof<StandardResponse>, StatusCodes.Status200OK)>]
    [<ProducesResponseType(typeof<StandardResponse>, StatusCodes.Status400BadRequest)>]
    member _.CalculateOptimalCoins (amount: decimal) = 
        
        let response =
            match calculate amount with
            | Ok coins -> coins |> okResponse "Calculate optimal coins successful" 
            | Error failure -> badRequestResponse failure

        ActionResult<StandardResponse>( base.StatusCode (response.StatusCode, response) )
     
