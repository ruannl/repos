using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RL.Areas.Accounting.Models {
    public class TransactionType {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 0)]
        public int TransactionTypeId { get; set; }

        [Required]
        [Column(Order = 1)]
        public string TransactionTypeName { get; set; }

        [ForeignKey("TransactionTypeId")]
        public virtual ICollection<TransactionTypeIdentifier> TransactionTypeIdentifiers { get; set; }
    }
}