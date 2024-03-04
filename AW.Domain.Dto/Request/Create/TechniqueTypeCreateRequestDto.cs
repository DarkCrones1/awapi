namespace AW.Domain.Dto.Request.Create;

public class TechniqueTypeCreateRequestDto : BaseCatalogCreateRequestDto
{
    private IEnumerable<PropertyTechniqueTypeCreateRequestDto> _propertys;

    public TechniqueTypeCreateRequestDto()
    {
        _propertys = new List<PropertyTechniqueTypeCreateRequestDto>();
    }

    public IEnumerable<PropertyTechniqueTypeCreateRequestDto> Propertys { get => _propertys; set => _propertys = value ?? new List<PropertyTechniqueTypeCreateRequestDto>(); }
}