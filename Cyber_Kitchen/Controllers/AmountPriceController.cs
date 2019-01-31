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
    public class AmountPriceController : Controller
    {
        private IRestaurantManager _restMgr;
        public AmountPriceController(IRestaurantManager restMgr)
        {
            _restMgr = restMgr;
        }
        public ActionResult Index()
        {
            if (TempData["message"] != null)
            {
                ViewBag.Success = (string)TempData["message"];
            }
            var results = _restMgr.GetAmountPrices();

            var myList = new List<AmountPriceViemodel>();


            var assign = new AmountPriceViemodel()
            {
                FiveHundred = results.Unwrap().Where(x => x.AmountPriceId.Trim().Equals("500")).Count(),
                SixHundred = results.Unwrap().Where(x => x.AmountPriceId.Trim().Equals("600")).Count(),
                SevenHundred = results.Unwrap().Where(x => x.AmountPriceId.Trim().Equals("700")).Count(),
                EightHundred = results.Unwrap().Where(x => x.AmountPriceId.Trim().Equals("800")).Count(),

                Yes = results.Unwrap().Where(x => x.IsMfongComingBack.Trim().Equals("Yes")).Count(),
                No = results.Unwrap().Where(x => x.IsMfongComingBack.Trim().Equals("No")).Count(),

            };
     
            if (results.Succeeded == true)
            {
                return View(assign);
            }
            else
            {
                ModelState.AddModelError(string.Empty, "An error occure");
                return View();
            }

        }
        [HttpGet]
        public ActionResult CreateAmountPrice()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateAmountPrice(AmountPriceModel model)
        {
            model.CreatedBy = User.Identity.GetUserName();
            var result = _restMgr.CreateAmountPrice(model);
            if (result.Succeeded == true)
            {
                TempData["message"] = " was successfully added!";
                return RedirectToAction("Index");
            }
            return View(model);
        }
    }
}