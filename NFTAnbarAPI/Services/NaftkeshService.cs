using System.Threading.Tasks;
using NFTAnbarAPI.Models;
using NFTAnbarAPI.DTOs;
using System.Linq;
using System.Linq.Dynamic.Core;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System;
using AutoMapper;

namespace NFTAnbarAPI.Services
{
    public class NaftkeshService : INaftkeshService
    {
        private readonly NFTAnbarContext _context;
        private readonly IMapper mapper;
        public NaftkeshService(NFTAnbarContext context, IMapper mapper)
        {
            this.mapper = mapper;
            _context = context;
        }

        public void Create(NaftkeshDTO dto)
        {
            _context.Naftkesh.Add(ConvertDTO.NaftkeshDTOToModel(dto));
        }

        public async Task Delete(long id)
        {
            var naftkesh = await _context.Naftkesh.FindAsync(id);
            _context.Naftkesh.Remove(naftkesh);
        }

        public async Task<GridData<NaftkeshDTO>> Get(GridData<NaftkeshDTO> qParams)
        {
            IQueryable<NaftkeshDTO> query = _context.Naftkesh.Select(n =>
                new NaftkeshDTO
                {
                    Id = n.Id,
                    Name = n.Name,
                    PlateNumber = n.PlateNumber,
                    DriverName = n.DriverName,
                    DriverNationalCode = n.DriverNationalCode,
                    DriverLicenseNumber = n.DriverLicenseNumber,  
                    ContractorId = n.Contractor.Id,
                    ContractorName = n.Contractor.Name
                }
            );
            int count;
            if (qParams.Filters != null)
            {
                foreach (Filter filter in qParams.Filters)
                {
                    if (filter.Key == "Name")
                        query = query.Where(c => c.Name.Contains(filter.Value));
                    if (filter.Key == "PlateNumber")
                        query = query.Where(c => c.PlateNumber.Contains(filter.Value));
                    if (filter.Key == "DriverName")
                        query = query.Where(c => c.DriverName.Contains(filter.Value));
                    if (filter.Key == "ContractorName")
                        query = query.Where(c => c.ContractorName.Contains(filter.Value));
                }
            }

            count = await query.CountAsync();
            query = query.OrderBy(qParams.SortBy + (qParams.SortType == SortType.Asc ? " asc" : " desc"));
            query = query.Skip((qParams.PageNumber - 1) * qParams.PageSize).Take(qParams.PageSize);

            return new GridData<NaftkeshDTO>
            {
                Data = await query.ToListAsync(),
                Filters = qParams.Filters,
                SortBy = qParams.SortBy,
                SortType = qParams.SortType,
                PageSize = qParams.PageSize,
                PageNumber = qParams.PageNumber,
                Count = count
            };
        }

        public async Task<NaftkeshDTO> GetById(long id)
        {
            return await _context.Naftkesh
                .Where(n => n.Id == id)
                .Select(n =>
                new NaftkeshDTO
                {
                    Id = n.Id,
                    Name = n.Name,
                    PlateNumber = n.PlateNumber,
                    DriverName = n.DriverName,
                    DriverNationalCode = n.DriverNationalCode,
                    DriverLicenseNumber = n.DriverLicenseNumber,  
                    ContractorId = n.Contractor.Id,
                    ContractorName = n.Contractor.Name
                }).FirstOrDefaultAsync();
        }

        public async Task Update(NaftkeshDTO dto)
        {
            Naftkesh naftkesh = await _context.Naftkesh.FindAsync(dto.Id);
            naftkesh.Id = dto.Id;
            naftkesh.Name = dto.Name;
            naftkesh.PlateNumber = dto.PlateNumber;
            naftkesh.DriverName = dto.DriverName;
            naftkesh.DriverNationalCode = dto.DriverNationalCode;
            naftkesh.DriverLicenseNumber = dto.DriverLicenseNumber;
            naftkesh.ContractorId = dto.ContractorId;
            naftkesh.Active = dto.Active;
        }

        public async Task<bool> Save()
        {
            int status = await _context.SaveChangesAsync();
            if (status > 0)
                return true;

            return false;
        }

        public bool NaftkeshExists(long id)
        {
            return _context.Naftkesh.Any(c => c.Id == id);
        }
    }
}