using System;

namespace NFTAnbarAPI.DTOs
{
    public class KhzemanatNamehTypeDTO
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public long? Gkey { get; set; }
        public string Gdesc { get; set; }
        public bool? Active { get; set; }
    }
}