using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RL.Database.Models {
	public class Customer {
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[Column(Order = 0)]
		public virtual int CustomerId { get; set; }

		[Required]
		[Column(Order = 1)]
		[DataType(DataType.Text)]
		public virtual string CustomerName { get; set; }

		[Column(Order = 2)]
		[DataType(DataType.Text)]
		public virtual string Quickbooks { get; set; }
	}
}