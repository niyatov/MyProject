using MyProject.Data; // Using the data context for database operations

namespace MyProject.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        private IEmployeeRepository? employeeRepository;

        // Property to access the Employee repository
        public IEmployeeRepository Employees
        {
            get
            {
                if (employeeRepository is null) employeeRepository = new EmployeeRepository(_context);
                return employeeRepository;
            }
        }

        // Constructor to initialize UnitOfWork with a data context
        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        // Method to save changes asynchronously
        public async ValueTask SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
