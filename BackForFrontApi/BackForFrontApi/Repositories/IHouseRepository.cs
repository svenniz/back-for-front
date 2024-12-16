using BackForFrontApi.Dtos;
using BackForFrontApi.Models;

namespace BackForFrontApi.Repositories
{
    public interface IHouseRepository : IRepository<HouseEntity>
    {
        Task<HouseDetailsDto?> AddHouse(HouseDetailsDto house);
        void DtoToEntity(HouseDetailsDto dto, HouseEntity entity);
        Task<List<HouseDto>> GetAllHouses();
        Task<List<HouseDto>> GetAllHousesWithMap();
        Task<HouseDetailsDto?> GetDetails(int id);
    }
}