﻿//#load "../AlgebraicTypes.fs"
//open Fsharp.Examples.AlgabraicTypes

// The F# Algabraic type system is basically a fancy name
// for being able to combine types in a "AND' or "OR" way.
// This gives us a powerful tool to model our domain
// in a very self documenting way

// Use case: Your client owns a computer store. He would like his sales people 
// to be able to quickly look up computers in stock.
// You, the software engineer need to figure this out. Let's ask some questions

// First question might be: "What types of computers do you sell?"
// Answer: "Both laptops and desktops"

type Style = Laptop | Desktop // The '|' in F# means "OR"

// Next question might be: "What brands of cumputers do you sell?"
// Answer: Lenovo, Acer, Hp and Dell.
type Brand = Acer | Lenovo | Hp | Dell

//What kind of operating systems come on those?"
//Answer: Well, you can get Win 10 Home or Win 10 Professional

type OS = Win10Home | Win10Pro

//What kind of memory comes on the computers?
//Answer: You can get 8, 16 or 32 GB of Ram

type RAM = Gb8 | Gb16 | Gb32

// Mmmm.. what about hard drives?
// Answer: I have HDD with 125 GB , 250GB, 500GB, 750GB, 1TB disk space. 
//         You can also get SSD drives in 125GB, 250GB, 500GB  and 1 TB.
//         I might be getting some Acer 2TB HDD's soon, but not really sure yet.

type DiskType = HDD | SSD
type DiskSpace = GigaByte of int | TeraByte of int
type Disk = DiskType * DiskSpace //the '*' means "AND". Can't do this in C#!!!

//define what a computer is
type Computer = { Brand: Brand; Os: OS; Memory: RAM; Disk: Disk; Style: Style }

//define what the stock is
type CurrentStock = InStock of Computer list | OnlyOneLeft of Computer | OutofStock of string

let getStock predicate inventory : CurrentStock = 
    let currentStock = inventory |> List.filter (predicate)
    match currentStock with
    | currentStock when currentStock.IsEmpty -> OutofStock "The computers you searched for are out of stock"
    | [computer] -> OnlyOneLeft computer
    | _ -> InStock currentStock

//have some fun!
let inventory = 
    [
        { Brand= Acer;   Os= Win10Home; Memory= Gb8;  Disk= (HDD, GigaByte 750); Style = Desktop }
        { Brand= Acer;   Os= Win10Pro;  Memory= Gb16; Disk= (SSD, GigaByte 500); Style = Laptop }
        { Brand= Acer;   Os= Win10Pro;  Memory= Gb16; Disk= (HDD, TeraByte 1);   Style = Laptop }
        { Brand= Lenovo; Os= Win10Pro;  Memory= Gb8;  Disk= (SSD, GigaByte 750); Style = Laptop }
        { Brand= Lenovo; Os= Win10Home; Memory= Gb16; Disk= (SSD, TeraByte 1);   Style = Laptop }
        { Brand= Hp;     Os= Win10Pro;  Memory= Gb32; Disk= (HDD, TeraByte 1);   Style = Desktop }
        { Brand= Hp;     Os= Win10Home; Memory= Gb8;  Disk= (SSD, GigaByte 500); Style = Laptop }
        { Brand= Hp;     Os= Win10Pro;  Memory= Gb16; Disk= (SSD, GigaByte 750); Style = Laptop }
        { Brand= Dell;   Os= Win10Pro;  Memory= Gb16; Disk= (HDD, TeraByte 1);   Style = Desktop }
    ]

let desktops= fun a -> a.Style = Desktop
let dellLaptops = fun a -> a.Brand = Dell && a.Style = Laptop
let teraByteSsds = fun { Disk= (ssd, gb) } -> ssd = SSD && gb = TeraByte 1

let desktopsInStock = inventory |> getStock desktops
let dellLaptopsInStock = inventory |> getStock dellLaptops
let ssdsInStock = inventory |> getStock teraByteSsds
