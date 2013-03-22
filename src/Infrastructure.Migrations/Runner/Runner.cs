﻿using System.Reflection;
using FluentMigrator;
using FluentMigrator.Runner;
using FluentMigrator.Runner.Announcers;
using FluentMigrator.Runner.Initialization;

namespace Infrastructure.Migrations.Runner
{
    public static class Runner
    {
        public static IAnnouncer Announcer { get; set; }

        public static string ConnectionString { get; set; }

        public const long VersionLatest = -1;

        static Runner()
        {
            Announcer = new NullAnnouncer();
        }

        public class MigrationOptions : IMigrationProcessorOptions
        {
            public bool PreviewOnly { get; set; }
            public int Timeout { get; set; }
        }

        public static void MigrateUp(long version = -1, string profile = "")
        {
            GetMigrationRunner(version, profile).MigrateUp(true);
        }

        public static void MigrateDown(long version)
        {
            GetMigrationRunner(version).MigrateDown(version);
        }

        public static void RollbackToVersion(long version)
        {
            GetMigrationRunner(-1).RollbackToVersion(version);
        }

        private static MigrationRunner GetMigrationRunner(long version, string profile = "")
        {
            if(string.IsNullOrEmpty(ConnectionString))
                throw new EmptyConnectionStringException("ConnectionString property not initialized");
            var options = new MigrationOptions() {PreviewOnly = false, Timeout = 60};
            var factory = new FluentMigrator.Runner.Processors.SqlServer.SqlServer2008ProcessorFactory();
            var processor = factory.Create(ConnectionString, Announcer, options);
            var assembly = Assembly.GetExecutingAssembly();
            var runner = new MigrationRunner(assembly, GetMigrationContext(version, profile), processor);
            return runner;
        }

        private static RunnerContext GetMigrationContext(long version, string profile)
        {
            var context = new RunnerContext(Announcer) 
            {
                Namespace = "Infrastructure.Migrations.Migrations",
            };
            if (version > -1)
                context.Version = version;
            if (!string.IsNullOrEmpty(profile))
                context.Profile = profile;
            return context;
        }
    }
}
