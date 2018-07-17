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
        public string UserId { get; set; } 
        public DateTime Day { get; set; }
        public DateTime Date { get; set; }
        public bool Status { get; set; }

        //public virtual VoterModel Voters { get; set; }
        public virtual RestaurantModel Restaurant { get; set; }
        public virtual ApplicationUser Users { get; set; }

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
            Users = new ApplicationUser();

        }

        public Meal Create(MealModel model)
        {
            return new Meal
            {
                UserId = model.UserId
             
             
               
            };
        }
        public Meal Edit(Meal entity, MealModel model)
        {
           
            entity.UserId = model.UserId;
           

            return entity;
        }
    }
}