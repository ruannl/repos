namespace RL.Controllers
{
    using System.Web;
    using System.Web.Http;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.Owin;

    using RL.Models;

    [Authorize]
    public class MeController : ApiController
    {
        private ApplicationUserManager _userManager;

        public MeController()
        {
        }

        public MeController(ApplicationUserManager userManager)
        {
            this.UserManager = userManager;
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return this._userManager ?? HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                this._userManager = value;
            }
        }

        // GET api/Me
        public GetViewModel Get()
        {
            var user = this.UserManager.FindById(this.User.Identity.GetUserId());
            return new GetViewModel() { Hometown = user.Hometown };
        }
    }
}