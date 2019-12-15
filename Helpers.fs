module Helpers

open System.Text.RegularExpressions

let getFirstNumber str =
    match Regex.Match(str,"\\d+") with
    | matches when not matches.Success -> failwith "no number"
    | matches ->  matches.Groups.[0].Value |> decimal

let getFirstWord str =
    match Regex.Match(str,@"^([\w\-]+)") with
    | matches when not matches.Success -> failwith "no word"
    | matches ->  matches.Groups.[0].Value

let getCustomerIdAndSpendAmount (str:string) =
    getFirstWord str,getFirstNumber str