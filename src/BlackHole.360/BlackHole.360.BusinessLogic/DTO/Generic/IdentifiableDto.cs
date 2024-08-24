using BlackHole._360.Domain.Abstractions.Interfaces;

namespace BlackHole._360.BusinessLogic.DTO.Generic;

public abstract class IdentifiableDto : IIdentifiable
{
    public Guid Id { get; set; }
}
