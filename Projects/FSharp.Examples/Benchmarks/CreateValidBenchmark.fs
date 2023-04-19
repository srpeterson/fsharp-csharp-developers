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
    type LagDay = { Quarter: int; DaysSinceEpoch: int; AsDateTime: DateTime; (* A bunch of other properties ..*) } 
    type ValidLagDay = ValidLagDay of LagDay

    // Pretend that this is the Dto passed to us from the API endpoint. We have no clue as to what user gave us and what is required
    type UnValidatedBenchmarkDto = { Name: string option; TypeId: byte; IsLagged: bool; LagDate: DateOnly option (* A bunch of other properties ..*) }

    // This is the Dto that has all the properties validated and will use for rest of code
    type ValidatedBenchmarkDto = { Name: ValidBenchmarkName; TypeId: ValidBenchmarkTypeId; IsLagged: bool; LagDate: ValidLagDay option }

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
        name |> validateOption predicate "Benchmark name can't be None"

    let private nameIsEmpty (name: string) = 
        let predicate (s: string) = not (String.IsNullOrWhiteSpace(s))
        name |> validate predicate $"Benchmark name can not be empty"

    let private nameIsTooLong (name: string) = 
        let predicate (s: string) = s.Length < MaxNameLength
        name |> validate predicate $"Name exceeds max character length of '{MaxNameLength}'"

    let create (name: string option) =
         validation {
            let! _ = name |> nameIsNone
            let! _ = name |> Option.traverseResult  nameIsEmpty
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

    let daysSinceEpoch (date: DateOnly) = 
        let baseDateDayNumber =(new DateOnly(1900, 1, 1)).DayNumber
        date.DayNumber - baseDateDayNumber

    let private ``date is before january 1 1900`` (lagDate: DateOnly) = 
        let predicate (value: DateOnly) = value >= new DateOnly (1900, 1, 1)
        lagDate |> validate predicate "Date can not be before Jaunary 1, 1900"

    let private isEndOfMonth (lagDate: DateOnly) = 
        let predicate (value: DateOnly)  =  DateTime.DaysInMonth(value.Year, value.Month) = value.Day;
        lagDate |> validate predicate "Lag Date must be end of month"
    
    let create (date: DateOnly option) =
        validation {
            let! _ = date |> Option.traverseResult ``date is before january 1 1900``
            and! _ = date |> Option.traverseResult isEndOfMonth
            let lagDate = (date |> Option.get)
            let quarter = (lagDate.Month - 1) / 3 + 1;
            let daysSinceEpoch = lagDate |> daysSinceEpoch
            let dateTime = new DateTime(lagDate.Year, lagDate.Month, lagDate.Day)
            return  Some (ValidLagDay { Quarter = quarter; DaysSinceEpoch = daysSinceEpoch;  AsDateTime = dateTime})
        }

    // "Unwraps" value if needed
    let value (ValidLagDay s) = s

module CreateValidatedAbsoluteReturnDto =
    
    // This validates the properties and returns a a ValidatedDto or a list of errors 
    let toValidDto (unValidatedDto: UnValidatedBenchmarkDto) = 
        validation {
            let! validName = BenchmarkName.create unValidatedDto.Name
            and! validHeight = BenchmarkTypeId.create BenchmarkType.AbsoluteReturnBenchmark unValidatedDto.TypeId
            and! validDob = LagDay.create unValidatedDto.LagDate

            return { Name = validName; TypeId = validHeight; IsLagged = unValidatedDto.IsLagged; LagDate = validDob }
        }
