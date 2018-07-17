using Cyber_Kitchen.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Cyber_Kitchen.Models
{
    public class VoterModel 
    {
        public int VoterId { get; set; }
        public string UserId { get; set; }
        [Required]
        public string VotName { get; set; }
        [Required]
        public string StaffNo { get; set; }
        public string Message { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public virtual ApplicationUser User { get; set; }

        public VoterModel()
        {

        }
        public VoterModel(Voter voter)
        {
            this.Assign(voter);
        }
        public Voter Create(VoterModel model)
        {
            return new Voter
            {
                UserId = model.UserId,
                VotName = model.VotName,
                StaffNo = model.StaffNo,
                CreatedBy = model.CreatedBy,
                CreatedDate = DateTime.Now,
                //ModifiedDate = DateTime.Now

            };

        }
        public Voter Edit(Voter entity, VoterModel model)
        {
            // entity.VoterId = model.VoterId;
            entity.UserId = model.UserId;
            entity.VotName = model.VotName;
            entity.StaffNo = model.StaffNo;
            //entity.CreatedBy = model.CreatedBy;
            entity.ModifiedBy = model.ModifiedBy;
            entity.ModifiedDate = DateTime.Now;
            return entity;

        }
    }
}