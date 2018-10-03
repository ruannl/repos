namespace RL.Areas.Accounting.Models {
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

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