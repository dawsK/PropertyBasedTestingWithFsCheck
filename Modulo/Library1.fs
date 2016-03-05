module ModuloExperiment

open Xunit
open FsUnit.Xunit



module ExampleTests =
    let congruent a b n = 
        (a - b) % n = 0
    
    [<Fact>]
    let ``1 is congruent to 1 mod 1`` () =
        congruent 1 1 1 |> should be True

    [<Fact>]
    let ``2 is not congruent to 3 mod 2`` () =
        congruent 2 3 2 |> should be False
        
    [<Fact>]
    let ``6 is not congruent to 8 mod 2`` () =
        congruent 6 8 2 |> should be True

    [<Fact>]
    let ``10 is congruent to 5 mod 5`` () =
        congruent 10 5 5 |> should be True

    [<Fact>]
    let ``-8 is congruent to 7 mod 5`` () =
        congruent -8 7 5 |> should be True


module PropertyTests =
    
    open FsCheck
    open FsCheck.Xunit

    let congruent a b n = (a - b) % n = 0

    type CongruencyProperty () =
        inherit PropertyAttribute(Arbitrary = [|typeof<CongruencyProperty>|])

        static member ModInt () = 
            Arb.Default.Int32 () 
            |> Arb.filter (fun i -> i <> 0)

    [<CongruencyProperty>]
    let ``congruency returns correct value`` a b n =
        let expectedValue = (a - b) % n = 0
        let actualValue = congruent a b n
        expectedValue = actualValue

    [<CongruencyProperty>]
    let ``is a congruence relation`` a1 b1 a2 b2 n =
        ((congruent a1 b1 n) && (congruent a2 b2 n)) ==> 
            ((congruent (a1 + a2) (b1 + b2) n) && (congruent (a1 - a2) (b1 - b2) n))

            
        
        