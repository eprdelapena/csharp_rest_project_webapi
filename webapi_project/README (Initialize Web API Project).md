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

# It creates a migration that represents the difference between: Current C# model vs Last EF snapshot
dotnet ef migrations add Init

# (optional) if you want to undo
ef migrations remove

# It generates a SQL script that safely applies all pending EF migrations, and writes it to Init.sql. Init.Sql can be any name
# --idempotent: Make the SQL script safe to run multiple times, without causing errors or duplicate migrations. Ensures migrations already applied are skipped Safe for production: can be run on a partially migrated database
# --o: -o (or --output) is a command-line option that tells EF Core to save the generated SQL to a file instead of printing it in the terminal.
# EF looks at all migration files in your migrations/ folder. EF compares your last applied migration (or 0 if nothing applied) with the current model snapshot (AppDbContextModelSnapshot.cs). It generates SQL for all pending changes up to the latest migration.
dotnet ef migrations script --idempotent -o sql/Init.sql

# migrate that said sql to your db
PGPASSWORD=12345 psql -h localhost -p 5439 -U csharpbackend -d dbcsharpbackend -f sql/CreateTable.sql