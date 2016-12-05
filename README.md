# MusicStore Demo

This project is a fork of the ASP.NET Core MusicStore Demo at [https://github.com/aspnet/MusicStore]() and it has been adapted for use with .NET Core 1.1 on RHEL7 and OpenShift. You can find original samples, documentation and getting started instructions for ASP.NET Core at the [Home](https://github.com/aspnet/home) repo.

## Run on RHEL7
If you haven't already done so you will first need to install .NET Core and enable the software collection:
```
sudo subscription-manager repos --enable=rhel-7-server-dotnet-rpms
sudo yum install scl-utils
sudo yum install rh-dotnetcore11
sudo scl enable rh-dotnetcore11 bash
```
At this point you can clone the repository and run the MusicStore appliation locally:
```
git clone https://github.com/gpiercey/s2i-aspnet-musicstore-ex.git
cd s2i-aspnet-musicstore-ex/samples/MusicStore
dotnet restore
dotnet build
dotnet run
```
The MusicStore demo should now be runing on [http://127.0.0.1:8080]()

## Run on OpenShift Origin

### OpenShift Web Console
- login and create a project
- click add to project
- select musicstore from the list of quick-start applications, OR
- import templates/musicstore-template.json from the source folder if the quickstart doesn't exist
- provide an application name
- provide a database type (inmemory, sqlite, pgsql, mariadb or mysql)
- provide a database hostname
- provide a database port
- provide a database name (ie. musicstore)
- provide a database username
- provide a database password
- click create and go refill your coffee, this will take a couple minutes

NOTE: on OpenShift, if you are trying to connect to one if the ephimeral database apps the value of the hostname will be the app name you gave the database. For example, if you installed mariadb and gave it an application name of 'redhatrocks' then the database hostname will also be 'redhatrocks'.
