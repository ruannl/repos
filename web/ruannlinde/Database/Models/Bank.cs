using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;


namespace RL.Database.Models {
	public class Bank {
		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Column(Order = 0)]
		public int BankId { get; set; }

		[Column(Order = 2)]
		public string ImageSource { get; set; }

		[Required]
		[Column(Order = 1)]
		public string Name { get; set; }

		[ForeignKey("BankAccountId")]
		public virtual ICollection<BankAccount> BankAccounts { get; set; }
	}
}