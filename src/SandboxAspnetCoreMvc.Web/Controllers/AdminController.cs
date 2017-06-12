using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SandboxAspnetCoreMvc1.Web.Controllers {

public class AdminController : BaseController
{
    protected readonly Data.Interfaces.ISystemRepository _repoSystem;

    /// <summary>Argument constructor.</summary>
    public AdminController(Data.Interfaces.ISystemRepository repoSystem)
    {
        _repoSystem = repoSystem;
    }

    /// <summary>GET /Admin</summary>
    public IActionResult Index()
    {
        return View();
    }

    /// <summary>GET /Admin/CreateSystemLog</summary>
    [HttpGet]
    public IActionResult CreateSystemLog()
    {
        return View(new Data.Entities.SystemLog());
    }

    /// <summary>POST /Admin/CreateSystemLog</summary>
    [HttpPost]
    public IActionResult CreateSystemLog(Data.Entities.SystemLog formInput)
    {
        if(ModelState.IsValid)
        {
            _repoSystem.CreateLog(new Data.Entities.SystemLog() {
                Id = 0,
                DateCreated = DateTime.Now,
                Thread = formInput.Thread ?? String.Empty,
                Level = formInput.Level ?? String.Empty,
                Logger = formInput.Logger ?? String.Empty,
                Message = formInput.Message ?? String.Empty,
                Exception = formInput.Exception ?? String.Empty
            });
            return RedirectToAction("ViewSystemLog");
        }
        return View();
    }

    /// <summary>GET /Admin/CreateSystemSetting</summary>
    [HttpGet]
    public IActionResult CreateSystemSetting()
    {
        return View(new Data.Entities.SystemSetting());
    }

    /// <summary>POST /Admin/CreateSystemSetting</summary>
    [HttpPost]
    public IActionResult CreateSystemSetting(Data.Entities.SystemSetting formInput)
    {
        if(ModelState.IsValid)
        {
            _repoSystem.CreateSetting(new Data.Entities.SystemSetting() {
                Id = Guid.NewGuid(),
                ApplicationName = formInput.ApplicationName,
                Name = formInput.Name,
                Value = formInput.Value ?? String.Empty,
                DateModified = DateTime.Now
            });
            return RedirectToAction("ViewSystemSetting");
        }
        return View();
    }

    /// <summary>POST /Admin/DeleteSystemLog</summary>
    [HttpPost]
    public IActionResult DeleteSystemLog(int? id)
    {
        if(id != null) {
            _repoSystem.DeleteLog(id ?? 0);
        } else {
            return StatusCode(Microsoft.AspNetCore.Http.StatusCodes.Status400BadRequest);
        }
        return RedirectToAction("ViewSystemLog");
    }

    /// <summary>POST /Admin/DeleteSystemLogs</summary>
    [HttpPost]
    public IActionResult DeleteSystemLogs()
    {
        _repoSystem.DeleteLogs();
        return RedirectToAction("ViewSystemLog");
    }

    /// <summary>POST /Admin/DeleteSystemSetting</summary>
    [HttpPost]
    public IActionResult DeleteSystemSetting(Guid? id)
    {
        if(id != null) {
            _repoSystem.DeleteSetting(id ?? Guid.Empty);
        } else {
            return StatusCode(Microsoft.AspNetCore.Http.StatusCodes.Status400BadRequest);
        }
        return RedirectToAction("ViewSystemSetting");
    }

    /// <summary>GET /Admin/EditSystemLog</summary>
    [HttpGet]
    public IActionResult EditSystemLog(int? id)
    {
        if(id != null) {
            var obj1 = _repoSystem.GetLog(id ?? 0);
            if(obj1 != null) {
                return View(obj1);
            } else {
                return StatusCode(Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound);
            }
        } else {
            return StatusCode(Microsoft.AspNetCore.Http.StatusCodes.Status400BadRequest);
        }
    }

    /// <summary>POST /Admin/EditSystemLog</summary>
    [HttpPost]
    public IActionResult EditSystemLog(Data.Entities.SystemLog formInput)
    {
        if(ModelState.IsValid)
        {
            _repoSystem.SetLog(new Data.Entities.SystemLog() {
                Id = formInput.Id,
                Thread = formInput.Thread ?? String.Empty,
                Level = formInput.Level ?? String.Empty,
                Logger = formInput.Logger ?? String.Empty,
                Message = formInput.Message ?? String.Empty,
                Exception = formInput.Exception ?? String.Empty
            });
            return RedirectToAction("ViewSystemLog");
        }
        return View(formInput);
    }

    /// <summary>GET /Admin/EditSystemSetting</summary>
    [HttpGet]
    public IActionResult EditSystemSetting(Guid? id)
    {
        if(id != null) {
            var obj1 = _repoSystem.GetSetting(id ?? Guid.Empty);
            if(obj1 != null) {
                return View(obj1);
            } else {
                return StatusCode(Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound);
            }
        } else {
            return StatusCode(Microsoft.AspNetCore.Http.StatusCodes.Status400BadRequest);
        }
    }

    /// <summary>POST /Admin/EditSystemSetting</summary>
    [HttpPost]
    public IActionResult EditSystemSetting(Data.Entities.SystemSetting formInput)
    {
        if(ModelState.IsValid)
        {
            _repoSystem.SetSetting(new Data.Entities.SystemSetting() {
                Id = formInput.Id,
                ApplicationName = formInput.ApplicationName,
                Name = formInput.Name,
                Value = formInput.Value ?? String.Empty,
                DateModified = DateTime.Now
            });
            return RedirectToAction("ViewSystemSetting");
        }
        return View(formInput);
    }

    /// <summary>GET /Admin/ViewSystemLog</summary>
    [HttpGet]
    public IActionResult ViewSystemLog(int? id)
    {
        if(id != null) {
            var obj1 = _repoSystem.GetLog(id ?? 0);
            if(obj1 == null) {
                return StatusCode(Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound);
            }
            var objs1 = Helpers.CollectionHelper.AsSingleEnumerable<Data.Entities.SystemLog>(obj1).Select(x => new ViewModels.AdminSystemLogViewModel() {
                Id = x.Id,
                DateCreated = x.DateCreated,
                Thread = x.Thread,
                Level = x.Level,
                Logger = x.Logger,
                Message = x.Message,
                Exception = x.Exception
            });
            ViewBag.ViewMode = "details";
            return View(objs1);
        } else {
            var objs1 = _repoSystem.GetLogs().Select(x => new ViewModels.AdminSystemLogViewModel() {
                Id = x.Id,
                DateCreated = x.DateCreated,
                Thread = x.Thread,
                Level = x.Level,
                Logger = x.Logger,
                Message = x.Message,
                Exception = x.Exception
            });
            if(objs1 == null) {
                return StatusCode(Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound);
            }
            ViewBag.ViewMode = "list";
            return View(objs1);
        }
    }

    /// <summary>GET /Admin/ViewSystemSetting</summary>
    [HttpGet]
    public IActionResult ViewSystemSetting(Guid? id)
    {
        if(id != null) {
            var obj1 = _repoSystem.GetSetting(id ?? Guid.Empty);
            if(obj1 == null) {
                return StatusCode(Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound);
            }
            var objs1 = Helpers.CollectionHelper.AsSingleEnumerable<Data.Entities.SystemSetting>(obj1).Select(x => new ViewModels.AdminSystemSettingViewModel() {
                Id = x.Id,
                ApplicationName = x.ApplicationName,
                Name = x.Name,
                Value = x.Value,
                DateModified = x.DateModified
            });
            ViewBag.Mode = "details";
            return View(objs1);
        } else {
            var objs1 = _repoSystem.GetSettings().Select(x => new ViewModels.AdminSystemSettingViewModel() {
                Id = x.Id,
                ApplicationName = x.ApplicationName,
                Name = x.Name,
                Value = x.Value,
                DateModified = x.DateModified
            });
            if(objs1 == null) {
                return StatusCode(Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound);
            }
            ViewBag.ViewMode = "list";
            return View(objs1);
        }
    }
}

}