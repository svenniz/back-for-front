using BackForFrontApi.Dtos;
using BackForFrontApi.Models;

namespace BackForFrontApi.Repositories
{
    public interface IHouseRepository : IRepository<HouseEntity>
    {
        Task<HouseDetailsDto?> AddHouse(HouseDetailsDto house);
        Task<HouseDetailsDto?> AddHouseWithMap(HouseDetailsDto house);
        Task<List<HouseDto>> GetAllHouses();
        Task<List<HouseDto>> GetAllHousesWithMap();
        Task<HouseDetailsDto?> GetDetails(int id);
    }
}