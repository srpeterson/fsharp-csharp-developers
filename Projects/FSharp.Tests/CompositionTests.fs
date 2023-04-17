namespace FSharp.Examples.Tests

open System
open Xunit
open FsUnit.Xunit

module ``F# Composition Tests`` =
    open Fsharp.Examples.Composition

    [<Fact>]
    let ``subtractOneSquareAddTenPiped should return 91 when the input is 10`` () =
        let actual = subtractOneSquareAddTenPiped 10
        let expected = 91
        actual |>  should equal expected

    [<Fact>]
    let ``subtractOneSquareAddTenComposed should return 91 when the input is 10`` () =
        let actual = subtractOneSquareAddTenComposed 10
        let expected = 91
        actual |>  should equal expected

    [<Fact>]
    let ``gimmeOddNumbers should return a list of odd numbers after applying subtractOneSquareAddTenComposed to a list of intergers`` () =
        let actual = [1..10] |> gimmeOddNumbers
        let expected = [11; 19; 35; 59; 91]
        actual |>  should equal expected

module ``C# Composition Tests`` =
    open CSharp.Examples

    [<Fact>]
    let ``SubtractOneSquareAddTen should return 91 when the input is 10`` () =
        let compositionExample = new Composition()
        let actual = compositionExample.SubtractOneSquareAddTen(10)
        let expected = 91
        actual |>  should equal expected

module ``Foo`` =
    open Fsharp.Examples.Benchmarks.BenchmarkRequests
    open Fsharp.Examples.Benchmarks.CreateBenchmarkDto

    let createBenchmarkLineItemDto : BenchmarkLineItemDto =  {
        ReturnComponentBenchmarkId = Some (Guid.NewGuid())
        ReturnComponentResourceId = Some 6L
        Weight = Some 0.1M
        WeightingComponentTagId = Some 3
        WeightingComponentResourceId = Some 6L }

    let createBenchmarkTimeWindowDto : BenchmarkTimeWindowDto = {
        FromDate = new DateTime(2022, 5, 31)
        AssetSelectionSetId = Some 6L
        TagRefPropertyDefinitionId = Some 8
        TagId = Some 3
        ReturnComponentBenchmarkId = Some (Guid.NewGuid())
        ReturnComponentResourceId = Some 6L
        BasisPointAdjustment = Some 9
        PullForwardLength = Some 8
        LineItems = Some [createBenchmarkLineItemDto] }

    let createBenchmarkRebalanceFrequencyCustomMonthsDto : BenchmarkRebalanceFrequencyCustomMonthsDto =  {
        January = true
        February = true
        March = true
        April = true
        May = true
        June= true
        July = true
        August = true
        September = true
        October = true
        November = true
        December = true }

    let createBenchmarkDto : Fsharp.Examples.Benchmarks.BenchmarkRequests.CreateBenchmarkDto = 
        {
            Name = Some "foo"
            TypeId = 1uy
            CalculationFrequencyId = 6uy
            RebalanceFrequencyId = Some 6uy
            CreateBenchmarkRebalanceFrequencyCustomMonthsDto = Some createBenchmarkRebalanceFrequencyCustomMonthsDto
            BaseCurrencyCode = Some "USD"
            WeightingMethodologyId = Some 6uy
            PortfolioAnalyticsCalendarId = Some 6
            IncludeBookedValues = true
            NumberOfDaysInYearId = Some 6uy
            UseDailyToMonthlyLinking = Some true
            PerformanceVariationCode = Some "PerfCode"
            IsLagged = true
            CustomLagDate = None
            Description = Some "foo"
            TimeWindows = Some [createBenchmarkTimeWindowDto]
        }

    //[<Fact>]
    //let ``Test`` () =
    //    //let expected = createBenchmarkDto
    //    let result = Fsharp.Examples.Benchmarks.CreateBenchmarkDto.toCreateAbsoluteReturnBenchmarkDto createBenchmarkDto
        
    //    let dto = result |> Result.defaultValue

    //    let actual = true
    //    let expected = true
    //    actual |>  should equal expected
        
