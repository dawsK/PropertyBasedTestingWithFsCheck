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

    let modulo (a : bigint) (n : bigint) =
        match a % n with
        | x when x < 0I -> 
            x + bigint.Abs(n)
        | x -> x
    
    [<Property>]
    let ``modulo 1 always returns 0`` a =
        modulo a 1I = 0I

    [<Property>]
    let ``a modulo n is always < |n|`` a n =
        (n <> 0I) ==> lazy(modulo a n < abs(n))

    [<Property>]
    let ``identity: (a mod n) mod n = a mod n`` a n =
        (n <> 0I) ==> lazy(modulo (modulo a n) n = modulo a n)

    [<Property>]
    let ``identity: n^x mod n = 0, x > 0`` n x =
        (x > 0 && n <> 0I) ==> lazy(modulo (pown n x) n = 0I)


    [<Property>]
    let ``inverse: [(−a mod n) + (a mod n)] mod n = 0`` a n =
        n <> 0I ==> lazy(modulo ((modulo -a n) + (modulo a n)) n = 0I)
    
    [<Property>]
    let ``distributive: (a + b) mod n = [(a mod n) + (b mod n)] mod n`` a b n =
        (n <> 0I) ==> lazy((modulo (a + b) n) = (modulo ((modulo a n) + (modulo b n)) n))
    
    [<Property>]
    let ``distributive: ab mod n = [(a mod n)(b mod n)] mod n`` a b n =
        (n <> 0I) ==> lazy((modulo (a * b) n) = (modulo ((modulo a n) * (modulo b n)) n))

