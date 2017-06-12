using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SandboxAspnetCoreMvc.Data.Rest.Tests {

[TestClass]
public class ContentRepositoryTest : TestBase
{
    [TestMethod]
    public void GetPost()
    {
        throw new NotImplementedException();
    }

    [TestMethod]
    public void GetPosts()
    {
        // Arrange.
        var repoContent = new Repositories.ContentRepository(TestConfig.WebServiceBaseUrl);
        IList<Entities.ContentPost> returnPosts = new List<Entities.ContentPost>();

        // Act.
        returnPosts = repoContent.GetPosts();

        // Assert.
        Assert.IsTrue(returnPosts.Count > 0);
    }
}

}
