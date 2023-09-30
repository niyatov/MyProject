using Mapster; // Using Mapster for object mapping
using Microsoft.EntityFrameworkCore; // Using Entity Framework Core for database operations
using MyProject.Models; // Using models for data representation
using MyProject.Repositories; // Using repositories for data access
using System.Globalization; // Using for culture-specific information

namespace MyProject.Services;

public class EmployeeService : IEmployeeService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly string error = "An error occurred, please try again later";

    // Constructor to initialize EmployeeService with a Unit of Work
    public EmployeeService(IUnitOfWork unitOfWork) 
    {
        _unitOfWork = unitOfWork;
    }

    // Method to insert a list of employees
    public async ValueTask<Result> InsertAsync(List<Employee> employees)
    {
        try
        {
            await _unitOfWork.Employees.AddRangeAsync(employees.Adapt<List<Entities.Employee>>());

            return new(true) { };
        }
        catch (DbUpdateException e)
        {
            return new(false) { ErrorMessage = error, ErrorDetails = $"The data could not be added to the database. The error is related to the database. {e}" };
        }
        catch (Exception e)
        {
            return new(false) { ErrorMessage = error, ErrorDetails = $"The data was not added to the database. {e}" };
        }
    }

    // Method to get all employees
    public Result<List<Employee>> GetAll()
    {
        try
        {
            var entity = _unitOfWork.Employees.GetAll();

            var result = entity.Adapt<List<Employee>>();

            return new(true) { Data = result };
        }
        catch (DbUpdateException e)
        {
            return new(false) { ErrorMessage = error, ErrorDetails = $"The data was not retrieved from the database. The error is related to the database. {e}" };
        }
        catch (Exception e)
        {
            return new(false) { ErrorMessage = error, ErrorDetails = $"The data was not retrieved from the database. {e}" };
        }
    }

    // Method to read employee data from a file
    public async ValueTask<Result<List<Employee>>> ReadFileAsync(IFormFile? file)
    {
        try
        {
            if (file == null || file.Length == 0)
                return new(false) { ErrorMessage = "No file selected!" };

            string extension = Path.GetExtension(file.FileName);

            if (extension != ".csv")
                return new(false) { ErrorMessage = "Only CSV files can be uploaded!" };

            List<Employee> employees = new List<Employee>();

            using (var reader = new StreamReader(file.OpenReadStream()))
            {
                int i = 0;
                while (!reader.EndOfStream)
                {
                    i++;

                    var line = await reader.ReadLineAsync();
                    var values = line.Split(',');

                    if (i is 1) continue;

                    try
                    {
                        DateOnly.ParseExact(values[3], "d/M/yyyy", CultureInfo.InvariantCulture);
                        DateOnly.ParseExact(values[10], "d/M/yyyy", CultureInfo.InvariantCulture);
                    }
                    catch (Exception e)
                    {
                        return new(false) { ErrorMessage = "Time was given incorrect for Date_of_Birth or/and Start_Date. Example: 1/1/2000 " };
                    }


                    var employee = new Employee
                    {
                        Payroll_Number = values[0],
                        Forenames = values[1],
                        Surname= values[2],
                        Date_of_Birth = values[3],                           
                        Telephone = int.Parse(values[4]),
                        Mobile = int.Parse(values[5]),
                        Address = values[6],
                        Address_2 = values[7],
                        Postcode = values[8],
                        EMail_Home = values[9],
                        Start_Date = values[10]
                    };


                    employees.Add(employee);
                }
            }

            return new(true) { Data = employees };
        }
        catch (DbUpdateException e)
        {
            return new(false) { ErrorMessage = error, ErrorDetails = $"Could not read information from the file. The error is related to the database. {e}" };
        }
        catch (Exception e)
        {
            return new(false) { ErrorMessage = error, ErrorDetails = $"Could not read data from file. {e}" };
        }
    }


    // Method to update an employee
    public async ValueTask<Result> UpdateAsync(int id, UpdateEmployee employee)
    {
        try
        {
            try
            {
                DateOnly.ParseExact(employee.Date_of_Birth, "d/M/yyyy", CultureInfo.InvariantCulture);
                DateOnly.ParseExact(employee.Start_Date, "d/M/yyyy", CultureInfo.InvariantCulture);
            }
            catch (Exception e)
            {
                return new(false) { ErrorMessage = "Time was given incorrect for Date_of_Birth or/and Start_Date. Example: 1/1/2000 " };
            }
            var entity = await _unitOfWork.Employees.GetByIdAsync(id);

            if (entity == null)
                return new(false) { ErrorMessage = "employee was not found" };

            entity.Id = id;
            entity.Surname = employee.Surname;
            entity.Address = employee.Address;
            entity.Address_2 = employee.Address_2;
            entity.Date_of_Birth = employee.Date_of_Birth;
            entity.EMail_Home = employee.EMail_Home;
            entity.Forenames = employee.Forenames;
            entity.Mobile = employee.Mobile;
            entity.Postcode = employee.Postcode;
            entity.Telephone = employee.Telephone;
            entity.Start_Date = employee.Start_Date;
            entity.Payroll_Number = employee.Payroll_Number;

            await _unitOfWork.Employees.Update(entity);
            
            return new(true) { };
        }
        catch (DbUpdateException e)
        {
            return new(false) { ErrorMessage = error, ErrorDetails = $"Could not read information from the file. The error is related to the database. {e}" };
        }
        catch (Exception e)
        {
            return new(false) { ErrorMessage = error, ErrorDetails = $"Could not read data from file. {e}" };
        }
    }
}
