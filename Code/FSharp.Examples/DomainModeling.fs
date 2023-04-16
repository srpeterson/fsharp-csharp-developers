namespace Fsharp.Examples
open System

module BenchmarkRequestDtos =

    type BenchmarkLineItemDto = { 
        ReturnComponentBenchmarkId: Guid option
        ReturnComponentResourceId: int64 option
        Weight: decimal option
        WeightingComponentTagId: int option
        WeightingComponentResourceId: int64 option
    }

    type BenchmarkTimeWindowDto = { 
        FromDate: DateTime
        AssetSelectionSetId: int64 option
        TagRefPropertyDefinitionId: int option
        TagId: int option
        ReturnComponentBenchmarkId: Guid option
        ReturnComponentResourceId: int64 option
        BasisPointAdjustment: int option
        PullForwardLength: int option
        LineItems: BenchmarkLineItemDto list option
    }

    type BenchmarkRebalanceFrequencyCustomMonthsDto = { 
        January: bool
        February: bool
        March: bool
        April: bool
        May: bool
        June: bool
        July: bool
        August: bool
        September: bool
        October: bool
        November: bool
        December: bool
    }
    
    type CreateBenchmarkDto = { 
        Name: string option
        TypeId: byte
        CalculationFrequencyId: byte
        RebalanceFrequencyId: byte option
        CreateBenchmarkRebalanceFrequencyCustomMonthsDto: BenchmarkRebalanceFrequencyCustomMonthsDto option
        BaseCurrencyCode: string option
        WeightingMethodologyId: byte option
        PortfolioAnalyticsCalendarId: int option
        IncludeBookedValues: bool
        NumberOfDaysInYearId: byte option
        UseDailyToMonthlyLinking: bool option
        PerformanceVariationCode: string option
        IsLagged: bool
        CustomLagDate: DateOnly option
        Description: string option
        TimeWindows: BenchmarkTimeWindowDto list option
    }

    type UpdateBenchmarkDto = { 
        Name: string option
        TypeId: byte
        CalculationFrequencyId: byte
        RebalanceFrequencyId: byte option
        CreateBenchmarkRebalanceFrequencyCustomMonthsDto: BenchmarkRebalanceFrequencyCustomMonthsDto option
        BaseCurrencyCode: string option
        WeightingMethodologyId: byte option
        PortfolioAnalyticsCalendarId: int option
        IncludeBookedValues: bool
        NumberOfDaysInYearId: byte option
        UseDailyToMonthlyLinking: bool option
        PerformanceVariationCode: string option
        IsLagged: bool
        CustomLagDate: DateOnly option
        Description: string option
        TimeWindows: BenchmarkTimeWindowDto list option
    }

module CreateAbsoluteReturnBenchmarkDto =
    open BenchmarkRequestDtos

    type TimeWindowDto = { 
        FromDate: DateTime
        ReturnComponentBenchmarkId: Guid option
        ReturnComponentResourceId: int64 option
        BasisPointAdjustment: int
    }

    type CreateAbsoluteReturnBenchmarkDto = { 
        Name: string option
        CalculationFrequencyId: byte
        BaseCurrencyCode: string option
        PortfolioAnalyticsCalendarId: int option
        IncludeBookedValues: bool
        NumberOfDaysInYearId: byte option
        IsLagged: bool
        CustomLagDate: DateOnly option
        Description: string option
        TimeWindows: TimeWindowDto list option
    }

    let toCreateAbsoluteReturnBenchmarkDto (createBenchmarkDto: CreateBenchmarkDto) : CreateAbsoluteReturnBenchmarkDto =

        let timeWindows = 
            match  createBenchmarkDto.TimeWindows with
            | None -> None
            | Some tw -> 
                tw 
                |> List.map(fun tw -> 
                    { 
                        FromDate= tw.FromDate
                        ReturnComponentBenchmarkId = tw.ReturnComponentBenchmarkId
                        ReturnComponentResourceId = tw.ReturnComponentResourceId
                        BasisPointAdjustment = tw.BasisPointAdjustment |> Option.defaultValue 0
                    })
                |> Some

        {
             Name = createBenchmarkDto.Name
             CalculationFrequencyId = createBenchmarkDto.CalculationFrequencyId
             BaseCurrencyCode = createBenchmarkDto.BaseCurrencyCode
             PortfolioAnalyticsCalendarId = createBenchmarkDto.PortfolioAnalyticsCalendarId
             IncludeBookedValues = createBenchmarkDto.IncludeBookedValues
             NumberOfDaysInYearId = createBenchmarkDto.NumberOfDaysInYearId
             IsLagged = createBenchmarkDto.IsLagged
             CustomLagDate = createBenchmarkDto.CustomLagDate
             Description = createBenchmarkDto.Description
             TimeWindows = timeWindows
        }

module CreateAssetWeightedBenchmarkDto =
    open BenchmarkRequestDtos

    type AssetWeightedLineItemDto = { 
        ReturnComponentBenchmarkId: Guid option
        ReturnComponentResourceId: int64 option
        WeightingComponentResourceId: int64
    }

    type TimeWindowDto = { 
        FromDate: DateTime
        LineItems: AssetWeightedLineItemDto list option
    }

    type CreateAssetWeightedBenchmarkDto = { 
        Name: string option
        CalculationFrequencyId: byte
        BaseCurrencyCode: string option
        WeightingMethodologyId: byte
        PortfolioAnalyticsCalendarId: int option
        IncludeBookedValues: bool
        PerformanceVariationCode: string option
        IsLagged: bool
        CustomLagDate: DateOnly option
        Description: string option
        TimeWindows: TimeWindowDto list option
    }

    let toCreateAssetWeightedBenchmarkDto (createBenchmarkDto: CreateBenchmarkDto) : CreateAssetWeightedBenchmarkDto =

        let lineItems (benchmarkLineItemDtos: BenchmarkLineItemDto list option) = 
            match  benchmarkLineItemDtos with
            | None -> None
            | Some tw -> 
                tw 
                |> List.map(fun li -> 
                    { 
                        ReturnComponentBenchmarkId= li.ReturnComponentBenchmarkId
                        ReturnComponentResourceId= li.ReturnComponentResourceId
                        WeightingComponentResourceId = li.WeightingComponentResourceId |> Option.defaultValue 0L
                    })
                |> Some

        let timeWindows = 
            match  createBenchmarkDto.TimeWindows with
            | None -> None
            | Some tw -> 
                tw 
                |> List.map(fun tw -> 
                    { 
                        FromDate= tw.FromDate
                        LineItems = tw.LineItems |> lineItems
                    })
                |> Some

        {
             Name = createBenchmarkDto.Name
             CalculationFrequencyId = createBenchmarkDto.CalculationFrequencyId
             BaseCurrencyCode = createBenchmarkDto.BaseCurrencyCode
             WeightingMethodologyId = createBenchmarkDto.WeightingMethodologyId |> Option.defaultValue 0uy
             PortfolioAnalyticsCalendarId = createBenchmarkDto.PortfolioAnalyticsCalendarId
             IncludeBookedValues = createBenchmarkDto.IncludeBookedValues
             PerformanceVariationCode = createBenchmarkDto.PerformanceVariationCode
             IsLagged = createBenchmarkDto.IsLagged
             CustomLagDate = createBenchmarkDto.CustomLagDate
             Description = createBenchmarkDto.Description
             TimeWindows = timeWindows
        }


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
      

