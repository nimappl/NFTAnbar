using System.Threading.Tasks;
using NFTAnbarAPI.DTOs;

namespace NFTAnbarAPI.Services
{
    public interface IContractorService
    {
        Task<ContractorDTO> GetById(long id);
        Task<GridData<ContractorDTO>> Get(GridData<ContractorDTO> qParams);
        void Create(ContractorDTO dto);
        Task Update(ContractorDTO dto);
        Task Delete(long id);
        bool ContractorExists(long id);
        Task<bool> Save();
    }
}