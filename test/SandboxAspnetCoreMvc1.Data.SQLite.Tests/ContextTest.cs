using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SandboxAspnetCoreMvc1.Data.SQLite.Tests {

[TestClass]
public class ContextTest
{
    [TestMethod]
    public void Connection_works()
    {
        // Arrange.
        var connection = new Microsoft.Data.Sqlite.SqliteConnection("Data Source=:memory:");

        // Act.
        connection.Open();

        // Assert.
        Assert.AreEqual("main", connection.Database);
        Assert.AreEqual(System.Data.ConnectionState.Open, connection.State);

        // Act.
        connection.Dispose();

        // Assert.
        Assert.AreEqual("main", connection.Database);
        Assert.AreEqual(System.Data.ConnectionState.Closed, connection.State);
    }

    [TestMethod]
    public void File_system_path_works()
    {
        // Arrange.
        string appCurrentFolderPath1;
        string appCurrentFolderPath2;
        System.IO.DirectoryInfo mvsCurrentProjectFolderInfo;
        string mvsCurrentProjectFolderName;
        string mvsCurrentProjectFolderPath;
        System.IO.DirectoryInfo mvsSolutionFolderInfo;
        string mvsSolutionFolderPath;
        string mvsBaseProjectName;
        string mvsWebProjectFolderPath;
        string[] mvsCurrentProjectFolderNameParts;

        // Act.
        appCurrentFolderPath1 = System.AppContext.BaseDirectory;
        appCurrentFolderPath2 = System.IO.Directory.GetCurrentDirectory();
        mvsCurrentProjectFolderInfo = System.IO.Directory.GetParent(appCurrentFolderPath1).Parent.Parent;
        mvsCurrentProjectFolderName = mvsCurrentProjectFolderInfo.Name;
        mvsCurrentProjectFolderPath = mvsCurrentProjectFolderInfo.FullName;
        mvsCurrentProjectFolderNameParts = mvsCurrentProjectFolderName.Split('.');
        if(mvsCurrentProjectFolderNameParts.Length > 1)
        {
            mvsBaseProjectName = mvsCurrentProjectFolderNameParts[0];
        }
        else
        {
            mvsBaseProjectName = String.Empty;
        }
        mvsSolutionFolderInfo = mvsCurrentProjectFolderInfo.Parent.Parent;
        mvsSolutionFolderPath = mvsSolutionFolderInfo.FullName;
        mvsWebProjectFolderPath = System.IO.Path.Combine(mvsSolutionFolderPath, "src", String.Concat(mvsBaseProjectName, ".Web"));

        // Output.
        System.Diagnostics.Debug.WriteLine("App Current Folder Path, 1st Attempt : {0}", appCurrentFolderPath1, String.Empty);
        System.Diagnostics.Debug.WriteLine("App Current Folder Path, 2nd Attempt : {0}", appCurrentFolderPath2, String.Empty);
        System.Diagnostics.Debug.WriteLine("VS Current Project Folder Name : {0}", mvsCurrentProjectFolderName, String.Empty);
        System.Diagnostics.Debug.WriteLine("VS Current Project Folder Path : {0}", mvsCurrentProjectFolderPath, String.Empty);
        System.Diagnostics.Debug.WriteLine("VS Solution Folder Path : {0}", mvsSolutionFolderPath, String.Empty);
        System.Diagnostics.Debug.WriteLine("VS Web Project Folder Path {0}", mvsWebProjectFolderPath, String.Empty);

        // Assert.
        Assert.IsFalse(String.IsNullOrEmpty(appCurrentFolderPath1));
        Assert.IsFalse(String.IsNullOrEmpty(appCurrentFolderPath2));
        Assert.IsTrue(System.IO.Directory.Exists(mvsWebProjectFolderPath));
    }

    [TestMethod]
    public void Path_to_database()
    {
        throw new NotImplementedException();
    }

    [TestMethod]
    public void Connect_to_database()
    {
        throw new NotImplementedException();
    }

}

}
