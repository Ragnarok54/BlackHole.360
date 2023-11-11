namespace BlackHole._360.BusinessLogic.DTO.Import;

public class ImportUserDto
{
    public string DisplayName { get; set; }
    public string Email { get; set; }
    public string InternalId { get; set; }
    public string JobTitle {  get; set; }
    public string Department { get; set; }

    public ImportDepartmentDetailsDto? DepartmentDetails { get; set; }
}
