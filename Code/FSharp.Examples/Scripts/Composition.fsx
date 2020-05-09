
let subtractOne input  = input - 1
let square input = input * input
let addTen input = input + 10
let theAnswer prefix input = sprintf "%s %d" prefix input
   
//the '|>' operator allows us to take the output from a function
// and give it as the last parameter of another function
let whatIsTheAnswerPiped input =  // signature: int -> string
    input 
    |> subtractOne 
    |> square 
    |> addTen
    |> theAnswer "The piped answer is:"

let answerToTenPiped = whatIsTheAnswerPiped 10

//the '>>' operator allows us to combine one or more functions
//together into one function, just as long as the output of the "giver" function
//is the same as the input of the "reciever" function
let whatIsTheAnswerComposed = //signature: (int -> string)
    subtractOne >> square >> addTen >> theAnswer "The composed answer is:"

let answerToTenComposed = whatIsTheAnswerComposed 10

// The advantage to compostion is that because it is a function
// we can supply it as the predicate (or parameter to a higher order function)
let tellMeList = [1..10] |> List.map (whatIsTheAnswerComposed)
