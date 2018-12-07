using Cyber_Kitchen.Entities;
using Cyber_Kitchen.Interface;
using Cyber_Kitchen.Interface.Utils;
using Cyber_Kitchen.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Cyber_Kitchen.Manager
{
    public class UploadManager : IUploadManager
    {
        private IDataRepository _db;
        private IExcelProcessor _excel;
        private IVoterManager _votgr;
    public UploadManager (IDataRepository db,  IExcelProcessor excel , IVoterManager votgr)
        {
            _db = db;
            _excel = excel;
            _votgr = votgr;
        }
       

        public Operation <List<VoterModel>> UploadStaffNames(Stream stream, VoterModel model)
        {
            return Operation.Create((Func<List<VoterModel>>)(() =>
            {
                var sheet = _excel.Load<VoterModel>(stream);
                var errors = new List<VoterModel>();
                foreach (var row in sheet)
                {
                    var staffNm = _db.Query<Voter>().Where(v => v.VoterId == row.VoterId ).FirstOrDefault();
                   // var votername = _votgr.GetVoters().Result.Where(v => v.StaffName == model.StaffName).FirstOrDefault();
                    if (staffNm == null)
                    {
                        row.Message = "Voter does not exist";
                        errors.Add(row);
                        continue;
                    }
                    else
                    {
                        row.Message = "Voter does not exist";
                    }
                    row.CreatedBy = model.CreatedBy;
                    row.ModifiedBy = model.ModifiedBy;
                    row.StaffName = model.StaffName;
                    row.StaffNo = model.StaffNo;
                    row.Email = model.Email;
                    row.StaffName = row.StaffName /*== staffNm.StaffName ? row.StaffName : staffNm.StaffName*/;
                    row.StaffNo = row.StaffNo;
                    row.Email = row.Email  /*== staffNm.StaffNo ? row.StaffNo : staffNm.StaffNo*/;

                    if (staffNm == null)
                    {
                        var entity = row.Create(row);
                        _db.Add(entity);
                    }
                    else
                    {
                        var entity = row.Edit(staffNm, row);
                        _db.Update(entity);
                    }
                    var result = _db.SaveChanges();
                    if (result.Succeeded == false)
                    {
                        row.Message = result.Message;
                        errors.Add(row);
                    }
                }
                return errors;
            }));
                
            
        }
    }
}