using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RL.Database.Models {
	public class BudgetItem {
		[Required]
		public string BudgetDescription { get; set; }

		[Required]
		public double BudgetItemAmount { get; set; }

		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[Column(Order = 0)]
		public int BudgetItemId { get; set; }

		[InverseProperty("BudgetItemCategoryId")]
		public int? BudgetItemCategoryId { get; set; }
	}
}