using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Cyber_Kitchen.Entities
{
<<<<<<< HEAD
    public partial class Period
=======
    public class Period
>>>>>>> 45d48fb9b9502dc94d7482958e81beb27f2b68e7
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
        //public bool IsApplicationActive { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}