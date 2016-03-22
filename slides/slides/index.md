- title : Property-Based Testing with F# and FsCheck
- description : Property-Based Testing with F# and FsCheck
- author : Dawson
- theme : black
- transition : concave

***

## Property-Based Testing ##
### with F# and FsCheck ###

![F# Logo](images/logo.png)

<div>
    <div><i class="fa fa-user"></i> Dawson Kroeker</div>
    <div><i class="fa fa-envelope"></i> dkroeker@gmail.com</div>
    <div><i class="fa fa-github-square"></i> github.com/dawsk</div>
</div>

' About Me:
' - Developer at Online Business Systems
' - Worked with Amir on a project
' - Introduced to F# and fell in love with it
' - Amir introduced me to FsCheck

***

## <span class="accent">Where </span>are we going?

- Define Property-based testing
- FsCheck Overview
- Demo
- Application

***

## <span class="altHeader">Property-Based</span> ##
# Testing #

' Property-based testing asserts that the characteristics, traits, or attributes of some code always
' holds true, rather than just in example scenarios. It uses randomly generated input data to create
' deterministic automated tests.
' 
' It might be helpful to constrast this with what we are more familiar with, example-based testing.
' Let's consider an example...

***

- data-background : images/basement.jpg

***

- data-background : images/basement.jpg

# <span class="introrust">Clean Up The Basement!!!</span>

***

### Example-based ###
# <span class="altHeader">Questions</span> #

---

Did you put the <span class="accentYellow">yellow</span> car in the <span class="accent">cupboard?</span>
<br />
<br />
<i class="fa fa-car fa-3x"></i>

---

Did you hang the <span class="accent">Spiderman</span> costume that was on the floor by the couch, back on the wall on the 
<span class="accentYellow">third</span> hook?

--- 

Did you <span class="accent">pick up</span> the brown LEGO piece by the <span class="accent">table</span> and put it in the lego box?
<br />
<br />
![Lego Head](images/legohead.png)

---

- data-background : images/basement2.jpg

# <span class="introrust">FAIL!!!</span>

***

### Property-based ###
# <span class="altHeader">Questions</span> #

---

Are all the <span class="accentYellow">toys</span> picked up and put where they belong?
<br />
<br />
<br />
<i class="fa fa-truck fa-3x"></i>

---

Does the floor have anything on it besides <span class="accent">furniture</span>?

---

Is the furniture in the <span class="accentYellow">right</span> place?

***

#### Example with ####
# Modulo

' Suppose we would like to implement the modulo operation
' How would we go about testing it?

---    

## Example-based testing ##

    open Xunit
    open FsUnit.Xunit
    
    [<Fact>]
    let ``10 % 5 should equal 0`` () =
        modulo 10 5 |> should equal 0
---

    let modulo m n = 0

---
    
    [<Fact>]
    let ``20 % 3 should equal 2`` () =
        modulo 20 3 |> should equal 2
        
---

    let modulo m n = 
        match (m, n) with
        | (20, 3) -> 2
        | (_, _) -> 0
        
---
        
    [<Fact>]
    let ``3453234 % 32 should equal 18`` () =
        modulo 3453234 32 |> should equal 18

--- 

    let modulo m n = 
        match (m, n) with
        | (20, 3) -> 2
        | (3453234, 32) -> 18
        | (_, _) -> 0

***

## Property-based ##
# <span class="accentYellow">testing</span> #

---

## <span class="altHeader">Properties of</span> ##
# Modulo? #

---

$ a \mod n = 0, \text{when } n = 1 $

---

## <span class="altHeader">Identity</span>

$ (a \mod n) \mod n = a \mod n $

' 15 mod 4 = 3, 3 mod 4 still = 3

---
 
$ n^x \mod n = 0, \text{when } x > 0 $

' If you take a multiple of n, of course you can divide by n it evently

---

## <span class="altHeader">Inverse</span>

$ [(−a \mod n) + (a \mod n)] \mod n = 0 $

--- 

## <span class="altHeader">Distributive</span>

$ (a + b) \mod n = \\ [(a \mod n) + (b \mod n)] \mod n $

---

$ ab \mod n = \\ [(a \mod n)(b \mod n)] \mod n $

***

## FsCheck ##
<span class="yellow">Random Testing for .NET</span>

---

### NuGet Package ###

    PM> Install-Package FsCheck
    PM> Install-Package FsCheck.NUnit
    PM> Install-Package FsCheck.Xunit
    
---

### <span class="yellow">Features</span> ###

- F# or C#
- Asserts your properties multiple times with random data
- Automatic input shrinking
- Conditional Properties
- Classifying test cases
- Custom input generators

---

### Modulo
# <span class="accentYellow">DEMO</span>

***

## How to find properties ##

[http://fsharpforfunandprofit.com/posts/property-based-testing-2/](http://fsharpforfunandprofit.com/posts/property-based-testing-2/)

' Scott Wlaschin
' Choosing properties for property-based testing

---

### Different paths
## <span class="accentYellow">same destination</span>

<i class="fa fa-code-fork fa-5x"></i>

' add 1, add 2 same as add 2, add 1

---

### <span class="accentYellow">There and back again</span>

<i class="fa fa-exchange fa-5x"></i>

' serialization/deserialization

---

### Some things <span class="accentYellow">never</span> change

<i class="fa fa-exchange fa-5x"></i>

' invariants, like ordering of a sorted list

---

The more things change, the more they stay the same
##<span class="accentYellow">Idempotence</span>

' doing an operation twice results in the same state
' distinct, twice

---

<span class="larger">Solve a <span class="altHeader">smaller</span> problem <span class="accentYellow">first</span></span>
<br/><br/>
<i class="fa fa-scissors fa-5x"></i>

' structural induction. If a property holds true for a small partitition, it should be true for a large thing as well

---

### Hard to prove
## <span class="accentYellow">easy to verify</span>

<i class="fa fa-check-circle-o fa-5x"></i>

' NP-complete problems, like the knapsack problem
' path through a maze

---

### The test <span class="altHeader larger">oracle</span>
<br/>
<i class="fa fa-magic fa-5x"></i>

' comparing algorithm against refactored or improved algorithm

***

## Roman Numerals ##
# <span class="accentYellow">Demo</span>

*** 

## When to Use Property-based Testing ##

---

- Functions
- Lots of possible inputs
- Not for integration/slow tests


' Functions with an easily defined input and output
' If the input space and output space is small, example based testing is fine, just write tests for every scenario
' Don't use them for integration style tests, or any test that is inherently slow. You will be calling your code lots of times





## Sites you <span class="accent">must</span> visit ##

1. [http://fsharpforfunandprofit.com/ *](http://fsharpforfunandprofit.com/)
2. [http://blog.ploeh.dk/2015/01/10/diamond-kata-with-fscheck/](http://blog.ploeh.dk/2015/01/10/diamond-kata-with-fscheck/)
3. 

<br /><br />
<div class="footnote">* Note: I stole a LOT of material from here for this presentation.</div>

***

# Questions? #