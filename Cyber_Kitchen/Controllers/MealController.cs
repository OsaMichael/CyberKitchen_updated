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
    public class MealController : Controller
    {
        private IVoterManager _votMgr;

        public MealController( IVoterManager votMgr)
        {
            _votMgr = votMgr;
        }
        // GET: Meal
        public ActionResult Index()
        {
            if (TempData["message"] != null)
            {
                ViewBag.Success = (string)TempData["message"];
            }
            var results = _votMgr.GetMeals();
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
        public ActionResult CreateMeal()
        {

            return View();
        }
        [HttpPost]

        public ActionResult CreateMeal(MealModel model)
        {
            
            model.Status = true;
            model.UserId = User.Identity.GetUserId();
            var result = _votMgr.CreateMeal(model);
            if (result.Succeeded == true)
            {
                TempData["message"] = $"Meal{model.UserId} was successfully added!";
                return RedirectToAction("Index");
            }
            return View(model);
        }
        [HttpGet]
        public ActionResult EditMeal(int id = 0)
        {
            var result = _votMgr.GetMealById(id);
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
        public ActionResult EditMeal(MealModel model)
        {
            if (ModelState.IsValid)
            {
                //model.ModifiedBy = User.Identity.GetUserName();
                var result = _votMgr.UpdateMeal(model);
                if (result.Succeeded)
                {
                    TempData["message"] = $"Meal{model.UserId} was successfully added!";
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
        public ActionResult DeleteMeal(int id)
        {
            var result = _votMgr.GetMealById(id);
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
        public ActionResult DeleteMeal(int id, Meal model)
        {
            var result = _votMgr.GetMealById(id);
            if (result == null)
            {
                return View("not found");
            }

            _votMgr.DeleteMeal(id);
            return RedirectToAction("Index");

            //return View();
        }

    }
  }

