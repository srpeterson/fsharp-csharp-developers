namespace Challenge.Api.FSharp

module Validation = 

    let validate validator failure input =
        if validator input then Ok input else Error failure
