﻿using Cyber_Kitchen.Interface;
using Cyber_Kitchen.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.IO;
using Cyber_Kitchen.Interface.Utils;
using Cyber_Kitchen.Entities;
using Microsoft.AspNet.Identity;

namespace Cyber_Kitchen.Manager
{
    public class VoterManager : IVoterManager
    {
        private ApplicationDbContext _context;
        private IExcelProcessor _excel;
        //private UserManager<ApplicationUser> _usermanager;
        public VoterManager(ApplicationDbContext context, IExcelProcessor excel)
        {
            _context = context;
            _excel = excel;
           // _usermanager = usermanager;
        }

        public Operation<VoterModel> CreateVoter(VoterModel model)
        {
            return Operation.Create(() =>
            {
                try
                {
                    var isExists = _context.Voters.Where(c =>c.Email == model.Email /*c.StaffName == model.StaffName &&*//* c.StaffNo == model.StaffNo*/).FirstOrDefault();
                    if (isExists != null) throw new Exception("user email already exist");

                    var exist = _context.Voters.Where(e => e.StaffNo == model.StaffNo).FirstOrDefault();
                    if (exist != null) throw new Exception("user staffno already exist");
                  
                    var entity = model.Create(model);
                    _context.Voters.Add(entity);
                    _context.SaveChanges();

                }
                catch  (Exception xe)
                {
                    throw xe;
                }
                //model.Validate();

                return model;
            });
        }
        public Operation<VoterModel[]> GetVoters()
        {
            return Operation.Create(() =>
            {
                var entities = _context.Voters.ToList();

                var models = entities.Select(c => new VoterModel(c)
                {
                    // this are fk to aviod the id displaying in the UI
                    //User = new ApplicationUser(c.User)
                    StaffName = c.StaffName,
                    Email = c.Email,
                    StaffNo = c.StaffNo,
                    UserId = c.UserId
                }
                ).ToArray();
                return models;
            });
        }
        // this mothod was added to validate users in the db before login
        public Operation<Voter> GetVoters(string email)
        {
            return Operation.Create(() =>
            {
               // List<Voter> model = new List<Voter>();
                var entities = _context.Voters.Where(x => x.Email == email).FirstOrDefault();
                if (entities == null) throw new Exception("user does not exist");

                return entities;
            });
        }
        public Operation<VoterModel> UpdateVoter(VoterModel model)
        {
            return Operation.Create(() =>
            {
                //model.Validate();
                var isExist = _context.Voters.Find(model.VoterId);
                if (isExist == null) throw new Exception("Voter does not exist");

                var entity = model.Edit(isExist, model);
                _context.Entry(entity);
                _context.SaveChanges();
                return model;
            });
        }
        public Operation<VoterModel> GetVoterById(int voterId)
        {
            return Operation.Create(() =>
            {
                var entity = _context.Voters.Where(c => c.VoterId == voterId).FirstOrDefault();
                if (entity == null) throw new Exception("Voter does not exist");
                return new VoterModel(entity);

            });
        }
     
        public Operation Details(int id)
        {
            return Operation.Create(() =>
            {
                var entity = _context.Voters.Include(s => s.VoterId == id).FirstOrDefault();
                if (entity == null) throw new Exception("Voter does not  exist");
                return new VoterModel(entity);
            });
        }
        public Operation DeleteVoter(int id)
        {
            return Operation.Create(() =>
            {
                var entity = _context.Voters.Find(id);
                if (entity == null) throw new Exception("Voter does not exist");

                _context.Voters.Remove(entity);
                _context.SaveChanges();
            });
        }
        public Operation<List<VoterModel>> UploadVoterNames(Stream stream, VoterModel model)
        {
            return Operation.Create(() =>
            {
                var sheet = _excel.Load<VoterModel>(stream);
                var errors = new List<VoterModel>();
                foreach (var row in sheet)
                {
                    // note: I check if staffNo exist in the database, if null, add the data and save it. if yes, edit the data and save it.
                    var voter = _context.Voters.Where(v => v.StaffName == row.StaffName && v.StaffNo ==row.StaffNo && v.Email == row.Email).FirstOrDefault();
                    row.CreatedBy = model.CreatedBy;
                    row.ModifiedBy = model.ModifiedBy;
                    row.CreatedDate = DateTime.Now;

                    if (voter != null) throw new Exception("Name already exist");
                    //{

                    if(row.StaffName == null || row.StaffNo == null || row.Email == null)
                    {
                        throw new Exception("An Empty cell in the file");
                    }
                        var voterEntity = new Voter
                        {
                            CreatedBy = row.CreatedBy,
                            ModifiedBy = row.ModifiedBy,
                            CreatedDate = DateTime.Now,

                            StaffName = row.StaffName,
                            StaffNo = row.StaffNo,
                            Email = row.Email
                            //ModifiedDate  = DateTime.Now
                        };
                        _context.Voters.Add(voterEntity);
                        //continue;
                    //}
            
                    //else
                    //{
                     
                    //    voter.StaffNo = row.StaffNo;
                    //    voter.StaffName = row.StaffName;
                    //    voter.Email = row.Email;
                    //    voter.CreatedDate = DateTime.Now;
                    //    //var entity1 = row.Edit(voter, row);
                    //    _context.Entry(voter);
                     
                    //}
                    //errors.Add(row); 
                }
                _context.SaveChanges();
                return errors;
            });
        }

        ///////////////// beginning of  meal

    }
}