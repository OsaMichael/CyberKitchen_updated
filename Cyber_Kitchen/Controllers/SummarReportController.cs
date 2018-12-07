using Cyber_Kitchen.Entities;
using Cyber_Kitchen.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cyber_Kitchen.Controllers
{
  [Authorize]
    public class SummaryReportController : Controller
    {
        private IScoreManager _scoreMgr;
        private IRatingManager _ratMgr;

        public SummaryReportController(IScoreManager scoreMgr, IRatingManager ratMgr)
        {
            _scoreMgr = scoreMgr;
            _ratMgr = ratMgr;
        }
        // GET: SummaryReport
        public ActionResult Index()
        {
           try
            {

            if (TempData["message"] != null)
            {
                ViewBag.Success = (string)TempData["message"];
            }
             var results = _ratMgr.GetRestaurantSummaryReport();
                //var results = _scoreMgr.GetRestaurantSummaryReport();

                if (results.Succeeded == true)
                {
                    //ViewData["MyRestaurant"] = results;
                    TempData["message"] = "was successfully added!";
                    return View(results.Unwrap());
                }

            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }


            return View();

        }
        public ActionResult Details(int id)
        {
            var results = _ratMgr.GetRatingById(id);
            return View(results);
        }
        //public ActionResult Details(int id)
        //{
        //    var result = _scoreMgr
        //    return View();
        //}
        //[HttpGet]
        //public ActionResult DeleteSummaryReport(int id)
        //{
        //    var result = _ratMgr.GetSummaryReportById(id);
        //    if (result.Succeeded)
        //    {
        //        return View(result.Result);
        //    }
        //    else
        //    {
        //        ModelState.AddModelError(string.Empty, result.Message);
        //        return View();
        //    }
        //}

        //[HttpPost]
        //public ActionResult DeleteSummaryReport(int id, SummaryReport model)
        //{
        //    var result = _ratMgr.GetSummaryReportById(id);
        //    if (result == null)
        //    {
        //        return View("not found");
        //    }
        //    _ratMgr.DeleteSummaryReport(id);
        //    return RedirectToAction("Index");

        //    //return View();
        //}

    }
}