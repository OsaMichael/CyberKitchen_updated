using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cyber_Kitchen.Entities
{
    public class AmountPrice
    {
        public int ID { get; set; }
        public int? RestId { get; set; }
        public string AmountPriceId { get; set; }
        public string IsMfongComingBack { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}