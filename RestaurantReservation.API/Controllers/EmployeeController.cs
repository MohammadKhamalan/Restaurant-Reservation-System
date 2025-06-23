using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantReservation.API.Models.Customers;
using RestaurantReservation.API.Models.Employees;
using RestaurantReservation.Db.Interfaces;
using RestaurantReservation.Db.Models.Entities;

namespace RestaurantReservation.API.Controllers
{
    [Route("api/Employee")]
    [ApiController]
    [Authorize]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;
        private readonly IOrderRepository _orderRepository;
        public EmployeeController(IEmployeeRepository employeeRepository, IMapper mapper, IOrderRepository orderRepository)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
            _orderRepository = orderRepository;
        }
        /// <summary>
        /// Retrieves all employees.
        /// </summary>
        /// <returns>List of employees</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<EmployeeDto>>> GetAllEmployees()
        {
            var employees = await _employeeRepository.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<EmployeeDto>>(employees));
        }
        /// <summary>
        /// Retrieves an employee by ID.
        /// </summary>
        /// <param name="id">Employee ID</param>
        /// <returns>Employee details</returns>
        /// <response code="200">Returns the employee</response>
        /// <response code="404">If employee not found</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<EmployeeDto>> GetEmployeeById(int id)
        {
            var employee = await _employeeRepository.GetByIdAsync(id);
            if (employee == null) return NotFound();
            return Ok(_mapper.Map<EmployeeDto>(employee));
        }
        /// <summary>
        /// Retrieves a list of managers.
        /// </summary>
        /// <returns>A list of employee DTOs representing managers.</returns>
        [HttpGet("managers")]
        public async Task<ActionResult<IEnumerable<EmployeeDto>>> GetManagers()
        {
            var managers= await _employeeRepository.ListManagersAsync();
            return Ok(_mapper.Map<IEnumerable<EmployeeDto>>(managers));

        }
        /// <summary>
        /// Calculates the average order amount handled by a specific employee.
        /// </summary>
        /// <param name="employeeId">The ID of the employee for whom to calculate the average order amount.</param>
        /// <returns>A 200 OK response with the average order amount; or a 404 Not Found response if no orders exist for the specified employee.</returns>
        [HttpGet("{employeeId}/average-order-amount")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> CalculateAverageOrderAmount(int employeeId)
        {
            var AvaregeOrderAmount = await _orderRepository.CalculateAverageOrderAmount(employeeId);
            if (AvaregeOrderAmount == null)
            {
                return NotFound(new { Message = "No orders found for the specified employee." });
            }

            return Ok(new { AvaregeOrderAmount });
        }

        /// <summary>
        /// Creates a new employee.
        /// </summary>
        /// <param name="employeeCreationDto">The employee creation data.</param>
        /// <returns>The created employee DTO.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
       
        public async Task<ActionResult<EmployeeDto>> CreateEmployee(EmployeeCreationDto employeeCreationDto)
        {
            var employee = _mapper.Map<Employee>(employeeCreationDto);
            await _employeeRepository.AddAsync(employee);
            return CreatedAtAction(nameof(GetEmployeeById), new { id = employee.EmployeeId }, _mapper.Map<EmployeeDto>(employee));
        }
        /// <summary>
        /// Updates an existing employee by ID.
        /// </summary>
        /// <param name="id">The ID of the employee to update.</param>
        /// <param name="employeeUpdateDto">The employee update data.</param>
        /// <returns>A 204 No Content response if successful; otherwise, a 404 Not Found response if the employee does not exist.</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateEmployee(int id, EmployeeUpdateDto EmployeeUpdateDto)
        {
            var existingEmployee = await _employeeRepository.GetByIdAsync(id);
            if (existingEmployee == null) return NotFound();
            _mapper.Map(EmployeeUpdateDto , existingEmployee);
            await _employeeRepository.UpdateAsync(existingEmployee);
            return NoContent();
        }
        /// <summary>
        /// Deletes a Employee by ID.
        /// </summary>
        /// <param name="id">The ID of the employee to delete.</param>
        /// <returns>A 204 No Content response if successful; otherwise, a 404 Not Found response if the employee does not exist.</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var existingEmployee = await _employeeRepository.GetByIdAsync(id);
            if (existingEmployee == null) return NotFound();
            await _employeeRepository.DeleteAsync(id);
            return NoContent();
        }
    }
}
