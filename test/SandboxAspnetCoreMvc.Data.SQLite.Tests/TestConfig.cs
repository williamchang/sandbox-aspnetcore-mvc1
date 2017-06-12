using System;

namespace SandboxAspnetCoreMvc1.Data.SQLite.Tests {

public static class TestConfig
{
    public static string AppCurrentFolder {
        get {return System.AppContext.BaseDirectory;}
    }

    public static string ConnectionString {
        get {return String.Format("Data Source={0}", System.IO.Path.Combine(CurrentProjectFolderPath, DataSource));}
    }

    public static System.IO.DirectoryInfo CurrentProjectFolderInfo {
        get {return System.IO.Directory.GetParent(AppCurrentFolder).Parent.Parent;}
    }

    public static string CurrentProjectFolderPath {
        get {return System.IO.Directory.GetParent(AppCurrentFolder).Parent.Parent.FullName;}
    }

    public static string DataSource {
        get {return "Test.sqlite3";}
    }
}

}
