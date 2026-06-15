using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xna.Framework;
using JairLib.FootballBoilerPlate;
using JairLib.Utility;

namespace UnitTestSuite
{
    [TestClass]
    public class FootballPlayerTests
    {
        private static Color ColorForRating(int numberId)
        {
            return new FootballPlayer { NumberId = numberId }.SetColor();
        }

        [TestMethod]
        public void DefaultConstructor_SetsWhiteColorAndTileSizedRectangle()
        {
            var player = new FootballPlayer();

            Assert.AreEqual(Color.White, player.color);
            Assert.AreEqual(0, player.rectangle.X);
            Assert.AreEqual(0, player.rectangle.Y);
            Assert.AreEqual(Globals.TileSize, player.rectangle.Width);
            Assert.AreEqual(Globals.TileSize, player.rectangle.Height);
        }

        [TestMethod]
        public void VectorConstructor_PositionsRectangleAt64x64()
        {
            var player = new FootballPlayer(new Vector2(120, 250));

            Assert.AreEqual(120, player.rectangle.X);
            Assert.AreEqual(250, player.rectangle.Y);
            Assert.AreEqual(64, player.rectangle.Width);
            Assert.AreEqual(64, player.rectangle.Height);
            Assert.AreEqual(Color.White, player.color);
        }

        [TestMethod]
        public void SetColor_NinetyNine_IsGold()
        {
            Assert.AreEqual(Color.Gold, ColorForRating(99));
        }

        [TestMethod]
        public void SetColor_AboveEightyEight_IsPurple()
        {
            Assert.AreEqual(Color.Purple, ColorForRating(90));
        }

        [TestMethod]
        public void SetColor_AboveEighty_IsLightBlue()
        {
            Assert.AreEqual(Color.LightBlue, ColorForRating(85));
        }

        [TestMethod]
        public void SetColor_AboveSeventy_IsLightGreen()
        {
            Assert.AreEqual(Color.LightGreen, ColorForRating(75));
        }

        [TestMethod]
        public void SetColor_AboveFifty_IsWhite()
        {
            Assert.AreEqual(Color.White, ColorForRating(60));
        }

        [TestMethod]
        public void SetColor_AboveThirty_IsOrange()
        {
            Assert.AreEqual(Color.Orange, ColorForRating(40));
        }

        [TestMethod]
        public void SetColor_LowRating_IsRed()
        {
            Assert.AreEqual(Color.Red, ColorForRating(10));
        }

        [TestMethod]
        public void SetColor_FiftyExactly_FallsThroughToOrange()
        {
            // 50 is not "> 50", so it falls through to the "> 30" branch.
            Assert.AreEqual(Color.Orange, ColorForRating(50));
        }
    }
}
