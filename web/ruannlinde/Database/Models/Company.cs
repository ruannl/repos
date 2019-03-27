using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ruann.Linde.Database.Models {
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

		[Column(Order = 3)]
		public virtual IList<Transaction> Transactions { get; set; }

		[Column(Order = 4)]
		public virtual IList<CompanyIdentifier> CompanyIdentifiers { get; set; }
	}
}