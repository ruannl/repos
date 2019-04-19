using System;
using System.Collections.Generic;

namespace Ruann.Linde.Extensions {
	public class DomainException : Exception {

		public DomainException(string message, dynamic additionalData) : base(message) {
			AdditionalData = additionalData;
		}

		public DomainException(string message) : base(message) {
			//
		}

		public DomainException() {
			Exceptions = new List<DomainException>();
		}

		public dynamic AdditionalData { get; }
		public List<DomainException> Exceptions { get; set; }
	}
}