# MyEshop

A modern ASP.NET Core MVC e-commerce application developed as a learning project to practice real-world e-commerce concepts and backend development.

## Features

### Customer Features

* User registration and login
* Secure authentication with cookies
* User account management
* Browse products
* View product details
* Add products to shopping cart
* Manage shopping cart items
* Product categorization and filtering

### Admin Panel

* User management

  * View users
  * Edit user information
  * Manage user roles and permissions

* Product management

  * Create products
  * Edit products
  * Delete products
  * Manage product information

* Category management

  * Create categories
  * Edit categories
  * Delete categories
  * Organize products into categories

### Security

* Authentication system
* Authorization and access control
* Admin-only routes and pages
* Role-based permissions

## Technologies

* ASP.NET Core MVC
* C#
* Entity Framework Core
* SQLite
* Razor Views
* HTML
* CSS
* JavaScript

## Learning Objectives

This project was built to practice:

* ASP.NET Core MVC architecture
* Entity Framework Core and database migrations
* Authentication and authorization
* CRUD operations
* Database relationships
* E-commerce application design
* Admin panel development
* Role-based access control

## Setup

```bash
git clone <repository-url>
cd MyEshop
dotnet restore
dotnet ef database update
dotnet run
```
