﻿using Cyber_Kitchen.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cyber_Kitchen.Entities
{
  
        public partial class Voter
        {
            public int VoterId { get; set; }
        public string UserId { get; set; }
        public string StaffName { get; set; }
            public string Email { get; set; }
            public string StaffNo { get; set; }
        public string Department { get; set; }
        public string CreatedBy { get; set; }
            public DateTime CreatedDate { get; set; }
            public string ModifiedBy { get; set; }
            public DateTime ModifiedDate { get; set; }
            //public bool isPasswordChanged { get; set; }

            public virtual ApplicationUser User { get; set; }
    }
}