using Cyber_Kitchen.Entities;
using Cyber_Kitchen.Interface;
using Cyber_Kitchen.Manager;
using Cyber_Kitchen.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
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
            
            Bind<DbContext>().ToMethod(ctx => ApplicationDbContext.Create()).InRequestScope();
            Bind<ApplicationDbContext>().To<ApplicationDbContext>().InRequestScope();
            Bind<IDataRepository>().To<EntityRepository>().InRequestScope();
            Bind<ApplicationSignInManager>().To<ApplicationSignInManager>().InRequestScope();
            //Bind<ApplicationUserManager>().To<ApplicationUserManager().InRequestScope();

            // Bind<IVoterManager>().To<VoterManager>();

            Bind<UserManager<ApplicationUser>>().ToSelf();
            Bind<IUserStore<ApplicationUser>>().To<UserStore<ApplicationUser>>();
            //Kernel.Bind<ApplicationUserManager>().ToSelf();

            //Bind<ApplicationSignInManager>().ToMethod((context) =>
            //{
            //    var cbase = new HttpContextWrapper(HttpContext.Current);
            //    return cbase.GetOwinContext().Get<ApplicationSignInManager>();
            //});


        }
    }
}