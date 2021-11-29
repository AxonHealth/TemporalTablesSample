using Microsoft.EntityFrameworkCore;

namespace TemporalTablesSample
{
    public class AxonDbContext : DbContext
    {
        public DbSet<Setting> Settings { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            #region ConnectionString
            optionsBuilder.UseSqlServer("...");
            #endregion
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Setting>()
                .ToTable("Setting", b => b.IsTemporal(b =>
                {
                    b.UseHistoryTable("SettingHistory");
                    b.HasPeriodStart("ValidFrom");
                    b.HasPeriodEnd("ValidTo");
                }))
                .HasData(new Setting { Id = 1, Key = Setting.TaxRateSettingKey, Value = 10 });
        }
    }
}
