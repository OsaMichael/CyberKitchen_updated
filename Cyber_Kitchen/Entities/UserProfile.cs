using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Cyber_Kitchen.Entities
{
    public partial class UserProfile
    {
        [Key]
        public string FirstName { get; set; }
      
        public string LastName { get; set; }
      
        public string UserName { get; set; }
    }
}