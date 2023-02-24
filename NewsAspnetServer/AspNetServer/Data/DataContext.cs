using Microsoft.EntityFrameworkCore;
using AspNetServer.Models;

namespace AspNetServer.Data
{
    public class DataContext : DbContext
    {
        public DbSet<News> News { get; set; }


        public DataContext() : base()
        {

        }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
    }
}
