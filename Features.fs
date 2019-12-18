module Features

open FSharp.Data.Gherkin

type DiscountCalculatorFeature = 
    GherkinProvider<const(__SOURCE_DIRECTORY__ + "./DiscountCalculation.feature"),Sanitize="partial">
    
let DiscountCalculatorFeatureInstance = DiscountCalculatorFeature.CreateFeature()