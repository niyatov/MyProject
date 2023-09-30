using Mapster;
using Microsoft.AspNetCore.Http;
using Moq;
using MyProject.Dtoes;
using MyProject.Repositories;
using MyProject.Services;

namespace MyProject.Test;

public class EmployeeServiceTests
{
    [Fact]
    public async Task InsertAsync_ValidInput_ReturnsSuccessResult()
    {
        var employeeEntities = new List<Employee>(); // Provide valid employees here

        // Arrange
        var unitOfWorkMock = new Mock<IUnitOfWork>();
        var employeeRepositoryMock = new Mock<EmployeeRepository>();

        var employeeService = new EmployeeService(unitOfWorkMock.Object);


        // Act
        var result = await employeeService.InsertAsync(employeeEntities.Adapt<List<Models.Employee>>());

        // Assert
        Assert.True(result.IsSuccess);
    }


    /* [Fact]
     public void GetAll_ValidInput_ReturnsListOfEmployees()
     {

     var unitOfWorkMock = new Mock<IUnitOfWork>();
         var employeeRepositoryMock = new Mock<EmployeeRepository>();
         unitOfWorkMock.Setup(u => u.Employees).Returns(employeeRepositoryMock.Object);
         employeeRepositoryMock.Setup(r => r.GetAll()).Returns(employeeEntities);

         var employeeService = new EmployeeService(unitOfWorkMock.Object);

         // Arrange
         var unitOfWorkMock = new Mock<IUnitOfWork>();
         // Set up mock to return a list of employees when GetAll is called
         unitOfWorkMock.Setup(u => u.Employees.GetAll()).Returns(new List<Entities.Employee>());

         var employeeService = new EmployeeService(unitOfWorkMock.Object);

         // Act
         var result = employeeService.GetAll();

         // Assert
         Assert.NotNull(result.Data);
         Assert.IsType<List<Employee>>(result.Data);
     }



     [Fact]
     public async Task ReadFileAsync_ValidFile_ReturnsSuccessResultWithData()
     {
         // Arrange
         var unitOfWorkMock = new Mock<IUnitOfWork>();
         var employeeService = new EmployeeService(unitOfWorkMock.Object);

         var fileMock = new Mock<IFormFile>();
         // Set up mock to simulate a valid CSV file

         // Act
         var result = await employeeService.ReadFileAsync(fileMock.Object);

         // Assert
         Assert.True(result.IsSuccess);
         Assert.NotNull(result.Data);
         Assert.IsType<List<Employee>>(result.Data);
     }


     [Fact]
     public async Task UpdateAsync_ValidInput_ReturnsSuccessResult()
     {
         // Arrange
         var unitOfWorkMock = new Mock<IUnitOfWork>();
         var employeeService = new EmployeeService(unitOfWorkMock.Object);

         var updateEmployee = new UpdateEmployee(); // Provide valid update employee object

         // Act
         var result = await employeeService.UpdateAsync(1, updateEmployee.Adapt<Models.UpdateEmployee>());

         // Assert
         Assert.True(result.IsSuccess);
     }*/

}
