using Cyber_Kitchen.Entities;
using Cyber_Kitchen.Interface;
using Cyber_Kitchen.Models;
//using Cyber_Kitchen.ViewModels;
using Microsoft.AspNet.Identity;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cyber_Kitchen.Controllers
{
    public class RatingController : Controller
    {
        private IRatingManager _ratMgr;
        private IVoterManager _votMgr;
        private IRestaurantManager _restMgr;

        public RatingController(IRatingManager ratMgr, IVoterManager votMgr, IRestaurantManager restMgr)
        {
            _ratMgr = ratMgr;
            _votMgr = votMgr;
            _restMgr = restMgr;
        }


        //GET:                       // int? page, was added for pagination
        public ActionResult Index( int? page)
        {
            if (TempData["message"] != null)
            {
                ViewBag.Success = (string)TempData["message"];
            }
            var results = _ratMgr.GetRatings();

            if (results.Succeeded == true)
            {
                List<Rating> mylist = new List<Rating>();

                foreach (var item in results.Unwrap())
                {
                    item.TotalScore =
                        item.Quality + item.Quantity + item.Taste + item.CustomerServices + item.TimeLiness;
                }                             // this was added for pagination
                return View(results.Unwrap().ToPagedList(page ?? 1, 12));
            }
            else
            {
                ModelState.AddModelError(string.Empty, "An error occure");
                                             // this was added for pagination
                return View(results.Unwrap().ToPagedList(page ?? 1, 12));
            }
        }
       

        [HttpGet]
        public ActionResult CreateRating()
        {
            ViewBag.voters = new SelectList(_votMgr.GetVoters().Result, "VoterId", "VotName");
            // ViewBag.restaurants = new SelectList(_restMgr.GetRestaurants().Result, "RestId", "RestName");
            // here i pass the data to the view
            ViewBag.restaurants = _restMgr.GetRestaurants().Result;

            return View();
        }

        [HttpPost]
        public ActionResult CreateRating(List<RatingModel> model)
        {
            //ViewBag.voters = new SelectList(_votMgr.GetVoters().Result, "VoterId", "VotName");
            //var userId = User.Identity.GetUserId();
            //model.UserId = userId;
            //model.CreatedBy = User.Identity.GetUserName();

            var ratingModel = new Operation<RatingModel>();
            foreach (var item in model)
            {
                
                item.CreatedBy = User.Identity.GetUserId();
                //to get the userId that login
                item.UserId = User.Identity.GetUserId();
                ratingModel = _ratMgr.CreateRating(item);
            }
           
            if (ratingModel.Succeeded == true)
            {
                TempData["message"] = $"{model} was successfully added!";


                if (User.IsInRole("Admin"))
                {
                    return RedirectToAction("Index");
                }
                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult EditRating(int id = 0)
        {
            var result = _ratMgr.GetRatingById(id);
            if (result.Succeeded == true)
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
        public ActionResult EditRating(RatingModel model)
        {

            if (ModelState.IsValid)
            {
                model.ModifiedBy = User.Identity.GetUserId();
                var result = _ratMgr.UpdateRating(model);
                if (result.Succeeded == true)
                {
                    TempData["message"] = $"Rating{model.RatId} was successfully added!";
                    ModelState.AddModelError(string.Empty, "Update was successfully ");
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError(string.Empty, result.Message);
                ViewBag.Error = $"Error occured : {result.Message}";
                return View(model);
            }
            else
            {
                return View(model);
            }
        }

        //[HttpGet]
        //public ActionResult DeleteRating(int id)
        //{
        //    var result = _ratMgr.GetRatingById(id);
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

        [HttpPost]
        public ActionResult DeleteRating(int id, Score model)
        {
            var result = _ratMgr.GetRatingById(id);
            if (result == null)
            {
                return View("not found");
            }
            _ratMgr.DeleteRating(id);
            return RedirectToAction("Index");
        }
    }
}