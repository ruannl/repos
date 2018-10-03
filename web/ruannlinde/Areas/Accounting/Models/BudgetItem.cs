using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RL.Areas.Accounting.Models {
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