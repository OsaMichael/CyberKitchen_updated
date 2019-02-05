using Cyber_Kitchen.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Cyber_Kitchen.Models
{
    public class AmountPriceModel
    {
        public int ID { get; set; }
        public int? RestId { get; set; }
        [Required]
        public string AmountPriceId { get; set; }
        public string IsMfongComingBack { get; set; }
        [Required]
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }

        public AmountPriceModel()
        {
           
            //new HashSet<CatererInfoModel>();
        }
        public AmountPriceModel(AmountPrice amountPrice)
        {
            this.Assign(amountPrice);
         
            //CatererInfos = new HashSet<CatererInfoModel>();
        }
        public AmountPrice Create(AmountPriceModel model)
        {
            return new AmountPrice
            {
                 AmountPriceId = model.AmountPriceId,
                IsMfongComingBack = model.IsMfongComingBack,
                CreatedBy = model.CreatedBy,          
                CreatedDate = DateTime.Now,
                RestId = model.RestId
             

            };
        }
    }
}