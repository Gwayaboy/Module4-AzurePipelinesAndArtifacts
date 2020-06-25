Feature: Web Bing Search engine text search
    As a User
    I want to be able to perform searches 
    So that I can find the information I need easily

Background: 
  Given the user navigated to the url "https://www.bing.com/" 

Scenario: Empty Search Text

When the user submits the search 
Then the the URL should remain "https://www.bing.com/"
  And the URL should not contains "search?q="
  And the Page's title should be "Bing"

Scenario: "Hello World" Search Text

  Given the user typed "Hello World!" 
  When the user submits the search 
  Then more than 1 result(s) should be listed 
    And the first result item's title and url should contain ""Hello, World!" program - Wikipedia" and "https://en.wikipedia.org/wiki/%22Hello,_World!%22_program"

Scenario: Current Pin location search

  When the user select the current pin location 
  Then the results related to the background's image location should be listed


