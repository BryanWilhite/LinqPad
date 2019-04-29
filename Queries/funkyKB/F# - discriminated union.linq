<Query Kind="FSharpProgram" />

type Card = Card of int64
type Key = Key of string

type PaymentMethod =
    | Mastercard of Card
    | Visa of Card
    | Bitcoin of Key

let payment = Visa (Card 90210959696L)

let handler = function
    | Mastercard number       -> printf "%A" number
    | Visa       number       -> printf "%A" number
    | Bitcoin    bitcoinValue -> printf "%A" bitcoinValue

let result = payment |> handler

Dump result