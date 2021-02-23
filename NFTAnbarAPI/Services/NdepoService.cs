using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NFTAnbarAPI.DTOs;
using NFTAnbarAPI.Models;

namespace NFTAnbarAPI.Services
{
    public class NdepoService : INdepoService
    {
        private readonly NFTAnbarContext _context;
        public NdepoService(NFTAnbarContext context)
        {
            _context = context;

        }
        public void Create(NdepoDTO dto)
        {
            _context.Add(ConvertDTO.NdepoDTOToModel(dto));
        }

        public async Task Delete(long id)
        {
            var depo = await _context.Ndepo.FindAsync(id);
            _context.Ndepo.Remove(depo);
        }

        public async Task<GridData<NdepoDTO>> Get(GridData<NdepoDTO> qParams)
        {
            IQueryable<NdepoDTO> query =
                from d in _context.Ndepo
                from dt in _context.NdepoType
                from c in _context.City
                where d.NdepoTypeId == dt.Id
                where d.CityId == c.Id
                select new NdepoDTO
                {
                    Id = d.Id,
                    Name = d.Name,
                    Gcode = d.Gcode,
                    CityId = d.CityId,
                    City = c.Name,
                    NdepoTypeId = d.NdepoTypeId,
                    NdepoType = dt.Name,
                    Active = d.Active
                };
            int count;

            if (qParams.Filters != null) {
                foreach (Filter filter in qParams.Filters)
                {
                    switch(filter.Key) {
                        case "Name":
                            query = query.Where(d => d.Name.Contains(filter.Value));
                            break;
                        case "Gcode":
                            query = query.Where(d => d.Gcode == Int32.Parse(filter.Value));
                            break;
                        case "City":
                            query = query.Where(d => d.City.Contains(filter.Value));
                            break;
                        case "NdepoType":
                            query = query.Where(d => d.NdepoType.Contains(filter.Value));
                            break;
                        case "NdepoTypeId":
                            query = query.Where(d => d.NdepoTypeId == Int32.Parse(filter.Value));
                            break;
                    }
                }
            }

            count = await query.CountAsync();
            query = query.OrderBy(qParams.SortBy + (qParams.SortType == SortType.Asc ? " asc" : " desc"));
            query = query.Skip((qParams.PageNumber - 1) * qParams.PageSize).Take(qParams.PageSize);

            qParams.Count = count;
            qParams.Data = await query.ToListAsync();

            return qParams;
        }

        public async Task<NdepoDTO> GetById(long id)
        {
            return await (
                from d in _context.Ndepo
                from dt in _context.NdepoType
                from c in _context.City
                where d.NdepoTypeId == dt.Id
                where d.CityId == c.Id
                select new NdepoDTO
                {
                    Id = d.Id,
                    Name = d.Name,
                    Gcode = d.Gcode,
                    CityId = d.CityId,
                    City = c.Name,
                    NdepoTypeId = d.NdepoTypeId,
                    NdepoType = dt.Name,
                    Active = d.Active
                }
            ).FirstOrDefaultAsync();
        }

        public async Task Update(NdepoDTO dto)
        {
            var depo = await _context.Ndepo.FindAsync(dto.Id);
            depo.Name = dto.Name;
            depo.Gcode = dto.Gcode;
            depo.NdepoTypeId = dto.NdepoTypeId;
            depo.CityId = dto.CityId;
            depo.Active = dto.Active;
        }

        public bool NdepoExists(long id)
        {
            return _context.Ndepo.Any(d => d.Id == id);
        }

        public async Task<bool> Save()
        {
            if (await _context.SaveChangesAsync() > 0)
                return true;

            return false;
        }
    }
}