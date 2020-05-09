namespace CoinChallenge.Api.CSharp.Domain
{
    public enum AmountValidation
    {
        Valid,
        InValidNegativeAmount,
        InValidExceedsMaxAmount
    }

    public enum OperationResult
    {
        Ok,
        Fail,
        Error
    }

    public enum Coin
    {
        SilverDollar,
        HalfDollar,
        Quarter,
        Dime,
        Nickel,
        Penny
    }
}