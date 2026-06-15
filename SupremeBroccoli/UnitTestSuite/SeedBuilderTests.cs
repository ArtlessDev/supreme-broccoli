using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using JairLib.TileGenerators;
using JairLib.Utility;

namespace UnitTestSuite
{
    [TestClass]
    public class SeedBuilderTests
    {
        [TestMethod]
        public void TheStringGetsThisLength_Zero_ReturnsEmpty()
        {
            Assert.AreEqual("", SeedBuilder.TheStringGetsThisLength(0));
        }

        [TestMethod]
        public void TheStringGetsThisLength_ProducesRequestedNumberOfZeros()
        {
            var result = SeedBuilder.TheStringGetsThisLength(5);

            Assert.AreEqual(5, result.Length);
            Assert.AreEqual("00000", result);
        }

        [TestMethod]
        public void TheSeedGetsSomeOnes_PreservesLength()
        {
            var input = new string('0', 25);

            var result = SeedBuilder.TheSeedGetsSomeOnes(input);

            Assert.AreEqual(input.Length, result.Length);
        }

        [TestMethod]
        public void TheSeedGetsSomeOnes_OnlyProducesDigitsBelowCountOfTiles()
        {
            var input = new string('0', 50);

            var result = SeedBuilder.TheSeedGetsSomeOnes(input);

            // Each slot is either a roll < CountOfTiles, or 0 when the roll was too high,
            // so every character must be a digit strictly below CountOfTiles.
            foreach (var c in result)
            {
                Assert.IsTrue(char.IsDigit(c), $"'{c}' is not a digit");
                Assert.IsTrue(c - '0' < Globals.CountOfTiles, $"'{c}' is not below CountOfTiles");
            }
        }

        [TestMethod]
        public void SplitTheSeedToAGrid_PerfectSquareSeed_ProducesBorderedGrid()
        {
            // Length 16 -> sqrt = 4, adjusted = 6 rows.
            var seed = "0123456789012345";

            var grid = SeedBuilder.SplitTheSeedToAGrid(seed);

            Assert.AreEqual(6, grid.Length);
            // First and last rows are a border of '1's (adjusted + 1 of them).
            Assert.AreEqual("1111111", grid[0]);
            Assert.AreEqual("1111111", grid[grid.Length - 1]);
            // Interior rows hold the seed split into chunks of floor(sqrt(length)) = 4.
            Assert.AreEqual("0123", grid[1]);
            Assert.AreEqual("4567", grid[2]);
            Assert.AreEqual("8901", grid[3]);
            Assert.AreEqual("2345", grid[4]);
        }
    }
}
