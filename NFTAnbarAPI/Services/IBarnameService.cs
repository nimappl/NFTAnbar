using System.Threading.Tasks;
using NFTAnbarAPI.DTOs;

namespace NFTAnbarAPI.Services
{
    public interface IBarnameService
    {
        Task<BarnameDTO> GetById(long id);
        Task<GridData<BarnameDTO>> Get(GridData<BarnameDTO> qParams);
        void Create(BarnameDTO dto);
        Task Update(BarnameDTO dto);
        Task Delete(long id);
        bool BarnameExists(long id);
        Task<bool> Save();
    }
}