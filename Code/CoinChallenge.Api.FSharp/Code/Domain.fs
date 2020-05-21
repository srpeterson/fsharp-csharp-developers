namespace CoinChallenge.Api.FSharp

module Responses =
    open System

    type StandardResponse = { Status: string; StatusCode: int; Message: string; Data: Object }

    let okResponse message data : StandardResponse = 
           { Status= "Ok"; StatusCode= 200; Message= message; Data= data }

    let badRequestResponse message : StandardResponse = 
        { Status= "Bad Request"; StatusCode= 400; Message= message; Data= null }

module DomainTypes =

    //coin type
    type Coin = 
        | SilverDollar
        | HalfDollar
        | Quarter
        | Dime
        | Nickel
        | Penny

    //coins
    type SilverDollars = SilverDollars of int
    type HalfDollars = HalfDollars of int
    type Quarters = Quarters of int
    type Dimes = Dimes of int
    type Nickels = Nickels of int
    type Pennies = Pennies of int

    type Coins = 
        { 
            SilverDollars: SilverDollars
            HalfDollars: HalfDollars 
            Quarters: Quarters 
            Dimes: Dimes
            Nickels: Nickels 
            Pennies: Pennies 
        }


