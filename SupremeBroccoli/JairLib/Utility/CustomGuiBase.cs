using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using System; 
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JairLib.Utility
{
    public class CustomGuiGroup
    {
        public CustomGuiBase baseGui = new();
        public SelectionGui selectionGui = new();

        public CustomGuiGroup()
        {

        }

        public void update(GameTime gt)
        {
            if (baseGui.isGuiEnabled && selectionGui.isGuiEnabled)
            {
                selectionGui.update(gt);
            }

            baseGui.update(gt);
        }

        public void draw(SpriteBatch sb)
        {
            baseGui.draw(sb);

            if (baseGui.isGuiEnabled && baseGui.DemandsPlayerResponse && selectionGui.isGuiEnabled)//Globals.keyb.WasKeyPressed(Keys.E))
            {
                selectionGui.draw(sb);
            }
        }

        public void load()
        {
            baseGui.load();
        }
    }

    public class CustomGuiBase
    {
        public CustomGuiBase()
        {
            int tempx = (int)RpgPlayer.PlayerOverworld.rectangle.X - 566;
            int tempy = (int)RpgPlayer.PlayerOverworld.rectangle.Y + 128;
            int tempW = (int)(Globals.WindowWidth * .85f);
            int tempH = (int)(Globals.WindowHeight * .3f);

        }
        public CustomGuiBase(bool isGuiEnabled, string currentText)
        {
            this.isGuiEnabled = isGuiEnabled;
            this.currentText = currentText;
        }

        public Rectangle PrimaryContainer = new((int)RpgPlayer.PlayerOverworld.rectangle.X, (int)RpgPlayer.PlayerOverworld.rectangle.Y, (int)(Globals.TileSize * 1.5f), Globals.TileSize * 2);
        public bool isGuiEnabled = false;
        public string currentText;
        public bool DemandsPlayerResponse = false;

        public void load()
        {

        }
        public virtual void update(GameTime gt)
        {

            int tempx = (int)RpgPlayer.PlayerOverworld.rectangle.X - 566;
            int tempy = (int)RpgPlayer.PlayerOverworld.rectangle.Y + 128;
            int tempW = (int)(Globals.WindowWidth * .85f);
            int tempH = (int)(Globals.WindowHeight * .3f);

            PrimaryContainer = new(tempx, tempy, tempW, tempH);

            //if (isGuiEnabled)
                
        }
        public virtual void draw(SpriteBatch sb)
        {
            if (!isGuiEnabled)
                return;

            sb.FillRectangle(PrimaryContainer.X, PrimaryContainer.Y, PrimaryContainer.Width, PrimaryContainer.Height, Color.Black);
            sb.DrawRectangle(PrimaryContainer.X, PrimaryContainer.Y, PrimaryContainer.Width, PrimaryContainer.Height, Color.White);
            sb.DrawString(Globals.font, currentText, new(PrimaryContainer.X + Globals.fontSize, PrimaryContainer.Y), Color.White);
        }
    }

    public class SelectionGui : CustomGuiBase
    {
        public bool tempSelectionFlag = true;
        public SelectionGui()
        {
            int tempx = (int)RpgPlayer.PlayerOverworld.rectangle.X - 566;
            int tempy = (int)RpgPlayer.PlayerOverworld.rectangle.Y + 128;
            int tempW = (int)(Globals.WindowWidth * .2f);
            int tempH = (int)(Globals.WindowHeight * .1f);

            currentText = "null";

        }
        public override void update(GameTime gt)
        {
            if (!isGuiEnabled)
                return;

            int tempx = (int)RpgPlayer.PlayerOverworld.rectangle.X - 566;
            int tempy = (int)RpgPlayer.PlayerOverworld.rectangle.Y;
            int tempW = (int)(Globals.WindowWidth * .2f);
            int tempH = (int)(Globals.WindowHeight * .1f);

            PrimaryContainer = new(tempx, tempy, tempW, tempH);
            currentText = tempSelectionFlag.ToString();
        }
        
        public override void draw(SpriteBatch sb)
        {
            if (!isGuiEnabled)
                return;

            sb.FillRectangle(PrimaryContainer.X, PrimaryContainer.Y, PrimaryContainer.Width, PrimaryContainer.Height, Color.Green);
            sb.DrawRectangle(PrimaryContainer.X, PrimaryContainer.Y, PrimaryContainer.Width, PrimaryContainer.Height, Color.White);
            sb.DrawString(Globals.font, currentText, new(PrimaryContainer.X + Globals.fontSize, PrimaryContainer.Y), Color.White);
        }

        internal CustomGuiGroup ChooseDialogueOption(CustomGuiGroup gui)
        {
            if (Globals.keyb.WasKeyPressed(Keys.S) || Globals.keyb.WasKeyPressed(Keys.Down))
                gui.selectionGui.tempSelectionFlag = !gui.selectionGui.tempSelectionFlag;
            if (Globals.keyb.WasKeyPressed(Keys.W) || Globals.keyb.WasKeyPressed(Keys.Up))
                gui.selectionGui.tempSelectionFlag = !gui.selectionGui.tempSelectionFlag;

            currentText = tempSelectionFlag.ToString();
            gui.selectionGui.currentText = currentText;


            if (Globals.keyb.WasKeyPressed(Keys.E)
                && !Globals.LockEKey)
            {
                RpgPlayer.PlayerOverworld.interactWithBox();
                gui.baseGui.isGuiEnabled = false;
                gui.selectionGui.isGuiEnabled = false;
            }



            //foreach (CombatButton button in buttons)
            //{
            //    if (button == null)
            //        return;

            //    if (indexer == Array.IndexOf(buttons, button))
            //        button.color = Color.Red;
            //    else
            //        button.color = Color.White;
            //}

            return gui;
        }
    }
}
