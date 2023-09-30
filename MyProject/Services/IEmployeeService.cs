using MyProject.Models; // Using models for data representation

namespace MyProject.Services
{
    public interface IEmployeeService
    {
        // Method to get all employees
        Result<List<Employee>> GetAll();

        // Method to insert a list of employees
        ValueTask<Result> InsertAsync(List<Employee> employees);

        // Method to update an employee
        ValueTask<Result> UpdateAsync(int id, UpdateEmployee employee);

        // Method to read employee data from a file
        ValueTask<Result<List<Employee>>> ReadFileAsync(IFormFile? file);
    }
}
