namespace Fsharp.Examples.Benchmarks
open FsToolkit.ErrorHandling
open System

[<AutoOpen>]
module DomainTypes = 

    type BenchmarkType = 
        | AbsoluteReturnBenchmark
        | AssetWeightedBenchmark
        | CompositeReturnBenchmark
        | CompositeWeightedBenchmark
        | PullForwardBenchmark
        | StaticWeightedBenchmark

    type ValidBenchmarkName =  ValidBenchmarkName of string
    type ValidBenchmarkTypeId = ValidBenchmarkTypeId of byte
    type LagDay = { AsDateOnly: DateOnly; (* A bunch of other properties ..*) DaysSinceEpoch: int } 
    type ValidLagDay = ValidLagDay of LagDay

module BenchmarkValidation = 

    let validate validator failure input =
        if validator input then Ok input else Error failure

    let validateOption validator failure (input: 'a option) =
        if validator input then Ok (input |> Option.get) else Error failure

module BenchmarkName =
    open BenchmarkValidation

    [<Literal>]
    let private MaxNameLength = 255

    let private nameIsNone (name: string option) = 
        let predicate (value: string option) = value.IsSome
        name |> validateOption predicate "Name is none"

    let private nameIsTooLong (name: string) = 
        let predicate (s: string) = s.Length < MaxNameLength
        name |> validate predicate $"Name exceeds max character length of '{MaxNameLength}'"

    let create (name: string option) =
         result {
            let! _ = name |> nameIsNone
            let! _ = name |> Option.traverseResult  nameIsTooLong
            return  ValidBenchmarkName (name |> Option.get)
        }
    // "Unwraps" value if needed
    let value (ValidBenchmarkName s) = s

module BenchmarkTypeId =
    open BenchmarkValidation

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
        let predicate (id: ValidBenchmarkTypeId) = id = ValidBenchmarkTypeId benchmarkTypeId
        (ValidBenchmarkTypeId typId) |> validate predicate $"Invalid Benchmark TypeId"
    
    // "Unwraps" value if needed
    let value (ValidBenchmarkTypeId s) = s

module LagDay =
    open BenchmarkValidation

    let private ``date is before january 1 1900`` (lagDate: DateOnly) = 
        let predicate (value: DateOnly) = value >= new DateOnly (1900, 1, 1)
        lagDate |> validate predicate "Date can not be before Jaunary 1, 1900"

    let private isEndOfMonth (lagDate: DateOnly) = 
        let predicate (value: DateOnly)  =  DateTime.DaysInMonth(value.Year, value.Month) = value.Day;
        lagDate |> validate predicate "Lag Date must be end of month"
    
    let create (lagDate: DateOnly option) =
        result {
            let! _ = lagDate |> Option.traverseResult ``date is before january 1 1900``
            let! _ = lagDate |> Option.traverseResult isEndOfMonth
            return  Some (ValidLagDay { AsDateOnly = (lagDate |> Option.get); DaysSinceEpoch = 5})
        }
    
    // "Unwraps" value if needed
    let value (ValidLagDay s) = s

module CreateValidatedABsoluteReturnDto =

    // Pretend that this is the Dto passed to us from enpoint
    type UnValidatedDto = { Name: string option; TypeId: byte; (* A bunch of other properties ..*) LagDate: DateOnly option }

    // This is the Dto that has all the properties validated and will use for rest of code
    type ValidatedDto = { Name: ValidBenchmarkName; TypeId: ValidBenchmarkTypeId; LagDate: ValidLagDay option }

    // This validates the properties and returns a a ValidatedDto or a list of errors 
    let toValidatedDto (unValidatedDto: UnValidatedDto) = 

        validation {
            let! validName = BenchmarkName.create unValidatedDto.Name
            and! validHeight = BenchmarkTypeId.create BenchmarkType.AbsoluteReturnBenchmark unValidatedDto.TypeId
            and! validDob = LagDay.create unValidatedDto.LagDate

            return { Name = validName; TypeId = validHeight; LagDate = validDob }
        }

    
    //let testIt = toValidatedDto { Name = (Some "Steve"); TypeId = 1uy; LagDate = Some (new DateOnly(2022, 12, 31)) }