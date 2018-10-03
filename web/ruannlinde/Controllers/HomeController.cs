namespace RL.Controllers {
    using System.Web.Mvc;

    // [Authorize]
    public class HomeController : Controller {

        //private IList<EncoderDevice> AvailableAudioDevices { get; set; }
        //private IList<EncoderDevice> AvailableVideoDevices { get; set; }
            
        public ActionResult Index() {
            //AvailableAudioDevices = EncoderDevices.FindDevices(EncoderDeviceType.Audio);
            //AvailableVideoDevices = EncoderDevices.FindDevices(EncoderDeviceType.Video);
            
            return View();
        }
    }
}