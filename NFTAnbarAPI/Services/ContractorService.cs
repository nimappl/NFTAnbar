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
    public class ContractorService : IContractorService
    {
        private readonly NFTAnbarContext _context;
        private readonly IMapper _mapper;
        public ContractorService(NFTAnbarContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public void Create(ContractorDTO dto)
        {
            _context.Contractor.Add(_mapper.Map<Contractor>(dto));
        }

        public async Task Delete(long id)
        {
            var contractor = await _context.Contractor.FindAsync(id);
            _context.Contractor.Remove(contractor);
        }

        public async Task<GridData<ContractorDTO>> Get(GridData<ContractorDTO> qParams)
        {
            var query = _context.Contractor as IQueryable<Contractor>;
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

            return new GridData<ContractorDTO>
            {
                Data = _mapper.Map<List<ContractorDTO>>(await query.ToListAsync()),
                Filters = qParams.Filters,
                SortBy = qParams.SortBy,
                SortType = qParams.SortType,
                PageSize = qParams.PageSize,
                PageNumber = qParams.PageNumber,
                Count = count
            };
        }

        public async Task<ContractorDTO> GetById(long id)
        {
            return _mapper.Map<ContractorDTO>(await _context.Contractor.FindAsync(id));
        }

        public async Task Update(ContractorDTO dto)
        {
            Contractor contractor = await _context.Contractor.FindAsync(dto.Id);
            contractor.Id = dto.Id;
            contractor.Name = dto.Name;
            contractor.Active = dto.Active;
        }

        public async Task<bool> Save()
        {
            int status = await _context.SaveChangesAsync();
            if (status > 0)
                return true;

            return false;
        }

        public bool ContractorExists(long id)
        {
            return _context.Contractor.Any(c => c.Id == id);
        }
    }
}