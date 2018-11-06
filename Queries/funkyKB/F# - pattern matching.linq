<Query Kind="FSharpProgram" />

(*
    (Nearly) Everything You Ever Wanted to Know About F# Active Patterns
    https://www.youtube.com/watch?v=I5dKFT_Z-fc

    C# switch matches on a constant, 1 of a kind
    F# has 16 different kinds
    but these are not considered first-class
*)

let fizzBuzz i =
    match i % 3, i % 5 with
    | 0,0 -> printfn "FizzBuzz"
    | 0,_ -> printfn "Fizz"
    | _,0 -> printfn "Fizz"
    | n   -> printfn "%s" (string n)

[1..100] |> List.map fizzBuzz |> ignore