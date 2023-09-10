using Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Data.DbContexts
{
    /// <summary>
    /// This class is used to generate the migration files/scripts, basing the config on the webapi appsettings
    /// </summary>
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            // Get environment	
            string environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            var appSettingsPath = Path.Combine(Directory.GetCurrentDirectory());
            Console.WriteLine($"environment: {environment}");
            Console.WriteLine($"appSettingsPath: {appSettingsPath}");

            // Build config	
            IConfiguration config = new ConfigurationBuilder()
                .SetBasePath(appSettingsPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{environment}.json", optional: true)
                .AddEnvironmentVariables()
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

            return new AppDbContext(optionsBuilder.Options, config);
        }
    }

    public class AppDbContext : DbContext
    {
        private readonly IConfiguration _configuration;
        private string _connectionString;

        public DbSet<Transaction> Transactions { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options, IConfiguration configuration)
            : base(options)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));

            _connectionString = _configuration.GetConnectionString("WebApiDatabase");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLoggerFactory(_loggerFactory);
#if DEBUG
            optionsBuilder.EnableSensitiveDataLogging();
#endif

            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite(_connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new TransactionConfiguration());
        }

        public static readonly ILoggerFactory _loggerFactory = LoggerFactory.Create(builder =>
        {
            
            //builder.AddConsole();
        });
    }
}
