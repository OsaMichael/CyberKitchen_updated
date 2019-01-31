using Cyber_Kitchen.Entities;
using Cyber_Kitchen.Interface;
using Cyber_Kitchen.Models;
using OfficeOpenXml;
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

        public ActionResult Export()
        {
            //ViewBag.Profile = context.Cybers.ToList();
            var list = _ratMgr.GetRestaurantSummaryReport();
   
            


            ExcelPackage pck = new ExcelPackage();
            ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Report");

            ws.Cells["A1"].Value = "Restaurant Name";
            ws.Cells["B1"].Value = " EntryDate";
            ws.Cells["C1"].Value = "TotalSum";

            ws.Cells["D1"].Value = "Taste";
            ws.Cells["E1"].Value = "Quality";
            ws.Cells["F1"].Value = "CustomerServices";
            ws.Cells["G1"].Value = "TimeLiness";
    





            int rowStart = 2;
            foreach (var item in list.Result)
            {
                //converting expireddate format to string 
                //  var stringDate = item.ExpiringDate;

                ws.Cells[string.Format("A{0}", rowStart)].Value = item.RestName;
                ws.Cells[string.Format("B{0}", rowStart)].Value = item.EntryDate.ToString();
                ws.Cells[string.Format("C{0}", rowStart)].Value = item.RestSum;
                ws.Cells[string.Format("D{0}", rowStart)].Value = item.Quality;
                ws.Cells[string.Format("E{0}", rowStart)].Value = item.Quantity;
                ws.Cells[string.Format("F{0}", rowStart)].Value = item.CustomerServices;
                ws.Cells[string.Format("G{0}", rowStart)].Value = item.TimeLiness;
            


                rowStart++;
            }

            Response.Clear();
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment: filename=" + "ExcelReport.xlsx");
            Response.BinaryWrite(pck.GetAsByteArray());
            Response.End();

            return RedirectToAction("Index");


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