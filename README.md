# ASP.NET Full Stack Demo

This readme of the demo project is organized in three parts, for each part a resume and a description of the code implemented.

Download the code and search for matching label as "//LDP1_001, //LDP1_002" for detailed implementation descriptions.

## Project Part One 
- code first workflow
- script to seed the database
- build a form with bootstrap
- view models
  - saving data 
- implementing validation
  - custom validation (//LDP1_002)
  - enabling client side validation
- web application vulnerabilities
  - sql injection (execute malicious SQL statements in our application)
  - XSS, Cross Site Scripting
    - enable attacker to execute malicious scripts. How to prevent: "escape content", that say to the browser to treat the content like a string, IN OUR CASE: MVC rejects input, RAZOR views escape content
  - CSRF, Cross site Request Forgery. Attacker perform actions on behalf of the user, without their knowledge, he FORCE the request from another site and take advantage of the opened session.
    - how to prevent:
      - @Html.AntiForferyToken() into the view form
      - [ValidateAntiForgeryToken] in our controller action
- extending asp.net identity users (//LDP1_001)
- absolute and relative positioning in css
- join table and multiple cascade paths (//LDP1_003)
  - disable the cascade delete by fluent api (//LDP1_004)
- ajax, api for authenticated users 
  - building the api, web api 2  
  - test with postman
  - ajax call to api
  - applying dry principle

## Project Part Two
- crud operations (//LDP2_001)
- show the link when the mouse is hover by css
- use lampda expressions to replace magic strings (//LDP2_002)
  - def.: "A lambda expression is an unnamed method written in place of a delegate instance"
- anonymous methods
- implement logical delete
- API
  - avoid a full range reload
  - consuming using jquery, ajax
- bootbox dialogs
- use bootstrap labels
- avoiding pitfalls in domain model
- right use of linq
  - for each
- cohesion: 
  - "things that are related should be together", see  "GigController"+"Gigs.cs"
- reverse relationship
- factory method, immutable object (//LDP2_003)
- dto
  - manual mapping
  - automapper
- implementing search
  - query string to make the search bookmarkable

## Project Part Three

- linq 
  - "any", useful to query in a list and set a boolean under conditon
  - "Lookup", useful to look into a list by a specific attibute value. I used it in a view, under condition.
- "revealing module pattern", create classes in javascript with PUBLIC RETURN
- "repository "pattern", the data access layer must be responsible for queryes, the controller is just a manager of process flow. The main reason of this pattern is avoid duplication of queryes in provate methods of the controller and improve the separation of concern principle. **Repository** is a collection of objects in memory, so I don't need to save in the repository.
  - "dependency inversion principle", high level module should not depend on low level module, should depend on abstraction. For instance, in order to don't recompile the controller, controller and class must have in the middle an interface "**controller --> IUnitOfWork <-- UnitOfWork**". To recap:  Abstractions should not depend on details(or concrete implementation). Details(or concrete implementation) should depend on abstraction.
- extracting queries with eager loading
- clean architecture
- unit of work pattern (//LDP3_001) mantains a list of object affected by business transaction and coordinates the writing of the changes.
- consolidating dependencies
- programming against interfaces
- testing. Used libraries "Moq" and "Fluent Assertions"
  - controllers
    - integration. I worked on the "Cancel" action of API "GigController.cs" (//LDP3_002, //LDP3_003, //LDP3_004)
	- unit. (//LDP3_006)
  - mock the current user
  - repository mock
  - extensions method
  - classes (//LDP3_007, //LDP3_008, //LDP3_009)
  - repositoryes
    - unit (//LDP3_005)

