namespace CoinChallenge.Api.FSharp

module OptimalCoins =

    module Data =
        open CoinChallenge.Api.FSharp.DomainTypes

        //use primitive types so serializes correctly
        type OptimalCoinsData = 
            {
                SilverDollars: int
                HalfDollars: int 
                Quarters: int 
                Dimes: int
                Nickels: int 
                Pennies: int 
            }
    
        let setData (coins: Coins) = 
            {
                SilverDollars = match coins.SilverDollars with SilverDollars i -> i
                HalfDollars   = match coins.HalfDollars   with HalfDollars i -> i
                Quarters      = match coins.Quarters      with Quarters i -> i
                Dimes         = match coins.Dimes         with Dimes i -> i
                Nickels       = match coins.Nickels       with Nickels i -> i
                Pennies       = match coins.Pennies       with Pennies i -> i
            }


    module Validation =
        open CoinChallenge.Api.FSharp.ValidationResult

        type CalculateCoinsFailure = NegativeAmountFailure | ExceedsMaxIntegerFailure
        
        let decimalGreaterEqZero dec  =  dec >= LanguagePrimitives.GenericZero<decimal>
        let decimalLessThanIntMax dec  =  dec <= decimal System.Int32.MaxValue
                
        let validate validator failure input =
            if validator input
            then Valid input 
            else InValid failure
            
        let validateAmount amount = 
            let amountIsNotNegative amount = amount |> validate decimalGreaterEqZero NegativeAmountFailure
            let amountIsLessIntMax amount = amount |> validate decimalLessThanIntMax ExceedsMaxIntegerFailure
        
            Valid amount 
                |> ValidationResult.bind amountIsNotNegative 
                |> ValidationResult.bind amountIsLessIntMax
        
        let failureMessage = function
            | NegativeAmountFailure -> "Amount can not be less than zero"
            | ExceedsMaxIntegerFailure -> "Amount exceeds maximum allowed value of $2,147,483,647.00"
        
    module CalculateCoins =
        open CoinChallenge.Api.FSharp.DomainTypes

        let coins = [ SilverDollar; HalfDollar; Quarter; Dime; Nickel; Penny ]

        let intialCoins = 
            {   
                SilverDollars= SilverDollars 0
                HalfDollars= HalfDollars 0
                Quarters= Quarters 0 
                Dimes= Dimes 0 
                Nickels= Nickels 0 
                Pennies= Pennies 0 
            }
    
        let coinValue = function
            | SilverDollar -> 1.m
            | HalfDollar   -> 0.50m
            | Quarter      -> 0.25m
            | Dime         -> 0.10m
            | Nickel       -> 0.05m
            | Penny        -> 0.01m

        let calculateCoin  (amount, coins) coin =
            let coinValue = coinValue coin
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
            coins
            |> List.fold calculateCoin (amount, intialCoins)
            |> snd

    module WorkFlow = 
        open CoinChallenge.Api.FSharp.Responses
        open ValidationResult
        open Data
        open Validation
        
        open CalculateCoins

        let calculateCoinsWorkFlow amount = 

            let okWorkflow = calculate >> setData >> okResponse "Calculate optimal coins successful"
            let errorWorkflow = failureMessage >> badRequestResponse

            let response =
                amount
                |> validateAmount
                |> ValidationResult.map okWorkflow
                |> ValidationResult.mapError errorWorkflow

            match response with
            | Valid okResponse -> okResponse 
            | InValid badRequestResponse -> badRequestResponse

