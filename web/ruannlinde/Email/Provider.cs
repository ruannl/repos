using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Pop3;

namespace Ruann.Linde.Email {
	public class Provider {
		public Provider() {
			var pop3Client = new Pop3Client( );
			pop3Client.Connect("mail.ruannlinde.co.za", "info@ruannlinde.co.za", "K-7+kfHi84Qm", false);


		}
	}
}