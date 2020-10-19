using Microsoft.ApplicationInsights;
using Microsoft.EntityFrameworkCore;

namespace AppInsights.EfCore.Domain
{
    public class SchoolContext : DbContext
    {
        public TelemetryClient AppInsightsClient { get; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }

        public SchoolContext(TelemetryClient telemetryClient)
        {
            this.AppInsightsClient = telemetryClient;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESS;Database=SchoolDB;Trusted_Connection=True;");
            base.OnConfiguring(optionsBuilder);
        }
    }
}
