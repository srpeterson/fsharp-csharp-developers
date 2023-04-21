namespace Fsharp.Examples

// The CSharpy way
type Person(firstname, lastname) =

    let mutable firstname: string = firstname
    let mutable lastname: string  = lastname

    member _.FirstName with get() = firstname and set(value) = firstname <- value
    member _.LastName with get() = lastname and set(value) = lastname <- value

    member _.GetFullName() = $"{firstname}, {lastname}" 

// F# "normal" way
module Person =
    
    type Person = { FirstName: string; LastName: string }

    let getFullName person = $"{person.FirstName}, {person.LastName}"


