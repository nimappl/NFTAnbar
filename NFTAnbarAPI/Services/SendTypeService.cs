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
    public class SendTypeService : ISendTypeService
    {
        private readonly NFTAnbarContext _context;
        private readonly IMapper _mapper;
        public SendTypeService(NFTAnbarContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public void Create(SendTypeDTO dto)
        {
            _context.SendType.Add(_mapper.Map<SendType>(dto));
        }

        public async Task Delete(long id)
        {
            var sendType = await _context.SendType.FindAsync(id);
            _context.SendType.Remove(sendType);
        }

        public async Task<GridData<SendTypeDTO>> Get(GridData<SendTypeDTO> qParams)
        {
            var query = _context.SendType as IQueryable<SendType>;
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

            return new GridData<SendTypeDTO>
            {
                Data = _mapper.Map<List<SendTypeDTO>>(await query.ToListAsync()),
                Filters = qParams.Filters,
                SortBy = qParams.SortBy,
                SortType = qParams.SortType,
                PageSize = qParams.PageSize,
                PageNumber = qParams.PageNumber,
                Count = count
            };
        }

        public async Task<SendTypeDTO> GetById(long id)
        {
            return _mapper.Map<SendTypeDTO>(await _context.SendType.FindAsync(id));
        }

        public async Task Update(SendTypeDTO dto)
        {
            SendType sendType = await _context.SendType.FindAsync(dto.Id);
            sendType.Id = dto.Id;
            sendType.Name = dto.Name;
            sendType.Active = dto.Active;
        }

        public async Task<bool> Save()
        {
            int status = await _context.SaveChangesAsync();
            if (status > 0)
                return true;

            return false;
        }

        public bool SendTypeExists(long id)
        {
            return _context.SendType.Any(c => c.Id == id);
        }
    }
}