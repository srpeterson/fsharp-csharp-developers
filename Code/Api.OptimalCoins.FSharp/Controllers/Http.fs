namespace Api.OptimalCoins.FSharp.Http

// This file contains the stuff that deals with input/output at the controller level
module Requests =
    
    let placeholder () = failwith "Not implemented yet"
  
module Responses =
    open System
    
    type StandardResponse = { Status: string; StatusCode: int; Message: string; Result: Object }
    
    let okResponse message result : StandardResponse = 
            { Status= "Ok"; StatusCode = 200; Message = message; Result = result }
    
    let badRequestResponse message : StandardResponse = 
        { Status = "Bad Request"; StatusCode = 400; Message = message; Result= null }


