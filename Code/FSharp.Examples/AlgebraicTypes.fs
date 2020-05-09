namespace Fsharp.Examples

module AlgabraicTypes = 

    // The F# Algabraic type system is basically a fancy name
    // for being able to combine types in a "AND' or "OR" way.
    // This gives us a pweful tool to model our domain
    // in a very self documenting way
    
    // Use case: Your client owns a computer store. He would like his sales people 
    // to be able to quickly look up computers in stock.
    // You, the software engineer need to figure this out. Let's ask some questions
    
    //First question might be: "What brands do you sell?"
    //Answer: Lenovo, Acer, Hp and Dell. But I only carry Lenovo laptops"
    
    type Style = Desktop | Laptop // The '|' in F# means "OR"
    type Brand = Acer | Lenovo | Hp | Dell
    
    //What kind of operating systems come on those?"
    //Answer: Well, you can get Win 10 Home or Win 10 Professional
    
    type OS = Win10Home | Win10Pro
    
    //What kind of memory comes on the computers?
    //Answer: You can get 8, 16 or 32 GB of Ram
    
    type RAM = Gb8 | Gb16 | Gb32
    
    // Mmmm.. what about hard drives?
    // Answer: I have HDD with 125 BG , 250GB, 500GB, 750MB, 1 TB disk space. 
    //         You can also get SSD drives in 125GB, 250GB, 500GB  and 1 TB.
    //         I might be getting some Acer 2TB HDD's soon, but not really sure yet.
    
    type DiskType = SSD | HDD
    type DiskSpace = GigaByte of int | TeraByte of int
    type Disk = DiskType * DiskSpace //the '*' means "AND". Can't do this in C#!!!
    
    type Computer = { Brand: Brand; Os: OS; Memory: RAM; Disk: Disk; Style: Style }
    
    type CurrentStock = InStock of Computer list | OnlyOneLeft of Computer | OutofStock of string
    
    type Computers = Computer list
    type ComputerPredicate = Computer -> bool
    
    let getStock (predicate: ComputerPredicate) (inventory: Computers) : CurrentStock = 
        let currentStock = inventory |> List.filter (predicate)
        match currentStock with
        | currentStock when currentStock.IsEmpty -> OutofStock "The computers you searched for are out of stock"
        | [computer] -> OnlyOneLeft computer
        | _ -> InStock currentStock
    
    //have some fun!
    let computers: Computers = 
        [
            { Brand= Acer;   Os= Win10Home; Memory= Gb8;  Disk= (HDD, GigaByte 750); Style = Desktop }
            { Brand= Acer;   Os= Win10Pro;  Memory= Gb16; Disk= (SSD, GigaByte 500); Style = Laptop }
            { Brand= Acer;   Os= Win10Pro;  Memory= Gb16; Disk= (HDD, TeraByte 1);   Style = Laptop }
            { Brand= Lenovo; Os= Win10Pro;  Memory= Gb8;  Disk= (SSD, GigaByte 750); Style = Laptop }
            { Brand= Lenovo; Os= Win10Home; Memory= Gb16; Disk= (SSD, GigaByte 250); Style = Laptop }
            { Brand= Hp;     Os= Win10Pro;  Memory= Gb32; Disk= (HDD, TeraByte 1);   Style = Desktop }
            { Brand= Hp;     Os= Win10Home; Memory= Gb8;  Disk= (SSD, GigaByte 500); Style = Laptop }
            { Brand= Hp;     Os= Win10Pro;  Memory= Gb16; Disk= (SSD, GigaByte 750); Style = Laptop }
            { Brand= Dell;   Os= Win10Pro;  Memory= Gb16; Disk= (HDD, TeraByte 1);   Style = Desktop }
        ]
    
    let desktops: ComputerPredicate = fun a -> a.Style = Desktop
    let dellLaptops: ComputerPredicate = fun a -> a.Brand = Dell && a.Style = Laptop
    let ssds: ComputerPredicate = fun { Disk= (ssd, _) } -> ssd = SSD
    
    let desktopsInStock = computers |> getStock desktops
    let dellLaptopsInStock = computers |> getStock dellLaptops
    let ssdsInStock = computers |> getStock ssds

