<Query Kind="FSharpProgram" />

// https://stackoverflow.com/a/1117654/22944

let convertToSomeOption = function
  | null -> None
  | s -> Some s

let s: string = null

let sOption = convertToSomeOption s

(sOption = None).Dump("sTrap = None")
