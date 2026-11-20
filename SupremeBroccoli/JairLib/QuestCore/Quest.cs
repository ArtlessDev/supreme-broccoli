using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Text.Json;
using System.Diagnostics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Graphics;
using JairLib.Utility;

namespace JairLib.QuestCore
{
    public enum QuestList
    {
        None,
        Town_1_Quest,
        Town_1_Quest_2,
        A, B, C, D, E, F, G, H, I,
    }
    public class Quest
    {
        public Quest() { }

        public Quest(string _jsonString)
        {
            QuestFileString = _jsonString;

        }

        public Quest(string _jsonString, Texture2DAtlas _atlas)
        {
            QuestFileString = _jsonString;
            questAtlas = _atlas;

        }
        public string QuestFileString = ".\\Content\\FirstQuest.json";
        public string QuestIdentifier { get; set; }
        public string QuestDetails { get; set; }
        public KeyObjective StartingObjective { get; set; }
        public KeyObjective MiddleObjective { get; set; }
        public KeyObjective EndingObjective { get; set; }
        public List<KeyObjective> SideObjectives { get; set; }
        //shoutout dean for the dictionary idea
        public Dictionary<QuestList, KeyObjective> KvpQuests { get; set; }
        public bool QuestComplete {  get; set; }
        public Texture2DAtlas questAtlas;

        public void Draw(SpriteBatch _spriteBatch, PlayerOverworld player)
        {
            if (KvpQuests == null)
                return;

            foreach (KeyObjective obj in KvpQuests.Values)
            {
                obj.Draw(_spriteBatch);
            }
        }
        
        public void Load()
        {
            if (KvpQuests != null)
            {
                for (int i = 0; i < KvpQuests.Count; i++)
                {
                    var _reference = KvpQuests.ToArray();
                    KvpQuests[_reference[i].Key].Animations = _reference[i].Value.LoadAnimations();

                }
            }
        }

    }

    /// <summary>
    /// these will be used as 'stand-ins' 
    /// a stand-in is an inactive quest. the current quest will be attached 
    /// to the player and easier to save data that way. so whenever the player 
    /// wants a different quest, this will be used to load the quest into the current quest.
    /// </summary>
    public class QuestSystem
    {
        #region constructor and variables
        public Quest? CurrentQuest;
        public bool InitiatedFirstQuest = false;
        public string QuestString = ".\\Content\\FirstQuest.json";
        public string FirstQuestMod = "FirstQuest.json";
        public KeyObjective[] objectives;
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
            objectives =
            [
                CurrentQuest.StartingObjective,
                CurrentQuest.MiddleObjective,
                CurrentQuest.EndingObjective,
            ];
        }
        #endregion constructor and variables

        public void SetFirstQuestAsCurrent()
        {
            using Stream stream = TitleContainer.OpenStream(QuestString);
            string jsonString = File.ReadAllText(QuestString);
            CurrentQuest = JsonSerializer.Deserialize<Quest>(jsonString);
            CurrentQuest.QuestComplete = false;
        }

        public void DrawCurrentQuestObjective(SpriteBatch _spriteBatch, PlayerOverworld player)
        {

            if (RpgPlayer.CurrentQuest.QuestIdentifier == this.CurrentQuest.QuestIdentifier)
                return;
            //    DrawSideObjectives(_spriteBatch, player);

            foreach (KeyObjective obj in CurrentQuest.KvpQuests.Values)
            {
                //need to only show the first quest objective that is not completed, and then show the next one after that is completed
                //obj.texture = Atlases.beastiaryDexAtlas[obj.textureValue];
                //DrawKvpObjectives(_spriteBatch, player);
                obj.Draw(_spriteBatch);
            }



            //WasQuestCompletedGoodOrBad(_spriteBatch);

            //foreach (var objective in objectives)
            //{
            //    if (objective == null)
            //        return;

            //    if (!objective.IsCompletedFlag)
            //    {
            //        //objective.Draw(_spriteBatch);
            //        objective.texture = questAtlas[objective.textureValue];
            //        //return;
            //    }

            //    if (objective.IsCompletedFlag)
            //    {
            //        objective.Draw(_spriteBatch);
            //    }
            //}

        }

        private void WasQuestCompletedGoodOrBad(SpriteBatch _spriteBatch)
        {
            if (CurrentQuest.QuestComplete)
            {
                if (CurrentQuest.SideObjectives[2].IsCompletedFlag)
                    _spriteBatch.DrawString(Globals.font, "GameOver", new(Globals.MainCamera.Position.X + Globals.ViewportWidth / 2 - 32, Globals.MainCamera.Position.Y + Globals.ViewportHeight / 2 - 32), Color.White);
                else
                    _spriteBatch.DrawString(Globals.font, "GameOver", new(Globals.MainCamera.Position.X + Globals.ViewportWidth / 2 - 32, Globals.MainCamera.Position.Y + Globals.ViewportHeight / 2 - 32), Color.Red);
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
        private void DrawKvpObjectives(SpriteBatch _spriteBatch, PlayerOverworld player)
        {
            foreach (var obj in CurrentQuest.KvpQuests.Values)
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

            if(CurrentQuest.KvpQuests != null)
            {
                foreach (KeyObjective obj in CurrentQuest.KvpQuests.Values)
                {
                    HandleQuest(obj, player);

                }
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

        void HandleQuest(KeyObjective obj, PlayerOverworld player)
        {
            if (player.rectangle.Intersects(obj.rectangle)
                    && Globals.keyb.WasKeyPressed(Keys.E))
            {
                //Debug.WriteLine(obj.objectiveTitle);
                obj.IsCompletedFlag = true;

                
            }

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
                    objective.IsCompletedFlag = true;
                    InitiatedFirstQuest = true;
                    //return;
                }
            }
        }
        
    }
}
