using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ruann.Linde.Database.Models {
	public class PaymentType {
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[Column(Order = 0)]
		public virtual int PaymentTypeId { get; set; }

		[Required]
		[Column(Order = 1)]
		[DataType(DataType.Text)]
		public virtual string PaymentTypeName { get; set; }

		[Required]
		[Column(Order = 2)]
		[DataType(DataType.Text)]
		public virtual string PaymentTypeCode { get; set; }

		[Column(Order = 3)]
		public virtual IList<PaymentTypeIdentifier> PaymentTypeIdentifiers { get; set; }
	}
}