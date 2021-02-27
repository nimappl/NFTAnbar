using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NFTAnbarAPI.DTOs;
using NFTAnbarAPI.Models;

namespace NFTAnbarAPI.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly NFTAnbarContext _context;
        public CustomerService(NFTAnbarContext context)
        {
            _context = context;

        }
        public void Create(CustomerDTO dto)
        {
            _context.Customer.Add(ConvertDTO.CustomerDTOToModel(dto));
        }

        public async Task Delete(long id)
        {
            var Customer = await _context.Customer.FindAsync(id);
            _context.Customer.Remove(Customer);
        }

        public async Task<GridData<CustomerDTO>> Get(GridData<CustomerDTO> qParams)
        {
            var query = _context.Customer as IQueryable<Customer>;
            int count;
            if (qParams.Filters != null)
            {
                foreach (Filter filter in qParams.Filters)
                {
                    if (filter.Key == "Name")
                        query = query.Where(d => d.Name.Contains(filter.Value));
                    if (filter.Key == "Gcode" && filter.Value != "")
                        query = query.Where(d => d.Gcode == Int32.Parse(filter.Value));

                    // query = query.Where($"{filter.Key}.Contains({filter.Value})");
                }
            }

            count = await query.CountAsync();
            query = query.OrderBy(qParams.SortBy + (qParams.SortType == SortType.Asc ? " asc" : " desc"));
            query = query.Skip((qParams.PageNumber - 1) * qParams.PageSize).Take(qParams.PageSize);

            return new GridData<CustomerDTO>
            {
                Data = await query.Select(d => ConvertDTO.CustomerModelToDTO(d)).ToListAsync(),
                Filters = qParams.Filters,
                SortBy = qParams.SortBy,
                SortType = qParams.SortType,
                PageSize = qParams.PageSize,
                PageNumber = qParams.PageNumber,
                Count = count
            };
        }

        public async Task<CustomerDTO> GetById(long id)
        {
            return ConvertDTO.CustomerModelToDTO(await _context.Customer.FindAsync(id));
        }

        public async Task Update(CustomerDTO dto)
        {
            Customer customer = await _context.Customer.FindAsync(dto.Id);
            customer.Id = dto.Id;
            customer.Name = dto.Name;
            customer.NationalCode = dto.NationalCode;
            customer.Gcode = dto.Gcode;
            customer.Gkey = dto.Gkey;
            customer.Active = dto.Active;
        }

        public async Task<bool> Save()
        {
            int status = await _context.SaveChangesAsync();
            if (status > 0)
                return true;

            return false;
        }

        public bool CustomerExists(long id)
        {
            return _context.Customer.Any(d => d.Id == id);
        }
    }
}