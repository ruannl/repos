using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RL.Database.Models {
	public class Transaction {
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[Column(Order = 0)]
		public int TransactionId { get; set; }

		[Required]
		[Column(Order = 1)]
		[DataType(DataType.DateTime)]
		public DateTime Date { get; set; }

		[Required]
		[Column(Order = 2)]
		[DataType(DataType.Text)]
		public string Description { get; set; }

		[Required]
		[Column(Order = 3)]
		[DataType(DataType.Currency)]
		public decimal Amount { get; set; }

		[Column(Order = 4)]
		[DataType(DataType.Text)]
		public string CleanDescription { get; set; }

		[Column("StatementId", Order = 5)] public Statement Statement { get; set; }

		[Column("CompanyId", Order = 6)] public Company Company { get; set; }

		[Column("PaymentTypeId", Order = 7)] public PaymentType PaymentType { get; set; }

		[Column("BankCardId", Order = 8)] public BankCard BankCard { get; set; }

		[Column("RetailerId", Order = 9)] public Retailer Retailer { get; set; }

		[Column("BudgetItemId", Order = 10)] public BudgetItem BudgetItem { get; set; }

		[Column("TransactionTypeId", Order = 11)]
		public TransactionType TransactionType { get; set; }

		[Column("Failed", Order = 12)] public bool Failed { get; set; }

		[NotMapped]
		public bool NotAColumn {
			get {
				if (Company == null && PaymentType == null) return true;
				return false;
			}
		}
	}
}