﻿using Cyber_Kitchen.Infrastructure.Utils;
using Cyber_Kitchen.Interface;
using Cyber_Kitchen.Interface.Utils;
using Cyber_Kitchen.Manager;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Ninject.Modules;
using Ninject.Web.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cyber_Kitchen.Infrastructure
{
    public class Manager : NinjectModule
    {
        public override void Load()
        {
           
            Bind<IRestaurantManager>().To<RestaurantManager>();
            Bind<IScoreManager>().To<ScoreManager>();
            Bind<IRatingManager>().To<RatingManager>();
            Bind<IUploadManager>().To<UploadManager>();
            Bind<IMealManager>().To<MealManager>();
            Bind<IVoterManager>().To<VoterManager>();
            //Bind<RoleManager<IdentityRole>>().<ToRoleManager<IdentityRole>>();
            //Bind<ITemplateService1>().To<RazorTemplateService>();
            //Bind<RoleManager<IdentityRole>>().ToRoleManager<IdentityRole>().InRequestScope();




        }
    }
}