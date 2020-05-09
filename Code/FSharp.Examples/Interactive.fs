namespace Fsharp.Examples

module Interactive =

    let getLessThanFour = 
        [0..5] |> List.filter (fun i -> i < 4)

    