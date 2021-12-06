<Query Kind="FSharpProgram" />

let convertNullableToSomeOption(n: Nullable<_>) =
    match n.HasValue with
    | true -> Some n.Value
    | false -> None

let i: Nullable<int> = Nullable()

let iOption = convertNullableToSomeOption i

(iOption = None).Dump("iOption = None")

let j: Nullable<int> = Nullable 39

let jOption = convertNullableToSomeOption j

(jOption = None).Dump("jOption = None")

// alternative: use `Option.ofNullable`:

i
|> Option.ofNullable
|> Option.isNone
|> printf "i: Option.isNone: %A\n"


j
|> Option.ofNullable
|> Option.isNone
|> printf "j: Option.isNone: %A\n"

// [ https://fsharp.github.io/fsharp-core-docs/reference/fsharp-core-optionmodule.html#ofNullable ]
