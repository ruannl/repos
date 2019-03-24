using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RL.Database.Models {
	public class BudgetItemCategory {
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[Column(Order = 0)]
		public virtual int BudgetItemCategoryId { get; set; }

		[Required]
		[Column(Order = 1)]
		public virtual string BudgetItemCategoryName { get; set; }

		[Column(Order = 2)]
		public virtual ICollection<BudgetItem> BudgetItems { get; set; }
	}
}