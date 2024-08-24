﻿using BlackHole._360.Domain.Abstractions.Interfaces;

namespace BlackHole._360.BusinessLogic.DTO.Generic;

public abstract class BaseDto : IdentifiableDto, INameEntity
{
    public required string Name { get; set; }
}
