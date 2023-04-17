namespace Fsharp.Examples.Benchmarks

module Domain =
    
    type CalculationFrequency = Day of int | Month of int | Quarter of int

    type Currency = { Id: int; Name: string; Code: string }

module Benchmark =
    open BenchmarkRequests
    
    type BenchmarkDto = UnvalidatedBenchmarkDto | ValidatedBenchmarkDto

