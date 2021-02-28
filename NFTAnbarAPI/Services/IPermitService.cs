using System.Threading.Tasks;
using NFTAnbarAPI.DTOs;

namespace NFTAnbarAPI.Services
{
    public interface IPermitService
    {
        Task<PermitDTO> GetById(long id);
        Task<GridData<PermitDTO>> Get(GridData<PermitDTO> qParams);
        void Create(PermitDTO dto);
        Task Update(PermitDTO dto);
        Task Delete(long id);
        bool PermitExists(long id);
        Task<bool> Save();
    }
}