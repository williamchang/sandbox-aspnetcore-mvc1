/**
@file
    AppExtensions.cs
@author
    William Chang
@version
    0.1
@date
    - Created: 2017-06-04
    - Modified: 2017-06-05
    .
@note
    References:
    - General:
        - https://andrewlock.net/configuring-environment-specific-services-in-asp-net-core/
        - https://andrewlock.net/configuring-environment-specific-services-in-asp-net-core-part-2/
        .
    - Dependency Injection Pass Parameters To Constructors:
        - https://www.codefluff.com/dependency-injection-with-asp-net-core-mvc/
        - https://stackoverflow.com/questions/34834295/asp-net-5-dependency-injection-inject-with-parameters
        .
    - IOptions:
        - https://stackoverflow.com/questions/39752174/using-iconfigureoptions-to-configure-an-injected-dependency
        - http://henkmollema.github.io/advanced-options-configuration-in-asp.net-core/
        .
    - IConfigureOptions:
        - https://andrewlock.net/access-services-inside-options-and-startup-using-configureoptions/
        .
    - ServiceDescriptor:
        - https://stackoverflow.com/questions/39726203/why-does-asp-net-core-support-several-ways-for-registering-a-service
        .
    - SQLite:
        - https://github.com/aspnet/Microsoft.Data.Sqlite/wiki/Connection-Strings
        .
    .
*/

using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using SandboxAspnetCoreMvc1.Web.Configuration;

namespace SandboxAspnetCoreMvc1.Web {

public static class AppExtensions
{
    public static IServiceCollection AddHead(this IServiceCollection services, IConfiguration configuration)
    {
        if(services == null)
        {
            throw new ArgumentNullException(nameof(services));
        }

        // Register the IConfiguration instance which AppSettings binds against.
        services.Configure<Configuration.AppSettings>(configuration);

        // Add the configuration singleton.
        services.AddSingleton<IConfiguration>(configuration);

        services.AddSingleton<IAppHead, AppHead>();

        return services;
    }

    public static IServiceCollection AddData(this IServiceCollection services, IConfiguration configuration)
    {
        if(services == null)
        {
            throw new ArgumentNullException(nameof(services));
        }

        string sqlConnectionString = configuration["Data:ConnectionStrings:SQLite"];

        if(!String.IsNullOrEmpty(sqlConnectionString)) {
            sqlConnectionString = sqlConnectionString.Replace("|DataDirectory|\\", String.Concat(System.IO.Path.Combine(configuration["ASPNETCORE_CONTENTROOT"], "App_Data"), System.IO.Path.DirectorySeparatorChar));
        }

        services.AddScoped<Data.Interfaces.ISystemRepository>(x => new Data.SQLite.Repositories.SystemRepository(sqlConnectionString));
        services.AddScoped<Data.Interfaces.IUserRepository>(x => new Data.SQLite.Repositories.UserRepository(sqlConnectionString));

        return services;
    }
}

public interface IAppHead
{
    Microsoft.Extensions.Configuration.IConfiguration GetConfiguration();
    Configuration.AppSettings GetSettings();
}

public class AppHead : IAppHead
{
    protected readonly Microsoft.Extensions.Configuration.IConfiguration _configuration;
    protected readonly Configuration.AppSettings _settings;

    public AppHead(Microsoft.Extensions.Configuration.IConfiguration configuration, Microsoft.Extensions.Options.IOptions<Configuration.AppSettings> settings)
    {
        _configuration = configuration;
        _settings = settings.Value;
    }

    public IConfiguration GetConfiguration()
    {
        return _configuration;
    }

    public AppSettings GetSettings()
    {
        return _settings;
    }
}

}
