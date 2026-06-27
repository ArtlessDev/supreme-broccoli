//using Microsoft.Xna.Framework.Input;
//using MonoGame.Extended.Input;
//using System;
//using System.Diagnostics;
//using System.Linq;
//using static DualityGame.GameSpecificFiles.GameManager;

//namespace DualityGame.GameSpecificFiles
//{

//    public static class GameManager
//    {

//        public static void GamblingPhase(KeyboardStateExtended state)
//        {
//            Card tempCard = new();

//            if (Globals.PlayerHand.Count == 0 && state.WasKeyPressed(Keys.Space))
//            {
//                var index = Random.Shared.Next(0, Globals.PlayerDeck.Count);
//                tempCard = Globals.PlayerDeck.ToArray()[index];

//                Globals.HandTotal = Globals.HandTotal + tempCard.CardIntValue;
//                Globals.PlayerHand.Add(Globals.PlayerDeck.ToArray()[index]);

//                Globals.PlayerDeck.RemoveAt(index);
//            }

//            if (state.WasKeyPressed(Keys.Space))
//            {
//                var index = Random.Shared.Next(0, Globals.PlayerDeck.Count);
//                tempCard = Globals.PlayerDeck.ToArray()[index];

//                Globals.PlayerHand.Add(Globals.PlayerDeck.ToArray()[index]);

//                Globals.PlayerDeck.RemoveAt(index);

//                Globals.NewCardAdded = true;

//            }

//            if (state.WasKeyPressed(Keys.Enter) 
//                && Globals.PlayerHand.Count >= 2 
//                && Globals.HandTotal <= Globals.StandingBlackjack
//                && Globals.HandTotal >= Globals.CurrentDealerToBeat)
//            {
//                Globals.gameState = GameState.RoundWon;
//            }

//            if (Globals.NewCardAdded)
//            {
//                Globals.HandTotal = Globals.HandTotal + tempCard.CardIntValue;
//                Globals.NewCardAdded = false;

//                Globals.playerSpotWidth = Globals.blackjackSpotWidth * (Globals.HandTotal / (decimal)Globals.StandingBlackjack);
//                Globals.dealertobeatSpotWidth = Globals.blackjackSpotWidth * (Globals.CurrentDealerToBeat / (decimal)Globals.StandingBlackjack);
//                Globals.currentText = $"Current hand: {Globals.HandTotal.ToString()} | Hit? [Space] | Stand? [Enter]";
//            }

//            if (Globals.HandTotal == Globals.StandingBlackjack)
//            {
//                Globals.gameState = GameState.RoundWon;
//            }
//            if (Globals.HandTotal > Globals.StandingBlackjack)
//            {
//                Globals.gameState = GameState.Busted;
//            }
//        }


//        public static void HandBusted(KeyboardStateExtended state)
//        {

//            Globals.currentText = $"Busted: {Globals.HandTotal}. Press Space For New Hand";

//            Globals.CurrentRound = 1;
//            Globals.PlayerHand.Clear();
//            Globals.HandTotal = 0;
//            Globals.CurrentDealerToBeat = Globals.DealerToBeatSmall;
//            Globals.PlayerDeck.Clear();
//            Globals.LoadDeckFirstTime();
//            Globals.PlayerMoney = 0;
//            Globals.StandingBlackjack = 21;


//            if (state.WasKeyPressed(Keys.Space) || state.WasKeyPressed(Keys.Enter))
//                Globals.gameState = GameState.Gambling;
//        }

//        public static void RoundWon(KeyboardStateExtended state)
//        {
//            if (Globals.HandTotal == Globals.StandingBlackjack)
//                Globals.currentText = $"BLACKJACK! Round Complete. Press Space to Shop.";
//            else if (Globals.HandTotal <= Globals.StandingBlackjack
//                && Globals.HandTotal > Globals.CurrentDealerToBeat)
//                Globals.currentText = $"Round Won: Press Space to Shop.";
//            else
//                Globals.currentText = "Round Lost: Press Space to Shop.";

//            Globals.CurrentRound++;

//            Globals.PlayerMoney += Globals.PlayerHand.Count;

//            Globals.gameState = GameState.RoundEnded;
//        }

//        public static void RoundEnded(KeyboardStateExtended state)
//        {
//            if (Globals.HandTotal <= Globals.StandingBlackjack)
//            {
//                Globals.StandingBlackjack = (int)(Globals.BaseBlackjack + Globals.BaseBlackjack * (.1 * Globals.CurrentRound));
//                Globals.CurrentDealerToBeat = (int)(.1 * Globals.DealerToBeatSmall * Globals.CurrentRound) + Globals.DealerToBeatSmall;

//                Globals.PlayerHand.Clear();
//                Globals.HandTotal = 0;
//            }

//            if (Globals.CurrentRound % 3 == 0)
//            {
//                Globals.CurrentDealerToBeat = (int)(.1 * Globals.DealerToBeatLarge * Globals.CurrentRound) + Globals.DealerToBeatLarge;
//                Globals.StandingBlackjack = (int)(Globals.BaseBlackjack + .1 * Globals.CurrentRound * Globals.DealerToBeatLarge);

//            }

//            if (state.WasKeyPressed(Keys.Space))
//            {
//                Globals.gameState = GameState.Shopping;
//            }
//        }

//        public static void GetNewCards(KeyboardStateExtended state)
//        {
//            Globals.ShopCards.Add(new(50, 64));
//            Globals.ShopCards.Add(new(150, 64));
//            Globals.ShopCards.Add(new(250, 64));
//            Globals.ShopCards.Add(new(350, 64));
//            Globals.ShopCards.Add(new(450, 64));
//        }
//    }
//    public enum GameState
//    {
//        Gambling,
//        RoundWon,
//        RoundEnded,
//        Busted,
//        Shopping,
//    }
//}