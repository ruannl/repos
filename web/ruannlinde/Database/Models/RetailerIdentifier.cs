using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RL.Database.Models {
	public class RetailerIdentifier {
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[Column(Order = 0)]
		public virtual int RetailerIdentifierId { get; set; }

		[Required]
		[Column(Order = 1)]
		public virtual string Identity { get; set; }

		[Column(Order = 2)]
		public virtual Retailer Retailer { get; set; }
	}
}