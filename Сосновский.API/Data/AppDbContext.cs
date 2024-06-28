using Air.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Сосновский.API.Data
{
    public class AppDbContext: DbContext {
        public DbSet<Airplane> Airplanes { get; set; }
        public DbSet<Category> Categories { get; set; }


        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    }
}
