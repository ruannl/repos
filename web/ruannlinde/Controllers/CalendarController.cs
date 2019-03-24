using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Auth.OAuth2.Responses;
using Google.Apis.Calendar.v3;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using log4net;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using RL.Helpers;
using RL.Models;
using StackifyLib;

namespace RL.Controllers {
    [Authorize]
    public class CalendarController : Controller {
        private static readonly ILog Log = LogManager.GetLogger(typeof(CalendarController).Name);
        private readonly IDataStore _dataStore = new FileDataStore(GoogleWebAuthorizationBroker.Folder);

        private ApplicationUserManager _userManager;
        private ApplicationUserManager UserManager { get => _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>(); set => _userManager = value; }
        private ApplicationUser user;

        public CalendarController() {
            try { }
            catch (Exception e) {
                Log.Error(e.Message);
            }
        }

        private async Task<UserCredential> GetCredentialForApiAsync() {
            try {
                var initializer = new GoogleAuthorizationCodeFlow.Initializer {
                    ClientSecrets = new ClientSecrets {
                        ClientId = MyGoogleClientSecrets.ClientId
                      , ClientSecret = MyGoogleClientSecrets.ClientSecret
                    }
                  , Scopes = MyRequestedScopes.Scopes
                };

                var flow = new GoogleAuthorizationCodeFlow(initializer);
                var identity = await HttpContext.GetOwinContext().Authentication.GetExternalIdentityAsync(DefaultAuthenticationTypes.ApplicationCookie);
                var userId = identity.FindFirstValue(MyGoogleClaimTypes.GoogleUserId);
                var token = await _dataStore.GetAsync<TokenResponse>(userId);

                Log.Debug("userId", userId);
                Log.Debug("token", token);

                return new UserCredential(flow, userId, token);
            }
            catch (Exception e) {
                Log.Error(e.Message);
                return null;
            }
        }

        public async Task<ActionResult> Index() {
            user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            Log.Debug("user", user.Email);

            return View();
        }

        public async Task<ActionResult> UpcomingEvents() {
            var model = new UpcomingEventsViewModel();

            try {
                const int MaxEventsPerCalendar = 20;
                const int MaxEventsOverall = 50;

                var credential = await GetCredentialForApiAsync();
                
                Log.Debug("credential", credential);

                var initializer = new BaseClientService.Initializer() {
                    HttpClientInitializer = credential
                  , ApplicationName = "ASP.NET MVC5 Calendar Sample"
                  , ApiKey = "ruannlinde-web"
                };
                var service = new CalendarService(initializer);

                // Fetch the list of calendars.
                var calendars = await service.CalendarList.List().ExecuteAsync();

                // Fetch some events from each calendar.
                var fetchTasks = new List<Task<Google.Apis.Calendar.v3.Data.Events>>(calendars.Items.Count);

                foreach (var calendar in calendars.Items) {
                    var request = service.Events.List(calendar.Id);
                    request.MaxResults = MaxEventsPerCalendar;
                    request.SingleEvents = true;
                    request.TimeMin = DateTime.Now;
                    fetchTasks.Add(request.ExecuteAsync());
                }

                var fetchResults = await Task.WhenAll(fetchTasks);

                // Sort the events and put them in the model.
                var upcomingEvents = from result in fetchResults
                                     from evt in result.Items
                                     where evt.Start != null
                                     let date = evt.Start.DateTime.HasValue ? evt.Start.DateTime.Value.Date : DateTime.ParseExact(evt.Start.Date, "yyyy-MM-dd", null)
                                     let sortKey = evt.Start.DateTimeRaw ?? evt.Start.Date
                                     orderby sortKey
                                     select new {
                                         evt
                                       , date
                                     };
                var eventsByDate = from result in upcomingEvents.Take(MaxEventsOverall)
                                   group result.evt by result.date
                                   into g
                                   orderby g.Key
                                   select g;

                var eventGroups = new List<CalendarEventGroup>();
                foreach (var grouping in eventsByDate) {
                    eventGroups.Add(new CalendarEventGroup {
                        GroupTitle = grouping.Key.ToLongDateString()
                      , Events = grouping
                    });
                }

                model.EventGroups = eventGroups;

                return View(model);
            }
            catch (Exception e) {
                Log.Error(e);
            }

            return View(model);
        }
    }
}