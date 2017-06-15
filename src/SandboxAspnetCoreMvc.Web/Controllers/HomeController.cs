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

    /// <summary>GET Home</summary>
    public IActionResult Index()
    {
        return View();
    }

    /// <summary>GET Home/About</summary>
    public IActionResult About()
    {
        ViewData["Message"] = "Your application description page.";

        return View();
    }

    /// <summary>GET Home/Blog</summary>
    public IActionResult Blog()
    {
        // Start stopwatch to measure elapsed time.
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

        // Get and set the total elapsed time measured.
        ViewBag.StopwatchTime = stopwatch.Elapsed;

        return View(objs1);
    }

    /// <summary>GET /Home/Contact</summary>
    public IActionResult Contact()
    {
        ViewData["Message"] = "Your contact page.";

        return View();
    }

    /// <summary>GET Home/Error</summary>
    public IActionResult Error()
    {
        return View();
    }

    /// <summary>GET Home/Sandbox0001</summary>
    public IActionResult Sandbox0001([FromQuery]string echo, [FromQuery]string message)
    {
        // Start stopwatch to measure elapsed time.
        var stopwatch = System.Diagnostics.Stopwatch.StartNew();

        System.Text.StringBuilder sbDebug = new System.Text.StringBuilder();

        // References
        // http://www.hanselman.com/blog/ASPNETParamsCollectionVsQueryStringFormsVsRequestindexAndDoubleDecoding.aspx
        sbDebug.AppendFormat("\n<div>\n");
        sbDebug.AppendFormat("Request QueryString Echo : {0}", echo ?? "null");
        sbDebug.AppendFormat("\n<br />\n");
        sbDebug.AppendFormat("Request QueryString Message : {0}", message ?? "null");
        sbDebug.AppendFormat("\n</div>\n");

        ViewBag.DebugInformation = sbDebug.ToString();

        // Get and set the total elapsed time measured.
        ViewBag.StopwatchTime = stopwatch.Elapsed;

        return View();
    }

    /// <summary>GET Home/Sandbox0002</summary>
    public async Task<IActionResult> Sandbox0002([FromQuery]string echo, [FromQuery]string message)
    {
        // Start stopwatch to measure elapsed time.
        var stopwatch = System.Diagnostics.Stopwatch.StartNew();

        // Use the presentation layer to be responsible of asynchronous calling to synchronous operations.
        var comment = await Task.Run(() => _repoContent.GetComment(1));

        System.Text.StringBuilder sbDebug = new System.Text.StringBuilder();

        sbDebug.AppendFormat("\n<div>\n");
        sbDebug.AppendFormat("Comment.PostId : {0}", comment.PostId);
        sbDebug.AppendFormat("\n<br />\n");
        sbDebug.AppendFormat("Comment.Id : {0}", comment.Id);
        sbDebug.AppendFormat("\n<br />\n");
        sbDebug.AppendFormat("Comment.Name : {0}", comment.Name);
        sbDebug.AppendFormat("\n<br />\n");
        sbDebug.AppendFormat("Comment.Email : {0}", comment.Email);
        sbDebug.AppendFormat("\n<br />\n");
        sbDebug.AppendFormat("Comment.Body : {0}", comment.Body);
        sbDebug.AppendFormat("\n</div>\n");

        ViewBag.DebugInformation = sbDebug.ToString();

        // Get and set the total elapsed time measured.
        ViewBag.StopwatchTime = stopwatch.Elapsed;

        return View();
    }

    /// <summary>GET Home/Raw</summary>
    public String Raw()
    {
        return "Raw";
    }
}

}
