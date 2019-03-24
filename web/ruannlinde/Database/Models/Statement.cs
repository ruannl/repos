using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RL.Database.Models {

	public class Statement {
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[Column(Order = 0)]
		public int StatementId { get; set; }

		[Required]
		[Column(Order = 1)]
		[DataType(DataType.Text)]
		public string FileName { get; set; }

		[Required] [Column(Order = 2)] public int FileSize { get; set; }

		[Column(Order = 3)] public int ItemCount { get; set; }

		[Column(Order = 4)] public decimal Accuracy { get; set; }

		[Column(Order = 5)] [Required] public DateTime UploadDate { get; set; }

		[Column(Order = 6)] public bool Processed { get; set; }

		[Column(Order = 7)] public ICollection<Transaction> Transactions { get; set; }
		
		[Column(Order = 8)]
		public BankAccount  BankAccount { get; set; }
	}
}