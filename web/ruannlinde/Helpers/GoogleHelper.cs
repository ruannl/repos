using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Claims;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Responses;
using Google.Apis.Calendar.v3;
using Google.Apis.Util.Store;
using log4net;
using Microsoft.Owin.Security.Google;

namespace RL.Helpers {
    public class GoogleHelper {
        public const string GoogleClientId = "602348746582-7u8gv0lnkqdhfub2ap63qv48gcvpvcsb.apps.googleusercontent.com";
        public const string GoogleClientSecret = "AG_lH_eeg9O-F6sJUf_YpVY-";

        public static List<string> Scopes = new List<string> {
            "profile"
          , "email"
          , "openid"
          , CalendarService.Scope.CalendarReadonly
        };

        private static readonly ILog Log = LogManager.GetLogger(typeof(GoogleHelper).Name);
        public static readonly IDataStore DataStore = new FileDataStore(GoogleWebAuthorizationBroker.Folder);
        private static string BinLocation => Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "bin");

        public static ClientSecrets ClientSecrets() {
            var fileStream = new FileStream($"{BinLocation}\\google_client_secret.json", FileMode.Open, FileAccess.Read);
            return GoogleClientSecrets.Load(fileStream).Secrets;
        }

        public static GoogleOAuth2AuthenticationOptions OAuth2AuthenticationOptions {
            get {
                var authOptions = new GoogleOAuth2AuthenticationOptions {
                    ClientId = GoogleClientId
                  , ClientSecret = GoogleClientSecret
                  , AccessType = "offline" // Request a refresh token.
                  , Provider = new GoogleOAuth2AuthenticationProvider {
                        OnAuthenticated = async context => {
                            var userId = context.Id;
                            context.Identity.AddClaim(new Claim(MyGoogleClaimTypes.GoogleUserId, userId));

                            if (context.ExpiresIn != null) {
                                var tokenResponse = new TokenResponse {
                                    AccessToken = context.AccessToken
                                  , RefreshToken = context.RefreshToken
                                  , ExpiresInSeconds = (long) context.ExpiresIn.Value.TotalSeconds
                                  , IssuedUtc = DateTime.Now.ToUniversalTime()
                                };

                                await DataStore.StoreAsync(userId, tokenResponse);
                            }
                        }
                    }
                };

                Scopes.ForEach(item => authOptions.Scope.Add(item));

                return authOptions;
            }
        }
    }

    
}