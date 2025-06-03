using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantReservation.API.Models.MenuItems;
using RestaurantReservation.API.Models.OrderItems;
using RestaurantReservation.API.Models.Orders;
using RestaurantReservation.API.Models.Reservations;
using RestaurantReservation.Db.Interfaces;
using RestaurantReservation.Db.Models.Entities;
using RestaurantReservation.Db.Repositories;
using RestaurantReservation.Db.Repositories.Interfaces;

namespace RestaurantReservation.API.Controllers
{
 
    [Route("api/Reservations")]
    [ApiController]
    [Authorize]
    public class ReservationController : ControllerBase
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;
        private readonly IOrderRepository _orderRepository;
        private readonly IMenuItemRepository _menuItemRepository;

        public ReservationController(IReservationRepository reservationRepository, ICustomerRepository customerRepository, IMapper mapper, IOrderRepository orderRepository, IMenuItemRepository menuItemRepository)
        {
            _reservationRepository = reservationRepository;
            _customerRepository = customerRepository;
            _mapper = mapper;
            _orderRepository = orderRepository;
            _menuItemRepository = menuItemRepository;
        }
        /// <summary>
        /// Retrieves all reservations.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<ReservationDto>>> GetAllReservations()
        {
            var Reservations = await _reservationRepository.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<ReservationDto>>(Reservations));
        }
        /// <summary>
        /// Retrieves a reservation by ID.
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ReservationDto>> GetReservationById(int id)
        {
            var Reservations = await _reservationRepository.GetByIdAsync(id);
            if (Reservations == null) return NotFound();
            return Ok(_mapper.Map<ReservationDto>(Reservations));
        }
        /// <summary>
        /// Gets all reservations for a specific customer.
        /// </summary>
        /// <param name="customerId">The ID of the customer.</param>
        /// <returns>List of reservations.</returns>
        /// <response code="200">Returns the list of reservations</response>
        /// <response code="404">If the customer is not found</response>
        [HttpGet("customer/{customerId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<ReservationDto>>> GetReservationByCustomer(int customerId)
        {
            var customer = await _customerRepository.GetByIdAsync(customerId);
            if (customer == null) return NotFound(new { Message = "Customer not found." });
            var reservation = await _reservationRepository.GetReservationsByCustomer(customerId);
            return Ok(_mapper.Map<IEnumerable<ReservationDto>>(reservation));

        }
        /// <summary>
        /// Lists orders and their menu items for a given reservation.
        /// </summary>
        [HttpGet("{reservationId}/orders")]
        public async Task<ActionResult<IEnumerable<OrderWithMenuItemsDto>>> ListOrdersAndMenuItemsAsync(int reservationId)
        {
           var orders= await _orderRepository.ListOrdersAndMenuItemsAsync(reservationId);
            if (orders == null || !orders.Any())
                return NotFound(new { Message = "No orders found for the specified reservation." });

            return Ok(_mapper.Map<IEnumerable<OrderWithMenuItemsDto>>(orders));
        }
        /// <summary>
        /// Lists menu items ordered in a reservation.
        /// </summary>
        [HttpGet("{reservationId}/menu-items")]
        public async Task<ActionResult<IEnumerable<MenuItemDto>>> ListOrderedMenuItems(int reservationId)
        {
            var menuItems = await _menuItemRepository.ListOrderedMenuItems(reservationId);
            if (menuItems == null || !menuItems.Any())
                return NotFound(new { Message = "No menu items found for the specified order." });

            return Ok(_mapper.Map<IEnumerable<MenuItemDto>>(menuItems));
        }

        /// <summary>
        /// Creates a new reservation.
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<ReservationDto>> CreateReservation(ReservationCreationDto ReservationCreationDto)
        {
            var Reservations = _mapper.Map<Reservation>(ReservationCreationDto);
            await _reservationRepository.AddAsync(Reservations);

            return CreatedAtAction(nameof(GetReservationById), new { id = Reservations.ReservationId }, _mapper.Map<ReservationDto>(Reservations));
        }
        /// <summary>
        /// Updates a reservation.
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateReservation(int id, ReservationUpdatedDto ReservationUpdatedDto)
        {
            var existingReservation = await _reservationRepository.GetByIdAsync(id);
            if (existingReservation == null)
            {
                return NotFound();
            }
            _mapper.Map(ReservationUpdatedDto, existingReservation);
            await _reservationRepository.UpdateAsync(existingReservation);
            return NoContent();
        }
        /// <summary>
        /// Deletes a reservation.
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReservation(int id)
        {
            var OrderItem = await _reservationRepository.GetByIdAsync(id);
            if (OrderItem == null) return NotFound();

            await _reservationRepository.DeleteAsync(id);

            return NoContent();
        }
}
}
