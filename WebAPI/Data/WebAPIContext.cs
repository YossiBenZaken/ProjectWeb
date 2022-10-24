using Microsoft.EntityFrameworkCore;
using WebAPI.Models;

namespace WebAPI.Data
{
    public class WebAPIContext : DbContext
    {
        public WebAPIContext(DbContextOptions<WebAPIContext> options)
            : base(options)
        {
        }
        public DbSet<Snackbar> Snackbar { get; set; }
        public DbSet<Worker> Worker { get; set; }
    }
}
