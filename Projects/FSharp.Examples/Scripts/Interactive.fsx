#load "../Interactive.fs"
open Fsharp.Examples.Interactive

// use the function  that is already in code file
let myTestNumbers = [1..20]

myTestNumbers |> getEvenNumbers

// suppose you need to add a function in that gets all even numbers
// from a list of integers then square the integers in the list
// let's do that here to hammer it out



































//let getEvenNumbersAndThenSquare integers =
//    integers |>  getEvenNumbers |> List.map(fun i -> i * i)

//myTestNumbers |> getEvenNumbersAndThenSquare
