using CoinChallenge.Api.CSharp.Domain;

namespace CoinChallenge.Api.CSharp.Services
{
    public class OptimalCoinService : IOptimalCoinService
    {
        public (AmountValidation, string) ValidateAmount(decimal amount)
        {
            // make sure not negative amount
            if (amount.ValidateDecimal(Validation.DecimalLessThanZero))
                return (AmountValidation.InValidNegativeAmount, "Amount can not be less than zero");

            // since we are calculating the number of each coin as an int
            // we need to makes sure the decimal amount passed in does not exceed
            // max Int32 value
            if (amount.ValidateDecimal(Validation.DecimalGreaterThanIntMax))
                return (AmountValidation.InValidExceedsMaxAmount, "Amount exceeds maximum allowed value of $2,147,483,647.00");

            return (AmountValidation.Valid, string.Empty);
        }

        public CoinsModel GetOptimalCoins(decimal amount)
        {
            var (validationResult, message) = ValidateAmount(amount);

            if (validationResult == AmountValidation.InValidNegativeAmount 
                || validationResult == AmountValidation.InValidExceedsMaxAmount)
                return new CoinsModel { OperationResult = OperationResult.Fail, Message = message};

            //passed validation so calculate
            var coins = OptimalCoins.Calculate(amount);

            //return
            return new CoinsModel
            {
                SilverDollars = coins.SilverDollars,
                HalfDollars = coins.HalfDollars,
                Quarters = coins.Quarters,
                Dimes = coins.Dimes,
                Nickels = coins.Nickels,
                Pennies = coins.Pennies,
                OperationResult = OperationResult.Ok,
                Message = "Calculate optimal coins successful"
            };
        }
    }
}