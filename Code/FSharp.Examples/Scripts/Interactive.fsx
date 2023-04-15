#load "../Interactive.fs"
open Fsharp.Examples.Interactive

let evenNumbersTimesTwo = 
    getEvenNumbers 
    |> List.map(fun num -> num * 2) 