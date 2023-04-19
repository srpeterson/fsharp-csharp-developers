#r "nuget: FsToolkit.ErrorHandling"
#load "../Benchmarks/CreateValidBenchmark.fs"

open System
open Fsharp.Examples.Benchmarks.CreateValidatedABsoluteReturnDto

let testIt = toValidatedDto { Name = (Some "  "); TypeId = 1uy; IsLagged = true; LagDate = Some (new DateOnly(2022, 12, 31)) } //(Some "Steve")


let baseDate = new DateOnly(1900, 1, 1)
let baseDateDayNumber =(new DateOnly(1900, 1, 1)).DayNumber

let currentDate = new DateOnly(2023, 4, 19)
let currentDateNumber = currentDate.DayNumber

let daysSinceEpoch (date: DateOnly) = 
    let baseDateDayNumber =(new DateOnly(1900, 1, 1)).DayNumber
    date.DayNumber - baseDateDayNumber

daysSinceEpoch (new DateOnly(2023, 4, 19)) 