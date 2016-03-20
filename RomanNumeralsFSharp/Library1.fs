namespace RomanNumeralsFSharp

open Xunit
open FsUnit.Xunit
open FsCheck
open FsCheck
open FsCheck.Xunit
open RomanNumeralsCSharp

module Tests =

    let numerals = [|'M', 'D', 'C', 'L', 'X', 'V', 'I'|]    


    type RomanNumerals =
        static member ValidInteger = Arb.Default.Int32



    [<Property>]
    let ``To Rome and back gives the same result`` number =
        let result = 
            number 
            |> RomanNumerals.ToRoman 
            |> RomanNumerals.ToInteger
        result = number

//    [<Property>]
//    let ``Ends with a max of 3 I's`` number =
//        let numTrailingIs = 
//            number
//            |> RomanNumerals.ToRoman
//            |> Seq.toList
//            |> List.rev
//            |> Seq.takeWhile (fun c -> c = 'I')
//            |> Seq.length
////        numTrailingIs = 3

//    [<Property>]
//    let ``Roman numerals are always descending`` number =
//        let roman = number |> RomanNumerals.ToRoman
//
//        roman.ToCharArray()
//        |> Array.skipWhile(c => c = 'M')
//        |> Seq.take(2)

