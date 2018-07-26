using Cyber_Kitchen.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Cyber_Kitchen.Models
{
    public class MealModel
    {
        [Key]
        public int Id { get; set; }
        //public int? VoterId { get; set; }
        public int? RestId { get; set; }
        //public string UserId { get; set; }
        public string StaffId { get; set; } 
        public DateTime Day { get; set; }
        public DateTime Date { get; set; }
        public bool Status { get; set; }

        //public virtual VoterModel Voters { get; set; }
        [ForeignKey(nameof(RestId))]
        public virtual RestaurantModel Restaurant { get; set; }
        [ForeignKey(nameof(StaffId))]
        public virtual  ApplicationUser Staff { get; set; }
        

        public MealModel()
        {
            //new VoterModel();
            new RestaurantModel();
            new ApplicationUser();
           
        }

        public MealModel(Meal meals)
        {
            this.Assign(meals);
            Restaurant = new RestaurantModel();
            //Voters = new VoterModel();
            Staff = new ApplicationUser();
         

        }

        public Meal ClockIn(MealModel model)
        {
            return new Meal
            {
                StaffId = model.StaffId
            };
        }
        public Meal Edit(Meal entity, MealModel model)
        {           
            entity.StaffId = model.StaffId;

            return entity;
        }
    }
}