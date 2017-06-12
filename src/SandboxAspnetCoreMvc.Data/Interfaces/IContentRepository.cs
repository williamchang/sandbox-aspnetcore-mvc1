/**
@file
    IContentRepository.cs
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
using System.Linq;

namespace SandboxAspnetCoreMvc.Data.Interfaces {

/// <summary>Content repository interface.</summary>
/// <remarks>
/// Dependencies:
/// Data.Entities.ContentPost
/// </remarks>
public interface IContentRepository : IBaseRepository
{
    /// <summary>Get system log.</summary>
    Entities.ContentPost GetPost(int id);

    /// <summary>Get system logs.</summary>
    IList<Entities.ContentPost> GetPosts();
}

}
