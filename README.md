# employeehub-api

Backend for **employeehub** project built using .NET Core Web API

## Database
The database for the project was generated using Migrations. The generated migrations were then manually updated to support more ideal datatypes.

### To generate the database on your local machine:
### Steps
1. Open `employeehub-be` project in **Visual Studio**.
2. Open the Nuget Package Manager console by selecting `Tools > Nuget Package Manager > Package Manager Console`.
3. Run the `Update-Database` command to generate the **employeeshub** database on your local machine.

## Running the Project
- You can run the project on **Visual Studio**
- Or you can run the project by executing `dotnet run --project employeehub-api` on the command line at the root of the employeehub-be folder.

Open [https://localhost:7113/swagger/index.html](https://localhost:7113/swagger/index.html) to open Swagger UI API tester.  

