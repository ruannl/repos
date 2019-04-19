using System.Configuration;

namespace Ruann.Linde.Extensions {
	public class Pop3ConfigurationSection : ConfigurationSection {
		public Pop3ConfigurationSection() {
			var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
			var t = config.GetSection("pop3");


		}
	}
}