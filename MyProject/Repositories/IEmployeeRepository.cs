using MyProject.Entities; // Using the Employee entity

namespace MyProject.Repositories
{
    public interface IEmployeeRepository : IGenericRepository<Employee>
    {
    }
}
