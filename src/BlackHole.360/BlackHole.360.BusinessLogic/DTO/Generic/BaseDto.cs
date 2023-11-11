namespace BlackHole._360.BusinessLogic.DTO.Generic;

public abstract class BaseDto
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
}
