using System;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace SandboxAspnetCoreWebApi.Service.Tests.Integration {

/// <summary>A base class test which hosts the target web project in an in-memory server.</summary>
/// <typeparam name="TStartup">Target project's startup type.</typeparam>
public class TestIntegrationBase<TStartup>
{
    public static readonly string SolutionFileName = "SandboxAspnetCoreWebApi.sln";

    protected readonly Microsoft.AspNetCore.TestHost.TestServer _server;
    protected readonly System.Net.Http.HttpClient _client;

    /// <summary>Argument constructor.</summary>
    protected TestIntegrationBase(string projectParentFolderName = "src")
    {
        var startupAssembly = typeof(TStartup).GetTypeInfo().Assembly;
        var contentRoot = TestBase.GetProjectFolderPath(SolutionFileName, projectParentFolderName, startupAssembly);

        _server = new Microsoft.AspNetCore.TestHost.TestServer(new WebHostBuilder()
            .UseContentRoot(contentRoot)
            .UseEnvironment(EnvironmentName.Development)
            .UseStartup(typeof(TStartup))
        );
        _client = _server.CreateClient();
    }
}

}
