using System.Collections.Generic;
using System.Threading.Tasks;

namespace MVCAdoDemo.Models  
{
  public interface ICrudRepository
  {
  // General
    Task<bool> SaveAllAsync();
    void AddEntity(object entity);
    void Delete<T>(T entity) where T : class;

    // Employees
    IEnumerable<Employee> GetAllEmployees();
    Employee GetEmployeeById(int id);
  }
}