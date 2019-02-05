using Cyber_Kitchen.Entities;
using Cyber_Kitchen.Interface;
using Cyber_Kitchen.Models;
//using Cyber_Kitchen.ViewModels;
using Microsoft.AspNet.Identity;
using PagedList;
using System;
using System.Collections.Generic;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace Cyber_Kitchen.Controllers
{
    [Authorize]
    public class RatingController : Controller
    {
        private IRatingManager _ratMgr;
        private IVoterManager _votMgr;
        private IRestaurantManager _restMgr;
        //private string logedInUser;
        //private string sidUser;
        //private ClaimsPrincipal principal;


        public RatingController(IRatingManager ratMgr, IVoterManager votMgr, IRestaurantManager restMgr)
        {
            _ratMgr = ratMgr;
            _votMgr = votMgr;
            _restMgr = restMgr;

            //Get the current claims principal
            //principal = (ClaimsPrincipal)Thread.CurrentPrincipal;
            //// Get the claims values
            //logedInUser = principal.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier).Select(c => c.Value).SingleOrDefault();
            //sidUser = principal.Claims.Where(c => c.Type == ClaimTypes.Sid).Select(c => c.Value).SingleOrDefault();

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
            ViewBag.voters = new SelectList(_votMgr.GetVoters().Result, "VoterId", "StaffName");
            // ViewBag.restaurants = new SelectList(_restMgr.GetRestaurants().Result, "RestId", "RestName");
            // here i pass the data to the view
            ViewBag.restaurants = _restMgr.GetRestaurants().Result.ToList();
            ViewBag.periods = _ratMgr.GetPeriods().Result;
            //ViewBag.ratins = _ratMgr.GetRatings().Result;
            //var ratings = _ratMgr.GetPeriods().Result;
            //int RatingPeriod = ratings[0].PeriodId;
            //ViewBag.Period = RatingPeriod.ToString();

            return View();
        }

        [HttpPost, ActionName("CreateRating")]
        public ActionResult CreateRating(RatingExtensionModel model)
        {// RatingExtensionModel is a viewMoDEL that was use to pass the list of restaurants
            // voters was pass but was not used
            ViewBag.voters = new SelectList(_votMgr.GetVoters().Result, "VoterId", "StaffName");
            ViewBag.restaurants = _restMgr.GetRestaurants().Result.ToList();

            var ratingModel = new Operation<RatingModel>();
            // var ratingModel = new List<RatingModel>();//do this u re not using Operation class

            if (ModelState.IsValid)
            {
            
                try
                {
                    bool result = false;
                    string staffId = User.Identity.GetUserName();

                    foreach (var item in model.ListRating)
                    {
                  
                        ////to get the userId that login
                        item.CreatedBy = staffId;
                        RatingModel ratungmodel = new RatingModel
                        {
                           Quantity = item.Quantity,
                          Quality = item.Quality,
                          Taste = item.Taste,
                          TimeLiness = item.TimeLiness,
                          CustomerServices = item.CustomerServices,
                          CreatedBy = item.CreatedBy,
                           RestId = item.RestId
                                  
                        };
                         result = _ratMgr.CreateRating(ratungmodel);
                        ratingModel.Succeeded = true;


                        
                    }
                    if (result)
                    {
                        // here i save the other two questions into another table
                        var amountPrice = new AmountPriceModel
                        {
                            CreatedBy = staffId,
                            IsMfongComingBack = model.IsMfongComingBack,
                            AmountPriceId = model.IsCatererSelected.ToString(),
                            CreatedDate = DateTime.Now
                        };
                        // here i save the other two questions into another table
                        var amount = _restMgr.CreateAmountPrice(amountPrice);
                        {
                            if (amount.Succeeded)
                            {

                                TempData["message"] = $"    Your{""} voting was successfully added!";
                            }
                            //else
                            //{
                            //    throw new Exception("Please checked the button before submit");
                            //}
                        }

                    }


                    if (ratingModel.Succeeded == true)
                    {
                        //var results = _restMgr.GetRestaurants().Result.Where(x => x.IsChecked == true);
                        //return Content(String.Join(",", results.Select(x => x.RestId)));

                        TempData["message"] = $"    Your{""} voting was successfully added!";
                        if (User.IsInRole("Admin"))
                        {
                            return RedirectToAction("Index");
                        }

                        return RedirectToAction("Index", "Home");
                    }
                }
                //since model is a list, used foreach

                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }

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

        [HttpPost]
        public JsonResult DeleteRating(int id, string ratName)
        {
            int ratId = Convert.ToInt32(id);
            if (ratId > 0)
            {
                var result = _ratMgr.DeleteRating(ratId);
                if (result.Succeeded == true)
                {

                    return Json(new { status = true, message = $" {ratName} has been successfully deleted!", JsonRequestBehavior.AllowGet });
                }
                return Json(new { status = false, error = result.Message }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { status = false, error = "Invalid Id" }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public JsonResult DeleteRatings(string ids)
        {
            if (!string.IsNullOrEmpty(ids))
            {
                List<string> ratingIds = ids.Split('*').ToList();
                if (ratingIds.Count() > 0)
                {
                    Operation result = null;
                    foreach (var ratingids in ratingIds)
                    {
                        if (!string.IsNullOrEmpty(ratingids))
                        {
                            int ratId = Convert.ToInt32(ratingids);
                            result = _ratMgr.DeleteRating(ratId);
                        }
                    }
                    if (result.Succeeded == true)
                    {
                        return Json(new { status = true, message = " All selected rating(s) has been successfully deleted!", JsonRequestBehavior.AllowGet });
                    }
                    return Json(new { status = false, error = result.Message }, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(new { status = false, error = "Invalid Id" }, JsonRequestBehavior.AllowGet);
        }
    }
}