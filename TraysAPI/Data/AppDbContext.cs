using Microsoft.EntityFrameworkCore;

namespace TraysAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        private string BuildConnectionString()
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile(@"C:\Users\todor.chankov\source\repos\MrToKa\TraysAPI\TraysAPI\appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("DefaultConnection");

            return connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            optionsBuilder.UseNpgsql(
BuildConnectionString(),
options => options.UseAdminDatabase("postgres"));

            base.OnConfiguring(optionsBuilder);
        }

        public DbSet<Models.Cable> Cables { get; set; }
        public DbSet<Models.CableType> CableTypes { get; set; }
        public DbSet<Models.Tray> Trays { get; set; }
    }
}
