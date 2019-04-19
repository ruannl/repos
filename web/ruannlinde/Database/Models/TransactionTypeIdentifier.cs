using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ruann.Linde.Database.Models {
	[Table("TransactionTypeIdentifiers")]
	public class TransactionTypeIdentifier {
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[Column(Order = 0)]
		public virtual int TransactionTypeIdentifierId { get; set; }

		[Required] [Column(Order = 1)] public virtual string TransactionTypeIdentifierPrimary { get; set; }

		[Column(Order = 2)] public virtual string TransactionTypeIdentifierSecondary { get; set; }

		[Column(Order = 3)]
		public virtual TransactionType TransactionType { get; set; }
	}
}