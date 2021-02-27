using System.Threading.Tasks;
using NFTAnbarAPI.DTOs;

namespace NFTAnbarAPI.Services
{
    public interface ISendTypeService
    {
        Task<SendTypeDTO> GetById(long id);
        Task<GridData<SendTypeDTO>> Get(GridData<SendTypeDTO> qParams);
        void Create(SendTypeDTO dto);
        Task Update(SendTypeDTO dto);
        Task Delete(long id);
        bool SendTypeExists(long id);
        Task<bool> Save();
    }
}