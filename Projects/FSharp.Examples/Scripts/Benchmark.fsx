#r "nuget: FsToolkit.ErrorHandling"
#load "../Benchmarks/CreateValidBenchmark.fs"

open System
open Fsharp.Examples.Benchmarks.DomainTypes
open Fsharp.Examples.Benchmarks.CreateValidatedAbsoluteReturnDto

let testIt =  toValidDto { Name = Some("Steve"); TypeId = 1uy; IsLagged = true; LagDate = Some (new DateOnly(2022, 12, 31)) }

(*
Ok { Name = ValidName { Name = "Steve" }
    TypeId = ValidTypeId { TypeId = 1uy }
    IsLagged = true
    LagDate = Some (ValidLagDay { Quarter = 4
        DaysSinceEpoch = 44924
        AsDateTime = 12/31/2022 12:00:00 AM }) }
*)