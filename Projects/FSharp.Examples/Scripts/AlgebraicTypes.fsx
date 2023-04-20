//#load "../AlgebraicTypes.fs"
//open Fsharp.Examples.AlgabraicTypes

// The F# Algabraic type system is basically a fancy name
// for being able to combine types in a "AND' or "OR" way.
// This gives us a powerful tool to model our domain
// in a very self documenting way

// Use case: Your client owns a computer store. He would like his sales people 
// to be able to quickly look up computers in stock.
// You, the software engineer, need to figure this out. 
//
// Let's ask some questions

// First question might be: "What types of computers do you sell?"
// Answer: "Both laptops and desktops"

type Type = Laptop | Desktop // The '|' in F# means "OR"

// Next question might be: "What brands of cumputers do you sell?"
// Answer: Lenovo, Acer, Hp and Dell.
type Brand = Acer | Lenovo | Hp | Dell

// What kind of operating systems come on those?"
// Answer: Well, you can get Win 10 Home or Win 10 Professional

type OS = WIn10 | Win11

// What kind of memory comes on the computers?
// Answer: You can get 16, 32 or 34 GB of Ram

type RAM = Gb16 | Gb32 | Gb64

// Mmmm.. what about hard drives?
// Answer: I have HDD with 125 GB , 250GB, 500GB, 750GB, 1TB disk space. 
//         You can also get SSD drives in 125GB, 250GB, 500GB  and 1 TB.
//         I might be getting some Acer 2TB HDD's soon, but not really sure yet.

type DiskType = HDD | SSD
type DiskSpace = GigaByte of int | TeraByte of int
type Disk = DiskType * DiskSpace //the '*' means "AND". Can't do this in C#!!!

// Define what a computer is
type Computer = { Type: Type; Brand: Brand; Os: OS; Memory: RAM; Disk: Disk;}

// Define what the stock is
type CurrentStock = InStock of Computer list | OnlyOneLeft of Computer | OutofStock of string //blurb

// Here is a higher order function. 'predicate' is a function (Computer -> bool) 
let getStock predicate inventory = 
    let currentStock = inventory |> List.filter(predicate) |> Some
    match currentStock with
    | None -> OutofStock "The computers you searched for are out of stock!"
    | Some [computer] -> OnlyOneLeft computer
    | Some computers -> InStock computers

//have some fun!
let inventory = 
    [
        {  Type = Desktop; Brand = Acer;   Os = WIn10;  Memory = Gb16;  Disk = (HDD, GigaByte 750); }
        {  Type = Laptop;  Brand = Acer;   Os = Win11;  Memory = Gb32;  Disk = (SSD, GigaByte 500);}
        {  Type = Laptop;  Brand = Acer;   Os = Win11;  Memory = Gb32;  Disk = (HDD, TeraByte 1);  }
        {  Type = Laptop;  Brand = Lenovo; Os = Win11;  Memory = Gb16;  Disk = (SSD, GigaByte 750);}
        {  Type = Laptop;  Brand = Lenovo; Os = WIn10;  Memory = Gb32;  Disk = (SSD, TeraByte 1);  }
        {  Type = Desktop; Brand = Hp;     Os = Win11;  Memory = Gb64;  Disk = (HDD, TeraByte 1);   }
        {  Type = Laptop;  Brand = Hp;     Os = WIn10;  Memory = Gb16;  Disk = (SSD, GigaByte 500);}
        {  Type = Laptop;  Brand = Hp;     Os = Win11;  Memory = Gb32;  Disk = (SSD, GigaByte 750);}
        {  Type = Desktop; Brand = Dell;   Os = Win11;  Memory = Gb32;  Disk = (HDD, TeraByte 1);   }
    ]

// predicate functions all have signature (Computer -> bool)
let desktops = fun a -> a.Type = Desktop
let dellLaptops = fun a -> a.Brand = Dell && a.Type = Laptop
let teraByteSsds = fun { Disk= (ssd, gb) } -> ssd = SSD && gb = TeraByte 1

let desktopsInStock = inventory |> getStock desktops
let dellLaptopsInStock = inventory |> getStock dellLaptops
let ssdsInStock = inventory |> getStock teraByteSsds
