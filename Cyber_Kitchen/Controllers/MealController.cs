using Cyber_Kitchen.Entities;
using Cyber_Kitchen.Interface;
using Cyber_Kitchen.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cyber_Kitchen.Controllers
{
    public class MealController : Controller
    {
        //UserManager<ApplicationUser> _userManager;
        private IMealManager _meaMgr;

        public MealController( IMealManager meaMgr/* UserManager<ApplicationUser> userManager*/)
        {
            _meaMgr = meaMgr;
            //_userManager = userManager;
            
        }
        public ActionResult index()
        {
            var results = _meaMgr.GetMeals();
            return View(results);
        }
       
             [HttpGet]
        public ActionResult ClockIn()
        {

            return View();
        }
        [HttpPost]

        public ActionResult ClockIn(MealModel model)
        {             
            model.Status = true;
            /* ApplicationUser user = UserManager      */// find the user by the staff id//
            //ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext()
            //    .GetUserManager<ApplicationUserManager>().FindById(System.Web
            //    .HttpContext.Current.User.Identity.GetUserId());

            //var userID = User.Identity.GetUserId();

            //if (!string.IsNullOrEmpty(userID))
            //{
            //    var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(ApplicationDbContext.Create()));
            //    var currentUser = manager.FindById(User.Identity.GetUserId());
            //}

            var result = _meaMgr.ClockIn(model);
            if (result.Succeeded)
            {
                TempData["message"] = $"Meal{model.StaffId} was successfully added!";
                return RedirectToAction("Index");
            }
            return View(model);
        }
        //[HttpGet]
        //public ActionResult EditMeal(int id = 0)
        //{
        //    var result = _votMgr.GetMealById(id);
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
        //public ActionResult EditMeal(MealModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        //model.ModifiedBy = User.Identity.GetUserName();
        //        var result = _votMgr.UpdateMeal(model);
        //        if (result.Succeeded)
        //        {
        //            TempData["message"] = $"Meal{model.StaffId} was successfully added!";
        //            ModelState.AddModelError(string.Empty, "Update was successfully ");
        //            return RedirectToAction("Index");
        //        }
        //        else
        //        {
        //            ModelState.AddModelError(string.Empty, result.Message);
        //            return View(model);

        //        }
        //    }
        //    return View(model);
        //}
        //[HttpGet]
        //public ActionResult DeleteMeal(int id)
        //{
        //    var result = _votMgr.GetMealById(id);
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
        //public ActionResult DeleteMeal(int id, Meal model)
        //{
        //    var result = _votMgr.GetMealById(id);
        //    if (result == null)
        //    {
        //        return View("not found");
        //    }

        //    _votMgr.DeleteMeal(id);
        //    return RedirectToAction("Index");

            //return View();
        //}

    }
  }

