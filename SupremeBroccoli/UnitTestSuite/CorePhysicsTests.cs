using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xna.Framework;
using JairLib.CustomPhysics;
using JairLib.Utility;

namespace UnitTestSuite
{
    [TestClass]
    public class CorePhysicsTests
    {
        [TestMethod]
        public void GravitationalForce_HasExpectedConstant()
        {
            Assert.AreEqual(9.68f, CorePhysics.GravitationalForce);
        }

        [TestMethod]
        public void CalculateJump_CurrentlyReturnsZeroVector()
        {
            // The jump calculation is still a stub that returns Vector2.Zero regardless
            // of the object passed in. This test documents that current behavior.
            var result = CorePhysics.CalculateJump(new AnyObject());

            Assert.AreEqual(Vector2.Zero, result);
        }
    }
}
