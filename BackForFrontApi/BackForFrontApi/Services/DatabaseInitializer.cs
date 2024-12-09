using BackForFrontApi.Data_Access;
using Microsoft.EntityFrameworkCore;

namespace BackForFrontApi.Services
{
    public static class DatabaseInitializer
    {
        public static void SeedDatabase(this IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<HouseDbContext>();

            if (context.Database.IsInMemory())
            {
                //SeedData.Seed();
            }
        }
    }
}
