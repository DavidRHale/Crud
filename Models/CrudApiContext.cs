using Microsoft.EntityFrameworkCore;

namespace MVCAdoDemo.Models
{
  public class CrudApiContext : DbContext
  {
    public CrudApiContext(DbContextOptions<CrudApiContext> options) : base(options)
        {
        }

    public DbSet<Employee> Employees { get; set; }
  }
}