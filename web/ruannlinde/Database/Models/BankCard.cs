using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ruann.Linde.Database.Models {
	[Table("BankCards")]
	public class BankCard {
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[Column(Order = 0)]
		public virtual int BankCardId { get; set; }

		[Required]
		[Column(Order = 1)]

		public virtual string BankCardName { get; set; }
		[Column(Order = 2)]
		[Required]
		public virtual string BankCardNumber { get; set; }

		[Column(Order = 3)]
		public virtual BankAccount BankAccount { get; set; }
	}
}