namespace Fsharp.Examples.Benchmarks

module CreateBenchmark =
    let holder = 2


//module BenchmarkDtos2 =
//    open BenchmarkRequestDtos

//     type CreateAbsoluteReturnBenchmarkTimeWindowDto = { 
//        FromDate: DateTime
//        ReturnComponentBenchmarkId: Guid option
//        ReturnComponentResourceId: int64 option
//        BasisPointAdjustment: int
//    }

//    type CreateAbsoluteReturnBenchmarkDto = { 
//        Name: string option
//        CalculationFrequencyId: byte
//        BaseCurrencyCode: string option
//        PortfolioAnalyticsCalendarId: int option
//        IncludeBookedValues: bool
//        NumberOfDaysInYearId: byte option
//        IsLagged: bool
//        CustomLagDate: DateOnly option
//        Description: string option
//        TimeWindows: CreateAbsoluteReturnBenchmarkTimeWindowDto list option
//    }

//    type CreateAssetWeightedBenchmarkDto = { 
//        Name: string option
//        CalculationFrequencyId: byte
//        BaseCurrencyCode: string option
//        WeightingMethodologyId: byte
//        PortfolioAnalyticsCalendarId: int option
//        IncludeBookedValues: bool
//        PerformanceVariationCode: string option
//        IsLagged: bool
//        CustomLagDate: DateOnly option
//        Description: string option
//        TimeWindows: BenchmarkTimeWindowDto list option
//    }

//    type BenchmarkDto = CreateAbsoluteReturnBenchmarkDto | CreateAssetWeightedBenchmarkDto

//    type BenchmarkType = AbsoluteReturn | AssetWeighted

//    let getBenchmarkTypeId typeId =
//        match typeId with
//        | 1uy -> AbsoluteReturn
//        | 2uy -> AssetWeighted
//        | _  -> failwith "Unknown TypeId."

//    let toCreateAbsoluteReturnBenchmarkDto (createBenchmarkDto: CreateBenchmarkDto) : CreateAbsoluteReturnBenchmarkDto =

//        let timwWindows = 
//            match  createBenchmarkDto.TimeWindows with
//            | None -> None
//            | Some tw -> 
//                tw 
//                |> List.map(fun tw -> 
//                    { 
//                        FromDate= tw.FromDate
//                        ReturnComponentBenchmarkId = tw.ReturnComponentBenchmarkId
//                        ReturnComponentResourceId = tw.ReturnComponentResourceId
//                        BasisPointAdjustment = tw.BasisPointAdjustment |> Option.defaultValue 0
//                    })
//                |> Some
//        {
//             Name = createBenchmarkDto.Name
//             CalculationFrequencyId = createBenchmarkDto.CalculationFrequencyId
//             BaseCurrencyCode = createBenchmarkDto.BaseCurrencyCode
//             PortfolioAnalyticsCalendarId = createBenchmarkDto.PortfolioAnalyticsCalendarId
//             IncludeBookedValues = createBenchmarkDto.IncludeBookedValues
//             NumberOfDaysInYearId = createBenchmarkDto.NumberOfDaysInYearId
//             IsLagged = createBenchmarkDto.IsLagged
//             CustomLagDate = createBenchmarkDto.CustomLagDate
//             Description = createBenchmarkDto.Description
//             TimeWindows = timwWindows
//        }

//    let toCreateAssetWeightedBenchmarkDto (createBenchmarkDto: CreateBenchmarkDto) : CreateAssetWeightedBenchmarkDto =
//        {
//             Name = createBenchmarkDto.Name
//             CalculationFrequencyId = createBenchmarkDto.CalculationFrequencyId
//             BaseCurrencyCode = createBenchmarkDto.BaseCurrencyCode
//             WeightingMethodologyId = 1uy
//             PortfolioAnalyticsCalendarId = createBenchmarkDto.PortfolioAnalyticsCalendarId
//             IncludeBookedValues = createBenchmarkDto.IncludeBookedValues
//             PerformanceVariationCode = createBenchmarkDto.PerformanceVariationCode
//             IsLagged = createBenchmarkDto.IsLagged
//             CustomLagDate = createBenchmarkDto.CustomLagDate
//             Description = createBenchmarkDto.Description
//             TimeWindows = createBenchmarkDto.TimeWindows
//        }

//    let translateDto (createBenchmarkDto: CreateBenchmarkDto) =
//       let benchMarkType = createBenchmarkDto.TypeId |> getBenchmarkTypeId
//       match benchMarkType with
//       | AbsoluteReturn -> 5
//       | AssetWeighted-> 6

