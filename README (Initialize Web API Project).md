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

Step 4 Create folders

mkdir Models
mkdir Data
mkdir Controllers

Step 5 Create files

touch Models/Product.cs
touch Data/AppDbContext.cs
touch Controllers/ProductsController.cs

Step 6 Test if its running on postman

http://localhost:5000/weatherforecast GET