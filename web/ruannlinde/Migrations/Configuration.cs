using System.Data.Entity.Migrations;
using System.Data.Entity.Migrations.Model;
using System.Data.Entity.Migrations.Sql;
using System.Linq;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

using RL.Models;
using MySqlHistoryContext = RL.Models.MySqlHistoryContext;

namespace RL.Migrations {
    public class CustomMySqlMigrationSqlGenerator : MySqlMigrationSqlGenerator {
        protected override MigrationStatement Generate(CreateIndexOperation op) {
            var u = new MigrationStatement();
            string unique = op.IsUnique ? "UNIQUE" : "", columns = "";
            foreach (var col in op.Columns) columns += $"`{col}` DESC{(op.Columns.IndexOf(col) < op.Columns.Count - 1 ? ", " : "")}";
            u.Sql = $"CREATE {unique} INDEX `{op.Name}` ON `{TrimSchemaPrefix(op.Table)}` ({columns}) USING BTREE";

            return u;
        }

        private string TrimSchemaPrefix(string table) {
            if (table.StartsWith("dbo."))
                return table.Replace("dbo.", "");
            return table;
        }
    }

    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext> {
        public Configuration() {
            AutomaticMigrationsEnabled = true;

            SetSqlGenerator("MySql.Data.MySqlClient", new CustomMySqlMigrationSqlGenerator());
            SetHistoryContextFactory("MySql.Data.MySqlClient", (conn, schema) => new MySqlHistoryContext(conn, schema));
        }

        protected override void Seed(ApplicationDbContext context) {
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext()));

            var user = new ApplicationUser {
                UserName = "superuser",
                Email = "superuser@ruannlinde.com",
                PasswordHash = "password1*",
                EmailConfirmed = true
            };

            manager.Create(user, "superuser");

            if (!roleManager.Roles.Any()) {
                roleManager.Create(new IdentityRole { Name = "Admin" });
                roleManager.Create(new IdentityRole { Name = "Support" });
                roleManager.Create(new IdentityRole { Name = "Intuit" });
                roleManager.Create(new IdentityRole { Name = "Accounting" });
            }

            var adminUser = manager.FindByName("superuser");

            manager.AddToRoles(adminUser.Id, "Admin", "Support");
        }
    }
}