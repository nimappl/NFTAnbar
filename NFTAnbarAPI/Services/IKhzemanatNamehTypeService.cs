using System.Threading.Tasks;
using NFTAnbarAPI.DTOs;

namespace NFTAnbarAPI.Services
{
    public interface IKhzemanatNamehTypeService
    {
        Task<KhzemanatNamehTypeDTO> GetById(long id);
        Task<GridData<KhzemanatNamehTypeDTO>> Get(GridData<KhzemanatNamehTypeDTO> qParams);
        void Create(KhzemanatNamehTypeDTO dto);
        Task Update(KhzemanatNamehTypeDTO dto);
        Task Delete(long id);
        bool KhzemanatNamehTypeExists(long id);
        Task<bool> Save();
    }
}