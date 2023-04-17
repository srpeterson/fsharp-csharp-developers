namespace Fsharp.Examples.Benchmarks

module CreateBenchmarkDto =
    open BenchmarkRequestDtos

    type CreateBenchmarkDtoType = 
        | CreateAbsoluteReturnBenchmarkDto
        | CreateAssetWeightedBenchmarkDto
        | CreateCompositeReturnBenchmarkDto
        | CreateCompositeWeightedBenchmarkDto
        | CreatePullForwardBenchmarkDto
        | CreateStaticWeightedBenchmarkDto

    //let getBenchmarkTypeId createDto =
    //    match createDto with
    //    | CreateAbsoluteReturnBenchmarkDto -> 1uy
    //    | CreateAssetWeightedBenchmarkDto -> 2uy
    //    | CreateCompositeReturnBenchmarkDto -> 3uy
    //    | CreateCompositeWeightedBenchmarkDto -> 4uy
    //    | CreatePullForwardBenchmarkDto -> 5uy
    //    | CreateStaticWeightedBenchmarkDto -> 6uy

    let getBenchmarkType typeId =
        match typeId with
        | 1uy -> CreateAbsoluteReturnBenchmarkDto
        | 2uy -> CreateAssetWeightedBenchmarkDto
        | 3uy -> CreateCompositeReturnBenchmarkDto
        | 4uy -> CreateCompositeWeightedBenchmarkDto
        | 5uy -> CreatePullForwardBenchmarkDto
        | 6uy -> CreateStaticWeightedBenchmarkDto
        | _  -> failwith "Unknown TypeId."

    let toCreateAbsoluteReturnBenchmarkDto createBenchmarkDto =
        let benchmarkType = createBenchmarkDto.TypeId |> getBenchmarkType
        match benchmarkType with
        | CreateAbsoluteReturnBenchmarkDto -> createBenchmarkDto |> CreateAbsoluteReturnBenchmark.create |> Ok
        | _ -> "Invalid AbsoluteReturn TypId" |> Error

