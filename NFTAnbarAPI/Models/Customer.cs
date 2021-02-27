using System;
using System.Collections.Generic;

namespace NFTAnbarAPI.Models
{
    public partial class Customer
    {
        public Customer()
        {
            Permit = new HashSet<Permit>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public long? NationalCode { get; set; }
        public long? Gcode { get; set; }
        public long? Gkey { get; set; }
        public bool? Active { get; set; }
        public long? CuserId { get; set; }
        public DateTime? Cdate { get; set; }
        public long? MuserId { get; set; }
        public DateTime? Mdate { get; set; }
        public long? DuserId { get; set; }
        public DateTime? Ddate { get; set; }
        public long? DaUserId { get; set; }
        public DateTime? DaDate { get; set; }

        public virtual ICollection<Permit> Permit { get; set; }
    }
}
