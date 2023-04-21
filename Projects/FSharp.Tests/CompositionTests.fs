namespace FSharp.Examples.Tests

open System
open Xunit
open FsUnit.Xunit

module ``F# Composition Tests`` =
    open Fsharp.Examples.Composition

    [<Fact>]
    let ``subtractOneSquareAddTenPiped should return 91 when the input is 10`` () =
        let actual = subtractOneSquareAddTenPiped 10
        let expected = 91
        actual |>  should equal expected

    [<Fact>]
    let ``subtractOneSquareAddTenComposed should return 91 when the input is 10`` () =
        let actual = subtractOneSquareAddTenComposed 10
        let expected = 91
        actual |>  should equal expected

    [<Fact>]
    let ``gimmeOddNumbers should return a list of odd numbers after applying subtractOneSquareAddTenComposed to a list of integers`` () =
        let actual = [1..10] |> gimmeOddNumbers
        let expected = [11; 19; 35; 59; 91]
        actual |>  should equal expected

module ``C# Composition Tests`` =
    open CSharp.Examples

    [<Fact>]
    let ``SubtractOneSquareAddTen should return 91 when the input is 10`` () =
        let actual = Composition.SubtractOneSquareAddTen2(10)
        let expected = 91
        actual |>  should equal expected

        
