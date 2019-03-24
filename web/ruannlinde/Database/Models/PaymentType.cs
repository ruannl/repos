using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RL.Database.Models {
	public class PaymentType {
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[Column(Order = 0)]
		public int PaymentTypeId { get; set; }

		[Required]
		[Column(Order = 1)]
		[DataType(DataType.Text)]
		public string PaymentTypeName { get; set; }

		[Required]
		[Column(Order = 2)]
		[DataType(DataType.Text)]
		public string PaymentTypeCode { get; set; }

		[Column("PaymentTypeIdentifierId", Order = 3)]
		public virtual IList<PaymentTypeIdentifier> PaymentTypeIdentifiers { get; set; }
	}
}