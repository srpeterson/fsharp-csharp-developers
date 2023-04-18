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
        let predicate (value: string option) = value.IsSome
        name |> validateOption predicate "Name is none"

    let private nameIsTooLong (name: string) = 
        let predicate (s: string) = s.Length < MaxNameLength
        name |> validate predicate $"Name exceeds max character length of '{MaxNameLength}'"

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
        let predicate (id: ValidBenchmarkTypeId) = id = ValidBenchmarkTypeId benchmarkTypeId
        (ValidBenchmarkTypeId typId) |> validate predicate $"Invalid TypeId"

    let value (ValidBenchmarkTypeId s) = s

type LagDay = { AsDateOnly: DateOnly;  DaysSinceEpoch: int } 
type ValidLagDay = private ValidLagDay of LagDay

module LagDay =
    open Validation

    let private ``date is before january 1 1900`` (lagDate: DateOnly) = 
        let predicate (value: DateOnly) = value >= new DateOnly (1900, 1, 1)
        lagDate |> validate predicate "Date can not be before Jaunary 1, 1900"

    let private iEndOfMonth (lagDate: DateOnly) = 
        let predicate (value: DateOnly)  =  DateTime.DaysInMonth(value.Year, value.Month) = value.Day;
        lagDate |> validate predicate "Lag Date must be end of month"

    let create (lagDate: DateOnly) =
        let isValidLagDate = 
            Ok lagDate
            |> Result.bind ``date is before january 1 1900``
            |> Result.bind iEndOfMonth
        match isValidLagDate with
        | Ok s -> Ok (ValidLagDay {AsDateOnly = s; DaysSinceEpoch = 5})
        | Error err -> Error err

    let value (ValidLagDay s) = s

module Example =
    open BenchmarkName
    open BenchmarkTypeId
    open LagDay

    type UnValidatedDto = { Name: string option; TypeId: byte; LagDate: DateOnly option }
    type ValidatedDto = { Name: ValidBenchmarkName; TypeId: ValidBenchmarkTypeId; LagDate: ValidLagDay option  }

    let unValidatedDto: UnValidatedDto = { Name = (Some "Steve"); TypeId = 3uy; LagDate = Some (new DateOnly(2022, 12,31)) }

    //let fred = BenchmarkName.create unValidatedDto.Name

    //let ggg = 
    //    let foo = 
    //        match fred with
    //        | Ok s ->  BenchmarkName.value s
    //        | Error err -> err
    //    5

    //let fred2 = BenchmarkTypeId.create BenchmarkType.AbsoluteReturnBenchmark unValidatedDto.TypeId

    //let ggg3 = 
    //    let foo = 
    //        match fred2 with
    //        | Ok s ->  BenchmarkTypeId.value s
    //        | Error err -> err
    //    5

    //let fred3 = 
    //    match unValidatedDto.LagDate with 
    //    | Some s -> Some (LagDay.create s)
    //    | None -> None

    //let foo = fred |> Result.bind fred2

    //3

    //here I want to go from UnvalidatedDto to ValidatedDto
    
    //type transform = UnValidatedDto -> ValidatedDto

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