namespace CoinChallenge.Api.FSharp.Controllers

open Microsoft.AspNetCore.Mvc
open Microsoft.AspNetCore.Http
open CoinChallenge.Api.FSharp.Responses
open CoinChallenge.Api.FSharp.OptimalCoins.CalculateCoins

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
    ///        "data": {
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
    /// <response code="200">Calculating optimal coins for entered amount successful</response>
    /// <response code="400">Invalid amount. Example: -3.45</response>
    [<HttpGet("{amount}")>]
    [<ProducesResponseType(StatusCodes.Status200OK)>]
    [<ProducesResponseType(typeof<StandardResponse>, StatusCodes.Status400BadRequest)>]
    member _.CalculateOptimalCoins (amount: decimal) = 

        let response = calculateCoins amount
        ActionResult<StandardResponse>( base.StatusCode (response.StatusCode, response) )
     
