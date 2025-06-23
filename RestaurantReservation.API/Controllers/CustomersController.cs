using Microsoft.AspNetCore.Mvc;
using System;
using RestaurantReservation.Db.Data;
using Microsoft.EntityFrameworkCore;
using RestaurantReservation.Db.Models.Entities;
using AutoMapper;
using RestaurantReservation.Db.Interfaces;
using RestaurantReservation.API.Models.Customers;
using RestaurantReservation.Db.Repositories;
using Microsoft.AspNetCore.Authorization;

namespace RestaurantReservation.API.Controllers
{
    [Route("api/Customers")]
    [ApiController]
    [Authorize]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public CustomersController(ICustomerRepository customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }
        /// <summary>
        /// Retrieves all customers.
        /// </summary>
        /// <returns>List of customers</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<CustomerDto>>> GetAllCustomers()
        {
          var customers= await _customerRepository.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<CustomerDto>>(customers));

        }
        /// <summary>
        /// Retrieves a specific customer by their ID.
        /// </summary>
        /// <param name="id">Customer ID</param>
        /// <returns>Customer details</returns>
        /// <response code="200">Returns the customer</response>
        /// <response code="404">If customer is not found</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CustomerDto>> GetCustomerById(int id)
        {
            var customer = await _customerRepository.GetByIdAsync(id);
            if (customer == null) return NotFound();
            return Ok(_mapper.Map<CustomerDto>(customer));
        }
        /// <summary>
        /// Creates a new customer.
        /// </summary>
        /// <param name="CustomerCreationDto">Customer creation data</param>
        /// <returns>Created customer</returns>
        /// <response code="201">Returns the created customer</response>

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CustomerDto>> CreateCustomer(CustomerCreationDto CustomerCreationDto)
        {
            var customer = _mapper.Map<Customer>(CustomerCreationDto);
            await _customerRepository.AddAsync(customer);

            return CreatedAtAction(nameof(GetCustomerById), new { id = customer.CustomerId }, _mapper.Map<CustomerDto>(customer));


        }
        /// <summary>
        /// Updates an existing customer by ID.
        /// </summary>
        /// <param name="id">Customer ID</param>
        /// <param name="CustomerDto">Updated customer data</param>
        /// <returns>No content</returns>
        /// <response code="204">Update successful</response>
        /// <response code="404">If customer is not found</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateCustomer(int id, CustomerUpdatedDto CustomerDto)
        {
            var existingCustomer = await _customerRepository.GetByIdAsync(id);
            if (existingCustomer == null)
            {
                return NotFound();
            }
            _mapper.Map(CustomerDto, existingCustomer);
            await _customerRepository.UpdateAsync(existingCustomer);
            return NoContent();
        }

        /// <summary>
        /// Deletes a customer by ID.
        /// </summary>
        /// <param name="id">Customer ID</param>
        /// <returns>No content</returns>
        /// <response code="204">Deletion successful</response>
        /// <response code="404">If customer is not found</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            var customer = await _customerRepository.GetByIdAsync(id);
            if (customer == null) return NotFound();

            await _customerRepository.DeleteAsync(id);
            
            return NoContent();
        }
    }

}
