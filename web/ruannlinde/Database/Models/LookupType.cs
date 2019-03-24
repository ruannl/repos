using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RL.Database.Models {
	public class LookupType {
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[Column(Order = 0)]
		public int LookupTypeId { get; set; }

		[Required]
		[Column(Order = 1)]
		[DataType(DataType.Text)]
		public string Name { get; set; }

		public IList<CompanyIdentifier> CompanyIdentifiers { get; set; }
		public IList<PaymentTypeIdentifier> PaymentTypeIdentifiers { get; set; }
	}
}