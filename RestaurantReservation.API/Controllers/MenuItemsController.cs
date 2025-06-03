using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantReservation.API.Models.Customers;
using RestaurantReservation.API.Models.Employees;
using RestaurantReservation.API.Models.MenuItems;
using RestaurantReservation.Db.Interfaces;
using RestaurantReservation.Db.Models.Entities;
using RestaurantReservation.Db.Repositories;

namespace RestaurantReservation.API.Controllers
{
    [Route("api/MenuItems")]
    [ApiController]
    [Authorize]
    public class MenuItemsController : ControllerBase
    {
        private readonly IMenuItemRepository _menuItemRepository;
        private readonly IMapper _mapper;
        public MenuItemsController(IMenuItemRepository menuItemRepository, IMapper mapper)
        {
            _menuItemRepository = menuItemRepository;
            _mapper = mapper;
        }
        /// <summary>
        /// Retrieves all menu items.
        /// </summary>
        /// <returns>A list of menu items.</returns>
        /// <response code="200">Returns the list of menu items.</response>
        /// <response code="204">No menu items found.</response>
        /// <response code="400">Bad request if parameters are invalid.</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        
        public async Task<ActionResult<IEnumerable<MenuItemDto>>> GetAllMenuItems()
        {
            var MenuItems = await _menuItemRepository.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<MenuItemDto>>(MenuItems));
        }
        /// <summary>
        /// Retrieves a specific menu item by ID.
        /// </summary>
        /// <param name="id">Menu item ID</param>
        /// <returns>Menu item details</returns>
        /// <response code="200">Returns the menu item</response>
        /// <response code="404">If menu item is not found</response>

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<MenuItemDto>> GetMenuItemById(int id)
        {
            var MenuItems = await _menuItemRepository.GetByIdAsync(id);
            if (MenuItems == null) return NotFound();
            return Ok(_mapper.Map<MenuItemDto>(MenuItems));
        }
        /// <summary>
        /// Creates a new menu item.
        /// </summary>
        /// <param name="menuItemCreationDto">Menu item creation data</param>
        /// <returns>Created menu item</returns>
        /// <response code="201">Returns the created menu item</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<MenuItemDto>> CreateMenuItem(MenuItemCreationDto MenuItemCreationDto)
        {
            var MenuItem = _mapper.Map<MenuItem>(MenuItemCreationDto);
            await _menuItemRepository.AddAsync(MenuItem);
            return CreatedAtAction(nameof(GetMenuItemById), new { id = MenuItem.ItemId }, _mapper.Map<MenuItemDto>(MenuItem));
        }

        /// <summary>
        /// Updates an existing menu item by ID.
        /// </summary>
        /// <param name="id">Menu item ID</param>
        /// <param name="menuItemUpdateDto">Updated menu item data</param>
        /// <returns>No content</returns>
        /// <response code="204">Update successful</response>
        /// <response code="404">If menu item is not found</response>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMenuItem(int id, MenuItemUpdatedDto MenuItemUpdatedDto)
        {
            var existingMenuItem = await _menuItemRepository.GetByIdAsync(id);
            if (existingMenuItem == null) return NotFound();
            _mapper.Map(MenuItemUpdatedDto, existingMenuItem);
            await _menuItemRepository.UpdateAsync(existingMenuItem);
            return NoContent();
        }
        /// <summary>
        /// Deletes a menu item by ID.
        /// </summary>
        /// <param name="id">Menu item ID</param>
        /// <returns>No content</returns>
        /// <response code="204">Deletion successful</response>
        /// <response code="404">If menu item is not found</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteMenuItem(int id)
        {
            var existingMenuItem = await _menuItemRepository.GetByIdAsync(id);
            if (existingMenuItem == null) return NotFound();
            await _menuItemRepository.DeleteAsync(id);
            return NoContent();
        }

    }
}
