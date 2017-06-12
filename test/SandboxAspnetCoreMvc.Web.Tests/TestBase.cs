using System;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SandboxAspnetCoreMvc.Web.Tests {

[TestClass]
public class TestBase
{
    [TestInitialize]
    public void TestInit()
    {
        System.Diagnostics.Debug.WriteLine("TestBase.TestInit()");
    }

    [TestMethod]
    public void File_system_path_to_Web_project()
    {
        // Arrange.
        var solutionFileName = "SandboxAspnetCoreMvc.sln";
        var projectParentFolderName = "src";

        // Act.
        var targetAssembly = typeof(Startup).GetTypeInfo().Assembly;
        var projectFolderPath = GetProjectFolderPath(solutionFileName, projectParentFolderName, targetAssembly);

        // Output.
        System.Diagnostics.Debug.WriteLine("Assembly File Name : {0}", targetAssembly.ManifestModule.Name, String.Empty);
        System.Diagnostics.Debug.WriteLine("Assembly File Base Name : {0}", targetAssembly.GetName().Name, String.Empty);
        System.Diagnostics.Debug.WriteLine("Project Folder Path : {0}", projectFolderPath, String.Empty);

        // Assert.
        Assert.IsNotNull(targetAssembly);
        Assert.IsFalse(String.IsNullOrEmpty(projectFolderPath));
        Assert.IsTrue(System.IO.Directory.Exists(projectFolderPath));
    }

    /// <summary>Get the full path to the target project folder.</summary>
    /// <param name="solutionFullFileName">The target project's assembly.</param>
    /// <param name="solutionProjectsRelativePath">The parent folder of the target project. Eg docs, scripts, src, test.</param>
    /// <param name="targetAssembly">The target project's assembly.</param>
    /// <returns>The full path to the target project.</returns>
    public static string GetProjectFolderPath(string solutionFullFileName, string solutionProjectsRelativePath, Assembly targetAssembly)
    {
        // Get name of the target project which we want to test.
        var projectName = targetAssembly.GetName().Name;

        // Get currently executing test project path.
        var applicationBasePath = Microsoft.Extensions.PlatformAbstractions.PlatformServices.Default.Application.ApplicationBasePath;

        // Find the folder which contains the solution file and then use this information to find the target project which we want to test.
        var folderInfo = new System.IO.DirectoryInfo(applicationBasePath);
        do
        {
            var solutionFileInfo = new System.IO.FileInfo(System.IO.Path.Combine(folderInfo.FullName, solutionFullFileName));
            if(solutionFileInfo.Exists)
            {
                return System.IO.Path.GetFullPath(System.IO.Path.Combine(folderInfo.FullName, solutionProjectsRelativePath, projectName));
            }
            folderInfo = folderInfo.Parent;
        }
        while(folderInfo.Parent != null);

        throw new Exception($"Solution root could not be located using application root {applicationBasePath}.");
    }

    [TestCleanup]
    public void TestCleanup()
    {
        System.Diagnostics.Debug.WriteLine("TestBase.TestCleanup()");
    }
}

}
