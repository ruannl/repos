using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RL.Database.Models {
	public class PaymentTypeIdentifier {
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[Column(Order = 0)]
		public int PaymentTypeIdentifierId { get; set; }

		[Required]
		[Column(Order = 1)]
		[DataType(DataType.Text)]
		public string Expression { get; set; }

		[Column(Order = 2)]
		public virtual PaymentType PaymentType { get; set; }

		[Column(Order = 3)]
		public virtual LookupType LookupType { get; set; }
	}
}