using Microsoft.VisualStudio.TestTools.UnitTesting;
using JairLib.CombatSimulator;

namespace UnitTestSuite
{
    [TestClass]
    public class BattleFunctionsTests
    {
        private static Monster MonsterWithSpeed(int speed)
        {
            return new Monster { Speed = speed };
        }

        [TestMethod]
        public void TurnDecider_FirstMonsterFaster_ReturnsZero()
        {
            var fast = MonsterWithSpeed(50);
            var slow = MonsterWithSpeed(10);

            Assert.AreEqual(0, BattleFunctions.TurnDecider(fast, slow));
        }

        [TestMethod]
        public void TurnDecider_SecondMonsterFaster_ReturnsOne()
        {
            var slow = MonsterWithSpeed(10);
            var fast = MonsterWithSpeed(50);

            Assert.AreEqual(1, BattleFunctions.TurnDecider(slow, fast));
        }

        [TestMethod]
        public void TurnDecider_EqualSpeed_FavorsSecondMonster()
        {
            // mon1.Speed > mon2.Speed is false when equal, so it falls to the else branch.
            Assert.AreEqual(1, BattleFunctions.TurnDecider(MonsterWithSpeed(20), MonsterWithSpeed(20)));
        }

        [TestMethod]
        public void TurnDeciderOpp_FirstMonsterSlower_ReturnsZero()
        {
            var slow = MonsterWithSpeed(10);
            var fast = MonsterWithSpeed(50);

            Assert.AreEqual(0, BattleFunctions.TurnDeciderOpp(slow, fast));
        }

        [TestMethod]
        public void TurnDeciderOpp_FirstMonsterFaster_ReturnsOne()
        {
            var fast = MonsterWithSpeed(50);
            var slow = MonsterWithSpeed(10);

            Assert.AreEqual(1, BattleFunctions.TurnDeciderOpp(fast, slow));
        }

        [TestMethod]
        public void TurnDeciderOpp_EqualSpeed_ReturnsOne()
        {
            // mon1.Speed < mon2.Speed is false when equal, so it falls to the else branch.
            Assert.AreEqual(1, BattleFunctions.TurnDeciderOpp(MonsterWithSpeed(20), MonsterWithSpeed(20)));
        }

        [TestMethod]
        public void FaintCheck_BothMonstersAlive_ReturnsTrue()
        {
            var a = new Monster { Health = 20 };
            var b = new Monster { Health = 15 };

            Assert.IsTrue(BattleFunctions.FaintCheck(a, b));
        }

        [TestMethod]
        public void FaintCheck_CurrentMonsterFainted_ReturnsFalse()
        {
            var fainted = new Monster { Health = 0 };
            var alive = new Monster { Health = 15 };

            Assert.IsFalse(BattleFunctions.FaintCheck(fainted, alive));
        }

        [TestMethod]
        public void FaintCheck_OpponentFainted_ReturnsFalse()
        {
            var alive = new Monster { Health = 15 };
            var fainted = new Monster { Health = 0 };

            Assert.IsFalse(BattleFunctions.FaintCheck(alive, fainted));
        }

        [TestMethod]
        public void FaintCheck_NegativeHealth_ReturnsFalse()
        {
            var alive = new Monster { Health = 15 };
            var dead = new Monster { Health = -5 };

            Assert.IsFalse(BattleFunctions.FaintCheck(alive, dead));
        }

        [TestMethod]
        public void AttackAction_GuaranteedHitNoStab_DealsAttackTimesPower()
        {
            // Accuracy 100 guarantees a hit (random roll is 0..99). Move type differs
            // from the monster's type, so no STAB multiplier is applied.
            var monster = new Monster
            {
                Attack = 100,
                TypeOne = "Normal",
                MoveOne = new IndividualMove("TestMove", 0.5, 100, "Water", "physical"),
            };

            int damage = BattleFunctions.AttackAction(monster, 1);

            Assert.AreEqual(50, damage); // 100 * 0.5
        }

        [TestMethod]
        public void AttackAction_GuaranteedHitWithStab_AppliesOnePointFiveMultiplier()
        {
            // Move type matches the monster's type, so STAB applies: damage * 1.5.
            var monster = new Monster
            {
                Attack = 100,
                TypeOne = "Normal",
                MoveOne = new IndividualMove("TestMove", 0.5, 100, "Normal", "physical"),
            };

            int damage = BattleFunctions.AttackAction(monster, 1);

            Assert.AreEqual(75, damage); // (100 * 0.5) * 1.5
        }

        [TestMethod]
        public void AttackAction_ZeroAccuracy_AlwaysMisses()
        {
            var monster = new Monster
            {
                Attack = 100,
                TypeOne = "Normal",
                MoveOne = new IndividualMove("TestMove", 0.5, 0, "Water", "physical"),
            };

            // Accuracy 0 can never be greater than a roll of 0..99, so it always misses.
            for (int i = 0; i < 100; i++)
            {
                Assert.AreEqual(0, BattleFunctions.AttackAction(monster, 1));
            }
        }

        [TestMethod]
        public void AttackAction_UsesMoveMatchingActionDecision()
        {
            var monster = new Monster
            {
                Attack = 100,
                TypeOne = "Normal",
                MoveOne = new IndividualMove("One", 0.1, 0, "Water", "physical"),   // would miss
                MoveTwo = new IndividualMove("Two", 0.5, 100, "Water", "physical"), // guaranteed hit
            };

            // Choosing action 2 should resolve against MoveTwo, not MoveOne.
            Assert.AreEqual(50, BattleFunctions.AttackAction(monster, 2));
        }
    }
}
