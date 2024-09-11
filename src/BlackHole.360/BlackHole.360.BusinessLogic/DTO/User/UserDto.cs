using BlackHole._360.Domain.Enums;

namespace BlackHole._360.BusinessLogic.DTO.User;

public class UserDto
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Email { get; set; }
    public JobTitle JobTitleId { get; set; }
    public Guid? SubgroupId { get; set; }
    public string? Department { get; set; }


    public static implicit operator UserDto(Domain.Entities.User user)
        => new()
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
            JobTitleId = user.JobTitleId,
            SubgroupId = user.SubgroupId,
            Department = string.IsNullOrEmpty(user.SubGroup?.Name) ? string.Empty: user.SubGroup?.Group.Department.Name + user.SubGroup?.Group.Name + "." + user.SubGroup?.Name
        };
}
