using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Text.Json;
using System.Diagnostics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Graphics;

namespace JairLib
{
    public class Quest
    {
        public Quest() { }

        public string QuestIdentifier { get; set; }
        public KeyObjective StartingObjective { get; set; }
        public KeyObjective MiddleObjective { get; set; }
        public KeyObjective EndingObjective { get; set; }
        public List<KeyObjective> SideObjectives { get; set; }
        public bool QuestComplete {  get; set; }
    }

    public class QuestSystem
    {
        public Quest? CurrentQuest;
        public bool InitiatedFirstQuest = false;
        //public  string FirstQuest = "C:\\Code\\Jamsepticeye-submission\\JamSepticEyeGame\\JamSepticEyeGame\\Content\\JsonFiles\\FirstQuest.json";
        //NEED TO DOUBLE CHECK THIS IN THE RELEASE BUILD TO MAKE SURE IT WORKS
        public string QuestString = ".\\Content\\FirstQuest.json";
        public string FirstQuestMod = "FirstQuest.json";

        Texture2DAtlas questAtlas;

        public QuestSystem(string jsonString)
        {
            QuestString = jsonString;
            SetFirstQuestAsCurrent();
        }
        public QuestSystem(string jsonString, Texture2DAtlas _atlas)
        {
            QuestString = jsonString;
            SetFirstQuestAsCurrent();
            questAtlas = _atlas;
        }

        public void SetFirstQuestAsCurrent()
        {
            using Stream stream = TitleContainer.OpenStream(QuestString);
            string jsonString = File.ReadAllText(QuestString);
            CurrentQuest = JsonSerializer.Deserialize<Quest>(jsonString);
            CurrentQuest.QuestComplete = false;
        }

        public void DrawCurrentQuestObjective(SpriteBatch _spriteBatch, PlayerOverworld player)
        {
            KeyObjective[] objectives =
            {
                CurrentQuest.StartingObjective,
                CurrentQuest.MiddleObjective,
                CurrentQuest.EndingObjective,
            };

            if (CurrentQuest.SideObjectives != null)
                DrawSideObjectives(_spriteBatch, player);

            //WasQuestCompletedGoodOrBad(_spriteBatch);

            foreach (var objective in objectives)
            {

                if (!objective.IsCompletedFlag)
                {
                    objective.Draw(_spriteBatch);
                    objective.texture = questAtlas[objective.textureValue];
                    return;
                }

                if (objective.IsCompletedFlag)
                {
                    objective.Draw(_spriteBatch);
                }
            }

        }

        private void WasQuestCompletedGoodOrBad(SpriteBatch _spriteBatch)
        {
            if (CurrentQuest.QuestComplete)
            {
                if (CurrentQuest.SideObjectives[2].IsCompletedFlag)
                    _spriteBatch.DrawString(Globals.font, "GameOver", new(Globals.MainCamera.Position.X + (Globals.ViewportWidth / 2) - 32, Globals.MainCamera.Position.Y + (Globals.ViewportHeight / 2) - 32), Color.White);
                else
                    _spriteBatch.DrawString(Globals.font, "GameOver", new(Globals.MainCamera.Position.X + (Globals.ViewportWidth / 2) - 32, Globals.MainCamera.Position.Y + (Globals.ViewportHeight / 2) - 32), Color.Red);
            }
        }

        private void DrawSideObjectives(SpriteBatch _spriteBatch, PlayerOverworld player)
        {
            foreach (var obj in CurrentQuest.SideObjectives)
            {
                obj.DrawNoCheck(_spriteBatch, player);
                obj.texture = Atlases.gameTilePrototypeAtlas[obj.textureValue];
            }
        }

        public void Update(GameTime gameTime, PlayerOverworld player)
        {
            if (CurrentQuest.SideObjectives != null)
            {
                HandleSideQuest(player);
            }

            if (CurrentQuest.StartingObjective.IsCompletedFlag == true
                && CurrentQuest.MiddleObjective.IsCompletedFlag == true
                && CurrentQuest.EndingObjective.IsCompletedFlag == true)
            {
                CurrentQuest.QuestComplete = true;
            }

            IsSingleObjectiveComplete(player, CurrentQuest.StartingObjective);
            IsSingleObjectiveComplete(player, CurrentQuest.MiddleObjective);
            IsSingleObjectiveComplete(player, CurrentQuest.EndingObjective);
        }

        void HandleSideQuest(PlayerOverworld player)
        {
            foreach (var obj in CurrentQuest.SideObjectives)
            {
                //Debug.WriteLine(obj.texture);
                if (player.rectangle.Intersects(obj.rectangle)
                    && Globals.keyb.WasKeyPressed(Keys.E))
                {
                    Debug.WriteLine(obj.objectiveTitle);
                    obj.IsCompletedFlag = true;
                }
            }

            if (CurrentQuest.SideObjectives[2].IsCompletedFlag && Globals.keyb.WasKeyPressed(Keys.E)
                && CurrentQuest.SideObjectives[2].rectangle.Intersects(player.rectangle)
                && CurrentQuest.SideObjectives.Count < 3)
            {
                CurrentQuest.SideObjectives.RemoveAt(4);
                CurrentQuest.SideObjectives.RemoveAt(3);
                return;
            }
        }

        void IsSingleObjectiveComplete(PlayerOverworld player, KeyObjective objective)
        {
            if (objective.IsCompletedFlag == false)
            {
                if (player.rectangle.Intersects(objective.rectangle) && Globals.keyb.WasKeyPressed(Keys.E))
                {
                    CurrentQuest.StartingObjective.IsCompletedFlag = true;
                    InitiatedFirstQuest = true;
                    return;
                }
            }
        }
        
    }
}
