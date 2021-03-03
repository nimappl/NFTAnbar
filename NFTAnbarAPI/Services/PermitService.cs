using System.Threading.Tasks;
using NFTAnbarAPI.Models;
using NFTAnbarAPI.DTOs;
using System.Linq;
using System.Linq.Dynamic.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using AutoMapper;

namespace NFTAnbarAPI.Services
{
    public class PermitService : IPermitService
    {
        private readonly NFTAnbarContext _context;
        private readonly IMapper _mapper;
        public PermitService(NFTAnbarContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public void Create(PermitDTO dto)
        {
            _context.Permit.Add(ConvertDTO.PermitDTOToModel(dto));
        }

        public async Task Delete(long id)
        {
            var permit = await _context.Permit.FindAsync(id);
            _context.Permit.Remove(permit);
        }

        public async Task<GridData<PermitDTO>> Get(GridData<PermitDTO> qParams)
        {
            IQueryable<PermitDTO> query = _context.Permit.Select(p =>
            new PermitDTO
            {
                Id = p.Id,
                BarnameId = p.BarnameId,
                BarnameName = p.Barname.Name,
                CompanyStationId = p.CompanyStationId,
                CustomerId = p.CustomerId,
                CustomerName = p.Customer.Name,
                DirectForwardRequestId = p.DirectForwardRequestId,
                DischargeTankId = p.DischargeTankId,
                HavalehId = p.HavalehId,
                HavalehName = p.Havaleh.Name,
                InTheArea = p.InTheArea,
                IsWeightedProduct = p.IsWeightedProduct,
                LoadingTankId = p.LoadingTankId,
                LocalCustomerLogisticProgramId = p.LocalCustomerLogisticProgramId,
                LocalCustomerQuotaId = p.LocalCustomerQuotaId,
                LocalCustomerSellDraftId = p.LocalCustomerSellDraftId,
                LogisticDetailId = p.LogisticDetailId,
                OrgLocationId = p.OrgLocationId,
                OrgLocationName = p.OrgLocation.Name,
                ImportExportExchangable = p.ImportExportExchangable,
                Owid = p.Owid,
                PermitCode = p.PermitCode,
                Quantity = p.Quantity,
                SendTypeId = p.SendTypeId,
                SendTypeName = p.SendType.Name,
                TransportNaftkeshId = p.TransportNaftkeshId,
                TransportNaftkeshName = p.TransportNaftkesh.Name,
                UcdoneStatusId = p.UcdoneStatusId,
                ContractorId = p.ContractorId,
                ContractorName = p.Contractor.Name,
                WayBill = p.WayBill,
                NdepoWorkShiftId = p.NdepoWorkShiftId,
                NdepoWorkShiftName = p.NdepoWorkShift.Name,
                ProductId = p.ProductId,
                ProductName = p.Product.Name,
                PermitTypeId = p.PermitTypeId,
                PermitTypeName = p.PermitType.Name,
                Active = p.Active
            });
            int count;
            if (qParams.Filters != null)
            {
                foreach (Filter filter in qParams.Filters)
                {
                    if (filter.Key == "BarnameName")
                        query = query.Where(p => p.BarnameName.Contains(filter.Value));
                    if (filter.Key == "CustomerName")
                        query = query.Where(p => p.CustomerName.Contains(filter.Value));
                    if (filter.Key == "PermitCode")
                        query = query.Where(p => p.PermitCode == Int32.Parse(filter.Value));
                    if (filter.Key == "TransportNaftkeshName")
                        query = query.Where(p => p.TransportNaftkeshName.Contains(filter.Value));
                    if (filter.Key == "ProductName")
                        query = query.Where(p => p.ProductName.Contains(filter.Value));
                }
            }

            count = await query.CountAsync();
            query = query.OrderBy(qParams.SortBy + (qParams.SortType == SortType.Asc ? " asc" : " desc"));
            query = query.Skip((qParams.PageNumber - 1) * qParams.PageSize).Take(qParams.PageSize);

            return new GridData<PermitDTO>
            {
                Data = await query.ToListAsync(),
                Count = count,
                PageNumber = qParams.PageNumber,
                PageSize = qParams.PageSize,
                Filters = qParams.Filters,
                SortType = qParams.SortType,
                SortBy = qParams.SortBy
            };
        }

        public async Task<PermitDTO> GetById(long id)
        {
            return await _context.Permit.Where(p => p.Id == id).Select(p =>
            new PermitDTO
            {
                Id = p.Id,
                BarnameId = p.BarnameId,
                BarnameName = p.Barname.Name,
                CompanyStationId = p.CompanyStationId,
                CustomerId = p.CustomerId,
                CustomerName = p.Customer.Name,
                DirectForwardRequestId = p.DirectForwardRequestId,
                DischargeTankId = p.DischargeTankId,
                HavalehId = p.HavalehId,
                HavalehName = p.Havaleh.Name,
                InTheArea = p.InTheArea,
                IsWeightedProduct = p.IsWeightedProduct,
                LoadingTankId = p.LoadingTankId,
                LocalCustomerLogisticProgramId = p.LocalCustomerLogisticProgramId,
                LocalCustomerQuotaId = p.LocalCustomerQuotaId,
                LocalCustomerSellDraftId = p.LocalCustomerSellDraftId,
                LogisticDetailId = p.LogisticDetailId,
                OrgLocationId = p.OrgLocationId,
                OrgLocationName = p.OrgLocation.Name,
                ImportExportExchangable = p.ImportExportExchangable,
                Owid = p.Owid,
                PermitCode = p.PermitCode,
                Quantity = p.Quantity,
                SendTypeId = p.SendTypeId,
                SendTypeName = p.SendType.Name,
                TransportNaftkeshId = p.TransportNaftkeshId,
                TransportNaftkeshName = p.TransportNaftkesh.Name,
                UcdoneStatusId = p.UcdoneStatusId,
                ContractorId = p.ContractorId,
                ContractorName = p.Contractor.Name,
                WayBill = p.WayBill,
                NdepoWorkShiftId = p.NdepoWorkShiftId,
                NdepoWorkShiftName = p.NdepoWorkShift.Name,
                ProductId = p.ProductId,
                ProductName = p.Product.Name,
                PermitTypeId = p.PermitTypeId,
                PermitTypeName = p.PermitType.Name,
                Active = p.Active
            }).FirstOrDefaultAsync();
        }

        public async Task Update(PermitDTO dto)
        {
            Permit permit = await _context.Permit.FindAsync(dto.Id);
            permit.Id = dto.Id;
            permit.BarnameId = dto.BarnameId;
            permit.CompanyStationId = dto.CompanyStationId;
            permit.CustomerId = dto.CustomerId;
            permit.DirectForwardRequestId = dto.DirectForwardRequestId;
            permit.DischargeTankId = dto.DischargeTankId;
            permit.HavalehId = dto.HavalehId;
            permit.InTheArea = dto.InTheArea;
            permit.IsWeightedProduct = dto.IsWeightedProduct;
            permit.LoadingTankId = dto.LoadingTankId;
            permit.LocalCustomerLogisticProgramId = dto.LocalCustomerLogisticProgramId;
            permit.LocalCustomerQuotaId = dto.LocalCustomerQuotaId;
            permit.LocalCustomerSellDraftId = dto.LocalCustomerSellDraftId;
            permit.LogisticDetailId = dto.LogisticDetailId;
            permit.OrgLocationId = dto.OrgLocationId;
            permit.ImportExportExchangable = dto.ImportExportExchangable;
            permit.Owid = dto.Owid;
            permit.PermitCode = dto.PermitCode;
            permit.Quantity = dto.Quantity;
            permit.SendTypeId = dto.SendTypeId;
            permit.TransportNaftkeshId = dto.TransportNaftkeshId;
            permit.UcdoneStatusId = dto.UcdoneStatusId;
            permit.ContractorId = dto.ContractorId;
            permit.WayBill = dto.WayBill;
            permit.NdepoWorkShiftId = dto.NdepoWorkShiftId;
            permit.ProductId = dto.ProductId;
            permit.PermitTypeId = dto.PermitTypeId;
            permit.Active = dto.Active;
        }

        public async Task<bool> Save()
        {
            int status = await _context.SaveChangesAsync();
            if (status > 0)
                return true;

            return false;
        }

        public bool PermitExists(long id)
        {
            return _context.Permit.Any(c => c.Id == id);
        }
    }
}