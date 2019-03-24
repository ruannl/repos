using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RL.Database.Models {
	public class Company {
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[Column(Order = 0)]
		public virtual int CompanyId { get; set; }

		[Required]
		[Column(Order = 1)]
		[DataType(DataType.Text)]
		public virtual string Name { get; set; }

		[Required]
		[Column(Order = 2)]
		[DataType(DataType.Text)]
		public virtual string Code { get; set; }

		[Column("TransactionId", Order = 3)] public virtual IList<Transaction> Transactions { get; set; }

		[Column("CompanyIdentifierId", Order = 4)] public virtual IList<CompanyIdentifier> CompanyIdentifiers { get; set; }
	}
}