using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;
using System.Text.Json;

namespace JairLib
{
    public class Beastiary
    {
        public List<Beast> BeastList { get; set; }
    }
    public class Beast : AnyObject
    {
        public Beast() {
            //GridPlacement = new int[2, 2];

        }
        public int BeastId { get; set; }
        public int[,] GridPlacement { get; set; }
        public int[] FirstRow { get; set; }
        public int[] SecondRow { get; set; }
        public string BeastName { get; set; }

        public Beast(int value)
        {
            //gridPlacement = new int[2, 2];

            //BeastSystem.BeastDeserializer();

            //gridPlacement[0, 0] = 0;
            //gridPlacement[1, 0] = 1;
            //gridPlacement[1, 1] = 1;
            //gridPlacement[0, 1] = 0;

        }

        public void Update(GameTime gameTime)
        {

        }

        public void populateGrid()
        {
            foreach (int i in FirstRow)
            {
                Debug.WriteLine(i);
            }
        }

    }

    public static class BeastSystem
    {
        public static string jsonString = @".\Content\beasts.json";
        public static string defaultstr = @"C:\Code\supreme-broccoli\SupremeBroccoli\SupremeBroccoli\Content\beasts.json";

        public static Beast BeastDeserializer(int id)
        {
            using Stream stream = TitleContainer.OpenStream(jsonString);
            string jsonStr = File.ReadAllText(jsonString);
            Beastiary deserializer = JsonSerializer.Deserialize<Beastiary>(jsonStr);

            Beast tempBeast = deserializer.BeastList[id];

            return tempBeast;
        }

        public static void Draw(SpriteBatch sb)
        {

        }
    }
}