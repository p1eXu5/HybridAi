# HybridAi Test Task
Web-сервис для определения географического местоположения
пользователя по IP адресу.

### Description
- Сервис хранит географические данные для всех IP-адресов в собственной базе под управлением PostgreSQL.
- Сервис обновляет указанную базу, используя данные поставщика [MaxMind GeoLite2](https://dev.maxmind.com/geoip/geoip2/geolite2/).
- Сервис имеет REST API для получения географического местоположения (в формате JSON) по заданному IP адресу.
- Задача обновления базы данных реализована в виде консольного приложения.
- REST API реализовано на ASP.NET Core.
- Обе указанные части сервиса написаны на C#.


### Requirements
- PostgreSQL Version 12.1 ([download](https://www.enterprisedb.com/thank-you-downloading-postgresql?anid=1257093))
- Existing DB User (Username=testuser;Password=123)

<br/>

## HybridAi.TestTask.ConsoleDbUpdater
When run without arguments it downloads files from this ([GeoLite2 City, .csv](https://geolite.maxmind.com/download/geoip/database/GeoLite2-City-CSV.zip)) and parse data to database.
For simplicity, the file MD5 check is omitted.

Usage:

\> ConsoleDbUpdater.exe <br/>
\> ConsoleDbUpdater.exe <file.zip> <br/>
\> ConsoleDbUpdater.exe <uri.file.zip>

## HybridAi.TestTask.WebApi

Query example:

http://localhost:5000/api/CityLocationInfo/2001:240:28e3::


##### For closing connections:

- RMD -> Query Tool...

<code>
select pg_terminate_backend(pid) from pg_stat_activity where datname='&lt;name&gt;';
</code>

<br/>
<br/>
This product includes GeoLite2 data created by MaxMind, available from
<a href="https://www.maxmind.com">https://www.maxmind.com</a>.
