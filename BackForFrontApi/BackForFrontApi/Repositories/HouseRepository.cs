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
            return EntityToDetailsDto(house);
        }
        
        public async Task<HouseDetailsDto?> AddHouse(HouseDetailsDto house)
        {
            if (house == null)
            {
                return null;
            }
            var newHouse = new HouseEntity();
            DtoToEntity(house, newHouse);
            Add(newHouse);
            await SaveChanges();
            return EntityToDetailsDto(newHouse);
        }

        public async Task<HouseDetailsDto?> UpdateHouse(int id, HouseDetailsDto house)
        {
            var houseEntity = await _context.Houses.FirstOrDefaultAsync(h => h.Id == id);
            if (houseEntity == null)
            {
                return null;
            }
            DtoToEntity(house, houseEntity);
            _context.Entry(houseEntity).State = EntityState.Modified;
            await SaveChanges();
            return EntityToDetailsDto(houseEntity);
        }

        public async Task DeleteHouse(int id)
        {
            var houseEntity = await _context.Houses.FirstOrDefaultAsync(h => h.Id == id);
            if (houseEntity != null)
            {
                Delete(houseEntity);
                await SaveChanges();
            }
        }

        public async Task<HouseDetailsDto?> AddHouseWithMap(HouseDetailsDto house)
        {
            if (house == null)
            {
                return null;
            }
            var newHouse = _mapper.Map<HouseEntity>(house);
            Add(newHouse);
            await SaveChanges();
            return _mapper.Map<HouseDetailsDto>(newHouse);
        }

        public async Task<HouseDetailsDto?> UpdateHouseWithMap(int id, HouseDetailsDto house)
        {
            var houseEntity = await _context.Houses.FirstOrDefaultAsync(h => h.Id == id);
            if (houseEntity == null)
            {
                return null;
            }
            _mapper.Map(house, houseEntity);
            _context.Entry(houseEntity).State = EntityState.Modified;
            await SaveChanges();
            return _mapper.Map<HouseDetailsDto>(houseEntity);
        }

        private static HouseDetailsDto EntityToDetailsDto(HouseEntity entity)
        {
            return new HouseDetailsDto(entity.Id, entity.Address, entity.Country, entity.Price, entity.Description, entity.Photo);
        }
        private static void DtoToEntity(HouseDetailsDto dto, HouseEntity entity)
        {
            entity.Address = dto.Address;
            entity.Country = dto.Country;
            entity.Price = dto.Price;
            entity.Description = dto.description;
            entity.Photo = dto.photo;
        }
    }
}
