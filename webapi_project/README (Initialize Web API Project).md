Step 1 Initialize web project:

dotnet new webapi -n Project_Name
cd Project_name

Step 2 Add PostgreSQL + EF packages:

dotnet add package Microsoft.EntityFrameworkCore --version 8.0.8
dotnet add package Microsoft.EntityFrameworkCore.Design --version 8.0.8
dotnet add package Npgsql.EntityFrameworkCore.PostgreSQL --version 8.0.4

Step 3 Install Entity Framework CLI tool (one time) for database migrations

dotnet tool install --global dotnet-ef
export PATH="$PATH:/Users/os/.dotnet/tools"
dotnet-ef --version

Main DB Migrations Command

dotnet ef migrations add Init
dotnet ef database update

Step 4 Create folders

mkdir Models
mkdir Data
mkdir Controllers

Step 5 Create files

touch Models/Product.cs
touch Data/AppDbContext.cs
touch Controllers/ProductsController.cs

Step 6 Test if its running on postman

dotnet run

http://localhost:5000/weatherforecast GET

Dont PUSH BIN AND OBJ it is only generated when compiling code

The code dependencies similar to package.json are located at your project_name.csproj 

CREATE DATABASE SCHEMA

Step 1. Create Database Classes for EF Core must be classes not Record or anything /src/data/types
Step 2. Specify EF Core configurations main entry point for data migrations /src/data/config

