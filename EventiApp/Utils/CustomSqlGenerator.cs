
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Migrations.History;
using System.Data.Entity.Migrations.Model;
using System.Data.Entity.Migrations.Sql;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.EntityFramework;
using MySql.Data.MySqlClient;

namespace EventiApp.Utils
{
    
    public class MyCustomMigrationSQLGenerator : MySqlMigrationSqlGenerator
    {
        private string TrimSchemaPrefix(string table)
        {
            if (table.StartsWith("dbo."))
                return table.Replace("dbo.", "");
            return table;
        }

        protected override MigrationStatement Generate(CreateIndexOperation op)
        {
            StringBuilder sb = new StringBuilder();

            sb = sb.Append("CREATE ");

            if (op.IsUnique)
            {
                sb.Append("UNIQUE ");
            }

            sb.AppendFormat("index  `{0}` on `{1}` (", op.Name, TrimSchemaPrefix(op.Table));
            sb.Append(string.Join(",", op.Columns.Select(c => "`" + c + "`")) + ") ");

            return new MigrationStatement() { Sql = sb.ToString() };
        }
    }

    
    public class MyCustomEFConfiguration : DbConfiguration
    {
        public MyCustomEFConfiguration()
        {
            AddDependencyResolver(new MySqlDependencyResolver());
            SetProviderFactory(MySqlProviderInvariantName.ProviderName, new MySqlClientFactory());
            SetProviderServices(MySqlProviderInvariantName.ProviderName, new MySqlProviderServices());
            SetDefaultConnectionFactory(new MySqlConnectionFactory());
            SetMigrationSqlGenerator(MySqlProviderInvariantName.ProviderName, () => new MyCustomMigrationSQLGenerator());
            SetManifestTokenResolver(new MySqlManifestTokenResolver());
            SetHistoryContext(MySqlProviderInvariantName.ProviderName,
                (existingConnection, defaultSchema) => new MyCustomHistoryContext(existingConnection, defaultSchema));
        }
    }

    
    public class MyCustomHistoryContext : HistoryContext
    {
        public MyCustomHistoryContext(DbConnection existingConnection, string defaultSchema) : base(existingConnection, defaultSchema) { }

        protected override void OnModelCreating(System.Data.Entity.DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<HistoryRow>().HasKey(h => new { h.MigrationId });
        }
    }

}