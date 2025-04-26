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
    }
}
