using System.Data.Entity;
using RL.Areas.Accounting.Models;

namespace RL.Areas.Accounting.Providers
{
    public class AccountingContext : DbContext {
        public AccountingContext() : base("AccountingContext")
        {
            //Database.SetInitializer(new AccountingInitializer());
        }

        public DbSet<Bank> Banks { get; set; }
        public DbSet<BankAccount> BankAccounts { get; set; }
        public DbSet<BankCard> BankCards { get; set; }
        public DbSet<BudgetItem> BudgetItems { get; set; }
        public DbSet<BudgetItemCategory> BudgetItemCategories { get; set; }
        public DbSet<Retailer> Retailer { get; set; }
        public DbSet<RetailerIdentifier> RetailerIdentifiers { get; set; }
        public DbSet<Statement> Statements { get; set; }
        public DbSet<TransactionEntry> Transactions { get; set; }
        public DbSet<TransactionType> TransactionTypes { get; set; }
        public DbSet<TransactionTypeIdentifier> TransactionTypeIdentifiers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}