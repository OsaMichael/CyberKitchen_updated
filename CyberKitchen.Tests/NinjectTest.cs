using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using Cyber_Kitchen.Interface;
using System.Reflection;

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
            kernel.Load(Assembly.Load("Cyber_Kitchen"));
            kernel.Get<IRestaurantManager>();
        }
    }
}
