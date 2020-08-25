<Query Kind="FSharpProgram" />

(*
    📖 https://www.c-sharpcorner.com/uploadfile/mgold/writing-equivalent-linq-expressions-in-fsharp/
*)

(*
    int[] integers = new int[] {0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
    // or int[] integers = Enumerable.Range(0, 11).ToArray();
    var evens = integers.Select(e => e * 2).ToArray();
    // => {0, 2, 4, 6, 8, 10, 12, 14, 16, 18, 20}
*)

let integers= [0..10]
let evens = integers |> List.map(fun(x) -> x * 2)
evens.Dump("Projections")

(*
    var items = integers
        .Select(e => new { number = e, product = e * 2 })
        .ToList();
*)

type Pair = {Number : int; Product : int;}
let productPairs = integers |> List.map(fun e -> {Number = e; Product = e*2})
productPairs.Dump("Adding Anonymous Classes to the Projection")

(*
    var odds = integers
        .Select((index, x) => new { Index = index, Product = x * 3 })
        .Where(o => o.Product % 2 == 1).ToArray();
*)

let odds = integers |> List.mapi(fun index x -> {Number = index; Product = x*3})
odds.Dump("Getting Indexes from the Array")

(*
    “The F# List library has equivalent functionality
    with a function called mapi. …I like the way F# broke
    out the index functionality into a new method,
    because it adheres closer to the single responsibility principle
    than LINQ does for this function.”
*)


