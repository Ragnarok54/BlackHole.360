using BlackHole._360.BusinessLogic.DTO.Import;
using BlackHole._360.DataAccess.Abstractions;
using BlackHole._360.Domain.Entities;

using System.Text.Json;

using JobTitle = BlackHole._360.Domain.Enums.JobTitle;

namespace BlackHole._360.BusinessLogic.Services;

public class ImportService : BaseService
{
    public ImportService(IUnitOfWork unitOfWork) : base(unitOfWork) { }

    public async Task ImportUsersAsync(Stream stream, CancellationToken cancellationToken)
    {
        var importList = await JsonSerializer.DeserializeAsync<IEnumerable<ImportUserDto>>(stream, cancellationToken: cancellationToken) ?? Enumerable.Empty<ImportUserDto>();

        var importDepartmentDetails = new List<ImportDepartmentDetailsDto>();

        foreach (var importUser in importList)
        {
            var department = string.Concat(importUser.Department.TakeWhile(char.IsLetter));
            var group = importUser.Department.TrimStart(department.ToCharArray()).First().ToString();
            var subgroup = importUser.Department.Reverse().TakeWhile(c => c != '.').Reverse().ToString();

            importUser.DepartmentDetails = new ImportDepartmentDetailsDto
            {
                Department = department,
                Group = group,
                Subgroup = subgroup
            };

            importDepartmentDetails.Add(importUser.DepartmentDetails);
        }

        var subGroupMap = await ImportDeparmentDetailsAsync(importDepartmentDetails, cancellationToken);

        foreach (var importUser in importList)
        {
            var jobTitle = (JobTitle)Enum.Parse(typeof(JobTitle), importUser.JobTitle ?? "Unknown");

            var user = new User
            {
                InternalId = importUser.InternalId,
                Name = importUser.DisplayName,
                Email = importUser.Email,
                JobTitleId = jobTitle,
                SubGroup = subGroupMap[importUser.DepartmentDetails!]
            };

            await UnitOfWork.UserRepository.AddAsync(user, cancellationToken);
        }

        await UnitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task<IDictionary<ImportDepartmentDetailsDto, SubGroup>> ImportDeparmentDetailsAsync(IEnumerable<ImportDepartmentDetailsDto> importDepartmentDetails, CancellationToken cancellationToken)
    {
        var subgroupDictionary = new Dictionary<ImportDepartmentDetailsDto, SubGroup>();
        var importDepartments = importDepartmentDetails.GroupBy(a => a.Department);

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
