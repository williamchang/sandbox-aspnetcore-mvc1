using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SandboxAspnetCoreMvc.Data.Rest.Tests {

[TestClass]
public class TestBase
{
    [TestInitialize]
    public void TestInit()
    {
        System.Diagnostics.Debug.WriteLine("TestBase.TestInit()");
    }

    [TestCleanup]
    public void TestCleanup()
    {
        System.Diagnostics.Debug.WriteLine("TestBase.TestCleanup()");
    }
}

}
