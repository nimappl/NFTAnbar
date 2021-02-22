using System.Threading.Tasks;
using NFTAnbarAPI.DTOs;

namespace NFTAnbarAPI.Services
{
    public interface INdepoTypeService
    {
        Task<NdepoTypeDTO> GetById(long id);
        Task<GridData<NdepoTypeDTO>> Get(GridData<NdepoTypeDTO> qParams);
        void Create(NdepoTypeDTO dto);
        Task Update(NdepoTypeDTO dto);
        Task Delete(long id);
        bool NdepoTypeExists(long id);
        Task<bool> Save();
    }
}