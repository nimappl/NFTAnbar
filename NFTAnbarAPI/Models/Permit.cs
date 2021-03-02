using System;
using System.Collections.Generic;

namespace NFTAnbarAPI.Models
{
    public partial class Permit
    {
        public long Id { get; set; }
        public long? BarnameId { get; set; }
        public long? CompanyStationId { get; set; }
        public long? CustomerId { get; set; }
        public long? DirectForwardRequestId { get; set; }
        public long? DischargeTankId { get; set; }
        public long? HavalehId { get; set; }
        public bool? InTheArea { get; set; }
        public bool? IsWeightedProduct { get; set; }
        public long? LoadingTankId { get; set; }
        public long? LocalCustomerLogisticProgramId { get; set; }
        public long? LocalCustomerQuotaId { get; set; }
        public long? LocalCustomerSellDraftId { get; set; }
        public long? LogisticDetailId { get; set; }
        public long? OrgLocationId { get; set; }
        public bool? ImportExportExchangable { get; set; }
        public long? Owid { get; set; }
        public long? PermitCode { get; set; }
        public int? Quantity { get; set; }
        public long? SendTypeId { get; set; }
        public long? TransportNaftkeshId { get; set; }
        public long? UcdoneStatusId { get; set; }
        public long? ContractorId { get; set; }
        public long? WayBill { get; set; }
        public long? NdepoWorkShiftId { get; set; }
        public long? ProductId { get; set; }
        public long? PermitTypeId { get; set; }
        public bool? Active { get; set; }
        public long? CuserId { get; set; }
        public DateTime? Cdate { get; set; }
        public long? MuserId { get; set; }
        public DateTime? Mdate { get; set; }
        public long? DuserId { get; set; }
        public DateTime? Ddate { get; set; }
        public long? DaUserId { get; set; }
        public DateTime? DaDate { get; set; }

        public virtual Barname Barname { get; set; }
        public virtual Contractor Contractor { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual Havaleh Havaleh { get; set; }
        public virtual NdepoWorkShift NdepoWorkShift { get; set; }
        public virtual City OrgLocation { get; set; }
        public virtual PermitType PermitType { get; set; }
        public virtual Product Product { get; set; }
        public virtual SendType SendType { get; set; }
        public virtual Naftkesh TransportNaftkesh { get; set; }
    }
}
