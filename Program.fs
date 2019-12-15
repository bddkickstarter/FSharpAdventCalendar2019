open Features 
open Expecto

[<EntryPoint>]
let main argv = 
    match runTestsInAssembly defaultConfig argv with
    | result when result <> 0 -> result
    | _ ->
        DiscountCalculatorFeature.Background.Steps 
        |> Seq.iter(fun s -> 
            if not s.Visited then failwithf "Background Step %i not visited" s.Order else ())

        DiscountCalculatorFeature.Scenarios
        |> Seq.iter(fun s ->
            if not s.Visited then failwithf "Scenario %s not visted" s.Name else ()
            s.Steps
            |> Seq.iter(fun st -> 
                if not st.Visited then failwithf "Scenario %s Step %i not visited" s.Name st.Order else ()))
        0