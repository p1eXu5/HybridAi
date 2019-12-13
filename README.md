# HybridAi
Test task for hybrid.ai

### Requirements
- PostgreSQL Version 12.1 ([download](https://www.enterprisedb.com/thank-you-downloading-postgresql?anid=1257093))
- Existing DB User (Username=testuser;Password=123)

<br/>

## HybridAi.TestTask.ConsoleDbUpdater
When run without arguments it downloads files from this ([GeoLite2 City, .csv](https://geolite.maxmind.com/download/geoip/database/GeoLite2-City-CSV.zip)) and parse data to database.
For simplicity, the file MD5 check is omitted.


##### For closing connections:

- RMD -> Query Tool...

<code>
select pg_terminate_backend(pid) from pg_stat_activity where datname='&lt;name&gt;';
</code>