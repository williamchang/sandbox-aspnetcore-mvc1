/**
@file
    ContentPost.cs
@author
    William Chang
@version
    0.1
@date
    - Created: 2017-06-14
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
using System.ComponentModel.DataAnnotations;

namespace SandboxAspnetCoreMvc.Data.Entities {

public class ContentComment
{
    public virtual int PostId {get;set;}

    public virtual int Id {get;set;}

    public virtual string Name {get;set;}

    public virtual string Email {get;set;}

    public virtual string Body {get;set;}
}

}
