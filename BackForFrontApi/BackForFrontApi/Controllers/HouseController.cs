using BackForFrontApi.Data_Access;
using BackForFrontApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using BackForFrontApi.Repositories;
using BackForFrontApi.Dtos;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BackForFrontApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class HouseController : ControllerBase
    {
        private readonly IHouseRepository _houseRepository;
        public HouseController(IHouseRepository houseRepository) 
        {
            _houseRepository = houseRepository;
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
        public async Task<ActionResult<HouseEntity>> Get(int id)
        {
            var house = await _houseRepository.Get(id);
            if (house == null)
            {
                return NotFound(new {Message = $"House with ID {id} was not found!"});
            }
            return Ok(house);
        }

        // POST api/<HouseController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<HouseController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<HouseController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
