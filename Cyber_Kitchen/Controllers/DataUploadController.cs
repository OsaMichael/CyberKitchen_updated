using Cyber_Kitchen.Interface;
using Cyber_Kitchen.Interface.Utils;
using Cyber_Kitchen.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;


namespace Cyber_Kitchen.Controllers
{
    public class DataUploadController : Controller
    {
       
        private IUploadManager _uploadMgr;
        private IVoterManager _votMgr;
        private IExcelProcessor _excel;
        private ApplicationUserManager _userManager;
        public DataUploadController(ApplicationUserManager userManager, IUploadManager uploadMgr, IVoterManager votMgr, IExcelProcessor excel)
        {
            _uploadMgr = uploadMgr;
            _votMgr = votMgr;
            _excel = excel;
            _userManager = userManager;
          
        }
        //public async Task<ActionResult> Users(Stream stream, UserModel model)
        //{
           
        //        var shts = _excel.Load<UserModel>(stream);

        //        foreach (var row in shts)
        //        {
        //            var userModel = new UserModel
        //            {
        //                UserName = row.UserName,
        //                Password = "open"
        //            };
                  
        //              if (!string.IsNullOrEmpty(userModel.UserName))
        //            {
                                    
        //            var user = new ApplicationUser
        //               { UserName = userModel.UserName,
        //                 PasswordHash = userModel.Password
        //               };

        //             var result = await UserManager.CreateAsync(user, user.Password);
                   

        //            //if  user creation is successful
        //            if (result.Succeeded)
        //            {
        //                // create user role "User"
        //                var addRole = await UserManager.AddToRoleAsync(user.Id, "User");
        //            }
                                       
        //        }

        //    }

        //    return View(model);
           
        //}
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