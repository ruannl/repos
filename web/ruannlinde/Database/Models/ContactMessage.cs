using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RL.Database.Models {
	public class ContactMessage {
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[Column(Order = 0)]
		public int ContactId { get; set; }

		[Column(Order = 1)]
		public string FirstName { get; set; }

		[Column(Order = 2)]
		public string LastName { get; set; }

		[Column(Order = 3)]
		[EmailAddress]
		public string Email { get; set; }

		[Column(Order = 4)]
		public string Message { get; set; }
	}
}