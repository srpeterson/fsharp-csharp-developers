using Challenge.Api.CSharp.Domain;

namespace Challenge.Api.CSharp.Services;

public class CoinService : ICoinService
{
    public ValidateCoinsResult ValidateAmount(decimal amount)
    {
        // make sure not negative amount
        if (amount.ValidateDecimal(Validation.DecimalLessThanZero))
            return ValidateCoinsResult.IsNegative;

        // since we are calculating the number of each coin as an int
        // we need to makes sure the decimal amount passed in does not exceed
        // max Int32 value
        return amount.ValidateDecimal(Validation.DecimalGreaterThanIntMax) 
            ? ValidateCoinsResult.ExceedsMaxInt 
            : ValidateCoinsResult.Ok;
    }

    public CalculatedCoins CalculateCoins(decimal amount)
    {
        // validate
        var validateCoinsResult = ValidateAmount(amount);

        if (validateCoinsResult != ValidateCoinsResult.Ok)
            return new CalculatedCoins { ValidateCoinsResult = validateCoinsResult };

        // passed validation so calculate
        var coins = Services.CalculateCoins.Calculate(amount);

        // return
        return new CalculatedCoins
        {
            SilverDollars = coins.SilverDollars,
            HalfDollars = coins.HalfDollars,
            Quarters = coins.Quarters,
            Dimes = coins.Dimes,
            Nickels = coins.Nickels,
            Pennies = coins.Pennies,
            ValidateCoinsResult = ValidateCoinsResult.Ok
        };
    }

}
