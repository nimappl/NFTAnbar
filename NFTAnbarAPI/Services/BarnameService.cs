using System.Threading.Tasks;
using NFTAnbarAPI.Models;
using NFTAnbarAPI.DTOs;
using System.Linq;
using System.Linq.Dynamic.Core;
using Microsoft.EntityFrameworkCore;

namespace NFTAnbarAPI.Services
{
    public class BarnameService : IBarnameService
    {
        private readonly NFTAnbarContext _context;
        public BarnameService(NFTAnbarContext context)
        {
            _context = context;
        }

        public void Create(BarnameDTO dto)
        {
            _context.Barname.Add(ConvertDTO.BarnameDTOToModel(dto));
        }

        public async Task Delete(long id)
        {
            var barname = await _context.Barname.FindAsync(id);
            _context.Barname.Remove(barname);
        }

        public async Task<GridData<BarnameDTO>> Get(GridData<BarnameDTO> qParams)
        {
            var query = _context.Barname as IQueryable<Barname>;
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

            return new GridData<BarnameDTO>
            {
                Data = await query.Select(c => ConvertDTO.BarnameModelToDTO(c)).ToListAsync(),
                Filters = qParams.Filters,
                SortBy = qParams.SortBy,
                SortType = qParams.SortType,
                PageSize = qParams.PageSize,
                PageNumber = qParams.PageNumber,
                Count = count
            };
        }

        public async Task<BarnameDTO> GetById(long id)
        {
            return ConvertDTO.BarnameModelToDTO(await _context.Barname.FindAsync(id));
        }

        public async Task Update(BarnameDTO dto)
        {
            Barname barname = await _context.Barname.FindAsync(dto.Id);
            barname.Id = dto.Id;
            barname.Name = dto.Name;
            barname.Active = dto.Active;
        }

        public async Task<bool> Save()
        {
            int status = await _context.SaveChangesAsync();
            if (status > 0)
                return true;

            return false;
        }

        public bool BarnameExists(long id)
        {
            return _context.Barname.Any(c => c.Id == id);
        }
    }
}