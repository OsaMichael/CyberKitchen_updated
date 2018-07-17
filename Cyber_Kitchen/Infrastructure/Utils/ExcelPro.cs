using Cyber_Kitchen.Interface.Utils;
using Ninject.Modules;
using Ninject.Web.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cyber_Kitchen.Infrastructure.Utils
{
    public class ExcelPro : NinjectModule
    {
        public override void Load()
        {
            Bind<IExcelProcessor>().To<ExcelProcessor>().InRequestScope();
        }
        }
    }