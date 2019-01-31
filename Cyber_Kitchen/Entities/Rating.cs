using Cyber_Kitchen.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Cyber_Kitchen.Entities
{
    public partial class Rating
    {
        [Key]
        public int RatId { get; set; }
        public int? VoterId { get; set; }
        public int? RestId { get; set; }
        public string Sid { get; set; }
        //public string StaffNo { get; set; }
        public string UserId { get; set; }
        public int? PeriodId { get; set; }
        public int Taste { get; set; }
        public int Quality { get; set; }
        public string AmountPriceId { get; set; }
        public bool IsCatererSelected { get; set; }
        public string IsMfongCominBack { get; set; }
        public int Quantity { get; set; }
        public int TimeLiness { get; set; }
        public bool IsChecked { get; set; }
        public int CustomerServices { get; set; }

        public decimal AmountPay { get; set; }
        public bool IsBackTo { get; set; }
        public decimal TotalScore { get; set; }

        public string ImageUrl { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }

        public virtual Voter Voter { get; set; }
        public virtual Restaurant Restaurant { get; set; }
        public virtual ApplicationUser User { get; set; }
        public virtual PeriodModel Period { get; set; }
        //public virtual UserProfile UserPro { get; set; }
    }
}