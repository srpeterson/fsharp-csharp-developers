// F# doesn't have nulls - everything MUST return something
// But sometimes you have to deal with the concept that the
// "something" might not have a value. 
// F# solves the problem with the built in the "Option" type
// Basically, this means that you might have Some of "something"
// or "None" of that "something"

let foo: string = null

let bob = foo.Length

//F# Option type - keyword 'option'
let (name: string option) = Some "Bob" // = "Bob" by itself won't compile!
//or
let (name2: string option) = None

//"unwrap" with match 
let getName name = 
    match name with 
    | Some s -> s
    | None -> "No name assigned!"

//or "unwrap" by using  F#'s built in Option module
let getName' name = name |> Option.defaultValue "No name assigned!"

//optional lists
let someIntegers = [ Some 1; None; None; Some 4 ]

let justSomeInts = someIntegers |> List.filter(Option.isSome) // [Some 1; Some 4]
let justTheInts = someIntegers |> List.choose(id) // [1; 4] - only the values of the collection members that are "Some"


