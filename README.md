dotnet new webapi -n PortfolioApp
cd PortfolioApp
<!--Entity Framework ORM-->
dotnet add package Microsoft.EntityFrameworkCore --version 6.0.21
dotnet add package Npgsql.EntityFrameworkCore.PostgreSQL --version 6.0.7
<!--Swagger-->
dotnet add package Swashbuckle.AspNetCore --version 6.4.0
<!--Migration-->
dotnet tool install --global dotnet-ef --version 6.0.21
dotnet add package Microsoft.EntityFrameworkCore.Design --version 6.0.21
dotnet ef migrations add InitialCreate
dotnet ef database update
dotnet run
