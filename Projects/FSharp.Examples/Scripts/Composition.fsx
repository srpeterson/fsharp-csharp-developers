module Composition =

    let subtractOne input  = input - 1
    let square input = input * input
    let addTen input = input + 10
       
    // the '|>' operator allows us to supply a value to the
    // last parameter of another function. This allows us to chain
    // functions together just as long as the output of the "giver"
    // function matches the LAST argument of the "reciever' function
    let subtractOneSquareAddTenPiped input =  // signature: int -> int
        input 
        |> subtractOne 
        |> square 
        |> addTen
    
    // the '>>' operator allows us to combine one or more functions to a single function
    // just as long as the output of the "giver" function
    // is the same as the input of the "reciever" function
    let subtractOneSquareAddTenComposed = subtractOne >> square >> addTen // signature: (int -> int)

    let gimmeOddNumbers integers = 
        integers 
        |> List.map (subtractOneSquareAddTenComposed) // can't do this with piped
        |> List.filter(fun i -> i % 2 <> 0)