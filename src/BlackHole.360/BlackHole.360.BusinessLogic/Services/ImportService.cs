using BlackHole._360.BusinessLogic.DTO.Import;
using BlackHole._360.DataAccess.Abstractions;
using BlackHole._360.Domain.Entities;

using JobTitle = BlackHole._360.Domain.Enums.JobTitle;

namespace BlackHole._360.BusinessLogic.Services;

public class ImportService(IUnitOfWork unitOfWork) : BaseService(unitOfWork)
{
    public async Task ImportUsersAsync(IEnumerable<ImportUserDto> importList, CancellationToken cancellationToken)
    {
        //var importList = await JsonSerializer.DeserializeAsync<IEnumerable<ImportUserDto>>(stream, cancellationToken: cancellationToken) ?? Enumerable.Empty<ImportUserDto>();

        var importDepartmentDetails = new List<ImportDepartmentDetailsDto>();

        foreach (var importUser in importList)
        {
            if (!string.IsNullOrEmpty(importUser.Department))
            {
                var department = string.Concat(importUser.Department?.TakeWhile(char.IsLetter) ?? string.Empty);
                var group = importUser.Department?.TrimStart(department.ToCharArray()).FirstOrDefault().ToString();
                var subgroup = string.Concat(importUser.Department?.Reverse().TakeWhile(c => c != '.').Reverse() ?? []);

                importUser.DepartmentDetails = new ImportDepartmentDetailsDto
                {
                    Department = department,
                    Group = group,
                    Subgroup = subgroup
                };

                importDepartmentDetails.Add(importUser.DepartmentDetails);
            }
        }

        var subGroupMap = await ImportDeparmentDetailsAsync(importDepartmentDetails, cancellationToken);

        foreach (var importUser in importList)
        {
            var jobTitle = (JobTitle)Enum.Parse(typeof(JobTitle), string.IsNullOrEmpty(importUser.JobTitle) ? "Unknown" : importUser.JobTitle);

            var user = new User
            {
                InternalId = importUser.InternalId,
                Name = importUser.DisplayName,
                Email = importUser.Email,
                JobTitleId = jobTitle,
                SubGroup = importUser.DepartmentDetails != null?  subGroupMap[importUser.DepartmentDetails!] : null,
            };

            await UnitOfWork.UserRepository.AddAsync(user, cancellationToken);
        }

        await UnitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task<IDictionary<ImportDepartmentDetailsDto, SubGroup>> ImportDeparmentDetailsAsync(IEnumerable<ImportDepartmentDetailsDto> importDepartmentDetails, CancellationToken cancellationToken)
    {
        var subgroupDictionary = new Dictionary<ImportDepartmentDetailsDto, SubGroup>();
        var importDepartments = importDepartmentDetails.Distinct().GroupBy(a => a.Department).ToList();

        foreach (var importDepartment in importDepartments)
        {
            var importGroups = importDepartment.GroupBy(a => a.Group);

            var department = new Department
            {
                Name = importDepartment.Key!
            };

            foreach (var importGroup in importGroups)
            {
                var group = new Group
                {
                    Department = department,
                    Name = importGroup.Key!,
                };

                var subgroups = importGroup.Select(ig => ig.Subgroup).ToList();

                foreach (var importSubgroup in subgroups)
                {
                    var subgroup = new SubGroup
                    {
                        Name = importSubgroup!
                    };

                    group.SubGroups.Add(subgroup);

                    subgroupDictionary.Add(new ImportDepartmentDetailsDto 
                                           {
                                               Department = department.Name,
                                               Group = group.Name,
                                               Subgroup = importSubgroup!
                                           },
                                           subgroup);
                }

                await UnitOfWork.GroupRepository.AddAsync(group, cancellationToken);
            }
        }

        return subgroupDictionary;
    }
}
