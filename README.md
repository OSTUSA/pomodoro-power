Pomodoro Power
==============

A Pomodoro Application Written In ASP.NET MVC4

Building
--------
Thanks to NuGet, building is a snap. Just clone the repo, open the solution, and build. Building
should take care of fetching all the dependencies.

The Database
------------
The database is versioned using FluentMigrator. After building the project, getting the database setup
to the latest and greatest is easy.

1. Make sure you have created a database named "Pomodoro"
2. Run `Migrate.exe` from the `bin` folder.

This is as easy as opening `cmd`, navigating to the project directory and firing this bad boy off:

```
bin\Migrate.exe -c "server=.\SQLExpress;database=Pomodoro;Integrated Security=SSPI" -db sqlserver2008
-a "src\Infrastructure.Migrations\bin\Debug\Infrastructure.Migrations.dll" -t migrate:up
```

This of course is assuming you are using SQLExpress. You can always change the connection string and provider used.

That's it! Easy mode!

A note on IoC
-------------
This application sports a pattern for connecting NHibernate to multiple databases.

The Infrastructure.IoC project works together with Infrastructure.NHibernate and a tiny
attribute in order to make multi-session applications a snap! (Should it be needed of course).
