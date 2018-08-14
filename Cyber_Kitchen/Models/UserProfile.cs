using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Cyber_Kitchen.Models
{
    public class UserProfile
    {
        [Key]
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string UserName { get; set; }
        //public string CreatedBy { get; set; }
        // public string UserId { get; set; }

        //public UserProfile(UserProfile userProfile)
        //{
        //    this.Assign(userProfile);
          

        //}

        //public UserProfile Create(UserProfile model)
        //{
        //    return new UserProfile
        //    {
                

        //    }
    }
}