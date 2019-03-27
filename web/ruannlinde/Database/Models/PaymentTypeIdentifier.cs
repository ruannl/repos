using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ruann.Linde.Database.Models {
	public class PaymentTypeIdentifier {
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[Column(Order = 0)]
		public virtual int PaymentTypeIdentifierId { get; set; }

		[Required]
		[Column(Order = 1)]
		[DataType(DataType.Text)]
		public virtual string Expression { get; set; }

		[Column(Order = 2)]
		public virtual PaymentType PaymentType { get; set; }

		[Column(Order = 3)]
		public virtual LookupType LookupType { get; set; }
	}
}