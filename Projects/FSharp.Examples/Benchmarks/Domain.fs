namespace Fsharp.Examples.Benchmarks
open System

module CalculationFrequency =

    type CalculationFrequency = Day| Month | Quarter

    [<Literal>]
    let private DayValue = 1uy

    [<Literal>]
    let private MonthValue = 2uy

    [<Literal>]
    let private QuarterValue = 3uy

    let value calculationFrequency = 
        match calculationFrequency with
        | Day -> DayValue
        | Month -> MonthValue
        | Quarter -> QuarterValue

    let create frquencyId = 
        match frquencyId with
        | DayValue -> Ok Day
        | MonthValue -> Ok Month
        | QuarterValue -> Ok Quarter
        | _ -> Error "Invalid FrquencyId"

module NumberOfDaysInYear =

    type NumberOfDaysInYear = Days365PerYear | Days252PerYear

    [<Literal>]
    let private Days365PerYearValue = 1uy

    [<Literal>]
    let private Days252PerYearValue = 2uy

    let value numberOfDaysInYear = 
        match numberOfDaysInYear with
        | Days365PerYear -> Days365PerYearValue
        | Days252PerYear -> Days252PerYearValue

    let create numberOfDaysInYearId = 
        match numberOfDaysInYearId with
        | Days365PerYearValue -> Ok Days365PerYear
        | Days252PerYearValue -> Ok Days252PerYear
        | _ -> Error "Invalid FrquencyId"

module BenchmarkDtoValidation =
    open BenchmarkRequests

    [<Literal>]
    let private MaxNameLength = 255

    [<Literal>]
    let private MaxDescriptionLength = 350
    
    type Currency = { Id: int; Name: string; Code: string }
    type Day = { Day: DateOnly }
    type Calendar = { 
        ExcludeMonday: bool
        ExcludeTuesday: bool 
        ExcludeWednesday: bool
        ExcludeThursday: bool
        ExcludeFriday: bool 
        ExcludeSaturday: bool 
        ExcludeSunday: bool
        OtherDaysToExclude: Day list 
    }
    type NonBusinessDayCalendarData = { Id: int; Name: string; Calendar: Calendar}
    type LagDay = { LagDay: DateOnly }

    type CreateBenchmarkDtoType = 
        | CreateAbsoluteReturnBenchmark
        | CreateAssetWeightedBenchmark
        | CreateCompositeReturnBenchmark
        | CreateCompositeWeightedBenchmark
        | CreatePullForwardBenchmark
        | CreateStaticWeightedBenchmark

    let getBenchmarkTypeId benchamarkType =
        match benchamarkType with
        | CreateAbsoluteReturnBenchmark -> 1uy
        | CreateAssetWeightedBenchmark -> 2uy
        | CreateCompositeReturnBenchmark -> 3uy
        | CreateCompositeWeightedBenchmark -> 4uy
        | CreatePullForwardBenchmark -> 5uy
        | CreateStaticWeightedBenchmark -> 6uy

    let (|IsValidTypeId|) (createBenchmarkDtoType: CreateBenchmarkDtoType) (typId: byte)  = 
        let benchmarkTypeId = createBenchmarkDtoType |> getBenchmarkTypeId
        benchmarkTypeId = typId

    let validateTypeId benchmarkDtoType createBenchmarkDto =
        match createBenchmarkDto.TypeId with
        | IsValidTypeId benchmarkDtoType true  -> Ok createBenchmarkDto
        | _ -> Error "InvalidType Id"

    let validateFrequencyId (createBenchmarkDto: CreateBenchmarkDto) = 
        let result = createBenchmarkDto.TypeId |> CalculationFrequency.create
        match result with
        | Ok _ -> Ok createBenchmarkDto
        | _ -> Error "InvalidFrquencyType Id"

    let private nameIsNone (name: string option) = 
        let result = name.IsSome
        match result with
        | true  -> Ok (name |> Option.get)
        | _ -> Error "Name is Empty"

    let private nameIsTooLong (name: string) = 
        let result = name.Length > MaxNameLength
        match result with
        | true  -> Ok name
        | _ -> Error $"Name exceeds max character lenght of {MaxNameLength}"

    let validateName (createBenchmarkDto: CreateBenchmarkDto) = 
        let isValidName = 
            Ok createBenchmarkDto.Name
            |> Result.bind nameIsNone
            |> Result.bind nameIsTooLong
        match isValidName with
        | Ok s -> Ok createBenchmarkDto
        | Error err -> Error err

    let private numberOfDaysInYearIsProvided (numberOfDaysInYearId: byte option) = 
        let result = numberOfDaysInYearId.IsSome
        match result with
        | true  -> Ok (numberOfDaysInYearId |> Option.get)
        | _ -> Error "NumberOfDaysInYear must be provided"

    let private numberOfDaysInYearIdIsValid (numberOfDaysInYearId: byte) = 
        let result = numberOfDaysInYearId |> NumberOfDaysInYear.create
        match result with
        | Ok _  -> Ok numberOfDaysInYearId
        | _ -> Error "NumberOfDaysInYear is not valid"

    let validateNumberOfDaysInYearId (createBenchmarkDto: CreateBenchmarkDto) = 
        let isValidNumberOfDaysInYear = 
            Ok createBenchmarkDto.NumberOfDaysInYearId
            |> Result.bind numberOfDaysInYearIsProvided
            |> Result.bind numberOfDaysInYearIdIsValid
        match isValidNumberOfDaysInYear with
        | Ok _ -> Ok createBenchmarkDto
        | Error err -> Error err

    let validateDescription (description: string option) = 
        let result = description.IsNone || (description |> Option.get).Length < MaxDescriptionLength
        match result with
        | true  -> Ok (description |> Option.get)
        | _ -> Error "Name is Empty"

module CreateValidAbsoluteReturnBenchmarkDto =
    open BenchmarkRequests.CreateAbsoluteReturnBenchmarkDto
    open CalculationFrequency
    open NumberOfDaysInYear
    open BenchmarkDtoValidation

    type ValidatedCreateAbsoluteReturnBenchmarkDto = { 
        Name: string
        CalculationFrequency: CalculationFrequency
        Currency: Currency
        NonBusinessDayCalendarData: NonBusinessDayCalendarData
        IncludeBookedValues: bool
        NumberOfDaysInYear: NumberOfDaysInYear
        IsLagged: bool
        LagDay: LagDay option
        Description: string option
        BenchmarkTimeWindows : TimeWindowDto list option
    }

    let createAbsoluteReturnBenchmarkDto createBenchmarkDto = 
        toDto createBenchmarkDto |> Ok

    let calendar: Calendar = {
        ExcludeMonday = false
        ExcludeTuesday = false
        ExcludeWednesday = false
        ExcludeThursday = false
        ExcludeFriday = false
        ExcludeSaturday = true
        ExcludeSunday = true
        OtherDaysToExclude = []
    }

    let validatedBenchmark (createBenchmarkDto: CreateAbsoluteReturnBenchmarkDto) = { 
        Name = createBenchmarkDto.Name.Value
        CalculationFrequency = Day
        Currency = { Id = 1; Name = "US Dollar"; Code = "USD" }
        NonBusinessDayCalendarData = { Id = 5; Name = "Weekdays Only"; Calendar = calendar }
        IncludeBookedValues = createBenchmarkDto.IncludeBookedValues
        NumberOfDaysInYear = Days365PerYear
        IsLagged = createBenchmarkDto.IsLagged
        LagDay = None
        Description = createBenchmarkDto.Description
        BenchmarkTimeWindows = createBenchmarkDto.TimeWindows
    }

    let create (createBenchmarkDto: BenchmarkRequests.CreateBenchmarkDto) = 
        Ok createBenchmarkDto 
        |> Result.bind (validateTypeId CreateAbsoluteReturnBenchmark)
        |> Result.bind validateName
        |> Result.bind validateFrequencyId
        |> Result.bind validateNumberOfDaysInYearId
        |> Result.bind createAbsoluteReturnBenchmarkDto
        |> Result.map validatedBenchmark

//module BenchMarkName =

//    type T = BenchMarkName of string

//    // create with continuation
//    let createWithCont success failure (s: string) =
//        if s.Length < 255
//            then success (BenchMarkName s)
//            else failure "Email address must contain an @ sign"

//    // create directly
//    let create s =
//        let success e = Ok e
//        let failure _  = Error "foo"
//        createWithCont success failure s

//    // unwrap with continuation
//    let apply f (BenchMarkName e) = f e

//    // unwrap directly
//    let value e = apply id e

module Example =

    type UnValidatedName = UnValidatedName of string
    type ValidatedName = ValidatedName of string

    type UnValidatedDto = {Name: UnValidatedName; TypeId: byte }
    type ValidatedDto = {Name: ValidatedName; TypeId: byte}

    type Benchmark = UnValidatedDto of UnValidatedDto | ValidatedDto of ValidatedDto

    let unValidatedDto: UnValidatedDto = {Name = UnValidatedName "Steve"; TypeId = 3uy}
    let validatedDto: ValidatedDto = {Name = ValidatedName "Steve"; TypeId = 3uy }

    type transformName = UnValidatedName -> Result<ValidatedName, string>

    let validateName (unValidatedName: UnValidatedName) =
        let (UnValidatedName name) = unValidatedName
        if name.Length < 255 then Ok (ValidatedName name) else Error "Invalid name"
     
    let convert (f: transformName) (t: UnValidatedDto) =
        let bob = f t.Name
        match bob with
        | Ok s ->  Some (ValidatedDto {Name = s; TypeId = 3uy })
        | Error s ->  None


    //let foo name =
    //    let bob = BenchMarkName.create name
    //    match bob with 
    //    | Ok s -> ValidatedName (BenchMarkName.value |> string)
    //    | Error s -> UnValidatedName

    //let foo unValidatedDto = 
    //    let g = Ok |> Result.bind (convert validateName unValidatedDto)
    //    g |> Result.map ("DDD")

        //match convert validateName unValidatedDto with
        //| Ok s -> s
        //| Error s -> Error "D"
    