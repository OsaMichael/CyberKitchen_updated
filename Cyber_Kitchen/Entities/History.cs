using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cyber_Kitchen.Entities
{
    public class History
    {
        public int Id { get; set; }
        public int PeriodId { get; set; }
        public string StaffNo { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string CreatedBy { get; set; }
    }
}