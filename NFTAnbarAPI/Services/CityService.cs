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
    public class CityService : ICityService
    {
        private readonly NFTAnbarContext _context;
        private readonly IMapper _mapper;
        public CityService(NFTAnbarContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public void Create(CityDTO dto)
        {
            _context.City.Add(_mapper.Map<City>(dto));
        }

        public async Task Delete(long id)
        {
            var city = await _context.City.FindAsync(id);
            _context.City.Remove(city);
        }

        public async Task<GridData<CityDTO>> Get(GridData<CityDTO> qParams)
        {
            var query = _context.City as IQueryable<City>;
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

            return new GridData<CityDTO>
            {
                Data = _mapper.Map<List<CityDTO>>(await query.ToListAsync()),
                Filters = qParams.Filters,
                SortBy = qParams.SortBy,
                SortType = qParams.SortType,
                PageSize = qParams.PageSize,
                PageNumber = qParams.PageNumber,
                Count = count
            };
        }

        public async Task<CityDTO> GetById(long id)
        {
            return _mapper.Map<CityDTO>(await _context.City.FindAsync(id));
        }

        public async Task Update(CityDTO dto)
        {
            City city = await _context.City.FindAsync(dto.Id);
            city.Id = dto.Id;
            city.Name = dto.Name;
            city.Description = dto.Description;
            city.Active = dto.Active;
        }

        public async Task<bool> Save()
        {
            int status = await _context.SaveChangesAsync();
            if (status > 0)
                return true;

            return false;
        }

        public bool CityExists(long id)
        {
            return _context.City.Any(c => c.Id == id);
        }
    }
}