using System.Threading.Tasks;
using NFTAnbarAPI.DTOs;

namespace NFTAnbarAPI.Services
{
    public interface INdepoWorkShiftService
    {
        Task<NdepoWorkShiftDTO> GetById(long id);
        Task<GridData<NdepoWorkShiftDTO>> Get(GridData<NdepoWorkShiftDTO> qParams);
        void Create(NdepoWorkShiftDTO dto);
        Task Update(NdepoWorkShiftDTO dto);
        Task Delete(long id);
        bool NdepoWorkShiftExists(long id);
        Task<bool> Save();
    }
}