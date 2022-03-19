# VHC.Product

- .NET Core 6
- Microsoft.EntityFrameworkCore 6.0.3
- Newtonsoft.Json 13.0.1
- xUnit 2.4.1
- FluentAssertions 6.5.1

## How to execute the project

Start the VHC.Product.API project first. You can use RESTful operations with swagger or you can start VHC.Product.Web/VHC.Product.Console project after.

## Explaining the structure

### VHC.Product.API

The WebAPI Application has two controllers (Product and Import) with endpoints to manage products (CRUD) and to import database based on a JSON file.

### VHC.Product.Helpers

This project only defines important interfaces to Dependency Injection.

### VHC.Product.Infrastructure

This project contains all repositories responsable to perform actions in the database. It uses InMemory Database from EntityFramework.

### VHC.Product.Model

This project has all models needed for this assigment. 
At this project, you can find a class to filter products also.

### VHC.Product.Services

The Service Layer contains all business logic that the project needs.

### VHC.Product.Tests

The Test Layer performs tests with a test InMemory Database and tests product's service.

### VHC.Product.Web

The Web Application is a simple MVC project to import database from a JSON file.

### VHC.Product.Console

The Console Application is a simple MVC project to import database from a JSON file. You have to inform the path from you local file to upload to API.
