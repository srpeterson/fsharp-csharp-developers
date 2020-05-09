using CoinChallenge.Api.CSharp.Domain;
using CoinChallenge.Api.CSharp.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoinChallenge.Api.CSharp.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/optimalcoins")]
    public class OptimalCoinController : ControllerBase
    {
        private readonly IOptimalCoinService _optimalCoinService;

        public OptimalCoinController(IOptimalCoinService optimalCoinService) => _optimalCoinService = optimalCoinService;

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
        [HttpGet("{amount}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(StandardResponse), StatusCodes.Status400BadRequest)]
        public ActionResult<StandardResponse> CalculateOptimalCoins(decimal amount) 
            => StandardResponse(_optimalCoinService.GetOptimalCoins(amount));
    }
}