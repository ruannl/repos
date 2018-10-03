using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RL.Areas.Accounting.Models {
    public class Statement {
        [Column(Order = 1)]
        public double AccountBalance { get; set; }

        [Column(Order = 2)]
        public double AvailableBalance { get; set; }
        public virtual BankAccount BankAccount { get; set; }

        [Required]
        [InverseProperty("BankAccountId")]
        [Column(Order = 3)]
        public int? BankAccountId { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        [Column(Order = 0)]
        public int StatementId { get; set; }
    }
}