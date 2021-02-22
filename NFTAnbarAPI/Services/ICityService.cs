using System.Threading.Tasks;
using NFTAnbarAPI.DTOs;

namespace NFTAnbarAPI.Services
{
    public interface ICityService
    {
        Task<CityDTO> GetById(long id);
        Task<GridData<CityDTO>> Get(GridData<CityDTO> qParams);
        void Create(CityDTO dto);
        Task Update(CityDTO dto);
        Task Delete(long id);
        bool CityExists(long id);
        Task<bool> Save();
    }
}