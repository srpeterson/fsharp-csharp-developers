namespace Fsharp.Examples

module Interactive =

    let getEvenNumbers = 
        [1..30] |> List.filter (fun i -> i % 2 = 0)

    