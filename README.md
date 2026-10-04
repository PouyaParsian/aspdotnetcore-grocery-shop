# Grocery Shop

A modern ASP.NET Core MVC grocery store application developed as a learning project to simulate a real-world online supermarket experience.

## Features

### Customer Features

* User registration and login
* Account management
* Browse grocery products
* Product details page
* Shopping cart functionality
* Category-based product browsing
* Secure authentication

### Admin Panel

#### User Management

* View users
* Edit user information
* Manage user roles and permissions

#### Product Management

* Add products
* Edit products
* Delete products
* Manage product inventory and information

#### Category Management

* Add categories
* Edit categories
* Delete categories
* Organize products by category

### Security & Access Control

* Cookie-based authentication
* Role-based authorization
* Protected admin area
* Access control for administrative operations

## Technologies

* ASP.NET Core MVC
* C#
* Entity Framework Core
* SQLite
* Razor Views
* HTML
* CSS
* JavaScript

## Purpose

This project was created to practice:

* ASP.NET Core MVC
* Entity Framework Core
* Authentication & Authorization
* CRUD Operations
* Database Relationships
* Admin Panel Development
* Online Store Architecture

## Run Locally

```bash
git clone https://github.com/PouyaParsian/aspdotnetcore-grocery-shop.git

cd aspdotnetcore-grocery-shop

dotnet restore

dotnet ef database update

dotnet run
```
