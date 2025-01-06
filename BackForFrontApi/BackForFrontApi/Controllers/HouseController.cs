using BackForFrontApi.Data_Access;
using BackForFrontApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using BackForFrontApi.Repositories;
using BackForFrontApi.Dtos;
using AutoMapper;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BackForFrontApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class HouseController : ControllerBase
    {
        private readonly IHouseRepository _houseRepository;
        private readonly IMapper _mapper;
        public HouseController(IHouseRepository houseRepository, IMapper mapper)
        {
            _houseRepository = houseRepository;
            _mapper = mapper;
        }

        // GET: api/<HouseController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HouseDto>>> GetAll()
        {
            var houses = await _houseRepository.GetAllHousesWithMap();
            return Ok(houses);
        }

        // GET api/<HouseController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HouseDetailsDto>> Get(int id)
        {
            var house = await _houseRepository.GetDetails(id);
            if (house == null)
            {
                return NotFound(new {Message = $"House with ID {id} was not found!"});
            }
            return Ok(house);
        }

        // POST api/<HouseController>
        [HttpPost]
        public async Task<ActionResult<HouseDetailsDto>> Post([FromBody] HouseDetailsDto dto)
        {
            if(dto == null)
            {
                return NotFound();
            }
            var newHouse = _houseRepository.AddHouse(dto);
            await _houseRepository.SaveChanges();

            return CreatedAtAction(nameof(Get),new {id = newHouse.Id }, newHouse);
        }

        // PUT api/<HouseController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<HouseDetailsDto>> Put(int id, [FromBody] HouseDetailsDto dto)
        {
            if (dto == null || id != dto.Id)
            {
                return BadRequest();
            }

            var houseToUpdate = await _houseRepository.GetDetails(id);
            if (houseToUpdate == null)
            {
                return NotFound(new { Message = $"House with ID {id} was not found!" });
            }

            var updatedHouse = await _houseRepository.UpdateHouse(id, dto);
            await _houseRepository.SaveChanges();

            return Ok(updatedHouse);
        }

        // DELETE api/<HouseController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
