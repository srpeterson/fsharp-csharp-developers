namespace Fsharp.Examples

module ResultType = 

    // The F# ResultType returns a choice type of Ok or Error
    // The Error is NOT an Exception!! It is a way of saying:
    // "The result of what I was doing didn't work out so well"
    
    //very simple example
    let lessThanFive x = 
        if x < 5 then Ok (sprintf "%d is less than 5" x) 
        else Error (sprintf "%d is not less than 5" x)
    
    // We can "chain" any function that returns
    // a Result type  to another to achieve what is called
    // Railway Oriented Programming (ROP).
    // Basically, if one function in the chain 
    // returns an Error, the  rest of the functions
    // in the chain are skipped
    // This is very useful in situations where,
    // for example, you want to validate something.
    
    // Suppose, for example, we would like to check
    // if a number is even and is between 1 and 100
    // and we want to specify which validation rule
    // it failed on
    
    // Suppose, for example, we would like to check
    // if a number is even and is between 1 and 100
    // and we want to specify which validation rule
    // it failed on
    
    let greaterThanZero intToValidate = 
        if intToValidate > 0 then Ok intToValidate
        else Error (sprintf "%d must be greater than 1" intToValidate)
    
    let lessEqualHundred intToValidate = 
        if intToValidate <= 100 then Ok intToValidate 
        else Error (sprintf "%d must be less than or equal to 100" intToValidate)
    
    let isEven intToValidate = 
        if intToValidate % 2 = 0 then Ok intToValidate 
        else Error (sprintf "%d is an odd number" intToValidate)
    
    let validationResult intToValidate = 
        Ok intToValidate
        |> Result.bind greaterThanZero
        |> Result.bind lessEqualHundred
        |> Result.bind isEven
    
    let validate intToValidate =
        let result = validationResult intToValidate
        match result with
        | Ok i -> sprintf "%d is a valid number!" i
        | Error message -> message