using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ruann.Linde.Database.Models {
	public class Bank {
		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Column(Order = 0)]
		public virtual int BankId { get; set; }

		[Column(Order = 2)]
		public virtual string ImageSource { get; set; }

		[Required]
		[Column(Order = 1)]
		public virtual string Name { get; set; }

		[Column(Order = 3)]
		public virtual ICollection<BankAccount> BankAccounts { get; set; }
	}
}