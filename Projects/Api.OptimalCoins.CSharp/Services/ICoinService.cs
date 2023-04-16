using Api.OptimalCoins.Sharp.Domain;

namespace Api.OptimalCoins.Sharp.Services;

public interface ICoinService
{
    CalculatedCoins CalculateCoins(decimal amount);

    ValidateCoinsResult ValidateAmount(decimal amount);
}
