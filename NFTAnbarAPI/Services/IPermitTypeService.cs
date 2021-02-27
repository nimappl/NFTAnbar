using System.Threading.Tasks;
using NFTAnbarAPI.DTOs;

namespace NFTAnbarAPI.Services
{
    public interface IPermitTypeService
    {
        Task<PermitTypeDTO> GetById(long id);
        Task<GridData<PermitTypeDTO>> Get(GridData<PermitTypeDTO> qParams);
        void Create(PermitTypeDTO dto);
        Task Update(PermitTypeDTO dto);
        Task Delete(long id);
        bool PermitTypeExists(long id);
        Task<bool> Save();
    }
}