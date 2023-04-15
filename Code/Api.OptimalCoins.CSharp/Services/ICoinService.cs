using Challenge.Api.CSharp.Domain;

namespace Challenge.Api.CSharp.Services;

public interface ICoinService
{
    CalculatedCoins CalculateCoins(decimal amount);

    ValidateCoinsResult ValidateAmount(decimal amount);
}
