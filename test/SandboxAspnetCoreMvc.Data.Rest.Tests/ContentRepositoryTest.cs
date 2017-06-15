using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SandboxAspnetCoreMvc.Data.Rest.Tests {

[TestClass]
public class ContentRepositoryTest : TestBase
{
    [TestMethod]
    public void GetComment()
    {
        // Arrange.
        var repoContent = new Repositories.ContentRepository(TestConfig.WebServiceBaseUrl);
        Entities.ContentComment returnComment = null;

        // Act.
        returnComment = repoContent.GetComment(1);

        // Assert.
        Assert.IsNotNull(returnComment);
    }

    [TestMethod]
    public void GetComments()
    {
        throw new NotImplementedException();
    }

    [TestMethod]
    public void GetPost()
    {
        // Arrange.
        var repoContent = new Repositories.ContentRepository(TestConfig.WebServiceBaseUrl);
        Entities.ContentPost returnPost = null;

        // Act.
        returnPost = repoContent.GetPost(1);

        // Assert.
        Assert.IsNotNull(returnPost);
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
