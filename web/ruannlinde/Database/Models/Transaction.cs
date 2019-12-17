using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ruann.Linde.Database.Models {

	[Table("Transactions")]
	public class Transaction {
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[Column(Order = 0)]
		public virtual int TransactionId { get; set; }

		[Index("IX_UniqueTransaction",1, IsUnique = true)]
		[Required]
		[Column(Order = 1)]
		[DataType(DataType.DateTime)]
		public virtual DateTime Date { get; set; }

		[Required]
		[Column(Order = 2)]
		[DataType(DataType.Text)]
		[MaxLength(450)]
		[Index("IX_UniqueTransaction", 2, IsUnique = true)]
		public virtual string Description { get; set; }

		[Required]
		[Column(Order = 3)]
		[DataType(DataType.Currency)]
		[Index("IX_UniqueTransaction", 3, IsUnique = true)]
		public virtual decimal Amount { get; set; }

		[Column(Order = 4)]
		[DataType(DataType.Text)]
		public virtual string CleanDescription { get; set; }

		[Column("StatementId", Order = 5)] public virtual Statement Statement { get; set; }

		[Column("CompanyId", Order = 6)] public virtual Company Company { get; set; }

		[Column("PaymentTypeId", Order = 7)] public virtual PaymentType PaymentType { get; set; }

		[Column("BankCardId", Order = 8)] public virtual BankCard BankCard { get; set; }

		[Column("RetailerId", Order = 9)] public virtual Retailer Retailer { get; set; }

		[Column("BudgetItemId", Order = 10)] public virtual BudgetItem BudgetItem { get; set; }

		[Column("TransactionTypeId", Order = 11)]
		public virtual TransactionType TransactionType { get; set; }

		[Column("Failed", Order = 12)] public virtual bool Failed { get; set; }

		[NotMapped]
		public virtual bool NotAColumn {
			get {
				if (Company == null && PaymentType == null) return true;
				return false;
			}
		}
	}
}