using Mapster; // Using Mapster for object mapping
using Microsoft.AspNetCore.Mvc; // Using MVC for handling HTTP requests
using MyProject.Dtoes; // Using DTOs for data transfer
using MyProject.Services; // Using services for business logic

namespace MyProject.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;

        // Constructor to initialize the controller with an EmployeeService
        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        // Action to get all employees
        public IActionResult GetAll()
        {
            var entity = _employeeService.GetAll();
            var Response = entity.Data.Adapt<List<Employee>>();

            return Json(Response);
        }

        // Default action for Index view
        public IActionResult Index() => View(new List<Employee>());

        // Action to handle file upload
        [HttpPost]
        public async Task<IActionResult> Index(IFormFile? file)
        {
            // Read and process the file
            var entity = await _employeeService.ReadFileAsync(file);

            // If there's an error, add it to ModelState and return to view
            if (!entity.IsSuccess)
            {
                ModelState.AddModelError("", entity.ErrorMessage);
                return View();
            }

            var employees = entity?.Data;

            // Insert employees into database
            var entity2 = await _employeeService.InsertAsync(employees);

            // If there's an error, add it to ModelState and return to view
            if (!entity2.IsSuccess)
            {
                ModelState.AddModelError("", entity2.ErrorMessage);
                return View();
            }

            // If successful, show success message
            if (entity2.IsSuccess)
            {
                var extra = employees.Count() > 1 ? "s" : "";
                ModelState.AddModelError("", $"{employees.Count()} employee{extra} added successfully");
            }

            return View(new List<Employee>());
        }

        // Action to edit employee
        public IActionResult Edit(string id)
        {
            ViewData["id"] = id;
            return View(new UpdateEmployee());
        }

        // Action to handle editing employee
        [HttpPost]
        public async Task<IActionResult> Edit(string id, UpdateEmployee employee)
        {
            // Update employee in the database
            var entity = await _employeeService.UpdateAsync(Convert.ToInt32(id), employee.Adapt<Models.UpdateEmployee>());

            if (entity.IsSuccess)
            {
                return Json($"pass");
            }
            else
            {
                return Json(entity.ErrorMessage);
            }
        }

        // Action for custom error view
        public IActionResult CustomError() => View();
    }
}
