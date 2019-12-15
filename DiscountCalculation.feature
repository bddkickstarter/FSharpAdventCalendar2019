Feature: Discount Calculation

Background:
  Given the following registered Customers
|Customer Id|Is Eligible|
|    John   |    true   |
|    Mary   |    true   |
|  Richard  |   false   |

Scenario: Registered eligible customers spending less than £100 get no discount
When Mary spends 99
Then Mary's total will be 99

Scenario: Registered ineligible customers spending £100 or more get no discount
When Richard spends 100
Then Richard's total will be 100

Scenario: Unregistered customers spending £100 or more get no discount
When Sarah spends 100
Then Sarah's total will be 100

Scenario: Registered eligible customers spending £100 or more get the discount
When John spends 100
Then John's total will be 90

Scenario: Unregistered customers spending less than £100 get no discount
When Hannah spends 30
Then Hannah's total will be 30

