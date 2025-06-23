using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantReservation.API.Models.Customers;
using RestaurantReservation.API.Models.OrderItems;
using RestaurantReservation.Db.Interfaces;
using RestaurantReservation.Db.Models.Entities;
using RestaurantReservation.Db.Repositories;

namespace RestaurantReservation.API.Controllers
{
    [Route("api/OrderItems")]
    [ApiController]
    [Authorize]
    public class OrderItemsController:ControllerBase
    {
        private readonly IOrderItemRepository _orderItemRepository;
        private readonly IMapper _mapper;

        public OrderItemsController(IOrderItemRepository orderItemRepository, IMapper mapper)
        {
            _orderItemRepository = orderItemRepository;
            _mapper = mapper;
        }
        /// <summary>
        /// Retrieves all Order Items for a specific order with pagination.
        /// </summary>
        /// <returns>A list of Order Item DTOs if available; otherwise, a 204 No Content response if no Order Items exist.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        
        public async Task<ActionResult<IEnumerable<OrderItemDto>>> GetAllOrderItems()
        {
            var OrderItems = await _orderItemRepository.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<OrderItemDto>>(OrderItems));
        }
        /// <summary>
        /// Retrieves an order item by ID.
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderItemDto>> GetOrderItemById(int id)
        {
            var OrderItems = await _orderItemRepository.GetByIdAsync(id);
            if (OrderItems == null) return NotFound();
            return Ok(_mapper.Map<OrderItemDto>(OrderItems));
        }

        /// <summary>
        /// Creates a new order item.
        /// </summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<OrderItemDto>> CreateOrderItem(OrderItemCreationDto OrderItemCreationDto)
        {

            var OrderItems = _mapper.Map<OrderItem>(OrderItemCreationDto);
            await _orderItemRepository.AddAsync(OrderItems);

            return CreatedAtAction(nameof(GetOrderItemById), new { id = OrderItems.OrderItemId }, _mapper.Map<OrderItemDto>(OrderItems));
        }
        /// <summary>
        /// Updates an existing order item.
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrderItem(int id, OrderItemUpdatedDto OrderItemUpdatedDto)
        {
            var existingOrderItem = await _orderItemRepository.GetByIdAsync(id);
            if (existingOrderItem == null)
            {
                return NotFound();
            }
            _mapper.Map(OrderItemUpdatedDto, existingOrderItem);
            await _orderItemRepository.UpdateAsync(existingOrderItem);
            return NoContent();
        }
        /// <summary>
        /// Deletes an order item by ID.
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrderItem(int id)
        {
            var OrderItem = await _orderItemRepository.GetByIdAsync(id);
            if (OrderItem == null) return NotFound();

            await _orderItemRepository.DeleteAsync(id);

            return NoContent();
        }
    }
}

