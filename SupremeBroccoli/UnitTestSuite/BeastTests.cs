using Microsoft.VisualStudio.TestTools.UnitTesting;
using JairLib.BeastiaryCore;

namespace UnitTestSuite
{
    [TestClass]
    public class BeastTests
    {
        [TestMethod]
        public void DefaultConstructor_CreatesInstance()
        {
            var beast = new Beast();

            Assert.IsNotNull(beast);
        }

        [TestMethod]
        public void Properties_AreMutable()
        {
            var beast = new Beast
            {
                BeastId = 7,
                BeastName = "Grumblefang",
                FirstRow = new[] { 1, 0, 1 },
                SecondRow = new[] { 0, 1, 0 },
                GridPlacement = new[,] { { 1, 0 }, { 0, 1 } },
            };

            Assert.AreEqual(7, beast.BeastId);
            Assert.AreEqual("Grumblefang", beast.BeastName);
            CollectionAssert.AreEqual(new[] { 1, 0, 1 }, beast.FirstRow);
            CollectionAssert.AreEqual(new[] { 0, 1, 0 }, beast.SecondRow);
            Assert.AreEqual(1, beast.GridPlacement[0, 0]);
            Assert.AreEqual(1, beast.GridPlacement[1, 1]);
        }
    }
}
