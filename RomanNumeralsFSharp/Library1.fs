﻿namespace RomanNumeralsFSharp

open System
open Xunit
open FsUnit.Xunit
open FsCheck
open FsCheck.Xunit
open RomanNumeralsCSharp

module Tests =

    type RomanNumeral = | M of int | D | C | L | X | V 

    let numeralToString (n : RomanNumeral) = sprintf "%A" n

    type RomanNumerals =

        static member ValidInteger = 
            Gen.elements [1 .. 3999]
            |> Arb.fromGen
//            Arb.Default.Int32() 
//            |> Arb.filter (fun i -> i >= 1)
//            |> Arb.filter (fun i -> i <= 3999)

//        static member ValidNumeral =
//            Gen.elements ["M"; "D"; "C"; "L"; "X"; "V"; "I"]
//            |> Arb.fromGen

        


    type RomanNumeralProperty () =
        inherit PropertyAttribute(Arbitrary = [|typeof<RomanNumerals>|])

    //[<Property(Arbitrary = [|typeof<RomanNumerals>|])>]
    [<RomanNumeralProperty>]
    let ``Split by power of ten and reassemble`` number =
        let ones = (number % 10)
        let onesResult = ones |> RomanNumerals.ToRoman

        let tens = (number % 100) - ones
        let tensResult = tens |> RomanNumerals.ToRoman

        let hundreds = (number % 1000) - tens - ones
        let hundredsResult = hundreds |> RomanNumerals.ToRoman        
        
        let thousands = (number % 10000) - hundreds - tens - ones
        let thousandsResult = thousands |> RomanNumerals.ToRoman

        let reassembled = sprintf "%s%s%s%s" thousandsResult hundredsResult tensResult onesResult
        let result = number |> RomanNumerals.ToRoman

        result = reassembled
        

    //[<Property(Arbitrary = [|typeof<RomanNumerals>|])>]
    [<RomanNumeralProperty>]
    let ``To Rome and back gives the same result`` number =
        let result = 
            number 
            |> RomanNumerals.ToRoman 
            |> RomanNumerals.ToInteger
        result = number

    [<RomanNumeralProperty>]
    let ``Always uses valid characters`` number =
        let isRomanNumeral c = "MDCLXVI" |> String.exists (fun x -> x = c)

        let result = number |> RomanNumerals.ToRoman
        result |> String.forall isRomanNumeral

    [<RomanNumeralProperty>]
    let ``Never returns empty string`` number =
        let result = number |> RomanNumerals.ToRoman
        (result |> String.length) > 0

    let hasMaxRepeats x c (str:String) = 
        not (str.Contains(String.replicate (x + 1) c))

    [<RomanNumeralProperty>]
    let ``Repeats 'I' a maximum of 3 times`` number =        
        let result = number |> RomanNumerals.ToRoman
        result |> hasMaxRepeats 3 "I"

    [<RomanNumeralProperty>]
    let ``Repeats 'V' a maximum of 1 time`` number =
        let result = number |> RomanNumerals.ToRoman
        result |> hasMaxRepeats 1 "V"

    [<RomanNumeralProperty>]
    let ``Repeats any valid numeral a max of 3 times`` numeral number =
        let romanNumeral = numeralToString numeral
        let result = number |> RomanNumerals.ToRoman
        result |> hasMaxRepeats 3 romanNumeral

    [<RomanNumeralProperty>]
    let ``1000 is represented by M`` number =        
        let hasCorrectMs result = 
            let expected = number / 1000
            let numMs = result |> Seq.takeWhile (fun l -> l = 'M') |> Seq.length
            numMs = expected

        number >= 1000 ==> (number |> RomanNumerals.ToRoman |> hasCorrectMs)
        |> Prop.classify (number / 1000 = 1) "M"
        |> Prop.classify (number / 1000 = 2) "MM"
        |> Prop.classify (number / 1000 = 3) "MMM"


//        |> Prop.classify (number < 5) "<5"
//        |> Prop.classify (number < 10 && number >= 5) "<10"
//        |> Prop.classify (number < 50 && number >= 10) "<50"
//        |> Prop.classify (number < 100 && number >= 50) "<100"
//        |> Prop.classify (number < 500 && number >= 100) "<500"
//        |> Prop.classify (number < 1000 && number >= 500) "<1000"
//        |> Prop.classify (number <= 3999 && number >= 1000) "<=3999"
        


        

//    [<Property>]
//    let ``Ends with a max of 3 I's`` number =
//        let numTrailingIs = 
//            number
//            |> RomanNumerals.ToRoman
//            |> Seq.toList
//            |> List.rev
//            |> Seq.takeWhile (fun c -> c = 'I')
//            |> Seq.length
//        numTrailingIs = 3

//    [<Property>]
//    let ``Roman numerals are always descending`` number =
//        let roman = number |> RomanNumerals.ToRoman
//
//        roman.ToCharArray()
//        |> Array.skipWhile(c => c = 'M')
//        |> Seq.take(2)

