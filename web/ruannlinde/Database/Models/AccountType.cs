using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RL.Database.Models {
	public class AccountType {
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[Column(Order = 0)]
		public int AccountTypeId { get; set; }

		[Required]
		[Column(Order = 1)]
		[DataType(DataType.Text)]
		public string AccountTypeName { get; set; }

		[Required]
		[Column(Order = 2)]
		[DataType(DataType.Text)]
		public string AccountTypeCode { get; set; }

		[Column(Order = 3)]
		public ICollection<Account> Accounts { get; set; }
	}
}