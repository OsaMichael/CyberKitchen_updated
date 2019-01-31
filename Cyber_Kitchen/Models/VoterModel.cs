﻿using Cyber_Kitchen.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Cyber_Kitchen.Models
{
    public class VoterModel 
    {
        [Key]
        public int VoterId { get; set; }
        public string UserId { get; set; }
        [Required]
        public string StaffName { get; set; }
        [Required]
        public string StaffNo { get; set; }
        [Required]
        public string Email { get; set; }
     
        public string Department { get; set; }
        public string Message { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public virtual ApplicationUser User { get; set; }
        public virtual ICollection<RatingModel> Ratings { get; set; }

        public VoterModel()
        {
            new HashSet<RatingModel>();
            new ApplicationUser();
        }
        public VoterModel(Voter voter)
        {
            this.Assign(voter);
            new HashSet<RatingModel>();
            new ApplicationUser();
        }
        public Voter Create(VoterModel model)
        {
            return new Voter
            {
                UserId = model.UserId,
                StaffName = model.StaffName,
                StaffNo = model.StaffNo,
                Email =    model.Email,
                Department = model.Department,
                CreatedBy = model.CreatedBy,
                CreatedDate = DateTime.Now,
                //ModifiedDate = DateTime.Now

            };

        }
        public Voter Edit(Voter entity, VoterModel model)
        {
            // entity.VoterId = model.VoterId;
            entity.UserId = model.UserId;
            entity.StaffName = model.StaffName;
            entity.StaffNo = model.StaffNo;
            entity.Department = model.Department;
            entity.Email   = model.Email;
            //entity.CreatedBy = model.CreatedBy;
            entity.ModifiedBy = model.ModifiedBy;
            entity.ModifiedDate = DateTime.Now;
            return entity;

        }
    }
}