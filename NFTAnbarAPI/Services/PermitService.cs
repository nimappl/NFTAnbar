using System.Threading.Tasks;
using NFTAnbarAPI.Models;
using NFTAnbarAPI.DTOs;
using System.Linq;
using System.Linq.Dynamic.Core;
using Microsoft.EntityFrameworkCore;
using System;

namespace NFTAnbarAPI.Services
{
    public class PermitService : IPermitService
    {
        private readonly NFTAnbarContext _context;
        public PermitService(NFTAnbarContext context)
        {
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
            IQueryable<PermitDTO> query =
            from permit in _context.Permit
            from permitType in _context.PermitType
            from barname in _context.Barname
            from customer in _context.Customer
            from havaleh in _context.Havaleh
            from city in _context.City
            from sendType in _context.SendType
            from naftkesh in _context.Naftkesh
            from contractor in _context.Contractor
            from depoWorkShift in _context.NdepoWorkShift
            from product in _context.Product
            where permit.PermitTypeId == permitType.Id
            where permit.BarnameId == barname.Id
            where permit.CustomerId == customer.Id
            where permit.HavalehId == havaleh.Id
            where permit.OrgLocationId == city.Id
            where permit.SendTypeId == sendType.Id
            where permit.TransportNaftkeshId == naftkesh.Id
            where permit.ContractorId == contractor.Id
            where permit.NdepoWorkShiftId == depoWorkShift.Id
            where permit.ProductId == product.Id
            select new PermitDTO
            {
                Id = permit.Id,
                BarnameId = permit.BarnameId,
                BarnameName = barname.Name,
                CompanyStationId = permit.CompanyStationId,
                CustomerId = permit.CustomerId,
                CustomerName = customer.Name,
                DirectForwardRequestId = permit.DirectForwardRequestId,
                DischargeTankId = permit.DischargeTankId,
                HavalehId = permit.HavalehId,
                HavalehName = havaleh.Name,
                InTheArea = permit.InTheArea,
                IsWeightedProduct = permit.IsWeightedProduct,
                LoadingTankId = permit.LoadingTankId,
                LocalCustomerLogisticProgramId = permit.LocalCustomerLogisticProgramId,
                LocalCustomerQuotaId = permit.LocalCustomerQuotaId,
                LocalCustomerSellDraftId = permit.LocalCustomerSellDraftId,
                LogisticDetailId = permit.LogisticDetailId,
                OrgLocationId = permit.OrgLocationId,
                OrgLocationName = city.Name,
                ImportExportExchangable = permit.ImportExportExchangable,
                Owid = permit.Owid,
                PermitCode = permit.PermitCode,
                Quantity = permit.Quantity,
                SendTypeId = permit.SendTypeId,
                SendTypeName = sendType.Name,
                TransportNaftkeshId = permit.TransportNaftkeshId,
                TransportNaftkeshName = naftkesh.Name,
                UcdoneStatusId = permit.UcdoneStatusId,
                ContractorId = permit.ContractorId,
                ContractorName = contractor.Name,
                WayBill = permit.WayBill,
                NdepoWorkShiftId = permit.NdepoWorkShiftId,
                NdepoWorkShiftName = depoWorkShift.Name,
                ProductId = permit.ProductId,
                ProductName = product.Name,
                PermitTypeId = permit.PermitTypeId,
                PermitTypeName = permitType.Name,
                Active = permit.Active
            };
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
            return await (
            from permit in _context.Permit
            from permitType in _context.PermitType
            from barname in _context.Barname
            from customer in _context.Customer
            from havaleh in _context.Havaleh
            from city in _context.City
            from sendType in _context.SendType
            from naftkesh in _context.Naftkesh
            from contractor in _context.Contractor
            from depoWorkShift in _context.NdepoWorkShift
            from product in _context.Product
            where permit.PermitTypeId == permitType.Id
            where permit.BarnameId == barname.Id
            where permit.CustomerId == customer.Id
            where permit.HavalehId == havaleh.Id
            where permit.OrgLocationId == city.Id
            where permit.SendTypeId == sendType.Id
            where permit.TransportNaftkeshId == naftkesh.Id
            where permit.ContractorId == contractor.Id
            where permit.NdepoWorkShiftId == depoWorkShift.Id
            where permit.ProductId == product.Id
            where permit.Id == id
            select new PermitDTO
            {
                Id = permit.Id,
                BarnameId = permit.BarnameId,
                BarnameName = barname.Name,
                CompanyStationId = permit.CompanyStationId,
                CustomerId = permit.CustomerId,
                CustomerName = customer.Name,
                DirectForwardRequestId = permit.DirectForwardRequestId,
                DischargeTankId = permit.DischargeTankId,
                HavalehId = permit.HavalehId,
                HavalehName = havaleh.Name,
                InTheArea = permit.InTheArea,
                IsWeightedProduct = permit.IsWeightedProduct,
                LoadingTankId = permit.LoadingTankId,
                LocalCustomerLogisticProgramId = permit.LocalCustomerLogisticProgramId,
                LocalCustomerQuotaId = permit.LocalCustomerQuotaId,
                LocalCustomerSellDraftId = permit.LocalCustomerSellDraftId,
                LogisticDetailId = permit.LogisticDetailId,
                OrgLocationId = permit.OrgLocationId,
                OrgLocationName = city.Name,
                ImportExportExchangable = permit.ImportExportExchangable,
                Owid = permit.Owid,
                PermitCode = permit.PermitCode,
                Quantity = permit.Quantity,
                SendTypeId = permit.SendTypeId,
                SendTypeName = sendType.Name,
                TransportNaftkeshId = permit.TransportNaftkeshId,
                TransportNaftkeshName = naftkesh.Name,
                UcdoneStatusId = permit.UcdoneStatusId,
                ContractorId = permit.ContractorId,
                ContractorName = contractor.Name,
                WayBill = permit.WayBill,
                NdepoWorkShiftId = permit.NdepoWorkShiftId,
                NdepoWorkShiftName = depoWorkShift.Name,
                ProductId = permit.ProductId,
                ProductName = product.Name,
                PermitTypeId = permit.PermitTypeId,
                PermitTypeName = permitType.Name,
                Active = permit.Active
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