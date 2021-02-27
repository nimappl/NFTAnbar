using System.Threading.Tasks;
using NFTAnbarAPI.Models;
using NFTAnbarAPI.DTOs;
using System.Linq;
using System.Linq.Dynamic.Core;
using Microsoft.EntityFrameworkCore;

namespace NFTAnbarAPI.Services
{
    public class NdepoWorkShiftService : INdepoWorkShiftService
    {
        private readonly NFTAnbarContext _context;
        public NdepoWorkShiftService(NFTAnbarContext context)
        {
            _context = context;
        }

        public void Create(NdepoWorkShiftDTO dto)
        {
            _context.NdepoWorkShift.Add(ConvertDTO.NdepoWorkShiftDTOToModel(dto));
        }

        public async Task Delete(long id)
        {
            var depoWorkShift = await _context.NdepoWorkShift.FindAsync(id);
            _context.NdepoWorkShift.Remove(depoWorkShift);
        }

        public async Task<GridData<NdepoWorkShiftDTO>> Get(GridData<NdepoWorkShiftDTO> qParams)
        {
            var query = _context.NdepoWorkShift as IQueryable<NdepoWorkShift>;
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

            return new GridData<NdepoWorkShiftDTO>
            {
                Data = await query.Select(c => ConvertDTO.NdepoWorkShiftModelToDTO(c)).ToListAsync(),
                Filters = qParams.Filters,
                SortBy = qParams.SortBy,
                SortType = qParams.SortType,
                PageSize = qParams.PageSize,
                PageNumber = qParams.PageNumber,
                Count = count
            };
        }

        public async Task<NdepoWorkShiftDTO> GetById(long id)
        {
            return ConvertDTO.NdepoWorkShiftModelToDTO(await _context.NdepoWorkShift.FindAsync(id));
        }

        public async Task Update(NdepoWorkShiftDTO dto)
        {
            NdepoWorkShift depoWorkShift = await _context.NdepoWorkShift.FindAsync(dto.Id);
            depoWorkShift.Id = dto.Id;
            depoWorkShift.Name = dto.Name;
            depoWorkShift.Active = dto.Active;
        }

        public async Task<bool> Save()
        {
            int status = await _context.SaveChangesAsync();
            if (status > 0)
                return true;

            return false;
        }

        public bool NdepoWorkShiftExists(long id)
        {
            return _context.NdepoWorkShift.Any(c => c.Id == id);
        }
    }
}