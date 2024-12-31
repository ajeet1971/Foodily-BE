## Foodily Application

## Overview

Foodily is a web application designed to help users discover, create, and share recipes. Whether you're an experienced chef or a beginner cook, Foodily provides an easy and intuitive platform to explore new dishes, store your favorite recipes, and share your culinary creations with others.

This backend project, built using .NET, powers the core features of the Foodily platform. Key functionalities include:

- **User Authentication**: Users can create accounts, log in, and manage their profiles securely.
- **Recipe Management**: Users can create, update, and delete their recipes, as well as view and explore recipes shared by others.

With a focus on simplicity, performance, and security, Foodily aims to be your go-to platform for culinary inspiration and recipe sharing. The backend leverages technologies such as ASP.NET Core, Entity Framework Core, and JWT for authentication, ensuring a smooth and scalable experience for users.

 
## Requirements

This project is built using the following technologies:

- .NET 6 (or above)
- ASP.NET Core
- Entity Framework Core
- SQL Server
- Swagger for API documentation
- JWT (JSON Web Token) for authentication

## Features  

- RESTful API endpoints for CRUD operations
- User authentication and authorization with JWT
- Database migrations with Entity Framework Core
- API documentation with Swagger
- Configurable logging and exception handling
  
## Installation  

- Clone the repository:

 > https://github.com/ajeet1971/foodily-be.git

- Update the database connection string in <b>appsettings.json</b> as follows:

 > "ConnectionStrings": {
  "DefaultConnection": "Your_SQL_Server_Connection_String_Here"
}

## Usage    

After the project is running, you can interact with the API using tools like [Postman](https://www.postman.com/) or [Swagger](https://localhost:7088/swagger/index.html) for API documentation.
            
