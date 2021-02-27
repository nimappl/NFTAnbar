using System.Threading.Tasks;
using NFTAnbarAPI.DTOs;

namespace NFTAnbarAPI.Services
{
    public interface ICustomerService
    {
        Task<CustomerDTO> GetById(long id);
        Task<GridData<CustomerDTO>> Get(GridData<CustomerDTO> qParams);
        void Create(CustomerDTO dto);
        Task Update(CustomerDTO dto);
        Task Delete(long id);
        bool CustomerExists(long id);
        Task<bool> Save();
    }
}