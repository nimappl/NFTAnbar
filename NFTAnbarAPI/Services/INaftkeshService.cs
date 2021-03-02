using System.Collections.Generic;
using System.Threading.Tasks;
using NFTAnbarAPI.DTOs;

namespace NFTAnbarAPI.Services
{
    public interface INaftkeshService
    {
        Task<NaftkeshDTO> GetById(long id);
        Task<GridData<NaftkeshDTO>> Get(GridData<NaftkeshDTO> qParams);
        void Create(NaftkeshDTO dto);
        Task Update(NaftkeshDTO dto);
        Task Delete(long id);
        bool NaftkeshExists(long id);
        Task<bool> Save();
    }
}