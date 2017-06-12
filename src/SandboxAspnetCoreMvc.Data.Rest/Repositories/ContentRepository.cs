/**
@file
    ContentRepository.cs
@author
    William Chang
@version
    0.1
@date
    - Created: 2017-06-11
    - Modified: 2017-06-11
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

namespace SandboxAspnetCoreMvc1.Data.Rest.Repositories {

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

    public Entities.ContentPost GetPost(int id)
    {
        throw new NotImplementedException();
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
