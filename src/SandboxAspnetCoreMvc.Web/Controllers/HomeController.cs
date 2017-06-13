using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SandboxAspnetCoreMvc.Web.Controllers {

public class HomeController : BaseController
{
    protected readonly Data.Interfaces.IContentRepository _repoContent;

    /// <summary>Argument constructor.</summary>
    public HomeController(IAppHead appHead, Data.Interfaces.IContentRepository repoContent) : base(appHead)
    {
        _repoContent = repoContent;
    }

    /// <summary>GET /Home</summary>
    public IActionResult Index()
    {
        return View();
    }

    /// <summary>GET /Home/About</summary>
    public IActionResult About()
    {
        ViewData["Message"] = "Your application description page.";

        return View();
    }

    /// <summary>GET /Home/Blog</summary>
    public IActionResult Blog()
    {
        var stopwatch = System.Diagnostics.Stopwatch.StartNew();

        var objs1 = _repoContent.GetPosts().Take(3).Select(x => new Data.Entities.ContentPost() {
            Id = x.Id,
            UserId = x.UserId,
            Title = x.Title,
            Body = x.Body
        });
        if(objs1 == null) {
            return StatusCode(Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound);
        }
        ViewBag.ViewMode = "list";

        ViewBag.StopwatchTime = stopwatch.Elapsed;

        return View(objs1);
    }

    /// <summary>GET /Home/Contact</summary>
    public IActionResult Contact()
    {
        ViewData["Message"] = "Your contact page.";

        return View();
    }

    /// <summary>GET /Home/Error</summary>
    public IActionResult Error()
    {
        return View();
    }

    /// <summary>GET /Home/Sandbox</summary>
    public IActionResult Sandbox([FromQuery]string echo, [FromQuery]string message)
    {
        System.Text.StringBuilder sbDebug = new System.Text.StringBuilder();

        // References
        // http://www.hanselman.com/blog/ASPNETParamsCollectionVsQueryStringFormsVsRequestindexAndDoubleDecoding.aspx

        sbDebug.AppendFormat("\n<div>\n");
        sbDebug.AppendFormat("Request QueryString Echo : {0}", echo ?? "null");
        sbDebug.AppendFormat("\n<br />\n");
        sbDebug.AppendFormat("Request QueryString Message : {0}", message ?? "null");
        sbDebug.AppendFormat("\n</div>\n");

        ViewBag.DebugInformation = sbDebug.ToString();

        return View();
    }

    /// <summary>GET /Home/Raw</summary>
    public String Raw()
    {
        return "Raw";
    }
}

}
