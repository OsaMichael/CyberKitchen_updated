using Cyber_Kitchen.Entities;
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
    public class CatererInfoController : Controller
    {
        private IRestaurantManager _restMgr;
        private ICatererInfoManager _catInfMgr;
        public CatererInfoController(ICatererInfoManager catInfMgr, IRestaurantManager restMgr)
        {
            _catInfMgr = catInfMgr;
            _restMgr = restMgr;
        }
        // GET: CatererInfo
        public ActionResult Index()
        {
            if (TempData["message"] != null)
            {
                ViewBag.Success = (string)TempData["message"];
            }
            var results = _catInfMgr.GetCatererInfos();

            if (results.Succeeded == true)
            {
                List<CatererInfo> mylist = new List<CatererInfo>();

                foreach (var item in results.Unwrap())
                {
                    item.TotalScore =
                        item.Quality + item.Quantity + item.Taste + item.CustomerServices + item.TimeLiness;
                }
                return View(results.Unwrap());
            }
            else
            {
                ModelState.AddModelError(string.Empty, "An error occure");
                return View();


            }
        }

        //[HttpPost]
        //public ActionResult Vote(List(CatereInfo catererinfolist))
        //{
        //    return View();
        //  }

    [HttpGet]
        public ActionResult CreateCatererInfo(string Id)
        {
            ViewBag.restaurants = _restMgr.GetRestaurants().Result;
            return View();
        }

        [HttpPost]
        public ActionResult CreateCatererInfo(CatererInfoModel model)
        {
            model.CreatedBy = User.Identity.GetUserName();
            var result = _catInfMgr.CreateCatererInfo(model);
            if (result.Succeeded == true)
            {
                TempData["message"] = $"CatererInfo{model.CaterName} was successfully added!";
                return RedirectToAction("Index");
            }
            return View(model);
        }
        [HttpGet]
        public ActionResult EditCatererInfo(int id = 0)
        {
            var result = _catInfMgr.GetCatererInfoById(id);
            if (result.Succeeded)
            {
                return View(result.Result);
            }
            else
            {
                ModelState.AddModelError(string.Empty, result.Message);
                return View();

            }
        }
        [HttpPost]
        public ActionResult EditCatererInfo(CatererInfoModel model)
        {
            if (ModelState.IsValid)
            {
                model.ModifiedBy = User.Identity.GetUserName();

                model.CreatedBy = User.Identity.GetUserName();
                var result = _catInfMgr.UpdateCatererInfo(model);
                if (result.Succeeded)
                {
                    TempData["message"] = $"CatererInfo{model.CaterName} was successfully added!";
                    ModelState.AddModelError(string.Empty, "Update was successfully ");
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, result.Message);
                    return View(model);

                }
            }
            return View(model);
        }
        [HttpGet]
        public ActionResult DeleteCatererInfo(int id)
        {
            var result = _catInfMgr.GetCatererInfoById(id);
            if (result.Succeeded)
            {
                return View(result.Result);
            }
            else
            {
                ModelState.AddModelError(string.Empty, result.Message);
                return View();
            }
        }
        [HttpPost]
        public ActionResult DeleteCatererInfo(int id, CatererInfo model)
        {
            var result = _catInfMgr.GetCatererInfoById(id);
            if (result == null)
            {
                return View("not found");
            }
            _catInfMgr.DeleteCatererInfo(id);
            return RedirectToAction("Index");

        }
    }
}