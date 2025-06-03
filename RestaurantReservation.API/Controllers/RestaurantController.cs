using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantReservation.API.Models.Customers;
using RestaurantReservation.API.Models.Restaurants;
using RestaurantReservation.Db.Interfaces;
using RestaurantReservation.Db.Models.Entities;
using RestaurantReservation.Db.Repositories;

namespace RestaurantReservation.API.Controllers
{
    [Route("api/Restaurant")]
    [ApiController]
    [Authorize]
    public class RestaurantController:ControllerBase
    {
        private readonly IRestaurantRepository _restaurantRepository;
        private readonly IMapper _mapper;
        public RestaurantController(IRestaurantRepository restaurantRepository, IMapper mapper)
        {
            _restaurantRepository = restaurantRepository;
            _mapper = mapper;
        }
        /// <summary>
        /// Retrieves all restaurants.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<RestaurantDto>>> GetAllRestaurants()
        {
            var restaurants = await _restaurantRepository.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<RestaurantDto>>(restaurants));
        }
        /// <summary>
        /// Retrieves a restaurant by ID.
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<RestaurantDto>> GetRestaurantById(int id)
        {
            var restaurant = await _restaurantRepository.GetByIdAsync(id);
            if (restaurant == null) return NotFound();
            return Ok(_mapper.Map<RestaurantDto>(restaurant));
        }
        /// <summary>
        /// Creates a new restaurant.
        /// </summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<RestaurantDto>> CreateRestaurant(RestaurantCreationDto RestaurantCreationDto)
        {
            var restaurant = _mapper.Map<Restaurant>(RestaurantCreationDto);
            await _restaurantRepository.AddAsync(restaurant);

            return CreatedAtAction(nameof(GetRestaurantById), new { id = restaurant.RestaurantId }, _mapper.Map<RestaurantDto>(restaurant));
        }
        /// <summary>
        /// Updates an existing restaurant.
        /// </summary>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateRestaurant(int id, RestaurantUpdatedDto RestaurantUpdatedDto)
        {
            var existingrestaurant = await _restaurantRepository.GetByIdAsync(id);
            if (existingrestaurant == null)
            {
                return NotFound();
            }
            _mapper.Map(RestaurantUpdatedDto, existingrestaurant);
            await _restaurantRepository.UpdateAsync(existingrestaurant);
            return NoContent();
        }
        /// <summary>
        /// Deletes a restaurant by ID.
        /// </summary>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteRestaurant(int id)
        {
            var restaurant = await _restaurantRepository.GetByIdAsync(id);
            if (restaurant == null) return NotFound();

            await _restaurantRepository.DeleteAsync(id);

            return NoContent();
        }
    }
}
