using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SandboxAspnetCoreMvc1.Data.SQLite.Tests {

[TestClass]
public class SystemRepositoryTest : TestBase
{
    [TestMethod]
    public void CreateLog()
    {
        // Arrange.
        var logs = new List<Entities.SystemLog>() {
            new Entities.SystemLog {Id = 101, DateCreated = DateTime.Now, Thread = String.Empty, Level = String.Empty, Logger = String.Empty, Message = String.Empty, Exception = String.Empty},
            new Entities.SystemLog {Id = 102, DateCreated = DateTime.Now, Thread = String.Empty, Level = String.Empty, Logger = String.Empty, Message = String.Empty, Exception = String.Empty},
            new Entities.SystemLog {Id = 103, DateCreated = DateTime.Now, Thread = String.Empty, Level = String.Empty, Logger = String.Empty, Message = String.Empty, Exception = String.Empty}
        };
        var repoSystem = new Repositories.SystemRepository(TestConfig.ConnectionString);
        IList<Entities.SystemLog> returnLogs = new List<Entities.SystemLog>();

        // Act.
        foreach(var log in logs)
        {
            returnLogs.Add(repoSystem.CreateLog(log));
        }

        // Assert.
        Assert.AreEqual(logs.Count, returnLogs.Count);
        foreach(var log in returnLogs)
        {
            Assert.IsNotNull(log);
        }

        throw new NotImplementedException();
    }

    [TestMethod]
    public void GetLogs()
    {
        // Arrange.
        var repoSystem = new Repositories.SystemRepository(TestConfig.ConnectionString);
        IList<Entities.SystemLog> returnLogs = new List<Entities.SystemLog>();

        // Act.
        returnLogs = repoSystem.GetLogs();

        // Assert.
        Assert.IsTrue(returnLogs.Count > 0);
    }
}

}
