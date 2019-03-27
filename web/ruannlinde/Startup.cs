using Microsoft.Owin;
using Owin;
using Ruann.Linde;

[assembly: OwinStartup(typeof(Startup))]

namespace Ruann.Linde {
	public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
            ConfigureNinject(app);
        }
    }
}