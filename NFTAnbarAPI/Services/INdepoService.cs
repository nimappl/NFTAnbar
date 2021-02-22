using System.Threading.Tasks;
using NFTAnbarAPI.DTOs;

namespace NFTAnbarAPI.Services
{
    public interface INdepoService
    {
        Task<NdepoDTO> GetById(long id);
        Task<GridData<NdepoDTO>> Get(GridData<NdepoDTO> qParams);
        void Create(NdepoDTO dto);
        Task Update(NdepoDTO dto);
        Task Delete(long id);
        bool NdepoExists(long id);
        Task<bool> Save();
    }
}