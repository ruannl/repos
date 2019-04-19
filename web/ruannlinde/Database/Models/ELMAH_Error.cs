using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ruann.Linde.Database.Models {
	[Table("ELMAH_Error")]
	public class ELMAH_Error {

		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[Column(Order = 0)]
		public virtual Guid ErrorId { get; set; }

		[Column(Order = 1)]
		public virtual string Application { get; set; }

		[Column(Order = 2)]
		public virtual string Host { get; set; }

		[Column(Order = 3)]
		public virtual string Type { get; set; }

		[Column(Order = 4)]
		public virtual string Source { get; set; }

		[Column(Order = 5)]
		public virtual string Message { get; set; }

		[Column(Order = 6)]
		public virtual string User { get; set; }

		[Column(Order = 7)]
		public virtual int StatusCode { get; set; }

		[Column(Order = 8)]
		[Index]
		public virtual DateTime TimeUtc { get; set; }

		[Column(Order = 9)]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public virtual int Sequence { get; set; }

		[Column(Order = 10)]
		public virtual string AllXml { get; set; }

	}
}