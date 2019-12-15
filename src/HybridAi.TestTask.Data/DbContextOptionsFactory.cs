using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace HybridAi.TestTask.Data
{
    public class DbContextOptionsFactory
    {
        public const string DEFAULT_CONNECTION_STRING 
            = "Host=localhost;Database=IpDb;Username=testuser;Password=123;IntegratedSecurity=true";

        private static DbContextOptionsFactory _instance;

        public static DbContextOptionsFactory Instance
            => _instance ??= new DbContextOptionsFactory();

        protected DbContextOptionsFactory()
        {
            var coll = ConfigurationManager.ConnectionStrings["Default"];
            ConnectionString = coll != null ? coll.ConnectionString : DEFAULT_CONNECTION_STRING;
        }

        public string ConnectionString { get; set; }

        private DbContextOptions< IpDbContext > _dbContextOptions;

        public DbContextOptions< IpDbContext > DbContextOptions
        {
            get => _dbContextOptions ?? GetDbContextOptions();
            set => _dbContextOptions = value;
        }

        public DbContextOptions< IpDbContext > GetDbContextOptions()
        {
            var connectionString = ConnectionString;

            if ( !String.IsNullOrWhiteSpace( connectionString ) ) {
                return new DbContextOptionsBuilder< IpDbContext >().UseNpgsql( connectionString ).EnableSensitiveDataLogging().Options;
            }
            else {
                return new DbContextOptionsBuilder< IpDbContext >().UseNpgsql( DEFAULT_CONNECTION_STRING ).EnableSensitiveDataLogging().Options;
            }
        }
    }
}
