/**
@file
    ContentPost.cs
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
using System.ComponentModel.DataAnnotations;

namespace SandboxAspnetCoreMvc.Data.Entities {

public class ContentPost
{
    public virtual int UserId {get;set;}

    public virtual int Id {get;set;}

    public virtual string Title {get;set;}

    public virtual string Body {get;set;}
}

}
