<Query Kind="FSharpProgram" />

// https://stackoverflow.com/a/1117654/22944

let convertToSomeOption = function
    | null -> None
    | s -> Some s

let s: string = null

s
|> convertToSomeOption
|> Option.isNone
|> printf "convertToSomeOption: %A\n"

// alternative with `Option.ofObj`:

s
|> Option.ofObj
|> Option.isNone
|> printf "Option.ofObj: %A\n"