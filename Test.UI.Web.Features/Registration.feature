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

Scenario: Registering and omitting the email address
	Given I am on page "User.Register"
	When I submit the form using
	| Email | Name           | Password |
	|       | Brian Scaturro | password |
	Then element "span[data-valmsg-for='Email']" should have text

Scenario: Registering and omitting the name
	Given I am on page "User.Register"
	When I submit the form using
	| Email               | Name | Password |
	| scaturrob@gmail.com |      | password |
	Then element "span[data-valmsg-for='Name']" should have text

Scenario: Registering and omitting the password
	Given I am on page "User.Register"
	When I submit the form using
	| Email               | Name           | Password |
	| scaturrob@gmail.com | Brian Scaturro |          |
	Then element "span[data-valmsg-for='Password']" should have text

Scenario: Registering an in use email
	Given I am on page "User.Register"
	And I create a user with the following
	| Email               | Name           | Password |
	| bscaturro@gmail.com | Brian Scaturro | password |
	When I submit the form using
	| Email               | Name           | Password |
	| bscaturro@gmail.com | Brian Scaturro | password |
	Then element "span[data-valmsg-for='Email']" should have text
