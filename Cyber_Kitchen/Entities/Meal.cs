using Cyber_Kitchen.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Cyber_Kitchen.Entities
{
    public partial class Meal
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

        public virtual ApplicationUser Staffs { get; set; }
        public virtual Restaurant Restaurant { get; set; }
    }
}