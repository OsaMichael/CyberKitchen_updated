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
    public class DataUploadController : Controller
    {
        private IUploadManager _uploadMgr;
        private IVoterManager _votMgr;
        public DataUploadController(IUploadManager uploadMgr, IVoterManager votMgr)
        {
            _uploadMgr = uploadMgr;
            _votMgr = votMgr;
        }
        // GET: DataUpload
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult VoterNames()
        {
            CheckTempData();
            DropDown();
            var model = new VoterModel();
            return View(model);
        }
        [HttpPost]
        public ActionResult VoterNames(HttpPostedFileBase file, VoterModel model)
        {
            DropDown();
            if (file != null && file.ContentLength != 0 && (System.IO.Path.GetExtension(file.FileName).ToLower() == ".xlsx" || System.IO.Path.GetExtension(file.FileName).ToLower() == ".xls"))
            {
                model.CreatedBy = User.Identity.GetUserName();
                model.ModifiedBy = User.Identity.GetUserName();
                var uploadedResult = _uploadMgr.UploadStaffNames(file.InputStream, model);

                if (uploadedResult.Succeeded == false)
                {
                    ViewBag.Error = $"{uploadedResult.Message}";
                    ModelState.AddModelError(string.Empty, uploadedResult.Message);
                    return View(model);
                }
                TempData["message"] = new ResponseModel { Success = $"Voter Names was successfully Uploaded!", Error = "" };
                return RedirectToAction("AdmittedStudents");
            }
            ModelState.AddModelError(string.Empty, "Only Excel Sheets are allowed");
            return View(model);
        }
        private void DropDown()
        {
            ViewBag.voters = new SelectList(_votMgr.GetVoters().Result, "VoterId", "StaffName");

        }
        private void CheckTempData()
        {
            if (TempData["message"] != null)
            {
                var responseModel = (ResponseModel)TempData["message"];
                ViewBag.Success = !string.IsNullOrEmpty(responseModel.Success) ? responseModel.Success : "";
                ViewBag.Error = !string.IsNullOrEmpty(responseModel.Error) ? responseModel.Error : "";
                ModelState.AddModelError(string.Empty, !string.IsNullOrEmpty(responseModel.Success) ? responseModel.Success : responseModel.Error);
            }
            TempData["message"] = null;
        }
        public ActionResult DownloadVoterNameTemplate()
        {
            return Redirect("~/DataUploadTemplates/VoterNamesample.xlsx");
        }

    }
}