namespace Ruann.Linde.Database {
	using System.Data.Entity;
	using System.Data.Entity.ModelConfiguration.Conventions;
	using System.Diagnostics;
	using Ruann.Linde.Database.Models;

	public class ApplicationDatabaseContext : DbContext {
		public ApplicationDatabaseContext() {
			Database.SetInitializer(new ApplicationDatabaseInitializer());
		}

		/// <summary>
		///     Quickbooks accounts
		/// </summary>
		public DbSet<Account> Accounts { get; set; }

		/// <summary>
		///     Quickbooks Account Types
		///     <list type = "table" >
		///         <item>
		///             <term>Transaction</term>
		///         </item>
		///         <item>
		///             <term>Split</term>
		///         </item>
		///     </list>
		/// </summary>
		public DbSet<AccountType> AccountTypes { get; set; }

		/// <summary>
		///     List of your bank accounts associated with  <see cref = "Bank" />
		/// </summary>
		public DbSet<BankAccount> BankAccounts { get; set; }

		/// <summary>
		///     The bank card associated with the bank account
		/// </summary>
		public DbSet<BankCard> BankCards { get; set; }

		/// <summary>
		///     Bank Data Model
		/// </summary>
		public DbSet<Bank> Banks { get; set; }

		public DbSet<BudgetItemCategory> BudgetItemCategories { get; set; }

		/// <summary>
		///     List of your budget items
		/// </summary>
		public DbSet<BudgetItem> BudgetItems { get; set; }

		/// <summary>
		///     List of phrases to remove from the description when processing quickbooks statements
		/// </summary>
		public DbSet<CleanupExpression> CleanupExpressions { get; set; }

		public DbSet<Company> Companies { get; set; }

		public DbSet<CompanyIdentifier> CompanyIdentifiers { get; set; }

		public DbSet<ContactMessage> ContactMessages { get; set; }

		public DbSet<Customer> Customers { get; set; }

		/// <summary>
		///     ELMAH Error table
		/// </summary>
		public DbSet<ELMAH_Error> ELMAH_Error { get; set; }

		/// <summary>
		///     Log 4 Net logging table
		/// </summary>
		public DbSet<Log> Log { get; set; }

		/// <summary>
		///     The type of lookup to perform from the description field in the uploaded statement
		///     <list type = "bullet" >
		///         <item>
		///             <term>Contains</term>
		///             <description>Contains the expression to look for</description>
		///         </item>
		///         <item>
		///             <term>Starts With</term>
		///             <description>The expression starts with</description>
		///         </item>
		///         <item>
		///             <term>Ends With</term>
		///             <description>The expression ends with</description>
		///         </item>
		///     </list>
		/// </summary>
		public DbSet<LookupType> LookupTypes { get; set; }

		public DbSet<PaymentTypeIdentifier> PaymentTypeIdentifierIdentifiers { get; set; }

		public DbSet<PaymentType> PaymentTypes { get; set; }

		public DbSet<RetailerIdentifier> RetailerIdentifiers { get; set; }

		public DbSet<Retailer> Retailers { get; set; }

		public DbSet<Statement> Statements { get; set; }

		public DbSet<Transaction> Transactions { get; set; }

		public DbSet<TransactionTypeIdentifier> TransactionTypeIdentifiers { get; set; }

		public DbSet<TransactionType> TransactionTypes { get; set; }

		protected override void OnModelCreating(DbModelBuilder modelBuilder) {
			modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
			modelBuilder.Build(Database.Connection);

			Database.Log = message => Debug.WriteLine(message);
		}
	}
}