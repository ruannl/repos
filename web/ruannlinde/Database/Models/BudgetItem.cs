using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ruann.Linde.Database.Models {

	[Table("BudgetItems")]
	public class BudgetItem {

		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[Column(Order = 0)]
		public virtual int BudgetItemId { get; set; }

		[Required]
		[Column(Order = 1)]
		public virtual string BudgetDescription { get; set; }

		[Required]
		[Column(Order = 2)]
		public virtual double BudgetItemAmount { get; set; }
		
		[Column(Order = 3)]
		public virtual BudgetItemCategory  BudgetItemCategory { get; set; }
	}
}