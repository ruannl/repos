using System;
using System.Threading.Tasks;
using Microsoft.Owin.Security.OAuth;

namespace Ruann.Linde.Providers {
	public class ApplicationOAuthProvider : OAuthAuthorizationServerProvider {
		private readonly string _publicClientId;

		public ApplicationOAuthProvider(string publicClientId) {
			this._publicClientId = publicClientId ?? throw new ArgumentNullException("publicClientId");
		}

		public override Task ValidateClientRedirectUri(OAuthValidateClientRedirectUriContext context) {
			if (context.ClientId == this._publicClientId) {
				var expectedRootUri = new Uri(context.Request.Uri, "/");

				if (expectedRootUri.AbsoluteUri == context.RedirectUri) {
					context.Validated();
				}
				else if (context.ClientId == "web") {
					var expectedUri = new Uri(context.Request.Uri, "/");
					context.Validated(expectedUri.AbsoluteUri);
				}
			}

			return Task.FromResult<object>(null);
		}
	}
}