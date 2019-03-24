using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RL.Database.Models {
	public class BudgetItemCategory {
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[Column(Order = 0)]
		public int BudgetItemCategoryId { get; set; }

		[Required]
		public string BudgetItemCategoryName { get; set; }

		[ForeignKey("BudgetItemId")]
		public virtual ICollection<BudgetItem> BudgetItems { get; set; }
	}
}