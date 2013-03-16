Feature: Login
	As a user
	I can login to my Pomodoro Power account

Scenario: Logging in with a valid user
	Given I am on page "User.Login"
	And I create a user with the following
	| Email               | Name           | Password |
	| scaturrob@gmail.com | Brian Scaturro | password |
	When I submit the form using
	| Email               | Password |
	| scaturrob@gmail.com | password |
	Then A cookie named ".ASPXAUTH" should exist
	And I should be redirected to "/"

Scenario: Logging in and omitting the email address
	Given I am on page "User.Login"
	When I submit the form using
	| Email | Password |
	|       | password |
	Then element "span[data-valmsg-for='Email']" should have text

Scenario: Logging in and omitting the password
	Given I am on page "User.Login"
	When I submit the form using
	| Email               | Password |
	| scaturrob@gmail.com |          |
	Then element "span[data-valmsg-for='Password']" should have text

Scenario: Logging in with an invalid email
	Given I am on page "User.Login"
	When I submit the form using
	| Email          | Password |
	| test@gmail.com | password |
	Then element "span[data-valmsg-for='Email']" should have text

Scenario: Logging in with an invalid password
	Given I am on page "User.Login"
	And I create a user with the following
	| Email               | Name           | Password |
	| bscaturro@gmail.com | Brian Scaturro | password |
	When I submit the form using
	| Email               | Password    |
	| bscaturro@gmail.com | badpassword |
	Then element "span[data-valmsg-for='Email']" should have text
