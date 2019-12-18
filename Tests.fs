module Tests
open Helpers
open Features
open Expecto
open DiscountCalculation
open DiscountCalculation.Impl

let mockGetRegisteredCustomer  =
    let customers =
        DiscountCalculatorFeatureInstance.Background
            .``0 Given the following registered Customers``.Argument 
            |> Seq.map(fun c->
                {
                    Id = c.``Customer Id``.Value
                    IsEligible =c.``Is Eligible``.Value |> bool.Parse
                })

    fun (customerId:CustomerId) -> 
        customers 
        |> Seq.tryPick(fun c -> if c.Id = customerId then Some c else None)

let getDiscountedTotal (customerId,spend) =  
    DiscountCalculator mockGetRegisteredCustomer ApplyDiscount customerId spend


[<Tests>]
let t1 =
    let scenario = 
        DiscountCalculatorFeatureInstance
            .``Registered eligible customers spending less than _100 get no discount``
    
    test scenario.Name {
        //Act
        let actual = 
            scenario.``0 When Mary spends 99``.Text
            |> getCustomerIdAndSpendAmount
            |> getDiscountedTotal

        //Assert
        let expected =
            scenario.``1 Then Mary_s total will be 99``.Text
            |> getFirstNumber

        Expect.equal actual expected (sprintf "Expected %f got %f" expected actual)
    }

[<Tests>]
let t2 =
    let scenario = 
        DiscountCalculatorFeatureInstance
            .``Registered ineligible customers spending _100 or more get no discount``
    
    test scenario.Name {
        //Act
        let actual = 
            scenario.``0 When Richard spends 100``.Text
            |> getCustomerIdAndSpendAmount
            |> getDiscountedTotal

        //Assert
        let expected =
            scenario.``1 Then Richard_s total will be 100``.Text
            |> getFirstNumber

        Expect.equal actual expected (sprintf "Expected %f got %f" expected actual)
    }

[<Tests>]
let t3 =
    let scenario = 
        DiscountCalculatorFeatureInstance
            .``Unregistered customers spending _100 or more get no discount``
    
    test scenario.Name {
        //Act
        let actual = 
            scenario.``0 When Sarah spends 100``.Text
            |> getCustomerIdAndSpendAmount
            |> getDiscountedTotal

        //Assert
        let expected =
            scenario.``1 Then Sarah_s total will be 100``.Text
            |> getFirstNumber

        Expect.equal actual expected (sprintf "Expected %f got %f" expected actual)
    }

[<Tests>]
let t4 =
    let scenario = 
        DiscountCalculatorFeatureInstance
            .``Registered eligible customers spending _100 or more get the discount``
    
    test scenario.Name {
        //Act
        let actual = 
            scenario.``0 When John spends 100``.Text
            |> getCustomerIdAndSpendAmount
            |> getDiscountedTotal

        //Assert
        let expected =
            scenario.``1 Then John_s total will be 90``.Text
            |> getFirstNumber

        Expect.equal actual expected (sprintf "Expected %f got %f" expected actual)
    }

[<Tests>]
let t5 =
    let scenario = 
        DiscountCalculatorFeatureInstance
            .``Unregistered customers spending less than _100 get no discount``
    
    test scenario.Name {
        //Act
        let actual = 
            scenario.``0 When Hannah spends 30`` .Text
            |> getCustomerIdAndSpendAmount
            |> getDiscountedTotal

        //Assert
        let expected =
            scenario.``1 Then Hannah_s total will be 30``.Text
            |> getFirstNumber

        Expect.equal actual expected (sprintf "Expected %f got %f" expected actual)
    }

    