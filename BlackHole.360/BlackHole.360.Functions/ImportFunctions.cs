using Azure.Identity;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Microsoft.Graph;
using Microsoft.Graph.Models;

namespace BlackHole._360.Functions;

public class ImportFunctions
{
#if DEBUG
    const bool IS_DEBUG = true;
#else
    const bool IS_DEBUG = false;
#endif

    private readonly ILogger _logger;

    public ImportFunctions(ILoggerFactory loggerFactory)
    {
        _logger = loggerFactory.CreateLogger<ImportFunctions>();
    }
    
    [Function("ImportActiveDirectoryUsers")]
    public async Task ImportActiveDirectoryUsersAsync([TimerTrigger("* * * * * *", RunOnStartup = IS_DEBUG)] TimerInfo timerInfo, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
        _logger.LogInformation($"Next timer schedule at: {timerInfo.ScheduleStatus?.Next}");

        var authenticationProvider = new DefaultAzureCredential();
        var graphServiceClient = new GraphServiceClient(authenticationProvider);

        var list = new List<User>();

        var usersCollectionResponse = await graphServiceClient.Users.GetAsync((requestConfiguration) =>
        {
            requestConfiguration.QueryParameters.Select = new string[] { "displayName", "userPrincipalName", "jobTitle", "department", "id", "deletedDateTime", "userType" };
            requestConfiguration.QueryParameters.Count = true;
            requestConfiguration.QueryParameters.Top = 999;
        }, cancellationToken);
        
        if (usersCollectionResponse == null)
        {
            return;
        }

        var pageIterator = PageIterator<User, UserCollectionResponse>.CreatePageIterator(
            graphServiceClient,
            usersCollectionResponse,
            (user) =>
            {
                list.Add(user);
                return Task.FromResult(true);
            });

        await pageIterator.IterateAsync(cancellationToken);

        var mappedUsers = list.Select(u => new
        {
            u.Id,
            u.DisplayName,
            Email = u.UserPrincipalName,
            u.Department,
            Role = u.JobTitle,
            Type = u.UserType,
            DeletedAt = u.DeletedDateTime,
        }).ToList();
    }
}
