using Cyber_Kitchen.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Cyber_Kitchen.Models
{
    public class RestaurantModel
    {

        [Key]

        public int RestId { get; set; }
        //[Required]
        public string RestName { get; set; }
        public string TotalScore { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }


        //public virtual ICollection<CatererInfo> CatererInfos { get; set; }
        public virtual ICollection<RatingModel> Ratings { get; set; }
        public virtual ICollection<VoterModel> Voters { get; set; }
        public virtual ICollection<ScoreModel> Scores { get; set; }
        

        public RestaurantModel()
        {
            new HashSet<VoterModel>();
            new HashSet<ScoreModel>();
            new HashSet<RatingModel>();
            //new HashSet<CatererInfoModel>();
        }
        public RestaurantModel(Restaurant restaurant)
        {
            this.Assign(restaurant);
            Voters = new HashSet<VoterModel>();
            Scores = new HashSet<ScoreModel>();
            Ratings = new HashSet<RatingModel>();
            //CatererInfos = new HashSet<CatererInfoModel>();
        }

        public Restaurant Create(RestaurantModel model)
        {
            return new Restaurant
            {
                RestName = model.RestName,
                TotalScore = model.TotalScore,
                CreatedBy = model.CreatedBy,
                ModifiedBy = model.ModifiedBy,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now

            };
        }
        public Restaurant Edit(Restaurant entity, RestaurantModel model)
            {
            entity.RestId = model.RestId;
            entity.RestName = model.RestName;

            entity.ModifiedBy = model.ModifiedBy;
            //entity.CreatedBy = model.CreatedBy;
            entity.ModifiedDate = DateTime.Now;
            //entity.CreatedDate = DateTime.Now;
            return entity;
        }
    }
}