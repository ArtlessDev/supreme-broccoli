using Microsoft.VisualStudio.TestTools.UnitTesting;
using JairLib.CombatSimulator;

namespace UnitTestSuite
{
    [TestClass]
    public class IndividualMoveTests
    {
        [TestMethod]
        public void DefaultConstructor_SetsCutMoveDefaults()
        {
            var move = new IndividualMove();

            Assert.AreEqual("Cut", move.Name);
            Assert.AreEqual(0.50, move.Power);
            Assert.AreEqual(95, move.Accuracy);
            Assert.AreEqual("Normal", move.Type);
            Assert.AreEqual("physical", move.SpecPhys);
        }

        [TestMethod]
        public void ParameterizedConstructor_AssignsAllFields()
        {
            var move = new IndividualMove("Flamethrower", 0.9, 80, "Fire", "special");

            Assert.AreEqual("Flamethrower", move.Name);
            Assert.AreEqual(0.9, move.Power);
            Assert.AreEqual(80, move.Accuracy);
            Assert.AreEqual("Fire", move.Type);
            Assert.AreEqual("special", move.SpecPhys);
        }

        [TestMethod]
        public void Properties_AreMutable()
        {
            var move = new IndividualMove();

            move.Name = "Tackle";
            move.Power = 0.35;
            move.Accuracy = 100;
            move.Type = "Normal";
            move.SpecPhys = "physical";

            Assert.AreEqual("Tackle", move.Name);
            Assert.AreEqual(0.35, move.Power);
            Assert.AreEqual(100, move.Accuracy);
            Assert.AreEqual("Normal", move.Type);
            Assert.AreEqual("physical", move.SpecPhys);
        }

        [TestMethod]
        public void GetMove_Thundershock_ReturnsElectricSpecial()
        {
            var move = IndividualMove.GetMove("Thundershock");

            Assert.AreEqual("Thundershock", move.Name);
            Assert.AreEqual(0.40, move.Power);
            Assert.AreEqual(100, move.Accuracy);
            Assert.AreEqual("Electric", move.Type);
            Assert.AreEqual("special", move.SpecPhys);
        }

        [TestMethod]
        public void GetMove_Cut_ReturnsNormalPhysical()
        {
            var move = IndividualMove.GetMove("Cut");

            Assert.AreEqual("Cut", move.Name);
            Assert.AreEqual(0.50, move.Power);
            Assert.AreEqual(85, move.Accuracy);
            Assert.AreEqual("Normal", move.Type);
            Assert.AreEqual("physical", move.SpecPhys);
        }

        [TestMethod]
        public void GetMove_Ember_ReturnsFireSpecial()
        {
            var move = IndividualMove.GetMove("Ember");

            Assert.AreEqual("Ember", move.Name);
            Assert.AreEqual(0.40, move.Power);
            Assert.AreEqual(100, move.Accuracy);
            Assert.AreEqual("Fire", move.Type);
            Assert.AreEqual("special", move.SpecPhys);
        }

        [TestMethod]
        public void GetMove_Bubble_ReturnsWaterSpecial()
        {
            var move = IndividualMove.GetMove("Bubble");

            Assert.AreEqual("Bubble", move.Name);
            Assert.AreEqual(0.20, move.Power);
            Assert.AreEqual(100, move.Accuracy);
            Assert.AreEqual("Water", move.Type);
            Assert.AreEqual("special", move.SpecPhys);
        }

        [TestMethod]
        public void GetMove_RazorLeaf_ReturnsGrassPhysical()
        {
            var move = IndividualMove.GetMove("Razor Leaf");

            Assert.AreEqual("Razor Leaf", move.Name);
            Assert.AreEqual(0.55, move.Power);
            Assert.AreEqual(95, move.Accuracy);
            Assert.AreEqual("Grass", move.Type);
            Assert.AreEqual("physical", move.SpecPhys);
        }

        [TestMethod]
        public void GetMove_UnknownName_ReturnsEmptyMove()
        {
            var move = IndividualMove.GetMove("NotARealMove");

            Assert.AreEqual("", move.Name);
            Assert.AreEqual(0, move.Power);
            Assert.AreEqual(0, move.Accuracy);
            Assert.AreEqual("", move.Type);
            Assert.AreEqual("", move.SpecPhys);
        }

        [TestMethod]
        public void GetMove_EmptyString_ReturnsEmptyMove()
        {
            var move = IndividualMove.GetMove("");

            Assert.AreEqual("", move.Name);
            Assert.AreEqual(0, move.Power);
            Assert.AreEqual(0, move.Accuracy);
        }
    }
}
