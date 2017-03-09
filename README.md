# MusicStore Demo

This project is a fork of the ASP.NET Core MusicStore Demo at [https://github.com/aspnet/MusicStore](https://github.com/aspnet/MusicStore) and it has been extended to support several open source database providers. You can find original samples, documentation and getting started instructions for ASP.NET Core at the [Home](https://github.com/aspnet/home) repo.

## Supported database providers

The database providers can be selected by changing the config.json file or by setting the `Data__DefaultConnection__Provider` and `Data__DefaultConnection__ConnectionString` environment variables.

| Database             | Package                                 | Provider  | Connection string example |
| -------------------- | --------------------------------------- | --------- | ------------------------- |
| MySql/MariaDB        | Pomelo.EntityFrameworkCore.MySql        | mysql     | "server=127.0.0.1;port=3306;database=musicstore;uid=root;pwd=root;" |
| PostgreSQL           | Npgsql.EntityFrameworkCore.PostgreSQL   | npgsql    | "Host=localhost;Database=musicstore;Username=musicstore;Password=musicstore" |
| SQLite               | Microsoft.EntityFrameworkCore.Sqlite    | sqlite    | "data source=musicstore.db;" |


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
git clone https://github.com/redhat-developer/s2i-aspnet-musicstore-ex.git
cd s2i-aspnet-musicstore-ex/samples/MusicStore
dotnet restore
dotnet build
dotnet run
```
The MusicStore demo should now be runing on [http://127.0.0.1:8080]()

## Run on OpenShift

The **dotnet-pgsql-persistent** template in the [s2i-dotnetcore](https://github.com/redhat-developer/s2i-dotnetcore) repository
instantiates the MusicStore application with a PostgreSQL database. The [README.md](https://github.com/redhat-developer/s2i-dotnetcore/blob/master/README.md)
describes how you use the template.
