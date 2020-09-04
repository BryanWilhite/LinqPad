<Query Kind="FSharpProgram" />

(*
    The powerful type inferencing in F# comes at a price:

    There is no type coercion in F#.
*)
let doSomething f x =
    let y = f(x + 1) // `x` must be `int` because it is being added to an `int` 
    "hello: " + y // `y` must be a `string` because it is being concatenated to a `string`

(doSomething (fun i -> i.ToString()) 3).Dump()