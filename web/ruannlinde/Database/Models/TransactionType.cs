using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ruann.Linde.Database.Models {
	[Table("TransactionTypes")]
	public class TransactionType {
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[Column(Order = 0)]
		public virtual int TransactionTypeId { get; set; }

		[Required] [Column(Order = 1)] public virtual string TransactionTypeName { get; set; }

		[Column(Order = 2)]
		public virtual ICollection<TransactionTypeIdentifier> TransactionTypeIdentifiers { get; set; }
	}
}