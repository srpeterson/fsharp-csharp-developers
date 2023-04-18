let greeting = "Hello World!"

let ten = 10; 

let addTwoNumbers number1 number2 = number1 + number2

//list
let firstnames = ["Steve"; "Bob"; "Mary"]

//array
let lastNames = [| "Jones"; "Smith"; "Adams" |]

//F# dictionary way
let mapNames = Map.ofList ["key1", "Bob"; "key2", "Bob"] 

//set (HashSet in C#)
let setNames = set ["Steve"; "Bob"; "Mary"; "Bob"]

//tuple
let myTuple = (5, "five") 
    
//simple mult line function
let getMyTupleFirst =
    let first, _ = myTuple //is a function!
    first //notice no 'return' like in C#

//option - how F# deals with 'null' in C#
let eleven = Some 11
let twelve = None

//if conditional (in F# we use if conditional only if 2 possible outcomes)
let isLessThanTen x  = 
    if x < 10 then "Less than ten" else "Greater than ten"

//match: C# 8 switch expression
let switchOnInt x =
    match x with
    | 5 -> x + 10
    | _ when x < 5 -> x
    | _ -> x + 100

//commonly used F# collections (by no means all!!!!!!!)

//list
let oneToFive = [1..5] // [1; 2; 3; 4; 5]

//iter : C# LINQ 'ForEach'
let iterExample = oneToFive |> List.iter (printfn "%d") //printfn "%d" is C#'s 'Console.Write'

//filter: C# LINQ 'Where'
let filterExample = oneToFive |> List.filter (fun i -> i < 3)

//sum, max, min: C# LINQ has corresponding functions
let sumExample = oneToFive |> List.sum

//map: C# LINQ 'Select' 
let mapExample = oneToFive |> List.map((+) 10) // same as (fun i -> i + 10)

//some F# built in collection functions require C# to chain LINQ
let sumByExample = oneToFive |> List.sumBy(fun i -> i * 2) //30

//partial application - supply the first parameter. The result is a function
//with the first parameter "baked in"
let tellMeABoutYourPet animal age = sprintf "My pet is a %s and is %d years old!" animal age
let tellMeDog = tellMeABoutYourPet "dog"
    
//function that uses a 'partial application' function
let tellMeAboutYourDog age = tellMeDog age

//Interface type in F#. This defines any function that accepts an int and returns a bool. 
type DoSomethingWithInt = (int -> bool)

//higher order function (accepts a function as a parameter or refurns a function)
let withType (parm: DoSomethingWithInt) x = parm x

open System

module Validation = 

    let validate validator failure input =
        if validator input then Ok input else Error failure

    let validateOption validator failure (input: 'a option) =
        if validator input then Ok (input |> Option.get) else Error failure

type ValidBenchmarkName =  private ValidBenchmarkName of string

module BenchmarkName =
    open Validation

    [<Literal>]
    let private MaxNameLength = 255

    let private nameIsNone (name: string option) = 
        let isSome (value: string option) = value.IsSome
        name |> validateOption isSome "Name is none"

    let private nameIsTooLong (name: string) = 
        let lessThanMax (s: string) = s.Length < MaxNameLength
        name |> validate lessThanMax $"Name exceeds max character length of '{MaxNameLength}'"

    let create (name: string option) = 
        let isValidName = 
            Ok name
            |> Result.bind nameIsNone
            |> Result.bind nameIsTooLong
        match isValidName with
        | Ok s -> Ok (ValidBenchmarkName s)
        | Error err -> Error err

    let value (ValidBenchmarkName s) = s

type ValidBenchmarkTypeId = private ValidBenchmarkTypeId of byte

module BenchmarkTypeId =
    open Validation

    // this goes in domain
    type BenchmarkType = 
        | AbsoluteReturnBenchmark
        | AssetWeightedBenchmark
        | CompositeReturnBenchmark
        | CompositeWeightedBenchmark
        | PullForwardBenchmark
        | StaticWeightedBenchmark

    let getBenchmarkTypeId benchamarkType =
        match benchamarkType with
        | AbsoluteReturnBenchmark -> 1uy
        | AssetWeightedBenchmark -> 2uy
        | CompositeReturnBenchmark -> 3uy
        | CompositeWeightedBenchmark -> 4uy
        | PullForwardBenchmark -> 5uy
        | StaticWeightedBenchmark -> 6uy

    let create (createBenchmarkDtoType: BenchmarkType) (typId: byte) = 
        let benchmarkTypeId = createBenchmarkDtoType |> getBenchmarkTypeId
        let existsId (id: ValidBenchmarkTypeId) = id = ValidBenchmarkTypeId benchmarkTypeId
        (ValidBenchmarkTypeId typId) |> validate existsId $"Invalid TypeId"

type LagDay = { AsDateOnly: DateOnly;  DaysSinceEpoch: int } 
type ValidLagDay = private ValidLagDay of LagDay

module LagDay =
    open Validation

    let private ``date is before january 1 1900`` (lagDate: DateOnly) = 
        let isValid (value: DateOnly) = value >= new DateOnly (1900, 1, 1)
        lagDate |> validate isValid "Date can not be before Jaunary 1, 1900"

    let private iEndOfMonth (lagDate: DateOnly) = 
        let isEndOfMonth (value: DateOnly)  =  DateTime.DaysInMonth(value.Year, value.Month) = value.Day;
        lagDate |> validate isEndOfMonth "Lag Date must be end of month"

    let create (lagDate: DateOnly) =
        let isValidLagDate = 
            Ok lagDate
            |> Result.bind ``date is before january 1 1900``
            |> Result.bind iEndOfMonth
        match isValidLagDate with
        | Ok s -> Ok (ValidLagDay {AsDateOnly = s; DaysSinceEpoch = 5})
        | Error err -> Error err

    let value (ValidLagDay s) = s

let foo = LagDay.create (new DateOnly(1900, 1, 31))

module Example =
    open BenchmarkName

    //type UnValidatedName = UnValidatedName of string option
    //type ValidatedName = ValidatedName of string

    type UnValidatedDto = { Name: string option; TypeId: byte; LagDate: DateOnly option }
    type ValidatedDto = { Name: ValidBenchmarkName; TypeId: ValidBenchmarkTypeId; LagDate: ValidLagDay option  }

    let unValidatedDto: UnValidatedDto = { Name = (Some"Steve"); TypeId = 3uy; LagDate = Some (new DateOnly(2022, 12,31)) }
    
    let foo = BenchmarkName.create unValidatedDto.Name

    //type UnValidatedName = UnValidatedName of string
    //type ValidatedName = ValidatedName of string

    //type UnValidatedDto = {Name: UnValidatedName; TypeId: byte }
    //type ValidatedDto = {Name: ValidatedName; TypeId: byte}

    //type Benchmark = UnValidatedDto of UnValidatedDto | ValidatedDto of ValidatedDto

    //let unValidatedDto: UnValidatedDto = {Name = UnValidatedName "Steve"; TypeId = 3uy}
    //let validatedDto: ValidatedDto = {Name = ValidatedName "Steve"; TypeId = 3uy }

    //type transformName = UnValidatedName -> Result<ValidatedName, string>

    //let validateName (unValidatedName: UnValidatedName) =
    //    let (UnValidatedName name) = unValidatedName
    //    if name.Length < 255 then Ok (ValidatedName name) else Error "Invalid name"
     
    //let convert (f: transformName) (t: UnValidatedDto) =
    //    let bob = f t.Name
    //    match bob with
    //    | Ok s ->  Some (ValidatedDto {Name = s; TypeId = 3uy })
    //    | Error s ->  None
