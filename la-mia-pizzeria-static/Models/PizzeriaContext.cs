using Microsoft.EntityFrameworkCore;

namespace la_mia_pizzeria_static.Models
{
    public class PizzeriaContext : DbContext
    {
        public DbSet<Pizza> Pizze { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=pizzeria;Integrated Security=True;Pooling=False");
        }
    }
}
