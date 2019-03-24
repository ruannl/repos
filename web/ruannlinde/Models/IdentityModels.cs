using System.Configuration;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace RL.Models {
	// You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
	public class ApplicationUser : IdentityUser {
		public string Hometown { get; set; }

		public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager) {
			// Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
			ClaimsIdentity userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);

			// Add custom user claims here
			return userIdentity;
		}
	}

	
	public class ApplicationDbContext : IdentityDbContext<ApplicationUser> {
		public ApplicationDbContext() : base("DefaultConnection", false) {
			Configuration.ProxyCreationEnabled = false;
			Configuration.LazyLoadingEnabled = false;

			Database.SetInitializer(new MySqlInitializer());
		}

		public static ApplicationDbContext Create() {
			return new ApplicationDbContext();
		}

		protected override void OnModelCreating(DbModelBuilder modelBuilder) {
			base.OnModelCreating(modelBuilder);

			modelBuilder
				.Entity<IdentityRole>()
				.Property(p => p.Name)
				.HasMaxLength(255);

			modelBuilder
				.Entity<IdentityUser>()
				.Property(p => p.UserName)
				.HasMaxLength(256);

			modelBuilder
				.Properties()
				.Where(p => p.PropertyType == typeof(string) &&
				            !p.Name.Contains("Id") &&
				            !p.Name.Contains("Provider"))
				.Configure(p => p.HasMaxLength(255));
		}
	}
}