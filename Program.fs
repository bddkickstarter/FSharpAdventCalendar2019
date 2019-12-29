open Features 
open Expecto
open FSharp.Data.Gherkin

[<EntryPoint>]
let main argv = 
    match runTestsInAssembly defaultConfig argv with
    | result when result <> 0 -> result
    | _ ->
        match validateFeatureAndExclude DiscountCalculatorFeature [|"@WIP";"@pending"|] with
        | None -> 0
        | Some report -> failwith(report.Summary)

