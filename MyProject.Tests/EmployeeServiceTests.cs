using Moq;
using MyProject.Repositories;
using MyProject.Services;

namespace MyProject.Test;

public class EmployeeServiceTests
{
    [Fact]
    public async Task InsertAsync_ValidInput_ReturnsSuccessResult()
    {
        // Arrange
        var employeeEntities = new List<Models.Employee>();

        var unitOfWorkMock = new Mock<IUnitOfWork>();
        var employeeRepositoryMock = new Mock<IEmployeeRepository>();
        unitOfWorkMock.Setup(u => u.Employees).Returns(employeeRepositoryMock.Object);

        var employeeService = new EmployeeService(unitOfWorkMock.Object);

        // Act
        var result = await employeeService.InsertAsync(employeeEntities);

        // Assert
        Assert.True(result.IsSuccess);
    }


    [Fact]
    public void GetAll_ValidInput_ReturnsListOfEmployees()
    {
        // Arrange
        var employeeEntities = new List<Entities.Employee>()
        { new Entities.Employee {
             Id = 1,
             Payroll_Number = "number12",
             Forenames = "aziz",
             Surname = "niyatov",
             Date_of_Birth = "1/1/2000",
             Telephone = 12313,
             Mobile = 12313,
             Address = "uzb",
             Address_2 = "uzb",
             Postcode = "postcode",
             EMail_Home = "EMail_Home",
             Start_Date = "1/1/2015"
        },
        new Entities.Employee {
             Id = 2,
             Payroll_Number = "number122",
             Forenames = "aziz2",
             Surname = "niyatov2",
             Date_of_Birth = "1/1/2001",
             Telephone = 123132,
             Mobile = 123132,
             Address = "uzb2",
             Address_2 = "uzb2",
             Postcode = "postcode2",
             EMail_Home = "EMail_Home2",
             Start_Date = "1/1/2016"
        }
        };


        var unitOfWorkMock = new Mock<IUnitOfWork>();
        var employeeRepositoryMock = new Mock<IEmployeeRepository>();
        unitOfWorkMock.Setup(u => u.Employees).Returns(employeeRepositoryMock.Object);
        employeeRepositoryMock.Setup(r => r.GetAll()).Returns(employeeEntities);

        var employeeService = new EmployeeService(unitOfWorkMock.Object);

        // Act
        var result = employeeService.GetAll();

        // Assert
        Assert.NotNull(result.Data);
        Assert.IsType<List<Models.Employee>>(result.Data);
        Assert.Equal(employeeEntities[0].Surname, result.Data[0].Surname);
    }



    [Fact]
    public async Task UpdateAsync_ValidInput_ReturnsSuccessResult()
    {
        // Arrange

        int id = 1;

        var employeeEntity =
         new Entities.Employee
         {
             Id = 1,
             Payroll_Number = "number12",
             Forenames = "aziz",
             Surname = "niyatov",
             Date_of_Birth = "1/1/2000",
             Telephone = 12313,
             Mobile = 12313,
             Address = "uzb",
             Address_2 = "uzb",
             Postcode = "postcode",
             EMail_Home = "EMail_Home",
             Start_Date = "1/1/2015"
         };

        var updateEmployee =
         new Models.UpdateEmployee
         {
             Payroll_Number = "number122",
             Forenames = "aziz2",
             Surname = "niyatov2",
             Date_of_Birth = "1/1/2001",
             Telephone = 123132,
             Mobile = 123132,
             Address = "uzb2",
             Address_2 = "uzb2",
             Postcode = "postcode2",
             EMail_Home = "EMail_Home2",
             Start_Date = "1/1/2016"
         };

        var unitOfWorkMock = new Mock<IUnitOfWork>();
        var employeeRepositoryMock = new Mock<IEmployeeRepository>();
        unitOfWorkMock.Setup(u => u.Employees).Returns(employeeRepositoryMock.Object);
        employeeRepositoryMock.Setup(r => r.GetByIdAsync(id)).ReturnsAsync(employeeEntity);

        var employeeService = new EmployeeService(unitOfWorkMock.Object);

        // Act
        var result = await employeeService.UpdateAsync(id, updateEmployee);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(updateEmployee.Forenames, employeeEntity.Forenames);
        Assert.Equal(updateEmployee.Start_Date, employeeEntity.Start_Date);
        Assert.Equal(updateEmployee.Postcode, employeeEntity.Postcode);
    }

}
