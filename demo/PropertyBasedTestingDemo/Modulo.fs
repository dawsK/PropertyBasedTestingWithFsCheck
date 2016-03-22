module PropertyBasedTestingDemo.Modulo

open Xunit
open FsUnit.Xunit


module ExampleTests =

    let modulo m n = 
        match (m, n) with
        | (20, 3) -> 2
        | (3453234, 32) -> 18
        | (_, _) -> 0
    
    [<Fact>]
    let ``10 % 5 should equal 0`` () =
        modulo 10 5
        |> should equal 0

    [<Fact>]
    let ``20 % 3 should equal 2`` () =
        modulo 20 3 |> should equal 2

    [<Fact>]
    let ``3453234 % 32 should equal 18`` () =
        modulo 3453234 32 |> should equal 18


module PropertyTests =
    
    open FsCheck
    open FsCheck.Xunit

    