using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RL.Areas.Accounting.Models {
    public class BankCard {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 0)]
        public int BankCardId { get; set; }

        [Required]
        [Column(Order = 1)]

        public string BankCardName { get; set; }
        [Column(Order = 2)]
        [Required]
        public string BankCardNumber { get; set; }

        [Column(Order = 3)]
        public BankAccount BankAccount { get; set; }
    }
}