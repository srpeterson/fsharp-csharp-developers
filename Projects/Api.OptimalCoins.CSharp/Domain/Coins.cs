namespace Api.OptimalCoins.Sharp.Domain;

public record Coins (
    int SilverDollars,
    int HalfDollars,
    int Quarters,
    int Dimes,
    int Nickels,
    int Pennies );