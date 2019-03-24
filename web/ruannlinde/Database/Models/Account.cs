using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RL.Database.Models {
	public class Account {
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[Column(Order = 0)]
		public int AccountId { get; set; }

		[Required]
		[Column(Order = 1)]
		[DataType(DataType.Text)]
		public string AccountName { get; set; }

		[Column(Order = 2)]
		public AccountType AccountType { get; set; }
	}
}