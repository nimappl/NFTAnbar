using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NFTAnbarAPI.DTOs;
using NFTAnbarAPI.Models;

namespace NFTAnbarAPI.Services
{
    public class KhzemanatNamehTypeService: IKhzemanatNamehTypeService
    {
        private readonly NFTAnbarContext _context;
        private readonly IMapper _mapper;
        public KhzemanatNamehTypeService(NFTAnbarContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;

        }
        public void Create(KhzemanatNamehTypeDTO dto)
        {
            _context.KhzemanatNamehType.Add(_mapper.Map<KhzemanatNamehType>(dto));
        }

        public async Task Delete(long id)
        {
            var KhzemanatNamehType = await _context.KhzemanatNamehType.FindAsync(id);
            _context.KhzemanatNamehType.Remove(KhzemanatNamehType);
        }

        public async Task<GridData<KhzemanatNamehTypeDTO>> Get(GridData<KhzemanatNamehTypeDTO> qParams)
        {
            var query = _context.KhzemanatNamehType as IQueryable<KhzemanatNamehType>;
            int count;
            if (qParams.Filters != null)
            {
                foreach (Filter filter in qParams.Filters)
                {
                    if (filter.Key == "Title")
                        query = query.Where(d => d.Title.Contains(filter.Value));
                    if (filter.Key == "Gkey" && filter.Value != "")
                        query = query.Where(d => d.Gkey == Int32.Parse(filter.Value));
                }
            }

            count = await query.CountAsync();
            query = query.OrderBy(qParams.SortBy + (qParams.SortType == SortType.Asc ? " asc" : " desc"));
            query = query.Skip((qParams.PageNumber - 1) * qParams.PageSize).Take(qParams.PageSize);

            return new GridData<KhzemanatNamehTypeDTO>
            {
                Data = _mapper.Map<List<KhzemanatNamehTypeDTO>>(await query.ToListAsync()),
                Filters = qParams.Filters,
                SortBy = qParams.SortBy,
                SortType = qParams.SortType,
                PageSize = qParams.PageSize,
                PageNumber = qParams.PageNumber,
                Count = count
            };
        }

        public async Task<KhzemanatNamehTypeDTO> GetById(long id)
        {
            return _mapper.Map<KhzemanatNamehTypeDTO>(await _context.KhzemanatNamehType.FindAsync(id));
        }

        public async Task Update(KhzemanatNamehTypeDTO dto)
        {
            KhzemanatNamehType zemanatNamehType = await _context.KhzemanatNamehType.FindAsync(dto.Id);
            zemanatNamehType.Id = dto.Id;
            zemanatNamehType.Title = dto.Title;
            zemanatNamehType.Gkey = dto.Gkey;
            zemanatNamehType.Gdesc = dto.Gdesc;
            zemanatNamehType.Active = dto.Active;
        }

        public async Task<bool> Save()
        {
            int status = await _context.SaveChangesAsync();
            if (status > 0)
                return true;

            return false;
        }

        public bool KhzemanatNamehTypeExists(long id)
        {
            return _context.KhzemanatNamehType.Any(d => d.Id == id);
        }
    }
}