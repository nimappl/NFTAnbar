using NFTAnbarAPI.Models;

namespace NFTAnbarAPI.DTOs
{
    public class ConvertDTO
    {
        public static CityDTO CityModelToDTO(City city) =>
            new CityDTO
            {
                Id = city.Id,
                Name = city.Name,
                Description = city.Description,
                Active = city.Active
            };

        public static City CityDTOToModel(CityDTO dto) =>
            new City
            {
                Id = dto.Id,
                Name = dto.Name,
                Description = dto.Description,
                Active = dto.Active
            };

        public static NdepoDTO NdepoModelToDTO(Ndepo depo) =>
            new NdepoDTO
            {
                Id = depo.Id,
                Name = depo.Name,
                Gcode = depo.Gcode,
                CityId = depo.CityId,
                NdepoTypeId = depo.NdepoTypeId,
                Active = depo.Active
            };

        public static Ndepo NdepoDTOToModel(NdepoDTO dto) =>
            new Ndepo
            {
                Id = dto.Id,
                Name = dto.Name,
                Gcode = dto.Gcode,
                CityId = dto.CityId,
                NdepoTypeId = dto.NdepoTypeId,
                Active = dto.Active
            };

        public static NdepoTypeDTO NdepoTypeModelToDTO(NdepoType depoType) =>
            new NdepoTypeDTO
            {
                Id = depoType.Id,
                Name = depoType.Name,
                Gcode = depoType.Gcode,
                Gkey = depoType.Gkey,
                Active = depoType.Active
            };

        public static NdepoType NdepoTypeDTOToModel(NdepoTypeDTO dto) =>
            new NdepoType
            {
                Id = dto.Id,
                Name = dto.Name,
                Gcode = dto.Gcode,
                Gkey = dto.Gkey,
                Active = dto.Active
            };

        public static BarnameDTO BarnameModelToDTO(Barname barname) =>
            new BarnameDTO
            {
                Id = barname.Id,
                Name = barname.Name,
                Active = barname.Active
            };

        public static Barname BarnameDTOToModel(BarnameDTO dto) =>
            new Barname
            {
                Id = dto.Id,
                Name = dto.Name,
                Active = dto.Active
            };

        public static ContractorDTO ContractorModelToDTO(Contractor contractor) =>
            new ContractorDTO
            {
                Id = contractor.Id,
                Name = contractor.Name,
                Active = contractor.Active
            };

        public static Contractor ContractorDTOToModel(ContractorDTO dto) =>
            new Contractor
            {
                Id = dto.Id,
                Name = dto.Name,
                Active = dto.Active
            };

        public static CustomerDTO CustomerModelToDTO(Customer customer) =>
            new CustomerDTO
            {
                Id = customer.Id,
                Name = customer.Name,
                NationalCode = customer.NationalCode,
                Gcode = customer.Gcode,
                Gkey = customer.Gkey,
                Active = customer.Active
            };

        public static Customer CustomerDTOToModel(CustomerDTO dto) =>
            new Customer
            {
                Id = dto.Id,
                Name = dto.Name,
                NationalCode = dto.NationalCode,
                Gcode = dto.Gcode,
                Gkey = dto.Gkey,
                Active = dto.Active
            };

        public static HavalehDTO HavalehModelToDTO(Havaleh havaleh) =>
            new HavalehDTO
            {
                Id = havaleh.Id,
                Name = havaleh.Name,
                Active = havaleh.Active
            };

        public static Havaleh HavalehDTOToModel(HavalehDTO dto) =>
            new Havaleh
            {
                Id = dto.Id,
                Name = dto.Name,
                Active = dto.Active
            };
        
        public static NaftkeshDTO NaftkeshModelToDTO(Naftkesh naftkesh) =>
            new NaftkeshDTO
            {
                Id = naftkesh.Id,
                Name = naftkesh.Name,
                PlateNumber = naftkesh.PlateNumber,
                DriverName = naftkesh.DriverName,
                DriverNationalCode = naftkesh.DriverNationalCode,
                DriverLicenseNumber = naftkesh.DriverLicenseNumber,  
                ContractorId = naftkesh.Contractor.Id,
                ContractorName = naftkesh.Contractor.Name
            };

        public static Naftkesh NaftkeshDTOToModel(NaftkeshDTO dto) =>
            new Naftkesh
            {
                Id = dto.Id,
                Name = dto.Name,
                PlateNumber = dto.PlateNumber,
                DriverName = dto.DriverName,
                DriverNationalCode = dto.DriverNationalCode,
                DriverLicenseNumber = dto.DriverLicenseNumber,
                ContractorId = dto.ContractorId,
                Active = dto.Active
            };

        public static NdepoWorkShiftDTO NdepoWorkShiftModelToDTO(NdepoWorkShift workShift) =>
            new NdepoWorkShiftDTO
            {
                Id = workShift.Id,
                Name = workShift.Name,
                Active = workShift.Active
            };

        public static NdepoWorkShift NdepoWorkShiftDTOToModel(NdepoWorkShiftDTO dto) =>
            new NdepoWorkShift
            {
                Id = dto.Id,
                Name = dto.Name,
                Active = dto.Active
            };

        public static PermitTypeDTO PermitTypeModelToDTO(PermitType permitType) =>
            new PermitTypeDTO
            {
                Id = permitType.Id,
                Name = permitType.Name,
                Active = permitType.Active
            };

        public static PermitType PermitTypeDTOToModel(PermitTypeDTO dto) =>
            new PermitType
            {
                Id = dto.Id,
                Name = dto.Name,
                Active = dto.Active
            };

        public static ProductDTO ProductModelToDTO(Product product) =>
            new ProductDTO
            {
                Id = product.Id,
                Name = product.Name,
                Active = product.Active
            };

        public static Product ProductDTOToModel(ProductDTO dto) =>
            new Product
            {
                Id = dto.Id,
                Name = dto.Name,
                Active = dto.Active
            };

        public static SendTypeDTO SendTypeModelToDTO(SendType sendType) =>
            new SendTypeDTO
            {
                Id = sendType.Id,
                Name = sendType.Name,
                Active = sendType.Active
            };

        public static SendType SendTypeDTOToModel(SendTypeDTO dto) =>
            new SendType
            {
                Id = dto.Id,
                Name = dto.Name,
                Active = dto.Active
            };

        public static Permit PermitDTOToModel(PermitDTO dto) =>
            new Permit
            {
                Id = dto.Id,
                BarnameId = dto.BarnameId,
                CompanyStationId = dto.CompanyStationId,
                CustomerId = dto.CustomerId,
                DirectForwardRequestId = dto.DirectForwardRequestId,
                DischargeTankId = dto.DischargeTankId,
                HavalehId = dto.HavalehId,
                InTheArea = dto.InTheArea,
                IsWeightedProduct = dto.IsWeightedProduct,
                LoadingTankId = dto.LoadingTankId,
                LocalCustomerLogisticProgramId = dto.LocalCustomerLogisticProgramId,
                LocalCustomerQuotaId = dto.LocalCustomerQuotaId,
                LocalCustomerSellDraftId = dto.LocalCustomerSellDraftId,
                LogisticDetailId = dto.LogisticDetailId,
                OrgLocationId = dto.OrgLocationId,
                ImportExportExchangable = dto.ImportExportExchangable,
                Owid = dto.Owid,
                PermitCode = dto.PermitCode,
                Quantity = dto.Quantity,
                SendTypeId = dto.SendTypeId,
                TransportNaftkeshId = dto.TransportNaftkeshId,
                ContractorId = dto.ContractorId,
                WayBill = dto.WayBill,
                NdepoWorkShiftId = dto.NdepoWorkShiftId,
                ProductId = dto.ProductId,
                PermitTypeId = dto.PermitTypeId,
                Active = dto.Active
            };
    }
}