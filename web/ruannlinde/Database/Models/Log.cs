using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RL.Database.Models {
	public class Log {
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[Column(Order = 0)]
		public virtual int Id { get; set; }

		[Required]
		[Column(Order = 1)]
		[DataType(DataType.DateTime)]
		public virtual DateTime Date { get; set; }

		[Required]
		[Column(Order = 2)]
		[DataType(DataType.Text)]
		public virtual string Thread { get; set; }

		[Required]
		[Column(Order = 3)]
		[DataType(DataType.Text)]
		public virtual string Level { get; set; }

		[Required]
		[Column(Order = 4)]
		[DataType(DataType.Text)]
		public virtual string Logger { get; set; }

		[Required]
		[Column(Order = 5)]
		[DataType(DataType.Text)]
		public virtual string Message { get; set; }

		[Required]
		[Column(Order = 6)]
		[DataType(DataType.Text)]
		public virtual string Exception { get; set; }
	}
}