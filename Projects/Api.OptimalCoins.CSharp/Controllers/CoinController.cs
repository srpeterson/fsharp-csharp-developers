using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Api.OptimalCoins.Sharp.Domain;
using Api.OptimalCoins.Sharp.Services;

namespace Api.OptimalCoins.Sharp.Controllers;

[ApiController]
[Produces("application/json")]
[Route("api/optimalcoins")]
public class CoinController : ControllerBase
{
    #region members

    private readonly ICoinService _optimalCoinService;

    #endregion

    #region constructors

    public CoinController(ICoinService optimalCoinService)
    {
        _optimalCoinService = optimalCoinService;
    }

    #endregion

    #region methods

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
    [HttpGet("{amount:decimal}")]
    [ProducesResponseType(typeof(StandardResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(StandardResponse), StatusCodes.Status400BadRequest)]
    public ActionResult<StandardResponse> CalculateOptimalCoins(decimal amount)
    {
        CalculatedCoins result = _optimalCoinService.CalculateCoins(amount);

        return result.ValidateCoinsResult switch
        {
            ValidateCoinsResult.Ok => OkResponse(result, "Calculate optimal coins successful"),
            ValidateCoinsResult.IsNegative => BadRequestResponse("Amount can not be less than zero"),
            ValidateCoinsResult.ExceedsMaxInt => BadRequestResponse("Amount exceeds maximum allowed value of $2,147,483,647.00"),
            _ => ServerErrorResponse("Unmatched OperationResults enum value")
        };

    }

    #endregion

}

