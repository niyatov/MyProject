using MyProject.Data; // Using the data context for database operations
using MyProject.Entities; // Using the Employee entity

namespace MyProject.Repositories
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        // Constructor to initialize EmployeeRepository with a data context
        public EmployeeRepository(AppDbContext context) : base(context)
        {
        }
    }
}
