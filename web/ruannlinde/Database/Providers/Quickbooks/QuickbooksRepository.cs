using System.Collections.Generic;
using log4net;
using RL.Database.Models;

namespace RL.Database.Providers.Quickbooks {
	public interface IQuickbooksRepository {
		IList<Transaction> GetTransactions(int statementId);
		IList<Statement> GetStatements();
		IList<Company> GetCompanies();
		IList<PaymentType> GetPaymentTypes();
		IList<Transaction> ProcessStatement(int statementId);
		IList<Account> QuickbooksAccounts();

		Customer QuickbooksCustomer();

		//TransactionReportViewModel TransactionReport(int statementId);
		void AddStatement(Statement             statement);
		void AddTransactions(IList<Transaction> transactions);
		void DeleteStatement(int                statementId);
	}

	public class QuickbooksRepository : IQuickbooksRepository {
		internal ApplicationDatabaseContext ApplicationDatabaseContext;
		internal ILog Log = LogManager.GetLogger(typeof(QuickbooksRepository).Name);

		public QuickbooksRepository(ApplicationDatabaseContext dbContext) {
			ApplicationDatabaseContext = dbContext;
		}

		public IList<Transaction> GetTransactions(int statementId) {
			return null;
		}

		public IList<Statement> GetStatements() {
			return null;
		}

		public IList<Company> GetCompanies() {
			return null;
		}

		public IList<PaymentType> GetPaymentTypes() {
			return null;
		}

		public IList<Transaction> ProcessStatement(int statementId) {
			return null;
		}

		public IList<Account> QuickbooksAccounts() {
			return null;
		}

		public Customer QuickbooksCustomer() {
			return null;
		}

		//public TransactionReportViewModel TransactionReport(int statementId) {
		//	return default;
		//}

		public void AddStatement(Statement statement) {
		}

		public void AddTransactions(IList<Transaction> transactions) {
		}

		public void DeleteStatement(int statementId) {
		}
	}
}