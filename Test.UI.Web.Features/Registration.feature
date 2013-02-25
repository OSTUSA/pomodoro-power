Feature: Registration
	As a user
	I can register for a Pomodoro Power account

Scenario: Registering an unused email address
	Given I am on page "User.Register"
	When I submit the form using
	| Email               | Name           | Password |
	| scaturrob@gmail.com | Brian Scaturro | password |
	Then A cookie named ".ASPXAUTH" should exist
	And I should be redirected to "/"