namespace NFTAnbarAPI.DTOs
{
    public class PermitDTO
    {
        public long Id { get; set; }
        public long? BarnameId { get; set; }
        public string BarnameName { get; set; }
        public long? CompanyStationId { get; set; }
        public long? CustomerId { get; set; }
        public string CustomerName { get; set; }
        public long? DirectForwardRequestId { get; set; }
        public long? DischargeTankId { get; set; }
        public long? HavalehId { get; set; }
        public string HavalehName { get; set; }
        public bool? InTheArea { get; set; }
        public bool? IsWeightedProduct { get; set; }
        public long? LoadingTankId { get; set; }
        public long? LocalCustomerLogisticProgramId { get; set; }
        public long? LocalCustomerQuotaId { get; set; }
        public long? LocalCustomerSellDraftId { get; set; }
        public long? LogisticDetailId { get; set; }
        public long? OrgLocationId { get; set; }
        public string OrgLocationName { get; set; }
        public bool? ImportExportExchangable { get; set; }
        public long? Owid { get; set; }
        public long? PermitCode { get; set; }
        public int? Quantity { get; set; }
        public long? SendTypeId { get; set; }
        public string SendTypeName { get; set; }
        public long? TransportNaftkeshId { get; set; }
        public string TransportNaftkeshName { get; set; }
        public long? UcdoneStatusId { get; set; }
        public long? ContractorId { get; set; }
        public string ContractorName { get; set; }
        public long? WayBill { get; set; }
        public long? NdepoWorkShiftId { get; set; }
        public string NdepoWorkShiftName { get; set; }
        public long? ProductId { get; set; }
        public string ProductName { get; set; }
        public long? PermitTypeId { get; set; }
        public string PermitTypeName { get; set; }
        public bool? Active { get; set; }
    }
}