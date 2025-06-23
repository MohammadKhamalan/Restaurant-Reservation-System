using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantReservation.API.Models.Employees;
using RestaurantReservation.API.Models.Orders;
using RestaurantReservation.Db.Interfaces;
using RestaurantReservation.Db.Models.Entities;

namespace RestaurantReservation.API.Controllers
{
    [Route("api/Orders")]
    [ApiController]
    [Authorize]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public OrderController(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }
        /// <summary>
        /// Retrieves all orders.
        /// </summary>
        /// <returns>List of orders</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetAllOrders()
        {
            var orders =await _orderRepository.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<OrderDto>>(orders));
        }
        /// <summary>
        /// Retrieves a specific order by ID.
        /// </summary>
        /// <param name="id">Order ID</param>
        /// <returns>Order details</returns>
        /// <response code="200">Returns the order</response>
        /// <response code="404">If order is not found</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<OrderDto>> GetOrderById(int id)
        {

            var order = await _orderRepository.GetByIdAsync(id);
            if (order == null) return NotFound();
            return Ok(_mapper.Map<OrderDto>(order));
        }
        /// <summary>
        /// Creates a new order.
        /// </summary>
        /// <param name="orderCreationDto">Order creation data</param>
        /// <returns>Created order</returns>
        /// <response code="201">Returns the created order</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<OrderDto>> CreateOrder(OrderCreationDto OrderCreationDto)
        {
            var orderAdded = _mapper.Map<Order>(OrderCreationDto);
             await _orderRepository.AddAsync(orderAdded);
            return CreatedAtAction(nameof(GetOrderById), new { id = orderAdded.OrderId }, _mapper.Map<OrderDto>(orderAdded));
        }
        /// <summary>
        /// Updates an existing order by ID.
        /// </summary>
        /// <param name="id">Order ID</param>
        /// <param name="orderUpdateDto">Updated order data</param>
        /// <returns>No content</returns>
        /// <response code="204">Update successful</response>
        /// <response code="404">If order is not found</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateOrder(int id, OrderUpdatedDto OrderUpdatedDto)
        {
            var existingOrder = await _orderRepository.GetByIdAsync(id);
            if (existingOrder == null) return NotFound();
             _mapper.Map(OrderUpdatedDto, existingOrder);
            await _orderRepository.UpdateAsync(existingOrder);
            return NoContent();
        }
        /// <summary>
        /// Deletes an order by ID.
        /// </summary>
        /// <param name="id">Order ID</param>
        /// <returns>No content</returns>
        /// <response code="204">Deletion successful</response>
        /// <response code="404">If order is not found</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var existingOrder = await _orderRepository.GetByIdAsync(id);
            if (existingOrder == null) return NotFound();
           
            await _orderRepository.DeleteAsync(id);
            return NoContent();
        }
    }
}
