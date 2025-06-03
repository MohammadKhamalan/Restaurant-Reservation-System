using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantReservation.API.Models.Reservations;
using RestaurantReservation.API.Models.Tables;
using RestaurantReservation.Db.Interfaces;
using RestaurantReservation.Db.Models.Entities;

namespace RestaurantReservation.API.Controllers
{
    [Route("api/Table")]
    [ApiController]
    [Authorize]
    public class TableController:ControllerBase
    {
        private readonly ITableRepository _tableRepository;
        private IMapper _mapper;
    public TableController(ITableRepository tableRepository, IMapper mapper)
        {
            _tableRepository = tableRepository;
            _mapper = mapper;
        }
        /// <summary>
        /// Retrieves all tables.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<TableDto>>> GetAllTables()
        {
            var Tables = await _tableRepository.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<TableDto>>(Tables));
        }
        /// <summary>
        /// Retrieves a table by ID.
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<TableDto>> GetTableById(int id)
        {
            var tables = await _tableRepository.GetByIdAsync(id);
            if (tables == null) return NotFound();
            return Ok(_mapper.Map<TableDto>(tables));
        }
        /// <summary>
        /// Creates a new table.
        /// </summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<TableDto>> CreateTable(TableCreationDto TableCreationDto)
        {
            var tables = _mapper.Map<Table>(TableCreationDto);
            await _tableRepository.AddAsync(tables);

            return CreatedAtAction(nameof(GetTableById), new { id = tables.TableId }, _mapper.Map<TableDto>(tables));
        }
        /// <summary>
        /// Updates a table by ID.
        /// </summary>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateTable(int id, TableUpdatedDto TableUpdatedDto)
        {
            var existingTable = await _tableRepository.GetByIdAsync(id);
            if (existingTable == null)
            {
                return NotFound();
            }
            _mapper.Map(TableUpdatedDto, existingTable);
            await _tableRepository.UpdateAsync(existingTable);
            return NoContent();
        }
        /// <summary>
        /// Deletes a table by ID.
        /// </summary>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteTable(int id)
        {
            var Table = await _tableRepository.GetByIdAsync(id);
            if (Table == null) return NotFound();

            await _tableRepository.DeleteAsync(id);

            return NoContent();
        }
    }

}
