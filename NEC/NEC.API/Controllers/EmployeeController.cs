//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using NEC.API.Models;
//using NEC.API.Repositories;

//namespace NEC.API.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class EmployeeController : ControllerBase
//    {
//        private readonly IRepository _repository;
//        private readonly ILogger<EmployeeController> _logger;

//        public EmployeeController(IRepository repository, ILogger<EmployeeController> logger)
//        {
//            _logger = logger;
//            _repository = repository;
//        }

//        [HttpPost("Create")]
//        public async Task<IActionResult> Create(Employee employee) 
//        {
//            if (!ModelState.IsValid)
//            {
//                return  BadRequest("Model Data Is Invalid");
//            }

//            var result = _repository.Create(employee);
//            return Ok(result);
//        }

//        [HttpGet("GetAll")]
//        public IActionResult GetAll() 
//        {
//            var result = _repository.GetAll();
//            return  Ok(result);
//        }

//        [HttpGet("GetById")]
//        public IActionResult GetById(int id)
//        {
//            var result = _repository.GetById(id);
//            return Ok(result);
//        }

//        [HttpPut("Update")]
//        public IActionResult Update(Employee employee)
//        {
//            var result = _repository.Update(employee);
//            return Ok(result);
//        }

//        [HttpDelete("Delete")]
//        public IActionResult Delete(int id) 
//        {
//            var result = _repository.Delete(id);
//            return Ok(result);
//        }
//    }
//}


//------------------------------------------------------------------------------------------------------------------------------------------
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NEC.API.Models;
using NEC.API.Repositories;

namespace NEC.API.Controllers
{
    /// <summary>
    /// API Controller for managing Employee data.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IRepository _repository;
        private readonly ILogger<EmployeeController> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="EmployeeController"/> class.
        /// </summary>
        /// <param name="repository">The repository for interacting with employee data.</param>
        /// <param name="logger">The logger for logging events.</param>
        public EmployeeController(IRepository repository, ILogger<EmployeeController> logger)
        {
            _logger = logger;
            _repository = repository;
        }

        /// <summary>
        /// Creates a new employee record.
        /// </summary>
        /// <param name="employee">The employee object to create.</param>
        /// <returns>An IActionResult indicating the result of the operation.</returns>
        [HttpPost("Create")]
        public async Task<IActionResult> Create(Employee employee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Model Data Is Invalid");
            }

            var result = await _repository.CreateAsync(employee);
            _logger.LogInformation("New Employee Created");
            return Ok(result);
        }

        /// <summary>
        /// Retrieves a list of all employees.
        /// </summary>
        /// <returns>An IActionResult containing a list of employees.</returns>
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _repository.GetAllAsync();
            _logger.LogInformation("Retrieved All Employees");
            return Ok(result);
        }

        /// <summary>
        /// Retrieves an employee by ID.
        /// </summary>
        /// <param name="id">The ID of the employee to retrieve.</param>
        /// <returns>An IActionResult containing the employee object.</returns>
        [HttpGet("GetById")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _repository.GetByIdAsync(id);
            _logger.LogInformation($"Retrieved {id} no Employee");
            return Ok(result);
        }

        /// <summary>
        /// Updates an existing employee record.
        /// </summary>
        /// <param name="employee">The updated employee object.</param>
        /// <returns>An IActionResult indicating the result of the operation.</returns>
        [HttpPut("Update")]
        public async Task<IActionResult> Update(Employee employee)
        {
            var result = await _repository.UpdateAsync(employee);
            _logger.LogInformation($"Updated: {employee.id_employee_key} no Employee ");
            return Ok(result);
        }

        /// <summary>
        /// Deletes an employee by ID.
        /// </summary>
        /// <param name="id">The ID of the employee to delete.</param>
        /// <returns>An IActionResult indicating the result of the operation.</returns>
        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _repository.DeleteAsync(id);
            _logger.LogInformation($"Deleted: {id} no Employee");
            return Ok(result);
        }
    }
}