## Employees Information Manager
A web form to manage employees’ basic information and their skills using .NET MVC & Entity Framework.
 
## Screenshots
![](https://github.com/Kallaf/Employees-information-manager-/blob/master/Screenshots/screenshot2.gif?raw=true)

## Tech/framework used
<b>Built with</b>
- [ASP.NET MVC](https://dotnet.microsoft.com/apps/aspnet/mvc)
- [Entity Framework](https://docs.microsoft.com/en-us/ef/)
- [JQUERY TAGATOR](https://www.jqueryscript.net/form/Tag-Input-With-Autocomplete-jQuery-Tagator.html)

## Features
1- A form includes the following fields:
  - Employee Full Name
  - Employee Email
  - Skills

2- A user-friendly way to enter the skills that allows the user to choose from the predefined list of skills using autocomplete, and easily select or remove any of them.

3- A list of saved employees in a table with edit and delete employees features.

## Setup local development environment
- Clone the repo
```
git clone https://github.com/Kallaf/Employees-information-manager-.git
```
- Add appsettings.json file in src/ folder as the following
```
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft": "Warning",
      "Microsoft.Hosting.Lifetime": "Information"
    }
  },
  "AllowedHosts": "*",
  "ConnectionStrings": {
    "DefaultConnection": "YOUR_REMOTE_CONNECTION_STRING"
  }
}

```
- Run the following CLI commands:
```
dotnet tool install --global dotnet-ef
cd src/
dotnet add package Microsoft.EntityFrameworkCore.SQLite
dotnet add package Microsoft.EntityFrameworkCore.Design
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
set ASPNETCORE_ENVIRONMENT=Development
dotnet ef database update
dotnet run
```


## Contribute

Pull requests are welcome. For major changes, please open an issue first to discuss what you would like to change.

## Credits
- [Get started with ASP.NET Core MVC](https://docs.microsoft.com/en-us/aspnet/core/tutorials/first-mvc-app/start-mvc?view=aspnetcore-3.1&tabs=visual-studio)
- [Getting Started with EF Core](https://docs.microsoft.com/en-us/ef/core/get-started/?tabs=netcore-cli)
- [Implementing the Repository and Unit of Work Patterns in an ASP.NET MVC Application](https://docs.microsoft.com/en-us/aspnet/mvc/overview/older-versions/getting-started-with-ef-5-using-mvc-4/implementing-the-repository-and-unit-of-work-patterns-in-an-asp-net-mvc-application)

## License
MIT © [Ahmed Elkalaf](https://github.com/Kallaf/Employees-information-manager-/blob/master/LICENSE)
