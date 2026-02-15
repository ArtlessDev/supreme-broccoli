using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JairLib
{
    public static class Atlases
    {

        public static Texture2D 
            gameTilePrototypeSet, 
            beastiary_tileset, 
            beastiaryDex,
            WorldMapSet;
        public static Texture2DAtlas 
            gameTilePrototypeAtlas, 
            tilesetAtlas, 
            beastiaryDexAtlas,
            WorldMapAtlas;

        public static void Load()
        {

            //gameTilePrototypeSet = Globals.GlobalContent.Load<Texture2D>("game_tileset_prototype");
            //gameTilePrototypeAtlas = Texture2DAtlas.Create("gameTileMapPrototype", gameTilePrototypeSet, Globals.TileSize, Globals.TileSize);
            WorldMapSet = Globals.GlobalContent.Load<Texture2D>("tilemaps\\tileset_original");
            WorldMapAtlas = Texture2DAtlas.Create("worldMapAtlas", WorldMapSet, Globals.TileSize, Globals.TileSize);

            beastiaryDex = Globals.GlobalContent.Load<Texture2D>("beastiary_dex");
            beastiaryDexAtlas = Texture2DAtlas.Create("beastTileMapPrototype", beastiaryDex, Globals.TileSize, Globals.TileSize);

            beastiary_tileset = Globals.GlobalContent.Load<Texture2D>("beastiary_tileset");
            tilesetAtlas = Texture2DAtlas.Create("playerTileMapPrototype", beastiary_tileset, Globals.TileSize, Globals.TileSize);

        }
    }
}
