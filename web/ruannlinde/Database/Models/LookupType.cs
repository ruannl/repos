using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ruann.Linde.Database.Models {
	public class LookupType {
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[Column(Order = 0)]
		public virtual int LookupTypeId { get; set; }

		[Required]
		[Column(Order = 1)]
		[DataType(DataType.Text)]
		public virtual string Name { get; set; }

		[Column(Order = 2)]
		public virtual IList<CompanyIdentifier> CompanyIdentifiers { get; set; }

		[Column(Order = 3)]
		public virtual IList<PaymentTypeIdentifier> PaymentTypeIdentifiers { get; set; }
	}
}