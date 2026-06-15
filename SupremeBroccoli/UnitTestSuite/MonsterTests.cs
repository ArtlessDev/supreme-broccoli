using Microsoft.VisualStudio.TestTools.UnitTesting;
using JairLib.CombatSimulator;

namespace UnitTestSuite
{
    [TestClass]
    public class MonsterTests
    {
        [TestMethod]
        public void DefaultConstructor_SetsEeveeStatBlock()
        {
            var monster = new Monster();

            Assert.AreEqual("Eevee", monster.Name);
            Assert.AreEqual(20, monster.Health);
            Assert.AreEqual(10, monster.Speed);
            Assert.AreEqual(15, monster.Attack);
            Assert.AreEqual("Normal", monster.TypeOne);
            Assert.AreEqual(12, monster.Defense);
            Assert.AreEqual(10, monster.SpecialAttack);
        }

        [TestMethod]
        public void DefaultConstructor_AssignsCutAsFirstMove()
        {
            var monster = new Monster();

            Assert.IsNotNull(monster.MoveOne);
            Assert.AreEqual("Cut", monster.MoveOne.Name);
        }

        [TestMethod]
        public void DefaultConstructor_RemainingMovesAreEmpty()
        {
            var monster = new Monster();

            Assert.AreEqual("", monster.MoveTwo.Name);
            Assert.AreEqual("", monster.MoveThree.Name);
            Assert.AreEqual("", monster.MoveFour.Name);
        }

        [TestMethod]
        public void Properties_AreMutable()
        {
            var monster = new Monster
            {
                Name = "Pikachu",
                Health = 35,
                Speed = 90,
                Attack = 55,
                TypeOne = "Electric",
                Defense = 40,
                SpecialAttack = 50,
            };

            Assert.AreEqual("Pikachu", monster.Name);
            Assert.AreEqual(35, monster.Health);
            Assert.AreEqual(90, monster.Speed);
            Assert.AreEqual(55, monster.Attack);
            Assert.AreEqual("Electric", monster.TypeOne);
            Assert.AreEqual(40, monster.Defense);
            Assert.AreEqual(50, monster.SpecialAttack);
        }

        [TestMethod]
        public void MoveSlots_AreMutable()
        {
            var monster = new Monster();
            var thundershock = IndividualMove.GetMove("Thundershock");

            monster.MoveTwo = thundershock;

            Assert.AreSame(thundershock, monster.MoveTwo);
            Assert.AreEqual("Thundershock", monster.MoveTwo.Name);
        }
    }
}
