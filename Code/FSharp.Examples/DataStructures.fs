namespace Fsharp.Examples

module DataStructures =

    //In F# a record type is how we normally declare a data structure
    type Customer = { FirstName:string; LastName: string; Age: int }
    
    //creating
    let customer = { FirstName= "Bob"; LastName= "Jones"; Age = 18 }
    let customer2 = { FirstName= "Bob"; LastName= "Jones"; Age = 18 }
    
    //equal?
    let equal = customer = customer2 //true because is a value type
    
    // However.....Sometimes you 
    // need to though. For example, when interopting with a C# library.
    type CSharpyCustomer(firstName: string, lastName: string, age: int) = 
        member this.FirstName = firstName
        member this.LastName = lastName
        member this.Age = age
    
    //creating
    let cSharpyCustomer1 = CSharpyCustomer("Bob", "Jones", 21)
    let cSharpyCustomer2 = CSharpyCustomer("Bob", "Jones", 21)
    
    //equal?
    let areEqual = cSharpyCustomer1 = cSharpyCustomer2 //false because is a reference type



