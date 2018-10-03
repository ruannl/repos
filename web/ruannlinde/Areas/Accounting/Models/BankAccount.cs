using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RL.Areas.Accounting.Models {
    public class BankAccount {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Column(Order = 0)]
        public int BankAccountId { get; set; }

        [Required]
        [Column(Order = 1)]
        public string AccountName { get; set; }

        [Required]
        [Column(Order = 2)]
        public string AccountNumber { get; set; }

        [Required]
        [InverseProperty("BankId")]
        [Column(Order = 3)]
        public int? BankId { get; set; }

        [ForeignKey("BankCardId")]
        public virtual ICollection<BankCard> BankCards { get; set; }

        [ForeignKey("StatementId")]
        public virtual ICollection<Statement> Statements { get; set; }
    }
}