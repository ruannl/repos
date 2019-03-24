using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RL.Database.Models {
	public class TransactionTypeIdentifier {
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[Column(Order = 0)]
		public int TransactionTypeIdentifierId { get; set; }

		[Required] [Column(Order = 1)] public string TransactionTypeIdentifierPrimary { get; set; }

		[Column(Order = 2)] public string TransactionTypeIdentifierSecondary { get; set; }

		[InverseProperty("TransactionTypeId")]
		[Column(Order = 3)]
		public int? TransactionTypeId { get; set; }
	}
}