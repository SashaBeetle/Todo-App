# To Do
### Project for `'Radency'` 
## Stack
* [.NET](https://dotnet.microsoft.com/) - free, open-source, cross-platform framework for building modern apps and powerful cloud services.
* [Postgres SQL](https://www.postgresql.org/) - is a powerful, open source object-relational database system with over 35 years of active development that has earned it a strong reputation for reliability, feature robustness, and performance.
* [Entity Framework](https://learn.microsoft.com/uk-ua/ef/) - object-relational mapping (ORM) framework for .NET developers that enables them to work with databases using .NET objects, simplifying the process of data access and manipulation.
* [NuGet packages](https://learn.microsoft.com/uk-ua/nuget/) - type of software package used in the Microsoft .NET ecosystem, containing compiled code and other resources, and are used by developers to easily add functionality to their projects and share code between teams.
* [Tailwind](https://tailwindui.com/) - beautifully designed, expertly crafted components and templates, built by the makers of Tailwind CSS.
* [Angular](https://angular.io/) - it is powerful tool for building web applications.
## How to run Backend
Open your system terminal and run commands:
```sh
git clone https://github.com/SashaBeetle/First-App.git
cd First-app/todo-backend
```
Add your already deployed database connection string to files:
In `To Do\todo-backend\todo-backend.Infrastructure\DIConfiguration.cs` method `GetConnectionString("ConnectionString")`. Instead of ConnectionString add your database connection string. Method should look like that:
```sh
private static void RegisterDatabaseDependencies(this IServiceCollection services, IConfigurationRoot configuration)
{
    services.AddDbContext<TodoDbContext>(options =>
        options.UseNpgsql(configuration.GetConnectionString("TodoDatabase")));
}
```
In `To Do\todo-backend\todo-backend.WEB\appsettings.json` in `"ConnectionStrings"` add line: `"NetworkConnection": "ConnectionString"`. Instead of `ConnectionString` add your database connection string. Code should look like this:
```sh
  "ConnectionStrings": {
    "TodoDatabase": "ConnectionString"
  }
```
## How to run Frontend
Open your system terminal and run commands:
```sh
git clone https://github.com/SashaBeetle/First-App.git
cd First-app/todo-frontend
```
Run the following command in your terminal to install the necessary dependencies for the Angular application:
```sh
npm install 
```
Once the dependencies are installed, run the following command to start the Angular development server:
```sh
ng serve 
```
