using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using NewsAPI.Models;

namespace NewsAPI.Controllers.Data
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
