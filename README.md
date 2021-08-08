# Short URL Generator.

### Running the application (Visual Studio)

To Run the application, first you must setup the database.
To do this edit the connection string inside the appSettings.json file to point to your local SQL Server instance, and set the database name to the desired name for your new database.

```
  "ConnectionStrings": {
    "ApplicationDbContextConnection": "Your connection string goes here."
  }
```

Then inside Visual Studio, go to the Package Manager and run the following command

```
PM> update-database
```
This will create the database and run the neccessary migrations to bring your database up to date.
Then you can use Ctrl + F5 to run the application through visual studio using IIS Express.

### Running the application (dotnet cli)

If you would prefer to use the dotnet cli, remember to add your connection to the appSettings.json.
Navigate into the application folder .\PayrocTest\
Then ensure you have dotnet-ef tools install by running 

```
> dotnet ef
```

If not you will need to run the following

```
> dotnet tool install --global dotnet-ef
```

Afterwards you can then use the dotnet cli to update your database.

```
> dotnet ef database update
```

Then you can run the application using the dotnet run command

```
> dotnet run
```

To run unit tests through the dotnet cli, navigate to the .\PayrocTest.Tests\ folder and run the dotnet test command

```
> dotnet test
```
