# Technical Test for Full Stack Web Developer

Thank you for taking time to do our technical test. This test consist of two parts. 

1. Coding test
2. A few technical questions 

## Coding Test

Some company has a weekly newsletter that goes out to more than 4000 contacts around the world. Writing the intro for this newsletter is done by one of the editors every week. When the news broadcast is sent out, the live intro is used, and the same live intro will be displayed on the some company website. The intro editor should also be able to write the next weeks' intro (Draft intro) while one intro is live. When a draft intro is made live, the current live intro has to be Archived. Each intro should keep track of the Created Date Time and Updated Date Time. Archived intros shouldn't be editable.

There can only be 1 live intro and 1 draft intro at any given moment. But there can be 0 or more Archive intros.

### API Implementation

Please use .net core 2.2 to develop the web api and database of your choice that is suitable for data that needs to be saved. Use Domain Driven Design (DDD) approach to design the domain model.

### Client Implementation

We'd like you to use React (an app initiated with create-react-app). On top of that, use whatever front-end libraries you feel comfortable with. For styling you can use anything you like, even your own CSS. Use Redux if you have used it before, but this is not required.

### User Story

As a user running the application  
I can View the Live intro (if there is one)  
So that I can edit and save it  

As a user running the application  
I can View the Draft intro (if there is one)  
So that I can Edit and save it  

As a user running the application  
I can View Archived intros (if there is any)  
So that I can check what intro was used in a previous weeks' news letter  

As a user running the application  
I should be able to make draft intro live  
And the current live intro (if there is one) will be archived automatically  

### Acceptance criteria

1. User is able to edit and save either live or draft intro
2. User is able to make a draft intro live making current live intro archive

### Running the project

Please make sure the API runs on localhost:5001 and the client runs on localhost:5003

See if you can use Docker and docker-compose to run both the API and the client

## Technical Questions

1. How long (in hours) did you spend on the coding test? 25-30 hours
2. What would you add to your API solution if you had more time? Authorization
3. What would you add to your React solution if you had more time? Convert some format from text editor to html and back. To see proper text + add redux
4. What libraries did you add to the frontend? What are they used for?
   material design - for components
   draft-js - this is text editor
   react-router - for routing
   ts-debounce - for auto handling what was changed in editor
5. Which parts did you spend the most time with? What did you find most difficult? I spend more less equal time for backend and front end. I didn't have any difficult parts.
