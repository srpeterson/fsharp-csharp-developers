namespace Fsharp.Examples.Benchmarks

module CreateBenchmarkDto =
    open BenchmarkRequests

    type CreateBenchmarkDtoType = 
        | CreateAbsoluteReturnBenchmark
        | CreateAssetWeightedBenchmark
        | CreateCompositeReturnBenchmark
        | CreateCompositeWeightedBenchmark
        | CreatePullForwardBenchmark
        | CreateStaticWeightedBenchmark

    let getBenchmarkTypeId benchamrkType =
        match benchamrkType with
        | CreateAbsoluteReturnBenchmark -> 1uy
        | CreateAssetWeightedBenchmark -> 2uy
        | CreateCompositeReturnBenchmark -> 3uy
        | CreateCompositeWeightedBenchmark -> 4uy
        | CreatePullForwardBenchmark -> 5uy
        | CreateStaticWeightedBenchmark -> 6uy

    let (|IsValidTypeId|) (typId2: CreateBenchmarkDtoType) (typId: byte)  = 
        let benchmarkTypeId = typId2 |> getBenchmarkTypeId
        benchmarkTypeId = typId

    //let toCreateAbsoluteReturnBenchmarkDto createBenchmarkDto =
    //    let benchmarkType = createBenchmarkDto.TypeId |> getBenchmarkType
    //    match benchmarkType with
    //    | CreateAbsoluteReturnBenchmark -> createBenchmarkDto |> CreateAbsoluteReturnBenchmarkDto.create |> Ok
    //    | _ -> "Invalid AbsoluteReturn TypId" |> Error

module CreateBenchmarkDto2 =
    open BenchmarkRequests.CreateAbsoluteReturnBenchmarkDto
    open CreateBenchmarkDto

    type ValidatedCreateAbsoluteReturnBenchmarkDto = { Name: string }

    type BenchmarkDto = UnvalidatedBenchmarkDto of CreateAbsoluteReturnBenchmarkDto | ValidatedBenchmarkDto of ValidatedCreateAbsoluteReturnBenchmarkDto

    let validateTypeId (createBenchmarkDto: BenchmarkRequests.CreateBenchmarkDto) =
        match createBenchmarkDto.TypeId with
        | IsValidTypeId CreateAbsoluteReturnBenchmark true  -> createBenchmarkDto.TypeId |> Ok
        | _ -> ("Invalid Id") |> Error
        

    // what do I want to do?
    // take an invalidated dto -> validate -> validated dto

