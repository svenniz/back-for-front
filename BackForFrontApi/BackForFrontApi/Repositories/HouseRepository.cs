using AutoMapper;
using BackForFrontApi.Data_Access;
using BackForFrontApi.Dtos;
using BackForFrontApi.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace BackForFrontApi.Repositories
{
    public class HouseRepository : GenericEfCoreRepository<HouseEntity>, IHouseRepository
    {
        private readonly HouseDbContext _context;
        private readonly IMapper _mapper;

        public HouseRepository(HouseDbContext context, IMapper mapper) : base(context)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<HouseDto>> GetAllHouses()
        {
            return await _context.Houses
                .Select(h => new HouseDto(h.Id, h.Address, h.Country, h.Price))
                .ToListAsync();
        }

        public async Task<List<HouseDto>> GetAllHousesWithMap()
        {
            var houseEntities = await _context.Houses.ToListAsync();
            return _mapper.Map<List<HouseDto>>(houseEntities);
        }
        public async Task<HouseDto?> GetHouseDto(int id) 
        {
            var houseEntity = await _context.Houses.FirstOrDefaultAsync(h => h.Id == id);
            if (houseEntity == null) 
            {
                return null;
            }
            return _mapper.Map<HouseDto>(houseEntity);
        }

        public async Task<HouseDetailsDto?> GetDetails(int id)
        {
            var house = await _context.Houses
                .SingleOrDefaultAsync(h=>h.Id == id);
            if(house == null)
            {
                return null;
            }
            return new HouseDetailsDto(house.Id, house.Address, house.Country, 
                house.Price, house.Description, house.Photo);
        }
        public async Task<HouseDetailsDto?> AddHouse(HouseDetailsDto house)
        {
            if(house == null)
            {
                return null;
            }
            var newHouse = _mapper.Map<HouseEntity>(house);
            Add(newHouse);
            return _mapper.Map<HouseDetailsDto>(newHouse);
        }

        public void DtoToEntity(HouseDetailsDto dto, HouseEntity entity)
        {
            entity.Address = dto.Address;
            entity.Country = dto.Country;
            entity.Price = dto.Price;
            entity.Description = dto.description;
            entity.Photo = dto.photo;
        }
    }
}
