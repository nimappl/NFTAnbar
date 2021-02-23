using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NFTAnbarAPI.DTOs;
using NFTAnbarAPI.Models;

namespace NFTAnbarAPI.Services
{
    public class NdepoTypeService : INdepoTypeService
    {
        private readonly NFTAnbarContext _context;
        public NdepoTypeService(NFTAnbarContext context)
        {
            _context = context;

        }
        public void Create(NdepoTypeDTO dto)
        {
            _context.NdepoType.Add(ConvertDTO.NdepoTypeDTOToModel(dto));
        }

        public async Task Delete(long id)
        {
            var NdepoType = await _context.NdepoType.FindAsync(id);
            _context.NdepoType.Remove(NdepoType);
        }

        public async Task<GridData<NdepoTypeDTO>> Get(GridData<NdepoTypeDTO> qParams)
        {
            var query = _context.NdepoType as IQueryable<NdepoType>;
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

            return new GridData<NdepoTypeDTO>
            {
                Data = await query.Select(d => ConvertDTO.NdepoTypeModelToDTO(d)).ToListAsync(),
                Filters = qParams.Filters,
                SortBy = qParams.SortBy,
                SortType = qParams.SortType,
                PageSize = qParams.PageSize,
                PageNumber = qParams.PageNumber,
                Count = count
            };
        }

        public async Task<NdepoTypeDTO> GetById(long id)
        {
            return ConvertDTO.NdepoTypeModelToDTO(await _context.NdepoType.FindAsync(id));
        }

        public async Task Update(NdepoTypeDTO dto)
        {
            NdepoType depoType = await _context.NdepoType.FindAsync(dto.Id);
            depoType.Id = dto.Id;
            depoType.Name = dto.Name;
            depoType.Gcode = dto.Gcode;
            depoType.Gkey = dto.Gkey;
            depoType.Active = dto.Active;
        }

        public async Task<bool> Save()
        {
            int status = await _context.SaveChangesAsync();
            if (status > 0)
                return true;

            return false;
        }

        public bool NdepoTypeExists(long id)
        {
            return _context.NdepoType.Any(d => d.Id == id);
        }
    }
}