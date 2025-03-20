# ETL Pipeline

The project provides a simple data pipeline using C# and SQL Server that can extract,
transform, and load (ETL) data. It is implemented using .NET 9 and Angular 19.

## Project structure  ##


**1. ETL.API**

This is the backend .NET Web API project. It contains also:

- ETL.Data - data access layer
- ETL.Services - business logic

**2. ETL.Clients**

This folder is dedicated for clients, consuming the API. Currently there is one client, implemented with Angular.

## How to use it  ##


**1. Backend**

- Open the ETL.API solution and configure your connection string in the `appsettings.json `file.
- In the Package Manager Console of Visual Studio select `ETL.Data` and execute update-database. This will build your database schema and will also add 1 stored procedure, defined in the migration `20250320144741_TransactionStoredProcedure.cs`
- Start the project. It will open a browser on `http://localhost:5266` with the swagger default view, where you can also test the API.

**2. Frontend**

- Open the `etl-ui-angular` inside Visual Studio Code
- In the terminal execute `npm install`. This will add all the needed dependencies for angular to work.
- Execute `ng-serve` and the application should start on `http://localhost:4200`

## How it works  ##

There is only one page with two buttons - **Start ETL** and **Clear Data**. The start will collect some data from a csv file and from a mocked server response, and then it will persist that data in the database using the stored procedure `usp_InsertTransactions`. This logic is inside the `EtlService.cs` class. The `ETL.Data` project contains a repository pattern to work with the database.

Clear data will delete everything from the database.

The frontend logic is in the **etl-monitor.component.ts**. That component uses NgRx actions to start the ETL or clear the execution. These actions are defined inside the `etl.actions.ts` file.

When an action is started, it is cached by the `etl.effects.ts` file, which is using `etl.service.ts` to make HTTP requests to the backend API. After the response is returned it is in the `etl.reducers.ts` where the data is stored, depending if there was an error, transactions are returned or cleared. The reducer is the actual store and a single source of truth, from which the **etl-monitor.component.ts** is reading its data.