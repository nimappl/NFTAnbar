namespace NFTAnbarAPI.DTOs
{
    public class CustomerDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long? NationalCode { get; set; }
        public long? Gcode { get; set; }
        public long? Gkey { get; set; }
    }
}