namespace RL.Models {
	using System.Data.Entity;
	using System.Data.Entity.Infrastructure;
	using System.Linq;

	public class MySqlInitializer : IDatabaseInitializer<ApplicationDbContext> {
		public void InitializeDatabase(ApplicationDbContext context) {
			if (!context.Database.Exists()) {
				// if database did not exist before - create it
				context.Database.Create();
			}
			else {
				var migrationHistoryTableExists = ((IObjectContextAdapter) context).ObjectContext.ExecuteStoreQuery<int>
					(string.Format("SELECT COUNT(*) FROM information_schema.tables WHERE table_schema = '{0}' AND table_name = '__MigrationHistory'", "authentication"));

				// if MigrationHistory table is not there (which is the case first time we run) - create it
				if (migrationHistoryTableExists.FirstOrDefault() == 0) {
					context.Database.Delete();
					context.Database.Create();
				}
			}
		}
	}
}