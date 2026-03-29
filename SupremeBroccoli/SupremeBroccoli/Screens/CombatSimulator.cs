using JairLib.QuestCore;
using JairLib.TileGenerators;
using JairLib;
using Microsoft.Xna.Framework;
using MonoGame.Extended.Screens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupremeBroccoli.Screens
{
    public class CombatSimulator : GameScreen
    {
        private new Game1 Game => (Game1)base.Game;

        public CombatSimulator(Game game) : base(game)
        {
            UpdateWhenInactive = false;
            DrawWhenInactive = false;
        }

        public override void Update(GameTime gameTime)
        {
            throw new NotImplementedException();
        }

        public override void Draw(GameTime gameTime)
        {
            throw new NotImplementedException();
        }
    }
}
