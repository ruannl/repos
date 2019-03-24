using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RL.Database.Models {
	public class CompanyIdentifier {
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[Column(Order = 0)]
		public int CompanyIdentifierId { get; set; }

		[Required]
		[Column(Order = 1)]
		[DataType(DataType.Text)]
		public string Expression { get; set; }

		[Column(Order = 2)]
		public virtual Company Company { get; set; }

		[Column(Order = 3)]
		public virtual LookupType LookupType { get; set; }
	}
}