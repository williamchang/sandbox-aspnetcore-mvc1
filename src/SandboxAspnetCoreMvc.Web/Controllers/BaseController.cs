/**
@file
    BaseController.cs
@author
    William Chang
@version
    0.1
@date
    - Created: 2015-08-20
    - Modified: 2017-06-05
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
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace SandboxAspnetCoreMvc.Web.Controllers {

/// <summary>Base controller (abstract) for all controllers.</summary>
public abstract class BaseController : Controller
{
    public static readonly string ObjectOwner = "SandboxAspnetCoreMvc";
    public static readonly string ObjectTypeName = "BaseController";
    public static readonly string ObjectTypeNamespace = "SandboxAspnetCoreMvc.Web.Controllers";
    public static readonly string ObjectTypeFullName = String.Concat(ObjectTypeNamespace, ".", ObjectTypeName);

    protected readonly IAppHead _appHead;

    /// <summary>Default constructor.</summary>
    public BaseController() : base()
    {
        System.Diagnostics.Debug.WriteLine("{0} : Constructor Started", ObjectTypeFullName, String.Empty);
        // Do something.
        System.Diagnostics.Debug.WriteLine("{0} : Constructor Ended", ObjectTypeFullName, String.Empty);
    }

    /// <summary>Argument constructor.</summary>
    public BaseController(IAppHead appHead) : base()
    {
        System.Diagnostics.Debug.WriteLine("{0} : Constructor Started", ObjectTypeFullName, String.Empty);
        _appHead = appHead;
        System.Diagnostics.Debug.WriteLine("{0} : Constructor Ended", ObjectTypeFullName, String.Empty);
    }

    /// <summary>Get database connection string.</summary>
    [NonAction]
    public string GetDatabaseConnectionString(string name = "Default")
    {
        if(_appHead.GetSettings()?.Data?.ConnectionStrings != null && !String.IsNullOrEmpty(name)) {
            return _appHead.GetSettings().Data.ConnectionStrings[name];
        } else {
            throw new Exception("Missing connecting string in appsettings.json file.");
        }
    }

    /// <summary>Get setting value. Eg Configuration["Logging:LogLevel:Default"]</summary>
    [NonAction]
    public string GetSetting(string settingName, string defaultValue)
    {
        return GetSafeValue(_appHead.GetConfiguration().GetValue<string>(settingName), defaultValue);
    }

#region Utilities

    [NonAction]
    public static string GetSafeValue(string value, string defaultValue)
    {
        if(!String.IsNullOrEmpty(value)) {
            return value;
        } else {
            return defaultValue;
        }
    }

    [NonAction]
    public static string GetSafeValue(string value, string anotherValue, string defaultValue)
    {
        if(!String.IsNullOrEmpty(value)) {
            return value;
        } else if(!String.IsNullOrEmpty(anotherValue)) {
            return anotherValue;
        } else {
            return defaultValue;
        }
    }

#endregion

}

}
