
// load nuget package from nuget.org
#r "nuget: FSharp.Data"

// open module we need
open FSharp.Data

// set source directory
[<Literal>]
let ResolutionFolder = __SOURCE_DIRECTORY__

// set provider
type Provider = CsvProvider<"../Data/Abs-Return-Daily-Returns.csv", ResolutionFolder = ResolutionFolder>

// get rows
let rows = Provider.GetSample().Rows

// suppose we think funny business with average in December - let's find out
let decemberAverage = 
    rows
    |> Seq.filter(fun r -> r.Interval.Month = 12 && r.Cumulative_Return <> 0M)
    |> Seq.averageBy(fun p -> p.Cumulative_Return)