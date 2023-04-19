
let subtractOne input  = input - 1
let square input = input * input
let addTen input = input + 10
let theAnswer prefix input = sprintf "%s %d" prefix input
   
// the '|>' operator allows us to supply a value to the
// last parameter of another function. This allows us to chain
// functions together just as long as the output of the "giver"
// function matches the LAST argument of the "reciever' function
let whatIsTheAnswerPiped input =  // signature: int -> int
    input 
    |> subtractOne 
    |> square 
    |> addTen
    |> theAnswer "The piped answer is:"

let answerToTenPiped = whatIsTheAnswerPiped 10

// the '>>' operator allows us to combine one or more functions to a single function
    // just as long as the output of the "giver" function
    // is the same as the input of the "reciever" function
let whatIsTheAnswerComposed = //signature: (int -> iint)
    subtractOne >> square >> addTen >> theAnswer "The composed answer is:"

let answerToTenComposed = whatIsTheAnswerComposed 10

// The advantage to compostion is that because it is a function
// we can supply it as the predicate (or parameter to a higher order function)
let tellMeList = [1..10] |> List.map (whatIsTheAnswerComposed)

// compared to pipe way
let tellMeList2 = [1..10] |> List.map (fun x -> whatIsTheAnswerPiped x)
