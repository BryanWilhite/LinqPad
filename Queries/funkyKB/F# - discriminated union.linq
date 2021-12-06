<Query Kind="FSharpProgram" />

type PaymentMethod =
    | Mastercard of int64
    | Visa of int64
    | Bitcoin of string

// `PaymentMethod` is a “union” of three types

let handler = function
    | Mastercard number       -> printf "Mastercard %A\n" number
    | Visa       number       -> printf "Visa %A\n" number
    | Bitcoin    bitcoinValue -> printf "Bitcoin %A\n" bitcoinValue

let payment1 = Mastercard 90210959696L
payment1 |> handler

let payment2 = Visa 32000L
payment2 |> handler

let payment3 = Bitcoin "dollahs"
payment3 |> handler