using System;
using System.Collections.Generic;

namespace NFTAnbarAPI.Models
{
    public partial class Naftkesh
    {
        public Naftkesh()
        {
            Permit = new HashSet<Permit>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public string PlateNumber { get; set; }
        public string DriverName { get; set; }
        public long? DriverNationalCode { get; set; }
        public long? DriverLicenseNumber { get; set; }
        public long? ContractorId { get; set; }
        public bool? Active { get; set; }
        public long? CuserId { get; set; }
        public DateTime? Cdate { get; set; }
        public long? MuserId { get; set; }
        public DateTime? Mdate { get; set; }
        public long? DuserId { get; set; }
        public DateTime? Ddate { get; set; }
        public long? DaUserId { get; set; }
        public DateTime? DaDate { get; set; }

        public virtual Contractor Contractor { get; set; }
        public virtual ICollection<Permit> Permit { get; set; }
    }
}
