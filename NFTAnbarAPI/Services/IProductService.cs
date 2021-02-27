using System.Threading.Tasks;
using NFTAnbarAPI.DTOs;

namespace NFTAnbarAPI.Services
{
    public interface IProductService
    {
        Task<ProductDTO> GetById(long id);
        Task<GridData<ProductDTO>> Get(GridData<ProductDTO> qParams);
        void Create(ProductDTO dto);
        Task Update(ProductDTO dto);
        Task Delete(long id);
        bool ProductExists(long id);
        Task<bool> Save();
    }
}