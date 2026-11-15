using Gum.Converters;
using Gum.DataTypes;
using Gum.Managers;
using Gum.Wireframe;
using JairLib.QuestCore;
using Microsoft.Xna.Framework;
using RenderingLibrary.Graphics;

using System.Linq;

namespace SupremeBroccoli.Screens
{
    partial class DialogueScreen
    {
        partial void CustomInitialize()
        {
            this.DialogueBox.LettersPerSecond = 0;
            this.DialogueBox.TextInstance.Color = Color.White;
            this.DialogueBox.TextInstance.FontSize = 16;
            this.IsEnabled = false;

        }
    }
}
