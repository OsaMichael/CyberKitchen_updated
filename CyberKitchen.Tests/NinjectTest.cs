using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using Cyber_Kitchen.Interface;
using System.Reflection;
using System.Linq;

namespace CyberKitchen.Tests
{
    [TestClass]
    public class NinjectTest
    {
        [TestMethod]
        public void TestNinjectBindings()
        {
            //Create Kernel
            var kernel = new StandardKernel();

            var assembly = Assembly.Load("Cyber_Kitchen");

            kernel.Load(assembly);
            kernel.Get<IRestaurantManager>();
            kernel.Get<IVoterManager>();

            // best way to do this
            var interfaces = assembly.GetTypes()
                                    .Where(t => t.FullName.StartsWith("Cyber_Kitchen.Interface"))
                                    .Where(t => t.IsInterface).ToList();

            foreach(var iInterface in interfaces)
            {
                kernel.Get(iInterface);
            }
        }
    }
}
