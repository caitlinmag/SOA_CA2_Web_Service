# SOA_CA2_Web_Service
MONGODB 
Setting up my database on MongoDB Atlas I used the following tutorials:
- https://learn.microsoft.com/en-us/aspnet/core/tutorials/first-mongo-app?view=aspnetcore-10.0&tabs=visual-studio
- https://www.mongodb.com/docs/drivers/csharp/current/crud/restful-api-tutorial/  

TABLES
- DrinkItems, DrinkSales, Suppliers and Users
Using MongoDB Atlas as my Database 

TOKENS
To generate tokens and include user login I used the following tutorials:
- https://medium.com/nerd-for-tech/net-jwt-authentication-with-mongodb-9bca4a33d3f0
- https://www.youtube.com/watch?v=6EEltKS8AwA 

LOGIN FEATURE
- Create user first, then the new user can be authenticated
- Without Token: can view data for drinks, sales and suppliers
- Token Required: can create, update, getbyid and delete data in drinks, sales and suppliers

TESTING 
To develop unit testing for my project I used the following tutorials: 
- https://learn.microsoft.com/en-us/dotnet/core/testing/unit-testing-best-practices
- https://www.c-sharpcorner.com/article/unit-test-crud-operation-in-web-api-using-nunit-testing-framework/
- https://dev.to/imdj/unit-testing-aspnet-core-web-api-with-moq-and-xunit-controllers-services-nci?utm_source

Unit Tests
- Testing CRUD functionality for drinks, sales, suppliers and user data.

Postman 
- Tested each of the CRUD API endpoints for drinks, sales, suppliers and user data.
- Tested the deployed API 

DEPLOYMENT
I used Azure to deploy my API, by following this tutorial:
https://learn.microsoft.com/en-us/aspnet/core/host-and-deploy/azure-apps

Show swagger UI on the deployed website:
https://stackoverflow.com/questions/30028736/how-to-use-swagger-as-welcome-page-of-iappbuilder-in-webapi

EXTRA - MOBILE CLIENT
- Created a small python flask app to display information from the API.
I used this website for taking in the data from API for python:
- https://www.geeksforgeeks.org/python/how-to-get-data-from-api-in-python-flask/
