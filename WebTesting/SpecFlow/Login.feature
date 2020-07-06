Feature: Login
	In order buy products from my account
	As a registered user
	I want login into my acc

Background: Go to start page
	Given I go to rozetka.com.ua

@login @posit
Scenario: Login with valid email and password
	Given I click the Log in to your account button
	And I input valid email and valid password into fields
		| email                     | password |
		| zanpolbelimondo@gmail.com | Q1w2e3 |
	When I click the "Log in" button
	Then I see username on the page
		| username |
		| artem    |

@login @negat
Scenario Outline: Login with invalid data
	Given I input valid <email> and invalid <password> into fields
	When I click the "Log in" button
	Then I see message <message>

	Examples:
		| email                     | password    | message |
		| zanpolbelimondo@gmail.com | hyyyyyyyyyi | Пароль |
		| admin@gmail.com           | Q1w2e3      | адрес электронной |