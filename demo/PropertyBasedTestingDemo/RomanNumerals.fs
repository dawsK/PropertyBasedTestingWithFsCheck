module PropertyBasedTestingDemo.RomanNumerals

open System
open FsUnit.Xunit
open FsCheck
open FsCheck.Xunit
open System.Collections.Generic

let ToInteger (roman:String) =
    let digitMappings = 
        ['I', 1
         'V', 5
         'X', 10
         'L', 50
         'C', 100
         'D', 500
         'M', 1000] |> Map.ofList

    let toValue n = digitMappings.[n]
    
    let withoutShorthand =
        roman.Replace("IV", "IIII")
             .Replace("IX", "VIIII")
             .Replace("XL", "XXXX")
             .Replace("XC", "LXXXX")
             .Replace("CD", "CCCC")
             .Replace("CM", "DCCCC")

    withoutShorthand |> Seq.sumBy toValue

    
let ToRomanNumeral number =
    (String.replicate number "I")
        .Replace("IIIII","V")
        .Replace("VV","X")
        .Replace("XXXXX","L")
        .Replace("LL","C")
        .Replace("CCCCC","D")
        .Replace("DD","M")
        .Replace("IIII","IV")
        .Replace("VIV","IX")
        .Replace("XXXX","XL")
        .Replace("LXL","XC")
        .Replace("CCCC","CD")
        .Replace("DCD","CM")

module PropertyTests =

    type Thousands = | Thousands of int with
        static member op_Explicit(Thousands i) = i
    
    type RomanNumeralTypes =
        static member ValidInteger = 
            Gen.elements [1 .. 3999]
            |> Arb.fromGen

        static member Thousands =
            Gen.elements [1000 .. 3999]
            |> Arb.fromGen
            |> Arb.convert Thousands int
       
    type RomanNumeralProperty () =
        inherit PropertyAttribute(Arbitrary = [|typeof<RomanNumeralTypes>|])

    [<RomanNumeralProperty>]
    let ``Split by digit and reassemble`` number =
        let numberAt n = (number % (10 * n) / n) * n
        let ones = numberAt 1 |> ToRomanNumeral
        let tens = numberAt 10 |> ToRomanNumeral
        let hundreds = numberAt 100 |> ToRomanNumeral
        let thousands = numberAt 1000 |> ToRomanNumeral

        let reassembled = sprintf "%s%s%s%s" thousands hundreds tens ones
        let result = number |> ToRomanNumeral

        result = reassembled

    [<RomanNumeralProperty>]
    let ``To Rome and back gives the same result`` number =
        let result = 
            number 
            |> ToRomanNumeral
            |> ToInteger
        result = number

    [<RomanNumeralProperty>]
    let ``Always uses valid characters`` number =
        let isRomanNumeral c = "MDCLXVI" |> String.exists (fun x -> x = c)

        let result = number |> ToRomanNumeral
        result |> String.forall isRomanNumeral

    [<RomanNumeralProperty>]
    let ``1000 is represented by M`` (Thousands number) =
        let hasCorrectMs result = 
            let expected = number / 1000
            let numMs = result |> Seq.takeWhile (fun l -> l = 'M') |> Seq.length
            numMs = expected

        number |> ToRomanNumeral |> hasCorrectMs
        |> Prop.classify (number / 1000 = 0) "<1000"
        |> Prop.classify (number / 1000 = 1) "M"
        |> Prop.classify (number / 1000 = 2) "MM"
        |> Prop.classify (number / 1000 = 3) "MMM"