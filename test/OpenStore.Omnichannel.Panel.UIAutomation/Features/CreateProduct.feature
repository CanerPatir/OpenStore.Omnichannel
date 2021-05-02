Feature: CreateProduct
	I want to create single product
	
Scenario Outline: Complete Contact Us Form with valid settings
When I submit the following contact details <name>, <email>, <phone>, <subject> and <message>
Then I should be told that the form was submitted
Examples: 
  | name | email         | phone       | subject | message                                |
  | Test | test@test.com | 01234567890 | Testing | Hello World, can I book a room please? |


Scenario: Create Single Variant Product
	Given The product should be created with the "Test Product"
		And should not be checked multiple variant CheckBox
	When I click the save button
	Then I should see the success toast message