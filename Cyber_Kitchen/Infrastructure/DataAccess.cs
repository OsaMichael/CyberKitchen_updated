using Cyber_Kitchen.Entities;
using Cyber_Kitchen.Interface;
using Cyber_Kitchen.Manager;
using Cyber_Kitchen.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Ninject;
using Ninject.Modules;
using Ninject.Web.Common;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Cyber_Kitchen.Infrastructure
{
    public class DataAccess : NinjectModule
    {
        public override void Load()
        {
            
            Kernel.Bind<DbContext>().ToSelf().InRequestScope();
            Bind<ApplicationDbContext>().To<ApplicationDbContext>().InRequestScope();
            Bind<IDataRepository>().To<EntityRepository>().InRequestScope();
           // Bind<IVoterManager>().To<VoterManager>();

            //Kernel.Bind<UserManager<ApplicationUser>>().ToSelf();
            //Kernel.Bind<UserManager<ApplicationUser>>().ToSelf();
            //Kernel.Bind<ApplicationUserManager>().ToSelf();

            //Bind<ApplicationSignInManager>().ToMethod((context) =>
            //{
            //    var cbase = new HttpContextWrapper(HttpContext.Current);
            //    return cbase.GetOwinContext().Get<ApplicationSignInManager>();
            //});


        }
    }
}