﻿namespace Fsharp.Examples.Benchmarks
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

module CreateAbsoluteReturnBenchmarkDto =
    open BenchmarkRequestDtos

    type TimeWindowDto = { 
        FromDate: DateTime
        ReturnComponentBenchmarkId: Guid option
        ReturnComponentResourceId: int64 option
        BasisPointAdjustment: int option
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
                        FromDate = tw.FromDate
                        ReturnComponentBenchmarkId = tw.ReturnComponentBenchmarkId
                        ReturnComponentResourceId = tw.ReturnComponentResourceId
                        BasisPointAdjustment = tw.BasisPointAdjustment
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
        WeightingComponentResourceId: int64 option
    }

    type TimeWindowDto = { 
        FromDate: DateTime
        LineItems: AssetWeightedLineItemDto list option
    }

    type CreateAssetWeightedBenchmarkDto = { 
        Name: string option
        CalculationFrequencyId: byte
        BaseCurrencyCode: string option
        WeightingMethodologyId: byte option
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
                        WeightingComponentResourceId = li.WeightingComponentResourceId
                    })
                |> Some

        let timeWindows = 
            match  createBenchmarkDto.TimeWindows with
            | None -> None
            | Some tw -> 
                tw 
                |> List.map(fun tw -> 
                    { 
                        FromDate = tw.FromDate
                        LineItems = tw.LineItems |> lineItems
                    })
                |> Some

        {
             Name = createBenchmarkDto.Name
             CalculationFrequencyId = createBenchmarkDto.CalculationFrequencyId
             BaseCurrencyCode = createBenchmarkDto.BaseCurrencyCode
             WeightingMethodologyId = createBenchmarkDto.WeightingMethodologyId
             PortfolioAnalyticsCalendarId = createBenchmarkDto.PortfolioAnalyticsCalendarId
             IncludeBookedValues = createBenchmarkDto.IncludeBookedValues
             PerformanceVariationCode = createBenchmarkDto.PerformanceVariationCode
             IsLagged = createBenchmarkDto.IsLagged
             CustomLagDate = createBenchmarkDto.CustomLagDate
             Description = createBenchmarkDto.Description
             TimeWindows = timeWindows
        }

module CreateCompositeReturnBenchmarkDto =
    open BenchmarkRequestDtos

    type TimeWindowDto = { 
        FromDate: DateTime
        AssetSelectionSetId: int64 option
        TagRefPropertyDefinitionId: int option
        TagId: int option
    }

    type CreateCompositeReturnBenchmarkDto = { 
        Name: string option
        CalculationFrequencyId: byte
        BaseCurrencyCode: string option
        PortfolioAnalyticsCalendarId: int option
        IncludeBookedValues: bool
        PerformanceVariationCode: string option
        IsLagged: bool
        CustomLagDate: DateOnly option
        Description: string option
        TimeWindows: TimeWindowDto list option
    }

    let toCreateCompositeReturnBenchmarkDto (createBenchmarkDto: CreateBenchmarkDto) : CreateCompositeReturnBenchmarkDto =

        let timeWindows = 
            match  createBenchmarkDto.TimeWindows with
            | None -> None
            | Some tw -> 
                tw 
                |> List.map(fun tw -> 
                    { 
                        FromDate = tw.FromDate
                        AssetSelectionSetId = tw.AssetSelectionSetId
                        TagRefPropertyDefinitionId = tw.TagRefPropertyDefinitionId
                        TagId = tw.TagId
                    })
                |> Some

        {
             Name = createBenchmarkDto.Name
             CalculationFrequencyId = createBenchmarkDto.CalculationFrequencyId
             BaseCurrencyCode = createBenchmarkDto.BaseCurrencyCode
             PortfolioAnalyticsCalendarId = createBenchmarkDto.PortfolioAnalyticsCalendarId
             IncludeBookedValues = createBenchmarkDto.IncludeBookedValues
             PerformanceVariationCode = createBenchmarkDto.PerformanceVariationCode
             IsLagged = createBenchmarkDto.IsLagged
             CustomLagDate = createBenchmarkDto.CustomLagDate
             Description = createBenchmarkDto.Description
             TimeWindows = timeWindows
        }

module CreateCompositeWeightedBenchmarkDto =
    open BenchmarkRequestDtos

    type CompositeWeightedLineItemDto = { 
        ReturnComponentBenchmarkId: Guid option
        ReturnComponentResourceId: int64 option
        WeightingComponentTagId: int option
    }

    type TimeWindowDto = { 
        FromDate: DateTime
        AssetSelectionSetId: int64 option
        TagRefPropertyDefinitionId: int option
        LineItems: CompositeWeightedLineItemDto list option
    }

    type CreateCompositeWeightedBenchmarkDto = { 
        Name: string option
        CalculationFrequencyId: byte
        BaseCurrencyCode: string option
        WeightingMethodologyId: byte option
        PortfolioAnalyticsCalendarId: int option
        IncludeBookedValues: bool
        PerformanceVariationCode: string option
        IsLagged: bool
        CustomLagDate: DateOnly option
        Description: string option
        TimeWindows: TimeWindowDto list option
    }

    let toCreateCompositeWeightedBenchmarkDto (createBenchmarkDto: CreateBenchmarkDto) : CreateCompositeWeightedBenchmarkDto =

        let lineItems (benchmarkLineItemDtos: BenchmarkLineItemDto list option) = 
            match  benchmarkLineItemDtos with
            | None -> None
            | Some tw -> 
                tw 
                |> List.map(fun li -> 
                    { 
                        ReturnComponentBenchmarkId= li.ReturnComponentBenchmarkId
                        ReturnComponentResourceId= li.ReturnComponentResourceId
                        WeightingComponentTagId = li.WeightingComponentTagId
                    })
                |> Some

        let timeWindows = 
            match  createBenchmarkDto.TimeWindows with
            | None -> None
            | Some tw -> 
                tw 
                |> List.map(fun tw -> 
                    { 
                        FromDate = tw.FromDate
                        AssetSelectionSetId = tw.AssetSelectionSetId
                        TagRefPropertyDefinitionId = tw.TagRefPropertyDefinitionId
                        LineItems = tw.LineItems |> lineItems
                    })
                |> Some

        {
             Name = createBenchmarkDto.Name
             CalculationFrequencyId = createBenchmarkDto.CalculationFrequencyId
             BaseCurrencyCode = createBenchmarkDto.BaseCurrencyCode
             WeightingMethodologyId = createBenchmarkDto.WeightingMethodologyId
             PortfolioAnalyticsCalendarId = createBenchmarkDto.PortfolioAnalyticsCalendarId
             IncludeBookedValues = createBenchmarkDto.IncludeBookedValues
             PerformanceVariationCode = createBenchmarkDto.PerformanceVariationCode
             IsLagged = createBenchmarkDto.IsLagged
             CustomLagDate = createBenchmarkDto.CustomLagDate
             Description = createBenchmarkDto.Description
             TimeWindows = timeWindows
        }

module CreatePullForwardBenchmarkDto =
    open BenchmarkRequestDtos

    type TimeWindowDto = { 
        FromDate: DateTime
        ReturnComponentBenchmarkId: Guid option
        ReturnComponentResourceId: int64 option
        PullForwardLength: int option
    }

    type CreatePullForwardBenchmarkDto = { 
        Name: string option
        CalculationFrequencyId: byte
        BaseCurrencyCode: string option
        PortfolioAnalyticsCalendarId: int option
        IncludeBookedValues: bool
        IsLagged: bool
        CustomLagDate: DateOnly option
        Description: string option
        TimeWindows: TimeWindowDto list option
    }

    let toCreatePullForwardBenchmarkDto (createBenchmarkDto: CreateBenchmarkDto) : CreatePullForwardBenchmarkDto =

        let timeWindows = 
            match  createBenchmarkDto.TimeWindows with
            | None -> None
            | Some tw -> 
                tw 
                |> List.map(fun tw -> 
                    { 
                        FromDate = tw.FromDate
                        ReturnComponentBenchmarkId = tw.ReturnComponentBenchmarkId
                        ReturnComponentResourceId = tw.ReturnComponentResourceId
                        PullForwardLength = tw.PullForwardLength
                    })
                |> Some

        {
             Name = createBenchmarkDto.Name
             CalculationFrequencyId = createBenchmarkDto.CalculationFrequencyId
             BaseCurrencyCode = createBenchmarkDto.BaseCurrencyCode
             PortfolioAnalyticsCalendarId = createBenchmarkDto.PortfolioAnalyticsCalendarId
             IncludeBookedValues = createBenchmarkDto.IncludeBookedValues
             IsLagged = createBenchmarkDto.IsLagged
             CustomLagDate = createBenchmarkDto.CustomLagDate
             Description = createBenchmarkDto.Description
             TimeWindows = timeWindows
        }

module CreateStaticWeightedBenchmarkDto =
    open BenchmarkRequestDtos

    type StaticWeightedLineItemDto = { 
        ReturnComponentBenchmarkId: Guid option
        ReturnComponentResourceId: int64 option
        Weight: decimal option
    }

    type TimeWindowDto = { 
        FromDate: DateTime
        LineItems: StaticWeightedLineItemDto list option
    }

    type CreateStaticWeightedBenchmarkDto = { 
        Name: string option
        CalculationFrequencyId: byte
        RebalanceFrequencyId: byte option
        RebalanceFrequencyCustomMonths: BenchmarkRebalanceFrequencyCustomMonthsDto option
        BaseCurrencyCode: string option
        PortfolioAnalyticsCalendarId: int option
        IncludeBookedValues: bool
        UseDailyToMonthlyLinking: bool option
        IsLagged: bool
        CustomLagDate: DateOnly option
        Description: string option
        TimeWindows: TimeWindowDto list option
    }

    let toCreateStaticWeightedBenchmarkDto (createBenchmarkDto: CreateBenchmarkDto) : CreateStaticWeightedBenchmarkDto =

        let lineItems (benchmarkLineItemDtos: BenchmarkLineItemDto list option) = 
            match  benchmarkLineItemDtos with
            | None -> None
            | Some tw -> 
                tw 
                |> List.map(fun li -> 
                    { 
                        ReturnComponentBenchmarkId= li.ReturnComponentBenchmarkId
                        ReturnComponentResourceId= li.ReturnComponentResourceId
                        Weight= li.Weight
                    })
                |> Some

        let timeWindows = 
            match  createBenchmarkDto.TimeWindows with
            | None -> None
            | Some tw -> 
                tw 
                |> List.map(fun tw -> 
                    { 
                        FromDate = tw.FromDate
                        LineItems = tw.LineItems |> lineItems
                    })
                |> Some

        {
             Name = createBenchmarkDto.Name
             CalculationFrequencyId = createBenchmarkDto.CalculationFrequencyId
             RebalanceFrequencyId = createBenchmarkDto.RebalanceFrequencyId
             RebalanceFrequencyCustomMonths = createBenchmarkDto.CreateBenchmarkRebalanceFrequencyCustomMonthsDto
             BaseCurrencyCode = createBenchmarkDto.BaseCurrencyCode
             PortfolioAnalyticsCalendarId = createBenchmarkDto.PortfolioAnalyticsCalendarId
             IncludeBookedValues = createBenchmarkDto.IncludeBookedValues
             UseDailyToMonthlyLinking = createBenchmarkDto.UseDailyToMonthlyLinking
             IsLagged = createBenchmarkDto.IsLagged
             CustomLagDate = createBenchmarkDto.CustomLagDate
             Description = createBenchmarkDto.Description
             TimeWindows = timeWindows
        }

      

