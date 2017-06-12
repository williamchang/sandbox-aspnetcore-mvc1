using System;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace SandboxAspnetCoreMvc1.Web.Tests.Integration {

/// <summary>A base class test which hosts the target web project in an in-memory server.</summary>
/// <typeparam name="TStartup">Target project's startup type.</typeparam>
public class TestIntegrationBase<TStartup>
{
    public static readonly string SolutionFileName = "SandboxAspnetCoreMvc1.sln";

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
            .ConfigureServices(OnConfigureServices)
            .UseStartup(typeof(TStartup))
        );
        _client = _server.CreateClient();
    }

    protected virtual void OnConfigureServices(IServiceCollection services)
    {
        // https://github.com/Microsoft/aspnet-api-versioning/blob/master/test/Microsoft.AspNetCore.Mvc.Acceptance.Tests/AcceptanceTest.cs
        services.Configure((Microsoft.AspNetCore.Mvc.Razor.RazorViewEngineOptions options) => {
            options.CompilationCallback += context => {
                var assembly = typeof(Startup).GetTypeInfo().Assembly;
                var assemblies = assembly.GetReferencedAssemblies().Select(x => Microsoft.CodeAnalysis.MetadataReference.CreateFromFile(Assembly.Load(x).Location)).ToList();

                // https://github.com/aspnet/Hosting/issues/954
                assemblies.Add(Microsoft.CodeAnalysis.MetadataReference.CreateFromFile(Assembly.Load(new AssemblyName("mscorlib")).Location));
                assemblies.Add(Microsoft.CodeAnalysis.MetadataReference.CreateFromFile(Assembly.Load(new AssemblyName("System.Private.Corelib")).Location));
                assemblies.Add(Microsoft.CodeAnalysis.MetadataReference.CreateFromFile(Assembly.Load(new AssemblyName("System.Linq")).Location));
                assemblies.Add(Microsoft.CodeAnalysis.MetadataReference.CreateFromFile(Assembly.Load(new AssemblyName("System.Threading.Tasks")).Location));
                assemblies.Add(Microsoft.CodeAnalysis.MetadataReference.CreateFromFile(Assembly.Load(new AssemblyName("System.Runtime")).Location));
                assemblies.Add(Microsoft.CodeAnalysis.MetadataReference.CreateFromFile(Assembly.Load(new AssemblyName("System.Dynamic.Runtime")).Location));
                assemblies.Add(Microsoft.CodeAnalysis.MetadataReference.CreateFromFile(Assembly.Load(new AssemblyName("Microsoft.AspNetCore.Razor")).Location));
                assemblies.Add(Microsoft.CodeAnalysis.MetadataReference.CreateFromFile(Assembly.Load(new AssemblyName("Microsoft.AspNetCore.Razor.Runtime")).Location));
                assemblies.Add(Microsoft.CodeAnalysis.MetadataReference.CreateFromFile(Assembly.Load(new AssemblyName("Microsoft.AspNetCore.Mvc")).Location));
                assemblies.Add(Microsoft.CodeAnalysis.MetadataReference.CreateFromFile(Assembly.Load(new AssemblyName("Microsoft.AspNetCore.Mvc.Razor")).Location));
                assemblies.Add(Microsoft.CodeAnalysis.MetadataReference.CreateFromFile(Assembly.Load(new AssemblyName("Microsoft.AspNetCore.Html.Abstractions")).Location));
                assemblies.Add(Microsoft.CodeAnalysis.MetadataReference.CreateFromFile(Assembly.Load(new AssemblyName("System.Text.Encodings.Web")).Location));

                // https://github.com/dotnet/roslyn/issues/12045
                assemblies.Add(Microsoft.CodeAnalysis.MetadataReference.CreateFromFile(Assembly.Load(new AssemblyName("System.Linq.Expressions")).Location));

                context.Compilation = context.Compilation.AddReferences(assemblies);
            };
        });
    }

    public static IHostingEnvironment GetHostingEnvironment(IServiceCollection services)
    {
        Func<ServiceDescriptor, bool> isHostingEnvironmet = service => service.ImplementationInstance is IHostingEnvironment;
        var hostingEnvironment = (IHostingEnvironment)services.Single(isHostingEnvironmet).ImplementationInstance;
        var assembly = typeof(TStartup).GetTypeInfo().Assembly;

        hostingEnvironment.ApplicationName = assembly.GetName().Name;
        return hostingEnvironment;
    }
}

}
