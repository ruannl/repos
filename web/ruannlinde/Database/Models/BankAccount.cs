using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RL.Database.Models {
	public class BankAccount {
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[Column(Order = 0)]
		public virtual int BankAccountId { get; set; }

		[Required] [Column(Order = 1)] public virtual string AccountName { get; set; }

		[Required] [Column(Order = 2)] public virtual string AccountNumber { get; set; }

		[Required]
		[Column(Order = 3)]
		public virtual Bank Bank { get; set; }

		[Column(Order = 4)]
		public virtual ICollection<BankCard> BankCards { get; set; }

		[Column(Order = 5)]
		public virtual ICollection<Statement> Statements { get; set; }
	}
}