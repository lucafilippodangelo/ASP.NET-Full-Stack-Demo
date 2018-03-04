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


