using CoinChallenge.Api.CSharp.Domain;

namespace CoinChallenge.Api.CSharp.Services
{
    public interface IOptimalCoinService
    {
        CoinsModel GetOptimalCoins(decimal amount);
        (AmountValidation, string) ValidateAmount(decimal amount);
    }
}