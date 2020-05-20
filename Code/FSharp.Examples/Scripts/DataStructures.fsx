
//In F# a record type is how we normally declare a data structure
type Person = { FirstName:string; LastName: string}

//creating
let person1 = { FirstName= "Bob"; LastName= "Jones" }
let person2 = { FirstName= "Bob"; LastName= "Jones" }
let person3 = { FirstName= "John"; LastName= "Jones" }

//equal?
let equalPerson1ToPerson2  = person1 = person2 //true

let equalPerson1ToPerson3  = person1 = person3 //false



