using Azure.Identity;
using Azure.Storage.Blobs;
using BlackHole._360.BusinessLogic.Services;
using BlackHole._360.DataAccess.Abstractions;
using BlackHole._360.Domain.Enums;
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
    private readonly ImportService _importService;

    public ImportFunctions(ILoggerFactory loggerFactory, ImportService importService)
    {
        _logger = loggerFactory.CreateLogger<ImportFunctions>();
        _importService = importService;
    }
    
    [Function(nameof(DownloadActiveDirectoryUsers))]
    public async Task DownloadActiveDirectoryUsers([TimerTrigger("* * * * * *", RunOnStartup = IS_DEBUG)] TimerInfo timerInfo, CancellationToken cancellationToken)
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

        var mappedUsers = list.Select(u => new Domain.Entities.User
        {
            InternalId = u.Id ?? string.Empty,
            Name = u.DisplayName ?? string.Empty,
            Email = u.UserPrincipalName ?? string.Empty,
            JobTitleId = (JobTitle)Enum.Parse(typeof(JobTitle), u.JobTitle ?? "Unknown"),
            //Role = u.Department,
            //DeletedAt = u.DeletedDateTime!.Value,
            //Deleted = 
        }).ToList();

        //var client = new BlobContainerClient(new Uri("https://127.0.0.1:10000/devstoreaccount1/container-name"), new DefaultAzureCredential());
        await _importService.ImportUsersAsync(mappedUsers);
        _logger.LogInformation($"C# Timer trigger function finished file upload at: {DateTime.Now}");
    }


    [Function(nameof(ImportActiveDirectoryUsers))]
    public async Task ImportActiveDirectoryUsers([BlobTrigger("samples-workitems/{name}", Connection = "Storage")] Stream stream, string name)
    {
        using var blobStreamReader = new StreamReader(stream);
        var content = await blobStreamReader.ReadToEndAsync();
        
        _logger.LogInformation($"C# Blob trigger function Processed blob\n Name: {name} \n Data: {content}");
    }
}
