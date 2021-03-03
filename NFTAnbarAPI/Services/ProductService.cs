using System.Threading.Tasks;
using NFTAnbarAPI.Models;
using NFTAnbarAPI.DTOs;
using System.Linq;
using System.Linq.Dynamic.Core;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using System.Collections.Generic;

namespace NFTAnbarAPI.Services
{
    public class ProductService : IProductService
    {
        private readonly NFTAnbarContext _context;
        private readonly IMapper _mapper;
        public ProductService(NFTAnbarContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public void Create(ProductDTO dto)
        {
            _context.Product.Add(_mapper.Map<Product>(dto));
        }

        public async Task Delete(long id)
        {
            var product = await _context.Product.FindAsync(id);
            _context.Product.Remove(product);
        }

        public async Task<GridData<ProductDTO>> Get(GridData<ProductDTO> qParams)
        {
            var query = _context.Product as IQueryable<Product>;
            int count;
            if (qParams.Filters != null)
            {
                foreach (Filter filter in qParams.Filters)
                {
                    if (filter.Key == "Name")
                        query = query.Where(c => c.Name.Contains(filter.Value));
                }
            }

            count = await query.CountAsync();
            query = query.OrderBy(qParams.SortBy + (qParams.SortType == SortType.Asc ? " asc" : " desc"));
            query = query.Skip((qParams.PageNumber - 1) * qParams.PageSize).Take(qParams.PageSize);

            return new GridData<ProductDTO>
            {
                Data = _mapper.Map<List<ProductDTO>>(await query.ToListAsync()),
                Filters = qParams.Filters,
                SortBy = qParams.SortBy,
                SortType = qParams.SortType,
                PageSize = qParams.PageSize,
                PageNumber = qParams.PageNumber,
                Count = count
            };
        }

        public async Task<ProductDTO> GetById(long id)
        {
            return _mapper.Map<ProductDTO>(await _context.Product.FindAsync(id));
        }

        public async Task Update(ProductDTO dto)
        {
            Product product = await _context.Product.FindAsync(dto.Id);
            product.Id = dto.Id;
            product.Name = dto.Name;
            product.Active = dto.Active;
        }

        public async Task<bool> Save()
        {
            int status = await _context.SaveChangesAsync();
            if (status > 0)
                return true;

            return false;
        }

        public bool ProductExists(long id)
        {
            return _context.Product.Any(c => c.Id == id);
        }
    }
}