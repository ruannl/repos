using System;
using Google.Apis.Calendar.v3;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;
using Ruann.Linde.Providers;

namespace Ruann.Linde {
	public static class MyRequestedScopes {
		public static string[] Scopes => new[] {
			"openid", "email", CalendarService.Scope.CalendarReadonly,
		};
	}

	public partial class Startup {
		//private readonly IDataStore _dataStore = new FileDataStore(GoogleWebAuthorizationBroker.Folder);

		static Startup() {
			PublicClientId = "web-oauth-client-id";

			OAuthOptions = new OAuthAuthorizationServerOptions {
				TokenEndpointPath = new PathString("/Token")
			  , AuthorizeEndpointPath = new PathString("/Account/Authorize")
			  , Provider = new ApplicationOAuthProvider(PublicClientId)
			  , AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(20)
			  , AllowInsecureHttp = true
			};
		}

		public static OAuthAuthorizationServerOptions OAuthOptions { get; }

		public static string PublicClientId { get; }

		// For more information on configuring authentication, please visit https://go.microsoft.com/fwlink/?LinkId=301864
		public void ConfigureAuth(IAppBuilder app) {
			//// Configure the db context, user manager and signin manager to use a single instance per request
			//app.CreatePerOwinContext(ApplicationDbContext.Create);
			//app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
			//app.CreatePerOwinContext<ApplicationSignInManager>(ApplicationSignInManager.Create);

			////app.CreatePerOwinContext(AccountingContext.Create);
			////app.CreatePerOwinContext<AccountingManager>(AccountingManager.Create);

			//// Enable the application to use a cookie to store information for the signed in user
			//app.UseCookieAuthentication(new CookieAuthenticationOptions {
			//    AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie
			//  , LoginPath = new PathString("/Account/Login")
			//  , Provider = new CookieAuthenticationProvider {
			//        OnValidateIdentity = SecurityStampValidator.OnValidateIdentity<ApplicationUserManager, ApplicationUser>(
			//            validateInterval: TimeSpan.FromMinutes(30)
			//          , regenerateIdentity: (manager, user) => user.GenerateUserIdentityAsync(manager))
			//    }
			//});

			//app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);
			//app.UseTwoFactorSignInCookie(DefaultAuthenticationTypes.TwoFactorCookie, TimeSpan.FromMinutes(5));
			//app.UseTwoFactorRememberBrowserCookie(DefaultAuthenticationTypes.TwoFactorRememberBrowserCookie);
			//app.UseOAuthBearerTokens(OAuthOptions);

			//var authOptions = new GoogleOAuth2AuthenticationOptions {
			//    ClientId = MyGoogleClientSecrets.ClientId
			//  , ClientSecret = MyGoogleClientSecrets.ClientSecret
			//  , AccessType = "offline" // Request a refresh token.
			//  , Provider = new GoogleOAuth2AuthenticationProvider {
			//        OnAuthenticated = async context => {
			//            var userId = context.Id;
			//            context.Identity.AddClaim(new Claim(MyGoogleClaimTypes.GoogleUserId, userId));

			//            if (context.ExpiresIn != null) {
			//                var tokenResponse = new TokenResponse {
			//                    AccessToken = context.AccessToken
			//                  , RefreshToken = context.RefreshToken
			//                  , ExpiresInSeconds = (long) context.ExpiresIn.Value.TotalSeconds
			//                  , IssuedUtc = DateTime.Now.ToUniversalTime()
			//                };

			//                await _dataStore.StoreAsync(userId, tokenResponse);
			//            }
			//        }
			//    }
			//};

			//foreach (var scope in MyRequestedScopes.Scopes) {
			//    authOptions.Scope.Add(scope);
			//}

			//app.UseGoogleAuthentication(authOptions);
			// Uncomment the following lines to enable logging in with third party login providers
			//app.UseMicrosoftAccountAuthentication(
			//    clientId: "",
			//    clientSecret: "");

			//app.UseTwitterAuthentication(
			//    consumerKey: "",
			//    consumerSecret: "");

			//app.UseFacebookAuthentication(
			//    appId: "",
			//    appSecret: "");
		}
	}
}