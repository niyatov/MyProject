namespace MyProject.Repositories
{
    public interface IUnitOfWork
    {
        // Property to access the Employee repository
        IEmployeeRepository Employees { get; }
    }
}
