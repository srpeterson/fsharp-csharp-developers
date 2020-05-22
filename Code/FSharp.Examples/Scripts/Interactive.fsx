#load "../Interactive.fs"
open Fsharp.Examples.Interactive

let getLessThanTwo = 
    //getLessThanFour is actually run from Interactive.fs
    getLessThanFour |> List.filter(fun i -> i < 2) //[0; 1]