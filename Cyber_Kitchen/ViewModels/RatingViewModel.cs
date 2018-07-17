using Cyber_Kitchen.Entities;
using Cyber_Kitchen.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Cyber_Kitchen.ViewModels
{
    public class RatingViewModel
    {
        public IEnumerable<Restaurant> Restaurants { get; set; } 
        //public IEnumerable<Voter> Voters { get; set; } 
        public Rating Rating { get; set; }
        public int? VoterId { get; set; }
    }
}