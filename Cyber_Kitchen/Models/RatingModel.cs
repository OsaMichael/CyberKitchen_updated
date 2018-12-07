﻿using Cyber_Kitchen.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Cyber_Kitchen.Models
{
    public class RatingModel
    {
        [Key]
        //[Required]
        public int RatId { get; set; }
        //[Required]
        public int? VoterId { get; set; }
        [DisplayName("Restaurants Name")]
        //[Required]
        public int? RestId { get; set; }
        //[Required]
        public string UserId { get; set; }
        //[Required]
        // public string StaffNo { get; set; }
        public string Sid { get; set; }
        //[Required]
        //public string UserName { get; set; }
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
        public decimal TotalScore { get; set; }
        public string Message { get; set; }
        public string ImageUrl { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public virtual RestaurantModel Restaurant { get; set; }
        public virtual VoterModel Voters { get; set; }
        public virtual ApplicationUser User { get; set; }
        //public virtual UserProfile UserPro { get; set; }


        public RatingModel()
        {
            new RestaurantModel();
            new VoterModel();
            new ApplicationUser();
          //  new UserProfile();
    }

        public RatingModel(Rating ratings)
        {
            this.Assign(ratings);
            Restaurant = new RestaurantModel();
            Voters = new VoterModel();
           User = new ApplicationUser();
           // UserPro = new UserProfile();

        }

        public Rating Create(RatingModel model)
        {
            return new Rating
            {
                RestId = model.RestId,
                VoterId = model.VoterId,
                UserId = model.UserId,
              
                Sid   = model.Sid,
                Taste = model.Taste,
                Quality = model.Quality,
                Quantity = model.Quantity,
                TimeLiness = model.TimeLiness,
                CustomerServices = model.CustomerServices,
                TotalScore = model.TotalScore, 
                //UserName = model.UserName,              
                //FirstName = model.FirstName,
                //LastName = model.LastName,
                ImageUrl = model.ImageUrl,
                CreatedBy = model.CreatedBy,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now
            };
        }
        public Rating Edit(Rating entity, RatingModel model)
        {
            entity.RatId = model.RatId;
            entity.RestId = model.RestId;
            entity.VoterId = model.VoterId;
            entity.UserId = model.UserId;
            entity.Taste = model.Taste;
            entity.Quality = model.Quality;
            entity.Quantity = model.Quantity;
            entity.TimeLiness = model.TimeLiness;
            entity.CustomerServices = model.CustomerServices;
            entity.TotalScore = model.TotalScore;
            entity.ImageUrl = model.ImageUrl;
            entity.ModifiedBy = model.ModifiedBy;
            entity.ModifiedDate = DateTime.Now;
            entity.CreatedDate = DateTime.Now;
            return entity;
        }
    }
}