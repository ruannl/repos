using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RL.Database.Models {
	public class Log {
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[Column(Order = 0)]
		public int Id { get; set; }

		[Required]
		[Column(Order = 1)]
		[DataType(DataType.DateTime)]
		public DateTime Date { get; set; }

		[Required]
		[Column(Order = 2)]
		[DataType(DataType.Text)]
		public string Thread { get; set; }

		[Required]
		[Column(Order = 3)]
		[DataType(DataType.Text)]
		public string Level { get; set; }

		[Required]
		[Column(Order = 4)]
		[DataType(DataType.Text)]
		public string Logger { get; set; }

		[Required]
		[Column(Order = 5)]
		[DataType(DataType.Text)]
		public string Message { get; set; }

		[Required]
		[Column(Order = 6)]
		[DataType(DataType.Text)]
		public string Exception { get; set; }
	}
}