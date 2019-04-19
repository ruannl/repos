using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ruann.Linde.Database.Models {

	[Table("Retailers")]
	public class Retailer {
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[Column(Order = 0)]
		public virtual int RetailerId { get; set; }

		[Required]
		[Column(Order = 1)]
		public virtual string RetailerName { get; set; }

		[Column(Order = 2)]
		public virtual ICollection<RetailerIdentifier> RetailerIdentifiers { get; set; }
	}
}