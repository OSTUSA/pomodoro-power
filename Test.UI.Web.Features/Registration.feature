Feature: Registration
	As a user
	I can register for a Pomodoro Power account

Scenario: Registering an unused email address
	Given I am on "/user/register"
	When I fill out the form
	And I submit it
	Then A cookie should be created
	And I should be redirected to the home screen