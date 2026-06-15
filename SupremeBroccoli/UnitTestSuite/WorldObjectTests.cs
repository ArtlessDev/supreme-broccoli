using Microsoft.VisualStudio.TestTools.UnitTesting;
using JairLib.QuestCore;

namespace UnitTestSuite
{
    [TestClass]
    public class WorldObjectTests
    {
        [TestMethod]
        public void CurrentMessage_IsMutable()
        {
            var worldObject = new WorldObject
            {
                CurrentMessage = "Welcome to town.",
            };

            Assert.AreEqual("Welcome to town.", worldObject.CurrentMessage);
        }

        [TestMethod]
        public void InheritedIdentifier_IsMutable()
        {
            // WorldObject derives from AnyObject, exposing the shared identifier slot.
            var worldObject = new WorldObject
            {
                identifier = "signpost",
            };

            Assert.AreEqual("signpost", worldObject.identifier);
        }
    }
}
