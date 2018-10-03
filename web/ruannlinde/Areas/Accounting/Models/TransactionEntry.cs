using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RL.Areas.Accounting.Models {
    public class TransactionEntry {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 0)]
        public int TransactionId { get; set; }

        [Required]
        [Column(Order = 1)]
        public DateTime TransactionDate { get; set; }

        [Required]
        [Column(Order = 2)]
        public string TransactionDescription { get; set; }

        [Required]
        [Column(Order = 3)]
        public double TransactionAmount { get; set; }

        [Required]
        [Column(Order = 4)]
        public double TransactionBalance { get; set; }

        [InverseProperty("BankCardId")]
        [Column(Order = 5)]
        public int? BankCardId { get; set; }

        [InverseProperty("RetailerId")]
        [Column(Order = 6)]
        public int? RetailerId { get; set; }

        [InverseProperty("BudgetItemId")]
        [Column(Order = 7)]
        public int? BudgetItemId { get; set; }
        
        [InverseProperty("StatementId")]
        [Column(Order = 8)]
        public int? StatementId { get; set; }
        
        [InverseProperty("TransactionTypeId")]
        [Column(Order = 9)]
        public int? TransactionTypeId { get; set; }

    }
}