namespace NFTAnbarAPI.DTOs
{
    public class NdepoDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long? Gcode { get; set; }
        public long? CityId { get; set; }
        public string City { get; set; }
        public long? NdepoTypeId { get; set; }
        public string NdepoType { get; set; }
        public bool Active { get; set; }

    }
}