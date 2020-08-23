<Query Kind="FSharpProgram" />

(*

    📖 https://fsharpforfunandprofit.com/posts/fsharp-in-60-seconds/
    
    📖 https://fsharpforfunandprofit.com/site-contents/
*)

// binding with `let`:

let myInt = 5
let myFloat = 3.14
let myString = "hello"	//note that no types needed

let twoToFive = [2;3;4;5]
twoToFive.Dump("list") //.Dump() is an F# Type extension

let oneToFive = 1 :: twoToFive
oneToFive.Dump("prepend element")

let zeroToFive = [0;1] @ twoToFive
zeroToFive.Dump("@ concats two lists")

let square x = x * x
(square 3).Dump("function output")

let add x y = x + y // “Whitespace is used to separate parameters rather than commas.”
(add 2 3).Dump("multi-parameter function output")

let evens list =
    let isEven x = x%2 = 0  //an inner (“nested”) function
    List.filter isEven list // `List.filter` is a library function

(evens oneToFive).Dump("multiline function output")

(*
    “In F# returns are implicit -- no "return" needed. A function always
    returns the value of the last expression used.”
*)

let sumOfSquaresTo100 =
    List.sum ( List.map square [1..100] ) // use parens to clarify precedence

sumOfSquaresTo100.Dump("“Without the parens, `List.map` would be passed as an arg to `List.sum`”")

let sumOfSquaresTo100piped =
   [1..100] |> List.map square |> List.sum

sumOfSquaresTo100piped.Dump("use `|>` to state precedence")

let sumOfSquaresTo100withFun =
   [1..100] |> List.map (fun x->x*x) |> List.sum

sumOfSquaresTo100withFun.Dump("use `fun` to define lambda functions inline")

let simplePatternMatch x =
   match x with
    | "a" -> printfn "x is a"
    | "b" -> printfn "x is b"
    | _ -> printfn "x is something else" // underscore matches anything

simplePatternMatch "a"
simplePatternMatch "b"
"g (piped in)" |> simplePatternMatch

let simplePatternMatchShorter = function
    | "a" -> printfn "x is a (shorter)"
    | "b" -> printfn "x is b (shorter)"
    | _ -> printfn "x is something else (shorter)"

simplePatternMatchShorter "a"
simplePatternMatchShorter "b"
simplePatternMatchShorter "g"

(*
    `function`is used as “a match expression in a lambda expression that
    has pattern matching on a single argument.”
    [ 📖 https://docs.microsoft.com/en-us/dotnet/fsharp/language-reference/keyword-reference ]
*)

// Some(..) and None are roughly analogous to Nullable wrappers
let validValue = Some(99)
let invalidValue = None

let optionPatternMatch input = // `input` is an option type
   match input with
    | Some i -> printfn "input is an int=%d" i
    | None -> printfn "input is missing"

optionPatternMatch validValue
optionPatternMatch invalidValue

let twoTuple = 1,2 // Tuples use commas.
let threeTuple = "a",2,true

twoTuple.Dump("Tuple")
threeTuple.Dump("Tuple of multiple types")
(twoTuple,threeTuple).Dump("Tuple of Tuples")

type Person = {First:string; Last:string}
let person1 = {First="John"; Last="Doe"}
person1.Dump("Record type")

type Temp = 
    | DegreesC of float
    | DegreesF of float

let temp1 =  98.6 |> DegreesF // looks like units with pipe syntax?
temp1.Dump("discriminated union (union type) output")

let temp2 =  DegreesF 98.6

let areEqual = temp1 = temp2
areEqual.Dump("equality test")

type Employee = 
    | Worker of Person
    | Manager of Employee list

let jdoe = {First="John";Last="Doe"}
let worker = Worker jdoe
worker.Dump("recursive discriminated union")

let manager = Manager [ Worker{First="Bloke";Last="One"}; Worker{First="Bloke";Last="Two"} ]
manager.Dump("recursive discriminated union")

printfn "Printing an int %i, a float %f, a bool %b" 1 2.0 true
printfn "A string %s, and something generic %A" "hello" [1;2;3;4]

// all complex types have pretty printing built in
printfn "twoTuple=%A,\nPerson=%A,\nTemp=%A,\nEmployee=%A" 
         twoTuple person1 temp1 worker

// There are also sprintf/sprintfn functions for formatting data
// into a string, similar to String.Format.