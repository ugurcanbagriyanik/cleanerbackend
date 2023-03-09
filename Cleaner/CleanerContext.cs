
using Cleaner.Entities;
using Microsoft.EntityFrameworkCore;
using SharedLibrary.Entities;

namespace Cleaner
{
    public class CleanerContext : DbContext
    {
        public CleanerContext(DbContextOptions<CleanerContext> options) : base(options) { }

        public CleanerContext()
        {
            Database.Migrate();
        }


        public DbSet<Player> Player { get; set; }
        public DbSet<GeneratableBodyPart> GeneratableBodyPart { get; set; }
        public DbSet<GeneratableCleaner> GeneratableCleaner { get; set; }
        public DbSet<PlayerBodyPart> PlayerBodyPart { get; set; }
        public DbSet<PlayerCleaner> PlayerCleaner { get; set; }
        public DbSet<PlayerWarMachinePart> PlayerWarMachinePart { get; set; }
        public DbSet<PlayerWarMachine> PlayerWarMachine { get; set; }


        
        public DbSet<Log> Log { get; set; }
        public DbSet<LogAction> LogAction { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Player>().HasData(
                new Player() { Id = 1, Username = "ugur", Email = "ugurcan.bagriyanik@ndgstudio.com.tr", PasswordHash= "5994471abb01112afcc18159f6cc74b4f511b99806da59b3caf5a9c173cacfc5",IsActive=true,IsAndroid=true,LastSeen=DateTime.Now,MobileUserId="dummyMobileUserId1" }
                );

        }
    }
}
