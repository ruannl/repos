using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RL.Database.Models {
	public class Retailer {
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[Column(Order = 0)]
		public int RetailerId { get; set; }

		[Required]
		[Column(Order = 1)]
		public string RetailerName { get; set; }

		[ForeignKey("RetailerId")]
		public virtual ICollection<RetailerIdentifier> RetailerIdentifiers { get; set; }
	}
}