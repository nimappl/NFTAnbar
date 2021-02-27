using System.Threading.Tasks;
using NFTAnbarAPI.DTOs;

namespace NFTAnbarAPI.Services
{
    public interface IHavalehService
    {
        Task<HavalehDTO> GetById(long id);
        Task<GridData<HavalehDTO>> Get(GridData<HavalehDTO> qParams);
        void Create(HavalehDTO dto);
        Task Update(HavalehDTO dto);
        Task Delete(long id);
        bool HavalehExists(long id);
        Task<bool> Save();
    }
}