using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ruann.Linde.Database.Models {

	public class Statement {
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[Column(Order = 0)]
		public virtual int StatementId { get; set; }

		[Required]
		[Column(Order = 1)]
		[DataType(DataType.Text)]
		public virtual string FileName { get; set; }

		[Required] [Column(Order = 2)]
		public virtual int FileSize { get; set; }

		[Column(Order = 3)]
		public virtual int ItemCount { get; set; }

		[Column(Order = 4)]
		public virtual decimal Accuracy { get; set; }

		[Column(Order = 5)]
		[Required]
		public virtual DateTime UploadDate { get; set; }

		[Column(Order = 6)]
		public virtual bool Processed { get; set; }

		[Column(Order = 7)]
		public virtual ICollection<Transaction> Transactions { get; set; }
		
		[Column(Order = 8)]
		public virtual BankAccount  BankAccount { get; set; }
	}
}