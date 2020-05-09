#load "../Interactive.fs"
open Fsharp.Examples.Interactive

let getLessThanTwo = 
    getLessThanFour |> List.filter(fun i -> i < 2) //[0; 1]