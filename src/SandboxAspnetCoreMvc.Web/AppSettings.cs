/**
@file
    AppSettings.cs
@author
    William Chang
@version
    0.1
@date
    - Created: 2017-06-04
    - Modified: 2017-06-05
    .
@note
    References:
    - General:
        - https://docs.microsoft.com/en-us/aspnet/core/fundamentals/configuration
        - https://blogs.msdn.microsoft.com/jpsanders/2017/05/16/azure-net-core-application-settings/
        - https://weblog.west-wind.com/posts/2016/may/23/strongly-typed-configuration-settings-in-aspnet-core
        - http://benfoster.io/blog/net-core-configuration-legacy-projects
        .
    .
*/

using System;
using System.Collections.Generic;
using System.Reflection;

namespace SandboxAspnetCoreMvc1.Web.Configuration {

public class AppSettings
{
    public Logging Logging {get;set;}
    public Data Data {get;set;}
}

public class Logging
{
    public bool IncludeScopes {get;set;}
    public LogLevel LogLevel {get;set;}
}

public class LogLevel
{
    public string Default {get;set;}
}

public class Data
{
    public ConnectionStrings ConnectionStrings {get;set;}
    public string ContentWebServiceBaseUrl {get;set;}
}

public class ConnectionStrings
{
    public string this[string propertyName]
    {
        get {return Convert.ToString(this.GetType().GetProperty(propertyName).GetValue(this, null));}
        set {this.GetType().GetProperty(propertyName).SetValue(this, value, null);}
    }

    public string Default {get;set;}
    public string SQLite {get;set;}
}

}
