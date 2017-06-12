/**
@file
    CollectionHelper.cs
@author
    William Chang
@version
    0.1
@date
    - Created: 2015-08-28
    - Modified: 2017-01-05
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

namespace SandboxAspnetCoreMvc.Web.Helpers {

public static class CollectionHelper
{
    /// <summary>Convert string (from request form) to type. If null, return default value.</summary>
    /// <remarks>
    /// Extension method.
    /// http://stackoverflow.com/questions/1019737/favorite-way-to-create-an-new-ienumerablet-sequence-from-a-single-value
    /// http://stackoverflow.com/questions/1577822/passing-a-single-item-as-ienumerablet
    /// </remarks>
    public static IEnumerable<T> AsSingleEnumerable<T>(this T item)
    {
        yield return item;
    }

    /// <summary>Indicates whether the specified IEnumerable is null or an empty string.</summary>
    /// <remarks>
    /// Extension method.
    /// http://haacked.com/archive/2010/06/10/checking-for-empty-enumerations.aspx/
    /// </remarks>
    public static bool IsNullOrEmpty<T>(this IEnumerable<T> items) {
        return items == null || !items.Any();
    }
}

}
