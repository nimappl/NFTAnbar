namespace NFTAnbarAPI.DTOs
{
    public class NaftkeshDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string PlateNumber { get; set; }
        public string DriverName { get; set; }
        public long? DriverNationalCode { get; set; }
        public long? DriverLicenseNumber { get; set; }
        public long? ContractorId { get; set; }
        public string ContractorName { get; set; }
        public bool? Active { get; set; }
    }
}