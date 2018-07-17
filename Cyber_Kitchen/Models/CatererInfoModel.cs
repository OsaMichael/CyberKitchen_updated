using Cyber_Kitchen.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Cyber_Kitchen.Models
{
    public class CatererInfoModel
    {
        public int CaterId { get; set; }
        [DisplayName("Restaurants Name")]
        [Required]
        public int? RestId { get; set; }
        [Required]
        public int? VoterId { get; set; }
        public string UserId { get; set; }
        [Required]
        public string CaterName { get; set; }
        [Required]
        public int Taste { get; set; }
        [Required]
        public int Quality { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public int TimeLiness { get; set; }
        [Required]
        public int CustomerServices { get; set; }
        [Required]
        public decimal TotalScore { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }

        public virtual Restaurant Restaurant { get; set; }
        public virtual Voter Voters { get; set; }
        public virtual ApplicationUser User { get; set; }

        public CatererInfoModel()
        {
            new RestaurantModel();
            new VoterModel();
            User = new ApplicationUser();
        }

        public CatererInfoModel(CatererInfo catererInfos)
        {
            this.Assign(catererInfos);
            // Restaurant = new RestaurantModel();
            //Voters = new VoterModel();
            User = new ApplicationUser();

        }

        public CatererInfo Create(CatererInfoModel model)
        {
            return new CatererInfo
            {
                RestId= model.RestId,
                VoterId = model.VoterId,
                Taste = model.Taste,
                Quality = model.Quality,
                Quantity = model.Quantity,
                TimeLiness = model.TimeLiness,
                CustomerServices = model.CustomerServices,
                TotalScore = model.TotalScore,
                CreatedBy = model.CreatedBy,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now
            };
        }
        public CatererInfo Edit(CatererInfo entity, CatererInfoModel model)
        {
            entity.CaterId = model.CaterId;
            entity.RestId = model.RestId;
            entity.Taste = model.Taste;
            entity.Quality = model.Quality;
            entity.Quantity = model.Quantity;
            entity.TimeLiness = model.TimeLiness;
            entity.CustomerServices = model.CustomerServices;
            entity.TotalScore = model.TotalScore;
            entity.ModifiedBy = model.ModifiedBy;
            entity.ModifiedDate = DateTime.Now;
            entity.CreatedDate = DateTime.Now;
            return entity;
        }
    }
}