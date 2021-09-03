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