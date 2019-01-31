using Cyber_Kitchen.Interface;
using Cyber_Kitchen.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cyber_Kitchen.Controllers
{
    public class PeriodController : Controller
    {
        private IRatingManager _ratMgr;

        public PeriodController(IRatingManager ratMgr)
        {
            _ratMgr = ratMgr;
        }
        // GET: Period
        public ActionResult Index()
        {
            if (TempData["message"] != null)
            {
                ViewBag.Success = (string)TempData["message"];
            }
            var results = _ratMgr.GetPeriods();
            if (results.Succeeded == true)
            {
                return View(results.Unwrap());
            }
            else
            {
                ModelState.AddModelError(string.Empty, "An error occure");
                return View();
            }

        }
        [HttpGet]
        public ActionResult CreatePeriod()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreatePeriod(PeriodModel model)
        {
            if (ModelState.IsValid)
            {
                model.CreatedBy = User.Identity.GetUserName();
                var result = _ratMgr.CreatePeriod(model);
                if (result == true)
                {
                    TempData["message"] = $"Period{model.PeriodName} was successfully added!";
                    return RedirectToAction("Index");
                }
            }

            return View(model);
        }
        public ActionResult Activate(int id)
        {
            _ratMgr.ActivateInstructor(id);
            return RedirectToAction("Index");
        }
        public ActionResult Deactivate(int id)
        {
            _ratMgr.DeactivateInstructor(id);
            return RedirectToAction("Index");
        }
    }
}