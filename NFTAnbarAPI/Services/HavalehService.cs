using System.Threading.Tasks;
using NFTAnbarAPI.Models;
using NFTAnbarAPI.DTOs;
using System.Linq;
using System.Linq.Dynamic.Core;
using Microsoft.EntityFrameworkCore;
using System;
using AutoMapper;
using System.Collections.Generic;

namespace NFTAnbarAPI.Services
{
    public class HavalehService : IHavalehService
    {
        private readonly NFTAnbarContext _context;
        private readonly IMapper _mapper;
        public HavalehService(NFTAnbarContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public void Create(HavalehDTO dto)
        {
            _context.Havaleh.Add(_mapper.Map<Havaleh>(dto));
        }

        public async Task Delete(long id)
        {
            var havaleh = await _context.Havaleh.FindAsync(id);
            _context.Havaleh.Remove(havaleh);
        }

        public async Task<GridData<HavalehDTO>> Get(GridData<HavalehDTO> qParams)
        {
            var query = _context.Havaleh as IQueryable<Havaleh>;
            int count;
            if (qParams.Filters != null)
            {
                foreach (Filter filter in qParams.Filters)
                {
                    if (filter.Key == "Name")
                        query = query.Where(c => c.Name == Int32.Parse(filter.Value));
                }
            }

            count = await query.CountAsync();
            query = query.OrderBy(qParams.SortBy + (qParams.SortType == SortType.Asc ? " asc" : " desc"));
            query = query.Skip((qParams.PageNumber - 1) * qParams.PageSize).Take(qParams.PageSize);

            return new GridData<HavalehDTO>
            {
                Data = _mapper.Map<List<HavalehDTO>>(await query.ToListAsync()),
                Filters = qParams.Filters,
                SortBy = qParams.SortBy,
                SortType = qParams.SortType,
                PageSize = qParams.PageSize,
                PageNumber = qParams.PageNumber,
                Count = count
            };
        }

        public async Task<HavalehDTO> GetById(long id)
        {
            return _mapper.Map<HavalehDTO>(await _context.Havaleh.FindAsync(id));
        }

        public async Task Update(HavalehDTO dto)
        {
            Havaleh havaleh = await _context.Havaleh.FindAsync(dto.Id);
            havaleh.Id = dto.Id;
            havaleh.Name = dto.Name;
            havaleh.Active = dto.Active;
        }

        public async Task<bool> Save()
        {
            int status = await _context.SaveChangesAsync();
            if (status > 0)
                return true;

            return false;
        }

        public bool HavalehExists(long id)
        {
            return _context.Havaleh.Any(c => c.Id == id);
        }
    }
}