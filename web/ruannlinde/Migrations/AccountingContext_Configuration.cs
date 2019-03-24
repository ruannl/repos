using System.Collections.Generic;
using System.Linq;
using MySql.Data.Entity;
using RL.Areas.Accounting.Models;

namespace RL.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class AccountingContext_Configuration : DbMigrationsConfiguration<Areas.Accounting.Providers.AccountingContext>
    {
        // pmc EF Commands

        // enable-migrations -ContextTypeName RL.Models.ApplicationDbContext
        // enable-migrations -ContextTypeName RL.Areas.Accounting.Providers.AccountingContext

        // add-migration InitialCreate -ContextTypeName RL.Models.ApplicationDbContext
        // add-migration InitialCreate -ContextTypeName  RL.Areas.Accounting.Providers.AccountingContext

        // update-database -Verbose 

        public AccountingContext_Configuration()
        {
            AutomaticMigrationsEnabled = true;
            SetSqlGenerator("MySql.Data.MySqlClient", new MySqlMigrationSqlGenerator());
            SetHistoryContextFactory("MySql.Data.MySqlClient", (conn, schema) => new MySqlHistoryContext(conn, schema));
        }

        protected override void Seed(Areas.Accounting.Providers.AccountingContext context)
        {
            var bank = context.Banks.SingleOrDefault(x => x.Name.ToLower() == "fnb");
            context.BankAccounts.AddOrUpdate(account =>
                                                 new { account.BankId, account.AccountName, account.AccountNumber },
                                             new BankAccount
                                             {
                                                 BankId = bank.BankId,
                                                 AccountName = "FNB Platinum Cheque Account",
                                                 AccountNumber = "62361804878"
                                             });

        }
    }
}