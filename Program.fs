open Features 
open Expecto
open FSharp.Data.Gherkin.Validation

[<EntryPoint>]
let main argv = 
    match runTestsInAssembly defaultConfig argv with
    | result when result <> 0 -> result
    | _ ->
        match FeatureValidator.Validate DiscountCalculatorFeatureInstance with
        | None -> 0
        | Some report -> 
            printf "%s" report.Summary
            0