namespace Fsharp.Examples

module ResultType = 

    // The F# ResultType returns a choice type of Ok or Error
    // The Error is NOT an Exception!! It is a way of saying:
    // "The result of what I was doing didn't work out so well"
    
    // We can "chain" any function that returns
    // a Result type  to another to achieve what is called
    // Railway Oriented Programming (ROP).
    // Basically, if one function in the chain 
    // returns an Error, the  rest of the functions
    // in the chain are skipped.
    // This is very useful in situations where,
    // for example, you want to validate something for more than one condition.
    
    // Suppose, for example, we would like to check
    // if a number is a multiple of 3 and multiple of 5
    // and is an even number.
    // We want to specify which validation rule
    // it failed on
    
    let isEven number = 
        if number % 2 = 0 then Ok number 
        else Error (sprintf "Invalid: %d is an odd number" number)
    
    let multipleOfThree number = 
        if number % 3 = 0 then Ok number 
        else Error (sprintf "Invalid: %d is not a multiple of 3" number)
    
    let multipleOfFive number = 
        if number % 5 = 0 then Ok number 
        else Error (sprintf "Invalid: %d is not a multiple of 5" number)
    
    let validationResult number = 
        Ok number
        |> Result.bind multipleOfThree
        |> Result.bind multipleOfFive
        |> Result.bind isEven
    
    let validate number =
        let result = validationResult number
        match result with
        | Ok i -> sprintf "%d is a valid number!" i
        | Error message -> message