using Cyber_Kitchen.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Cyber_Kitchen.Models
{
    public class PeriodModel
    {

        [Key]
        public int PeriodId { get; set; }
        public string PeriodName { get; set; }
        public bool IsActive { get; set; }
        public string Discription { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
<<<<<<< HEAD
        //public virtual RatingModel Rating { get; set; }
        public virtual ICollection<RatingModel> Ratings { get; set; }
=======
        public virtual RatingModel Rating { get; set; }
>>>>>>> 45d48fb9b9502dc94d7482958e81beb27f2b68e7



        public PeriodModel()
        {
            IsActive = true;
<<<<<<< HEAD
            new HashSet<RatingModel>();
=======
>>>>>>> 45d48fb9b9502dc94d7482958e81beb27f2b68e7
            //new RatingModel();
        }
        public PeriodModel(Period period)
        {
            this.Assign(period);
<<<<<<< HEAD
            //Rating = new RatingModel();
            Ratings = new HashSet<RatingModel>();
=======
            Rating = new RatingModel();
>>>>>>> 45d48fb9b9502dc94d7482958e81beb27f2b68e7

        }
        public Period Create(PeriodModel model)
        {
            return new Period
            {
                PeriodName = model.PeriodName,
                Discription = model.Discription,
                EndDate = model.EndDate,
                StartDate = model.StartDate,
                CreatedBy = model.CreatedBy,
                //CreatedDate = model.CreatedDate,
                CreatedDate = DateTime.Now,
                //IsApplicationActive = model.IsApplicationActive

            };
        }
        public Period Edit(Period entity, PeriodModel model)
        {

            entity.PeriodName = model.PeriodName;

            entity.ModifiedBy = model.ModifiedBy;
            //entity.CreatedBy = model.CreatedBy;
            entity.ModifiedDate = DateTime.Now;
            //IsApplicationActive = model.IsApplicationActive;
            //entity.CreatedDate = DateTime.Now;
            return entity;
        }
    }
}