using BackForFrontApi.Dtos;
using BackForFrontApi.Models;

namespace BackForFrontApi.Repositories
{
    public interface IHouseRepository : IRepository<HouseEntity>
    {
        Task<List<HouseDto>> GetAllHouses();
        Task<List<HouseDto>> GetAllHousesWithMap();
    }
}