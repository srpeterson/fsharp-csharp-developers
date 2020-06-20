namespace CoinChallenge.Api.FSharp

module OptimalCoins =

    module Validation =
        
        [<Struct>]
        type Result<'T,'TFail> =
            | ValidAmount of Valid:'T 
            | InValidAmount of InValid:'TFail

        let bind binder result = 
            match result with ValidAmount x -> binder x | InValidAmount e -> InValidAmount e 

        type CalculateCoinsFailure = NegativeAmountFailure | ExceedsMaxIntegerFailure
             
        let validate validator failure amount =
            if validator amount
            then ValidAmount amount 
            else InValidAmount failure

        let decimalGreaterEqZero dec  =  dec >= LanguagePrimitives.GenericZero<decimal>
        let decimalLessThanIntMax dec  =  dec <= decimal System.Int32.MaxValue
            
        let validateAmount amount = 
            let amountIsNotNegative amount = amount |> validate decimalGreaterEqZero NegativeAmountFailure
            let amountIsLessIntMax amount = amount |> validate decimalLessThanIntMax ExceedsMaxIntegerFailure
        
            ValidAmount amount 
                |> bind amountIsNotNegative 
                |> bind amountIsLessIntMax
        
        let invalidReason = function
            | NegativeAmountFailure -> "Amount can not be less than zero"
            | ExceedsMaxIntegerFailure -> "Amount exceeds maximum allowed value of $2,147,483,647.00"
        
    module Calculate =
        open CoinChallenge.Api.FSharp.DomainTypes
    
        let intialCoins = 
            {   
                SilverDollars= SilverDollars 0
                HalfDollars= HalfDollars 0
                Quarters= Quarters 0 
                Dimes= Dimes 0 
                Nickels= Nickels 0 
                Pennies= Pennies 0 
            }
    
        let getCoinValue coin = 
            match coin with
            | SilverDollar -> 1.m
            | HalfDollar   -> 0.50m
            | Quarter      -> 0.25m
            | Dime         -> 0.10m
            | Nickel       -> 0.05m
            | Penny        -> 0.01m

        let calculateCoin (amount, coins) coin =
            let coinValue = getCoinValue coin
            let numberOfCoins  = (amount / coinValue) |> int
            let remainingAmount  = amount % coinValue
        
            let coins =
                match coin with
                | SilverDollar -> { coins with SilverDollars = SilverDollars numberOfCoins } 
                | HalfDollar -> { coins with HalfDollars = HalfDollars numberOfCoins }
                | Quarter -> { coins with Quarters = Quarters numberOfCoins }
                | Dime -> { coins with Dimes = Dimes numberOfCoins }
                | Nickel -> { coins with Nickels = Nickels numberOfCoins }
                | Penny -> { coins with Pennies = Pennies numberOfCoins }

            remainingAmount, coins
        
        let calculate amount = 
            let coins = [ SilverDollar; HalfDollar; Quarter; Dime; Nickel; Penny ]
            coins
            |> List.fold calculateCoin (amount, intialCoins)
            |> snd

    module Result =
        open CoinChallenge.Api.FSharp.DomainTypes

        //use primitive types so serializes correctly
        type OptimalCoinsResult = 
            {
                SilverDollars: int
                HalfDollars: int 
                Quarters: int 
                Dimes: int
                Nickels: int 
                Pennies: int 
            }
       
        let setOptimalCoins (coins: Coins) = 
            {
                SilverDollars = match coins.SilverDollars with SilverDollars i -> i
                HalfDollars   = match coins.HalfDollars   with HalfDollars i -> i
                Quarters      = match coins.Quarters      with Quarters i -> i
                Dimes         = match coins.Dimes         with Dimes i -> i
                Nickels       = match coins.Nickels       with Nickels i -> i
                Pennies       = match coins.Pennies       with Pennies i -> i
            }

    module CalculateCoinsWorkflow = 
        open CoinChallenge.Api.FSharp.Responses
        open Validation
        open Calculate
        open Result

        let calculateCoins amount = 

            let okWorkflow = calculate >> setOptimalCoins >> okResponse "Calculate optimal coins successful"
            let errorWorkflow = invalidReason >> badRequestResponse

            match validateAmount amount with
            | ValidAmount amount -> okWorkflow  amount
            | InValidAmount failure -> errorWorkflow failure


