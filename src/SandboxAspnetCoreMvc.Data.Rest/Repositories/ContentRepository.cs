/**
@file
    ContentRepository.cs
@author
    William Chang
@version
    0.1
@date
    - Created: 2017-06-11
    - Modified: 2017-06-14
    .
@note
    References:
    - General:
        - Nothing.
        .
    .
*/

using System;
using System.Collections.Generic;
using System.Text;
using SandboxAspnetCoreMvc.Data.Entities;

namespace SandboxAspnetCoreMvc.Data.Rest.Repositories {

public class ContentRepository : BaseRepository, Interfaces.IContentRepository
{
    protected readonly string _restWebServiceBaseUrl;
    protected readonly Uri _restWebServiceBaseUri;

    /// <summary>Default constructor.</summary>
    protected ContentRepository() {}

    /// <summary>Argument constructor.</summary>
    public ContentRepository(string restWebServiceBaseUrl)
    {
        this._restWebServiceBaseUrl = restWebServiceBaseUrl;
        this._restWebServiceBaseUri = new Uri(restWebServiceBaseUrl);
    }

    /// <summary>Get content comment.</summary>
    public ContentComment GetComment(int id)
    {
        System.Threading.Thread.Sleep(20000); // Sleep for 20 seconds.
        return new ContentComment {
            PostId = 1,
            Id = 1,
            Name = "TestFirstName0001 TestLastName0001",
            Email = "name0001@test.com",
            Body = "TestBody0001"
        };
    }

    /// <summary>Get content comments.</summary>
    public IList<Entities.ContentComment> GetComments()
    {
        throw new NotImplementedException();
    }

    /// <summary>Get content post.</summary>
    public Entities.ContentPost GetPost(int id)
    {
        System.Threading.Thread.Sleep(10000); // Sleep for 10 seconds.
        return new ContentPost {
            UserId = 1,
            Id = 1,
            Title = "TestTitle0001",
            Body = "TestBody0001"
        };
    }

    /// <summary>Get content posts.</summary>
    public IList<Entities.ContentPost> GetPosts()
    {
        using(var client = new System.Net.Http.HttpClient()) {
            client.BaseAddress = _restWebServiceBaseUri;
            try
            {
                var response = client.GetAsync("posts").Result;
                if(response.IsSuccessStatusCode)
                {
                    var responseString = response.Content.ReadAsStringAsync().Result;
                    return Newtonsoft.Json.JsonConvert.DeserializeObject<IList<Entities.ContentPost>>(responseString);
                }
            }
            catch(System.Net.Http.HttpRequestException ex)
            {
                System.Diagnostics.Debug.WriteLine("HTTP Request Exception : {0}", ex);
            }
        }
        return null;
    }

}

}
