namespace Api.OptimalCoins.FSharp

module CalculateCoins =
    open Validation

    type Coin = 
        | SilverDollar
        | HalfDollar
        | Quarter
        | Dime
        | Nickel
        | Penny

    type Coins =  {
        SilverDollars: int
        HalfDollars: int 
        Quarters: int 
        Dimes: int
        Nickels: int 
        Pennies: int 
    }
       
    type ValidateCoinsFailure = NegativeAmountFailure | ExceedsIntMaxValueFailure

    let failureMessage failure = 
        match failure with
        | NegativeAmountFailure -> "Amount can not be less than zero"
        | ExceedsIntMaxValueFailure -> "Amount exceeds maximum allowed value of $2,147,483,647.00"

    let notNegative amount = 
        let greaterEqZero dec = dec >= LanguagePrimitives.GenericZero<decimal>
        amount |> validate greaterEqZero NegativeAmountFailure

    let notExceedsMaxInt amount =
        let lessIntMax dec  =  dec <= decimal System.Int32.MaxValue
        amount |> validate lessIntMax ExceedsIntMaxValueFailure

    let coinValue coin = 
        match coin with 
        | SilverDollar -> 1.00m
        | HalfDollar   -> 0.50m
        | Quarter      -> 0.25m
        | Dime         -> 0.10m
        | Nickel       -> 0.05m
        | Penny        -> 0.01m

    let calculateCoin (amount, coins) coin =
        let coinValue = coin |> coinValue
        let numberOfCoins  = (amount / coinValue) |> int
        let remainingAmount  = amount % coinValue

        let coins =
            match coin with
            | SilverDollar -> { coins with SilverDollars = numberOfCoins } 
            | HalfDollar -> { coins with HalfDollars = numberOfCoins }
            | Quarter -> { coins with Quarters = numberOfCoins }
            | Dime -> { coins with Dimes = numberOfCoins }
            | Nickel -> { coins with Nickels = numberOfCoins }
            | Penny -> { coins with Pennies = numberOfCoins }

        remainingAmount, coins
         
    let calculateCoins amount =
        let intialCoins = { SilverDollars = 0; HalfDollars = 0; Quarters = 0; Dimes= 0; Nickels = 0; Pennies = 0 }
        [ SilverDollar; HalfDollar; Quarter; Dime; Nickel; Penny ]
        |> List.fold calculateCoin (amount, intialCoins)
        |> snd

    let calculate amount =
        Ok amount
        |> Result.bind notNegative
        |> Result.bind notExceedsMaxInt
        |> Result.map calculateCoins
        |> Result.mapError failureMessage


   

