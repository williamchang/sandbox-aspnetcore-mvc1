using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SandboxAspnetCoreMvc1.Data.SQLite.Tests {

[TestClass]
public class TestBase
{
    [TestInitialize]
    public void TestInit()
    {
        System.Diagnostics.Trace.WriteLine("TestBase.TestInit()");
    }

    [TestCleanup]
    public void TestCleanup()
    {
        System.Diagnostics.Trace.WriteLine("TestBase.TestCleanup()");
    }
}

}
