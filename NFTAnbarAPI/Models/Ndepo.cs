using System;
using System.Collections.Generic;

namespace NFTAnbarAPI.Models
{
    public partial class Ndepo
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long? Gcode { get; set; }
        public long? CityId { get; set; }
        public long? NdepoTypeId { get; set; }
        public bool Active { get; set; }
        public DateTime? Cdate { get; set; }
        public long? CuserId { get; set; }
        public DateTime? Mdate { get; set; }
        public long? MuserId { get; set; }
        public DateTime? Ddate { get; set; }
        public long? DuserId { get; set; }
        public DateTime? DaDate { get; set; }
        public long? DaUserId { get; set; }

        public virtual City City { get; set; }
        public virtual NdepoType NdepoType { get; set; }
    }
}
