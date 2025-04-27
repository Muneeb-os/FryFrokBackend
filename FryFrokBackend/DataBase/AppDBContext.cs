using FryFrokBackend.Model;
using Microsoft.EntityFrameworkCore;

namespace FryFrokBackend.DataBase
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {

        }
        public DbSet<RegisterUser> Register { get; set; }
        public DbSet<ProductsDetail> Product { get; set; }
        public DbSet<AddCart> Orders {  get; set; }

    }
}
