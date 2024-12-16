using BackForFrontApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BackForFrontApi.Data_Access
{
    public class HouseDbContext : DbContext
    {
        public HouseDbContext(DbContextOptions<HouseDbContext> options) : base(options) { }
        public DbSet<HouseEntity> Houses => Set<HouseEntity>();
        public DbSet<BidEntity> Bids => Set<BidEntity>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            SeedData.Seed(modelBuilder);
            //base.OnModelCreating(modelBuilder);

        }
    }
}
