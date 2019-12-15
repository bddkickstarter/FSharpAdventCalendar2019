namespace DiscountCalculation

type CustomerId = string
type Spend = decimal
type Total = decimal

type RegisteredCustomer =
    {
        Id:CustomerId
        IsEligible:bool
    }

type GetRegisteredCustomer = CustomerId -> RegisteredCustomer option
type ApplyDiscount = Spend -> RegisteredCustomer option -> Total

module Impl =

    let DiscountCalculator (getCustomer:GetRegisteredCustomer) (applyDiscount:ApplyDiscount) =
        fun (customerId:CustomerId) (spend:Spend) -> getCustomer customerId |> applyDiscount spend

    let ApplyDiscount (spend:Spend) (customer:RegisteredCustomer option) :Total=
        match customer with
        | None -> spend
        | Some c when c.IsEligible && spend >= 100.0M -> spend * 0.9M
        | _ -> spend 