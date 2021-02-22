namespace NFTAnbarAPI.DTOs
{
    public class NdepoTypeDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long? Gcode { get; set; }
        public long? Gkey { get; set; }
        public bool Active { get; set; }
    }
}