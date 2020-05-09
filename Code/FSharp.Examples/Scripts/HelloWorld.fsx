﻿let (greeting: string) = "Hello World!"

//compiler knows that 'ten' is int, so you don't need to write it
let ten = 10; 

//list
let (firstnames: string list) = ["Steve"; "Bob"; "Mary"]

//array
let (lastNames: string array) = [| "Jones"; "Smith"; "Adams" |]

//F# dictionary way
let mapNames = Map.ofList ["key1", "Bob"; "key2", "Bob"] 

//set (HashSet in C#)
let setNames = set ["Steve"; "Bob"; "Mary"; "Bob"]

//tuple
let (myTuple: int * string) = (5, "five") 
    
//simple mult line function
let getMyTupleFirst =
    let first, _ = myTuple //is a function!
    first //notice no 'return' like in C#

//option - how F# deals with 'null' in C#
let (eleven: int option) = Some 11
let (twelve: int option) = None

//if conditional (in F# we use if conditional only if 2 possible outcomes)
let isLessThanTen x  = if x < 10 then "Less than ten" else "Greater than ten"

//match: C# 8 switch expression
let switchOnInt x =
    match x with
    | 5 -> x + 10
    | _ when x < 5 -> x
    | _ -> x + 100

//commonly used F# collections (by no means all!!!!!!!)

let oneToFive = [1..5] // [1; 2; 3; 4; 5]

//iter : C# LINQ 'ForEach'
let iterExample = oneToFive |> List.iter (printfn "%d") //printfn "%d" is C#'s 'Console.Write'

//filter: C# LINQ 'Where'
let filterExample = oneToFive |> List.filter (fun i -> i < 3)

//sum, max, min: C# LINQ has corresponding functions
let sumExample = oneToFive |> List.sum

//map: C# LINQ 'Select' 
let mapExample = oneToFive |> List.map((+) 10) // same as (fun i -> i + 10)

//some F# built in collection functions require C# to chain LINQ
let sumByExample = oneToFive |> List.sumBy(fun i -> i * 2) //30

//partial application - supply the first parameter. The result is a function
//with the first parameter "baked in"
let tellMeABoutYourPet animal age = sprintf "My pet is a %s and is %d years old!" animal age
let tellMeDog = tellMeABoutYourPet "dog"
    
//function that uses a 'partial application' function
let tellMeAboutYourDog age = tellMeDog age

//'Interface' in F#. Notice the 'parm". 
//'builtIn' is an example of a higher order function.
let builtIn (intToDoSOmethingWith: (int -> bool)) x = intToDoSOmethingWith x

//Better: Declare iterface as a type for self documenting code

type DoSomethingWithInt = (int -> bool)
let withType (parm: DoSomethingWithInt) x = parm x