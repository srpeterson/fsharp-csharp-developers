using System;
using Challenge.Api.CSharp.Domain;

namespace Challenge.Api.CSharp.Services;

public static class CalculateCoins
{
    private static decimal GetCoinValue(this Coin coin) =>
        coin switch
        {
            Coin.SilverDollar => 1.00m,
            Coin.HalfDollar   => 0.5m,
            Coin.Quarter      => 0.25m,
            Coin.Dime         => 0.10m,
            Coin.Nickel       => 0.05m,
            Coin.Penny        => 0.01m,
            _ => throw new NotSupportedException("Missing Coin enum value")
        };

    public static Coins Calculate(decimal amount)
    {
        var (_, coins) =
            (amount, new Coins(0,0,0,0,0,0))
            .CalculateCoin(Coin.SilverDollar)
            .CalculateCoin(Coin.HalfDollar)
            .CalculateCoin(Coin.Quarter)
            .CalculateCoin(Coin.Dime)
            .CalculateCoin(Coin.Nickel)
            .CalculateCoin(Coin.Penny);

        return coins;
    }

    private static (decimal remainingAmount, Coins coins) CalculateCoin(this (decimal amount, Coins coins) calculationValues, Coin coin)
    {
        var (amount, coins) = calculationValues;
        var coinValue = coin.GetCoinValue();
        var remainingAmount = amount % coinValue;
        var numberOfCoins = (int)(amount / coinValue);

        var _ = coin switch
        {
            Coin.SilverDollar => coins = coins with { SilverDollars = numberOfCoins },
            Coin.HalfDollar => coins = coins with { HalfDollars = numberOfCoins },
            Coin.Quarter => coins = coins with { Quarters = numberOfCoins },
            Coin.Dime => coins = coins with { Dimes = numberOfCoins },
            Coin.Nickel => coins = coins with { Nickels = numberOfCoins },
            Coin.Penny => coins = coins with { Pennies = numberOfCoins },
            _ => throw new NotSupportedException("Missing Coin enum value")
        };

        return (remainingAmount, coins);
    }
}