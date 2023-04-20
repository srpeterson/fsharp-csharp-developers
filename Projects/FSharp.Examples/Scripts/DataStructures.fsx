
// In F# a record type is how we normally declare a data structure.
// The more or less equivalent in C# record. 
// However, it is immutable
type Person = { FirstName: string; LastName: string; Pets: string list }

//creating
let person1 = { FirstName= "Bob"; LastName= "Jones"; Pets = ["Fluffy"; "Spot"] }

let person2 = { FirstName= "Bob"; LastName= "Jones"; Pets = ["Fluffy"; "Spot"] }

let person3 = { FirstName= "Bob"; LastName= "Jones"; Pets = ["Rover"; "Spot"] } // same name but different pets

let person4 = { FirstName= "John"; LastName= "Jones"; Pets = ["Fluffy"; "Spot"] } // different name same pets

//using '.' syntax
let person1FirstName = person1.FirstName

//equal?
let equalPerson1ToPerson2 = person1 = person2 //true

let equalPerson2ToPerson3 = person2 = person3 //false

let equalPerson1ToPerson4 = person1 = person4 //false

// equality works in list functions too
let persons = [person1; person2; person3; person4]
let uniquePersons = persons |> List.distinct

//anonymous record type - declared on the "fly". Defined by '{| |}'
let carDealers brand = 
    match brand with
    | "Ford" -> {| Dealer = "Sunny Ford"; Models = ["Mustang"; "Fusion"; "Fiesta"] |}
    | "Honda" -> {| Dealer = "Fred's Honda"; Models = ["Civic"; "Fit"] |}
    | _ -> {| Dealer = "Unknown"; Models = [] |}


let fordDealer = carDealers "Ford" // or "Ford" |> carDealers

let fordealerName = fordDealer.Dealer

let fordealerModels= fordDealer.Models


