using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SandboxAspnetCoreMvc.Data.SQLite.Tests {

[TestClass]
public class SmokeTest : TestBase
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
    public void File_system_path_to_Web_App_Data_folder()
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
    public void Open_connection_by_TestConfig()
    {
        // Arrange and act.
        using(var connection = new Microsoft.Data.Sqlite.SqliteConnection(TestConfig.ConnectionString))
        {
            connection.Open();

            // Assert.
            Assert.IsTrue(System.IO.Path.IsPathRooted(connection.DataSource));
            Assert.AreEqual(TestConfig.DataSource, System.IO.Path.GetFileName(connection.DataSource));
        }
    }

    [TestMethod]
    public void Open_connection_by_file_system_path_to_DataSource()
    {
        // Arrange.
        string dataSource = "Test.sqlite3";
        string currentProjectFolderPath;
        string connectionString;

        // Act.
        currentProjectFolderPath = System.IO.Directory.GetParent(System.AppContext.BaseDirectory).Parent.Parent.FullName;
        connectionString = String.Format("Data Source={0}", System.IO.Path.Combine(currentProjectFolderPath, dataSource));
        using(var connection = new Microsoft.Data.Sqlite.SqliteConnection(connectionString))
        {
            connection.Open();

            // Assert.
            Assert.IsTrue(System.IO.Path.IsPathRooted(connection.DataSource));
            Assert.AreEqual(dataSource, System.IO.Path.GetFileName(connection.DataSource));
        }
    }

    [TestMethod]
    public void BeginTransaction_works()
    {
        // Arrange and act.
        using(var connection = new Microsoft.Data.Sqlite.SqliteConnection("Data Source=:memory:"))
        {
            connection.Open();

            using(var transaction = connection.BeginTransaction(System.Data.IsolationLevel.Serializable))
            {
                // Assert.
                Assert.IsNotNull(transaction);
                Assert.AreEqual(connection, transaction.Connection);
                Assert.AreEqual(System.Data.IsolationLevel.Serializable, transaction.IsolationLevel);
            }

            // Assert.
            Assert.AreEqual(System.Data.ConnectionState.Open, connection.State);
        }
    }
}

}
