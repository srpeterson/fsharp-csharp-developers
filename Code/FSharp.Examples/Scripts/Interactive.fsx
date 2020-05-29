#load "../Interactive.fs"
open Fsharp.Examples.Interactive

let evenNumbersTimeTwo = 
    getEvenNumbers 
    |> List.map(fun num -> num * 2) 