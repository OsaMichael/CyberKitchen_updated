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
    public UploadManager (IDataRepository db,  IExcelProcessor excel )
        {
            _db = db;
            _excel = excel;
        }
        public Operation <List<VoterModel>> UploadStaffNames(Stream stream, VoterModel model)
        {
            return Operation.Create((Func<List<VoterModel>>)(() =>
            {
                var sheet = _excel.Load<VoterModel>(stream);
                var errors = new List<VoterModel>();
                foreach (var row in sheet)
                {
                    var staffNm = _db.Query<Voter>().Where(v => v.VoterId == row.VoterId).FirstOrDefault();
                    if (staffNm == null)
                    {
                        row.Message = "Voter does not exist";
                        errors.Add(row);
                        continue;
                    }
                    row.CreatedBy = model.CreatedBy;
                    row.ModifiedBy = model.ModifiedBy;
                    row.VotName = model.VotName;
                    row.StaffNo = model.StaffNo;
                    row.VotName = row.VotName /*== staffNm.VotName ? row.VotName : staffNm.VotName*/;
                    row.StaffNo = row.StaffNo /*== staffNm.StaffNo ? row.StaffNo : staffNm.StaffNo*/;

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