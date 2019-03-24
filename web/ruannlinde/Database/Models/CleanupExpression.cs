using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RL.Database.Models {
	public class CleanupExpression {

		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[Column(Order = 0)]
		public int CleanupExpressionId { get; set; }

		[Required]
		[Column(Order = 1)]
		[DataType(DataType.Text)]
		public string Expression { get; set; }
	}
}