using Cyber_Kitchen.Entities;
using Cyber_Kitchen.Interface;
using Cyber_Kitchen.Interface.Utils;
using Cyber_Kitchen.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cyber_Kitchen.Controllers
{
    [Authorize]
    public class VoterController : Controller
    {
        private IVoterManager _votMgr;
        private IExcelProcessor _excel;
        public VoterController(IVoterManager votMgr,IExcelProcessor excel )
        {
            _votMgr = votMgr;
            _excel = excel;
        }

        // GET: Restaurant
        public ActionResult Index()
        {
            if (TempData["message"] != null)
            {
                ViewBag.Success = (string)TempData["message"];
            }
            var results = _votMgr.GetVoters();
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
        public ActionResult CreateVoter()
        {

            return View();
        }
        [HttpPost]
       
        public ActionResult CreateVoter(VoterModel model)
        {
            model.CreatedBy = User.Identity.GetUserName();
            var result = _votMgr.CreateVoter(model);
            if (result.Succeeded == true)
            {
                TempData["message"] = $"Voter{model.StaffName} was successfully added!";
                return RedirectToAction("Index");
            }
            return View(model);
        }
        [HttpGet]
        public ActionResult EditVoter(int id = 0)
        {
            var result = _votMgr.GetVoterById(id);
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
        public ActionResult EditVoter(VoterModel model)
        {
            if (ModelState.IsValid)
            {
                model.ModifiedBy = User.Identity.GetUserName();
                var result = _votMgr.UpdateVoter(model);
                if (result.Succeeded)
                {
                    TempData["message"] = $"Voter{model.StaffName} was successfully added!";
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
        //[HttpGet]
        //public ActionResult DeleteVoter(int id)
        //{
        //    var result = _votMgr.GetVoterById(id);
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
        public JsonResult DeleteVoter(int id, string votName)
        {
            int votId = Convert.ToInt32(id);
            if (votId > 0)
            {
                var result = _votMgr.DeleteVoter(votId);
                if (result.Succeeded == true)
                {


                    return Json(new { status = true, message = $" {votName} has been successfully deleted!", JsonRequestBehavior.AllowGet });
                }
                return Json(new { status = false, error = result.Message }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { status = false, error = "Invalid Id" }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult UploadVoters()
        {
            DropDown();
            var model = new VoterModel();
            return View(model);
        } 
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult UploadVoters(HttpPostedFileBase file, VoterModel model)
        {
            DropDown();
            if (file != null && file.ContentLength != 0 && (System.IO.Path.GetExtension(file.FileName).ToLower() == ".xlsx" || System.IO.Path.GetExtension(file.FileName).ToLower() == ".xls"))
            {
                model.CreatedBy = User.Identity.GetUserName();
                model.ModifiedBy = User.Identity.GetUserName();
                var uploadedResult = _votMgr.UploadVoterNames(file.InputStream, model);
                if (uploadedResult.Succeeded == true)
                {
                    TempData["message"] = $"  Voter was successfully Uploaded!";
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, uploadedResult.Message);
                    ViewBag.Error = $"Error occured : {uploadedResult.Message}";
                    //log.Error(uploadedResult.Message);
                    return View(model);
                }
            }
            else
            {
                ViewBag.Error = "Only Excel Sheets are allowed";
            }
            return View();
        }
        private void DropDown()
        {

            ViewBag.voters = new SelectList(_votMgr.GetVoters().Result, "VoterId", "StaffName");

        }

        public ActionResult DownloadVoterTemplate()
        {
            return Redirect("~/DataUploadTemplates/VoterNamesample.xlsx");
        }
    }
}