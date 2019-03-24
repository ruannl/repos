using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RL.Database.Models {
	public class RetailerIdentifier {
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[Column(Order = 0)]
		public int RetailerIdentifierId { get; set; }

		[Required]
		[Column(Order = 1)]
		public string Identity { get; set; }

		[InverseProperty("RetailerId")]
		[Column(Order = 2)]
		public int? RetailerId { get; set; }
	}
}