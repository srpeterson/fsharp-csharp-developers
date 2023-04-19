namespace Fsharp.Examples

type Person(firstname, lastname, age) =

    let mutable firstname: string = firstname
    let mutable lastname: string  = lastname
    let mutable age: int  = age

    member _.FirstName with get() = firstname and set(value) = firstname <- value
    member _.LastName with get() = lastname and set(value) = lastname <- value
    member _.Age with get() = age and set(value) = age <- value

    member _.GetFullName() = sprintf "%s, %s" lastname firstname // F# now supports string interpolation so could have done: $"{firstname}, {lastname}"


