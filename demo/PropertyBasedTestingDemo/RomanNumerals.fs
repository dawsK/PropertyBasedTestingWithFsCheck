module PropertyBasedTestingDemo.RomanNumerals

open System
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

    open FsCheck
    open FsCheck.Xunit

    