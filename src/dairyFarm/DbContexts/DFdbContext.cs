using dairyFarm.Constants;
using dairyFarm.Entity;
using dairyFarm.TypeConfig;
using Microsoft.EntityFrameworkCore;

namespace dairyFarm.DbContexts
{
    public class DFdbContext : DbContext
    {
        public virtual DbSet<Product> Products { get; set; } = null!;
        public virtual DbSet<Login> Login { get; set; } = null!;

        public DFdbContext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new ProductEntityTypeConfiguration(nameof(Products), DatabaseConstants.DfSchema));
            modelBuilder.ApplyConfiguration(new LoginEntityTypeConfiguration(nameof(Login), DatabaseConstants.DfSchema));
        }
    }
}
