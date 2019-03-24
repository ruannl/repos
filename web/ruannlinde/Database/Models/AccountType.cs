using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RL.Database.Models {
	public class AccountType {
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[Column(Order = 0)]
		public virtual int AccountTypeId { get; set; }

		[Required]
		[Column(Order = 1)]
		[DataType(DataType.Text)]
		public virtual string AccountTypeName { get; set; }

		[Required]
		[Column(Order = 2)]
		[DataType(DataType.Text)]
		public virtual string AccountTypeCode { get; set; }

		[Column(Order = 3)]
		public virtual ICollection<Account> Accounts { get; set; }
	}
}