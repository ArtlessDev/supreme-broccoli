using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JairLib.Utility
{
    public class CustomGUI
    {
        public CustomGUI()
        {

        }
        public CustomGUI(bool isGuiEnabled, string currentText)
        {
            this.isGuiEnabled = isGuiEnabled;
            this.currentText = currentText;
        }

        public Rectangle PrimaryContainer = new((int)RpgPlayer.PlayerOverworld.rectangle.X, (int)RpgPlayer.PlayerOverworld.rectangle.Y, (int)(Globals.TileSize * 1.5f), Globals.TileSize * 2);
        public bool isGuiEnabled = false;
        public string currentText;

        public void load()
        {

        }
        public void update(GameTime gt)
        {
            int tempx = (int)RpgPlayer.PlayerOverworld.rectangle.X - 566;
            int tempy = (int)RpgPlayer.PlayerOverworld.rectangle.Y + 128;
            int tempW = (int)(Globals.WindowWidth * .85f);
            int tempH = (int)(Globals.WindowHeight * .3f);

            PrimaryContainer = new(tempx, tempy, tempW, tempH);
        }
        public void draw(SpriteBatch sb)
        {
            if (!isGuiEnabled)
                return;

            sb.FillRectangle(PrimaryContainer.X, PrimaryContainer.Y, PrimaryContainer.Width, PrimaryContainer.Height, Color.Black);
            //sb.FillRectangle()
            sb.DrawString(Globals.font, currentText, new(PrimaryContainer.X + Globals.fontSize, PrimaryContainer.Y), Color.White);
        }
    }
}
