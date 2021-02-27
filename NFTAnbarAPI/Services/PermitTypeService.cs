using System.Threading.Tasks;
using NFTAnbarAPI.Models;
using NFTAnbarAPI.DTOs;
using System.Linq;
using System.Linq.Dynamic.Core;
using Microsoft.EntityFrameworkCore;

namespace NFTAnbarAPI.Services
{
    public class PermitTypeService : IPermitTypeService
    {
        private readonly NFTAnbarContext _context;
        public PermitTypeService(NFTAnbarContext context)
        {
            _context = context;
        }

        public void Create(PermitTypeDTO dto)
        {
            _context.PermitType.Add(ConvertDTO.PermitTypeDTOToModel(dto));
        }

        public async Task Delete(long id)
        {
            var permitType = await _context.PermitType.FindAsync(id);
            _context.PermitType.Remove(permitType);
        }

        public async Task<GridData<PermitTypeDTO>> Get(GridData<PermitTypeDTO> qParams)
        {
            var query = _context.PermitType as IQueryable<PermitType>;
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

            return new GridData<PermitTypeDTO>
            {
                Data = await query.Select(c => ConvertDTO.PermitTypeModelToDTO(c)).ToListAsync(),
                Filters = qParams.Filters,
                SortBy = qParams.SortBy,
                SortType = qParams.SortType,
                PageSize = qParams.PageSize,
                PageNumber = qParams.PageNumber,
                Count = count
            };
        }

        public async Task<PermitTypeDTO> GetById(long id)
        {
            return ConvertDTO.PermitTypeModelToDTO(await _context.PermitType.FindAsync(id));
        }

        public async Task Update(PermitTypeDTO dto)
        {
            PermitType permitType = await _context.PermitType.FindAsync(dto.Id);
            permitType.Id = dto.Id;
            permitType.Name = dto.Name;
            permitType.Active = dto.Active;
        }

        public async Task<bool> Save()
        {
            int status = await _context.SaveChangesAsync();
            if (status > 0)
                return true;

            return false;
        }

        public bool PermitTypeExists(long id)
        {
            return _context.PermitType.Any(c => c.Id == id);
        }
    }
}