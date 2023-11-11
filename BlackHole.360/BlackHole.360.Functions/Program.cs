using Microsoft.Extensions.Hosting;
using BlackHole._360.DataAccess;
using BlackHole._360.BusinessLogic;
using BlackHole._360.Common;
using Microsoft.Extensions.Configuration;

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureAppConfiguration(x => x.AddJsonFile("local.settings.json"))
    .ConfigureServices((hostBuilderContext, services) =>
    {
        services.AddConfiguration(hostBuilderContext.Configuration)
                .AddDataAccess(hostBuilderContext.Configuration)
                .AddBusinessServices();
    })
    .Build();

host.Run();
