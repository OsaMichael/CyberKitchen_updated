using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cyber_Kitchen.Models
{
    public class RatingExtensionModel
    {
        public List<RestaurantModel> ListRating { get; set; }
        public string IsCatererSelected { get; set; }
        public string IsMfongComingBack { get; set; }
    }
}