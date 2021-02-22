using NFTAnbarAPI.Models;

namespace NFTAnbarAPI.DTOs
{
    public class ConvertDTO
    {
        public static CityDTO CityModelToDTO(City city) =>
            new CityDTO
            {
                Id = city.Id,
                Name = city.Name,
                Description = city.Description,
                Active = city.Active
            };

        public static City CityDTOToModel(CityDTO dto) =>
            new City
            {
                Id = dto.Id,
                Name = dto.Name,
                Description = dto.Description,
                Active = dto.Active
            };

        public static NdepoDTO NdepoModelToDTO(Ndepo depo) =>
            new NdepoDTO
            {
                Id = depo.Id,
                Name = depo.Name,
                Gcode = depo.Gcode,
                CityId = depo.CityId,
                NdepoTypeId = depo.NdepoTypeId,
                Active = depo.Active
            };

        public static Ndepo NdepoDTOToModel(NdepoDTO dto) =>
            new Ndepo
            {
                Id = dto.Id,
                Name = dto.Name,
                Gcode = dto.Gcode,
                CityId = dto.CityId,
                NdepoTypeId = dto.NdepoTypeId,
                Active = dto.Active
            };

        public static NdepoTypeDTO NdepoTypeModelToDTO(NdepoType depoType) =>
            new NdepoTypeDTO
            {
                Id = depoType.Id,
                Name = depoType.Name,
                Gcode = depoType.Gcode,
                Gkey = depoType.Gkey,
                Active = depoType.Active
            };

        public static NdepoType NdepoTypeDTOToModel(NdepoTypeDTO dto) =>
            new NdepoType
            {
                Id = dto.Id,
                Name = dto.Name,
                Gcode = dto.Gcode,
                Gkey = dto.Gkey,
                Active = dto.Active
            };
    }
}