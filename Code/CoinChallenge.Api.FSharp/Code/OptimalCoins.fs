namespace CoinChallenge.Api.FSharp
module OptimalCoins =

    module AmountValidationResult =

        [<Struct>]
        type AmountValidationResult<'T,'TResult> =
            | ValidAmount of Valid:'T 
            | InValidAmount of InValid:'TResult

        let bind binder result = 
            match result with ValidAmount x -> binder x | InValidAmount e -> InValidAmount e 

        let map mapping result = 
            match result with ValidAmount x -> ValidAmount (mapping x) | InValidAmount e -> InValidAmount e 

        let mapError mapping result = 
            match result with ValidAmount x -> ValidAmount x | InValidAmount e -> InValidAmount (mapping e) 

    module ValidateAmount =
        open AmountValidationResult

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
                |> AmountValidationResult.bind amountIsNotNegative 
                |> AmountValidationResult.bind amountIsLessIntMax
        
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
    
        let coinValue coin = 
            match coin with
            | SilverDollar -> 1.m
            | HalfDollar   -> 0.50m
            | Quarter      -> 0.25m
            | Dime         -> 0.10m
            | Nickel       -> 0.05m
            | Penny        -> 0.01m

        let calculateCoin (amount, coins) coin =
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
            let coins = [ SilverDollar; HalfDollar; Quarter; Dime; Nickel; Penny ]
            coins
            |> List.fold calculateCoin (amount, intialCoins)
            |> snd

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
       
        let setOptimalCoinsData (coins: Coins) = 
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
        open AmountValidationResult
        open ValidateAmount
        open Calculate
        open Data

        let calculateCoins amount = 

            let okWorkflow = calculate >> setOptimalCoinsData >> okResponse "Calculate optimal coins successful"
            let errorWorkflow = invalidReason >> badRequestResponse

            let response =
                amount
                |> validateAmount
                |> AmountValidationResult.map okWorkflow
                |> AmountValidationResult.mapError errorWorkflow

            match response with
            | ValidAmount okResponse -> okResponse 
            | InValidAmount badRequestResponse -> badRequestResponse

