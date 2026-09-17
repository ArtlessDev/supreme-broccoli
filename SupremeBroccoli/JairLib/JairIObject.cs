using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;

public interface IJairObject 
{
    public string identifier {get; set;}
    public Rectangle rectangle{get; set;}
    public Texture2D texture{get;}
    public Color color{get; set;}
}

//not used for this game
public interface ICustomButton
{
    public void ButtonClicked()
    {
        Debug.WriteLine("1");
    }
}

public interface ITileObject
{
    public string identifier { get; set; }
    public Rectangle rectangle { get; set; }
    public Color color { get; set; }
    public Vector3 absolutePosition { get; set; }
}

public enum PlayerState
{
    Walking,
    Waiting,
    Jumping,
    Freefall,
    Dead,
    InCommunication
}

public enum NpcStates
{
    None,
    Idle,
    Talking,
}

public enum Direction
{
    Left,
    Right,
    Up,
    Down,
}

public enum TileSpaceType
{
    Walkable,
    Wall,
    Pit,
}

public enum TileBeast
{
    nameless
}

public enum PlayerSide
{
    Offense,
    Defense,
}

public enum FootballStates
{
    None,
    GeneratePlayer,
    DraftPlayer,
    PickPlay,
    PlaceReceivers,
    RunPlay,
    HandlePass,
}

public enum DominantHand
{
    Left,
    Right,
}

public enum Element
{
    None,
    Ice,
    Fire,
    Lightning,
    Physical,
    RangedPhysical,
}

public enum KindOfAttack
{
    Physical,
    Magic,
    Status,
    Heal,
}

public enum MoveList
{
    None,
    Punch,
    Fuego_I,
    Hielo_I,
    Viento_I,
    Mag_Up,
    Phys_Up,
    Def_Up,
    Mag_Down,
    Phys_Down,
    Def_Down,
    Speed_Up,
    Speed_Down,
    Fuego_II,
    Hielo_II,
    Viento_II,
    Fuego_III,
    Hielo_III,
    Viento_III,
    SUMMON_GUNDAM_M,
    SUMMON_GUNDAM_P,
    Reflejo_P,
    Reflejo_M,
    Draco_Slash,
    Draco_Breath,
}

public enum CombatStates
{
    none,
    VerifyActors,
    SortTurnOrder,
    SelectMove,
    CombatMinigame,
    ResolveActions,
    CheckActorsHP,
    GameOverLost,
    GameOverWon,
    ReturnToScreen,
    SelectOpponent,
    ResolveSecondaryEffects
}