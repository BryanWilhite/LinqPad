<Query Kind="FSharpProgram" />

// 📖 https://docs.microsoft.com/en-us/dotnet/fsharp/language-reference/modules

module MyModule1 =
    let module1Value = 100
    let module1Function x = x + module1Value

module MyModule2 =
    let module2Value = 121
    let module2Function x = x * (MyModule1.module1Function module2Value)


let result = MyModule2.module2Function 42
printf "%i" result
