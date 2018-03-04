# ASP.NET Full Stack Demo

This readme of the demo project is organized in three parts, for each part a resume and a description of the code implemented.

Download the code and search for matching label as "//LDP1_001, //LDP1_002" for detailed implementation descriptions.

## Project Part One 
- code first workflow
- productivity tools
- convention over configuration
- script to fill the database
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
- usability best practises
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

- linq "any"
LINQ "Any" -> useful to query in a list and set a BOLEAN under conditon.
LINK "Lookup" -> useful to look into a list by a specific attibute value. I used it in a view, under condition(see code).
REVEALING MODULE PATTERN -> create classes in javascript with PUBLIC RETURN
REPOSITORY PATTERN
EXTRACTING QUERIES WITH EAGER LOADING
CLEAN ARCHITECTURE
DEPENDENCY INVERSION PRINCIPLE
UNIT OF WORK PATTERN
CONSOLIDATING DEPENDENCIES
PROGRAMMING AGAINST INTERFACES --> the key for anything
DEPENDENCY INVERSION PRINCIPLE (study from another course and come back hete... it is not easy)
DEPENDENCY INJECTION
CODE IS COMPLETELY DISCONNECTED FROM THE REPOSITORY
REORGANIZE DATA ANNOTATIONS BY USE FLUENT APY

TESTING CONTROLLERS

MOCKING THE CURRENT USER
EXTENSION METHOD (=== IMPORTANTE RIPASSARE LA TEORIA NON RICORDO BENE COSA MINCHIA FANNO STO TIPO DI METODI ===)
MOCK OF THE REPOSITORY
FIRST TEST
#
provare ad implementare semplici test utilizzando i metodi standard di visual studio e non semplicemente con le api scaricate FLUENT ASSERTION (posso cercare tra gli appunti)

#
TEST THE BEHAVIOR NOT THE IMPLEMENTATION
TESTING THE DOMAIN CLASSES

TESTING REPOSITORIES

INTEGRATION TESTS

NUNIT
TRANSACTION SCOPE

  ```
