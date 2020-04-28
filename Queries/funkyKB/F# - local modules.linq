<Query Kind="FSharpProgram" />

(*
   “An F# module is a grouping of F# code constructs such as
   types, values, function values, and code in do bindings.
   It is implemented as a common language runtime (CLR) class
   that has only static members.”

   [ 📖 https://docs.microsoft.com/en-us/dotnet/fsharp/language-reference/modules ]
*)

module MyModule1 =
    let module1Value = 100
    let module1Function x = x + module1Value

module MyModule2 =
    let module2Value = 121
    let module2Function x = x * (MyModule1.module1Function module2Value)


let result = MyModule2.module2Function 42
printf "%i" result
