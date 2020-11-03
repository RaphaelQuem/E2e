using Microsoft.VisualStudio.TestTools.UnitTesting;
using Repository.Interfaces;
using Repository;
using Infra;

namespace Test
{
    [TestClass]
    public class Initializer
    {
        [AssemblyInitialize]
        public static void AssemblyInit(TestContext context)
        {
            var cfg = Configuration.Config.InitConfiguration();
            EnvironmentHelper.SetConfiguration(cfg);
        }
    }
}
