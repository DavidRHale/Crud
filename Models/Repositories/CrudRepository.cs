using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCAdoDemo.Models  
{
  public class CrudRepository : ICrudRepository
  {
    private readonly CrudApiContext _context;

    public CrudRepository(CrudApiContext context) 
    {
      _context = context;
    }

    // ------------------------------------------------ EmployeeS ------------------------------------------------
    public IEnumerable<Employee> GetAllEmployees()
    {
      return _context.Employees.ToList();
    }
    
    public Employee GetEmployeeById(int id)
    {
      return _context
        .Employees
        .Where(r => r.Id == id)
        .FirstOrDefault();
    }

    // ------------------------------------------------ GENERAL ------------------------------------------------
    public void AddEntity(object entity)
    {
      _context.Add(entity);
    }

    public void Delete<T>(T entity) where T : class
    {
      _context.Remove(entity);
    }

    public async Task<bool> SaveAllAsync()
    {
      return (await _context.SaveChangesAsync()) > 0;
    }
  }
}