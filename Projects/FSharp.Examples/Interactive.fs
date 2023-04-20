namespace Fsharp.Examples

module Interactive =

    let getEvenNumbers numbers = 
        numbers |> List.filter (fun i -> i % 2 = 0)

    let getEvenNumbersAndThenSquare  integers =
        integers |>  getEvenNumbers |> List.map(fun i -> i * i)

    