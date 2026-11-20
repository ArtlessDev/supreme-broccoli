using JairLib.CombatSimulator;
using JairLib.InputHandler;
using JairLib.QuestCore;
using JairLib.TileGenerators;
using JairLib.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Graphics;
using System.Diagnostics;
using System.Timers;

namespace JairLib;
public class BasePlayer : AnyObject, QuestCore.IStats
{
    #region variables and stats
    public static Rectangle frameStartRectangle;
    public static int PLAYER_TILESIZE_IN_WORLD = 128;
    public SpriteEffects flipper { get; set; }
    public Direction playerDirection { get; set; }
    public PlayerState state { get; set; }
    public Rectangle interactionBox;
    public int HealthPoints { get; set; }
    public int BaseAttack { get; set; }
    public int BaseDefense { get; set; }
    public int BaseSpeed { get; set; }
    public int BaseMagic { get; set; }
    public Element BaseElement { get; set; }
    public int AttackPips { get; set; }
    public int DefensePips { get; set; }
    public int SpeedPips { get; set; }
    public int MagicPips { get; set; }
    private SpriteSheet _spriteSheet;
    public AnimatedSprite _walkDown, _walkSide, _walkUp, CURRENT_SPRITE;
    #endregion

    public void LoadAnimations()
    {

        _spriteSheet = new SpriteSheet("SpriteSheet/adventurer", Atlases.playerAtlas);

        _spriteSheet.DefineAnimation("walk-down", builder =>
        {
            builder.IsLooping(true)
                   .AddFrame("playerAtlas_0", TimeSpan.FromSeconds(0.2))
                   .AddFrame("playerAtlas_4", TimeSpan.FromSeconds(0.2))
                   .AddFrame("playerAtlas_8", TimeSpan.FromSeconds(0.2))
                   .AddFrame("playerAtlas_12", TimeSpan.FromSeconds(0.2))
                   .AddFrame("playerAtlas_16", TimeSpan.FromSeconds(0.2));
        });
        _spriteSheet.DefineAnimation("walk-side", builder =>
        {
            builder.IsLooping(true)
                   .AddFrame("playerAtlas_1", TimeSpan.FromSeconds(0.2))
                   .AddFrame("playerAtlas_5", TimeSpan.FromSeconds(0.2))
                   .AddFrame("playerAtlas_9", TimeSpan.FromSeconds(0.2))
                   .AddFrame("playerAtlas_13", TimeSpan.FromSeconds(0.2))
                   .AddFrame("playerAtlas_17", TimeSpan.FromSeconds(0.2));
        });
        _spriteSheet.DefineAnimation("walk-up", builder =>
        {
            builder.IsLooping(true)
                   .AddFrame("playerAtlas_3", TimeSpan.FromSeconds(0.2))
                   .AddFrame("playerAtlas_7", TimeSpan.FromSeconds(0.2))
                   .AddFrame("playerAtlas_11", TimeSpan.FromSeconds(0.2))
                   .AddFrame("playerAtlas_15", TimeSpan.FromSeconds(0.2))
                   .AddFrame("playerAtlas_19", TimeSpan.FromSeconds(0.2));
        });

        _walkDown = new AnimatedSprite(_spriteSheet, "walk-down");
        _walkSide = new AnimatedSprite(_spriteSheet, "walk-side");
        _walkUp = new AnimatedSprite(_spriteSheet, "walk-up");
        CURRENT_SPRITE = _walkDown;
    }
    //proving an alt color will allow for shadows. needs tweaking for polish
    public void Draw(SpriteBatch spriteBatch, Color? altColor = null)
    {
        var origin = new Vector2(0, 0);
        var position = new Vector2(rectangle.X, rectangle.Y);
        var scale = new Vector2(1f, 1f);
        var rotation = altColor != null ? .5f : 0f;
        color = altColor ?? Color.White;

        //var shadowRectangle = new Rectangle(rectangle.X+16, rectangle.Y+24, rectangle.Width, rectangle.Height);
        //spriteBatch.Draw(Globals.shadow, shadowRectangle, Color.White);

        if (state == PlayerState.Walking)
        {
            
            spriteBatch.Draw(CURRENT_SPRITE, position, 0, scale);
        }
        else
        {
            spriteBatch.Draw(texture, position, color, rotation, origin, scale, flipper, CURRENT_SPRITE.Depth);
        }

        //the size of the sprite will always match the size of the tile splitter 
        
    }
    public void DrawShader(SpriteBatch spriteBatch, Texture2D shaderTexture)
    {
        var origin = new Vector2(0, 0);
        var position = new Vector2(rectangle.X-128, rectangle.Y-256);
        var scale = new Vector2(1f, 1f);
        var rotation = 0f;

        //the size of the sprite will always match the size of the tile splitter 
        spriteBatch.Draw(shaderTexture, position, null, Color.White, rotation, origin, scale, SpriteEffects.None, 0f);

    }
}

#region platformer controller
public class PlayerPlatformer : BasePlayer
{
    public static System.Timers.Timer timer = new System.Timers.Timer(200f);
    public float playerTopSpeed { get; set; }
    public float playerCurrentSpeed { get; set; }

    public PlayerPlatformer()
    {
        identifier = "player";

        texture = Atlases.tilesetAtlas[0]; //blue
        LoadAnimations();
        rectangle = new Rectangle((int)Globals.STARTING_POSITION.X, (int)Globals.STARTING_POSITION.Y, RpgPlayer.PLAYER_TILESIZE_IN_WORLD, RpgPlayer.PLAYER_TILESIZE_IN_WORLD);
        frameStartRectangle = rectangle;
        color = Color.White;
        flipper = SpriteEffects.None;
        //state = PlayerState.Waiting;
        playerCurrentSpeed = 0f;
        playerTopSpeed = 10f;
    }

    public void Update(GameTime gameTime)
    {
        //HandleGravity();
        //HandleJump();

        CheckDirection();
        HorizontalMovement();
        HandleStop();
    }

    public void HandleStop()
    {
        if (!BasicInputs.HandleKeyDown(Keys.Left, Keys.A)
            && !BasicInputs.HandleKeyDown(Keys.Right, Keys.D))
        {
            playerCurrentSpeed -= .25f;

            if (playerCurrentSpeed < 0)
                playerCurrentSpeed = 0f;

            return;
        }
    }

    public void IncreaseSpeed()
    {
        if (playerTopSpeed != playerCurrentSpeed
            && (BasicInputs.HandleKeyDown(Keys.Left, Keys.A)
            || BasicInputs.HandleKeyDown(Keys.Right, Keys.D))
            )
        {
            playerCurrentSpeed = .25f + playerCurrentSpeed;

            if (playerCurrentSpeed > playerTopSpeed)
                playerCurrentSpeed = playerTopSpeed;

        }
    }

    public void HorizontalMovement()
    {
        var precheck = rectangle;

        IncreaseSpeed();

        if (BasicInputs.HandleKeyDown(Keys.Left, Keys.A))
        {
            //flipper = SpriteEffects.None;
            texture = Atlases.tilesetAtlas[0]; //blue
            playerDirection = Direction.Left;
            var adjustedX = (int)(rectangle.X - playerCurrentSpeed);
            
            rectangle = new Rectangle(adjustedX, rectangle.Y, PLAYER_TILESIZE_IN_WORLD, PLAYER_TILESIZE_IN_WORLD);
        }
        if (BasicInputs.HandleKeyDown(Keys.Right, Keys.D))
        {
            //flipper = SpriteEffects.FlipHorizontally;
            playerDirection = Direction.Right;
            texture = Atlases.tilesetAtlas[0]; //blue
            var adjustedX = (int)(rectangle.X + playerCurrentSpeed);

            rectangle = new Rectangle(adjustedX, rectangle.Y, PLAYER_TILESIZE_IN_WORLD, PLAYER_TILESIZE_IN_WORLD);
        }
    }

    public void HandleGravity()
    {
        var posY = rectangle.Y + 10;

        rectangle = new(rectangle.X, posY, rectangle.Width, rectangle.Height);
    }

    public void HandleJump()
    {
        float absoluteHeight;

        if (Globals.keyb.WasKeyPressed(Keys.Space))
        {
            timer.Elapsed += DisableTimer;
            timer.AutoReset = true;
            timer.Enabled = true;
            absoluteHeight = absolutePosition.Y;

        }

        if(timer.Enabled)
        {
            var posY = rectangle.Y - 30; //should change this from 30 to a proper calculation between mass and force

            rectangle = new(rectangle.X, posY, rectangle.Width, rectangle.Height);
        }

        
    }

    private void DisableTimer(object? sender, ElapsedEventArgs e)
    {
        timer.Enabled = false;
    }

    public void CheckDirection()
    {
        if (playerDirection == Direction.Right)
        {
            flipper = SpriteEffects.None;
        }
        else
        {
            flipper = SpriteEffects.FlipHorizontally;
        }
    }

}
#endregion

public class PlayerOverworld : BasePlayer
{
    public int playerSpeed { get; set; }
    public Rectangle Collider2d { get; set; }
    public PlayerOverworld()
    {
        identifier = "player";
        texture = Atlases.playerAtlas[0]; //blue
        LoadAnimations();
        int startposx = 0;
        int startposy = 0;
        rectangle = new Rectangle(startposx, startposy, RpgPlayer.PLAYER_TILESIZE_IN_WORLD, RpgPlayer.PLAYER_TILESIZE_IN_WORLD);
        Position = new(startposx, startposy);

        int halfDaHeight = rectangle.Height / 2;
        Collider2d = new Rectangle(startposx, startposy + halfDaHeight, rectangle.Width, halfDaHeight);

        color = Color.White;
        flipper = SpriteEffects.None;
        state = PlayerState.Waiting;
        playerSpeed = 8;
    }
    public void Update(GameTime gameTime, MapBuilder mapBuilder)
    {
        GridMovement(mapBuilder);
        HandleWhatToDraw();
        CURRENT_SPRITE.Update(gameTime);
        CURRENT_SPRITE.Depth = Collider2d.Y / 10000f;
    }

    public PlayerState interactWithBox()
    {
        if (!Globals.keyb.WasKeyPressed(Keys.E))
            return state;

        if (state == PlayerState.InCommunication)
            state = PlayerState.Waiting;
        else
            state = PlayerState.InCommunication;

        return state;
    }

    public void GridMovement(MapBuilder mapBuilder)
    {
        var precheck = rectangle;

        if (state == PlayerState.InCommunication)
            return;

        int halfDaHeight = rectangle.Height / 2;
        
        if (Globals.keyb.IsKeyDown(Keys.Left) || Globals.keyb.IsKeyDown(Keys.A))
        {
            playerDirection = Direction.Left;
            flipper = SpriteEffects.None;
            state = PlayerState.Walking;

            Collider2d = new Rectangle(Collider2d.X - playerSpeed, Collider2d.Y, RpgPlayer.PLAYER_TILESIZE_IN_WORLD, halfDaHeight);
            Position = new Vector2(Collider2d.X, Collider2d.Y);

            interactionBox = new Rectangle(rectangle.X - 64, rectangle.Y, RpgPlayer.PLAYER_TILESIZE_IN_WORLD, RpgPlayer.PLAYER_TILESIZE_IN_WORLD);
        }
        else if (Globals.keyb.IsKeyDown(Keys.Right) || Globals.keyb.IsKeyDown(Keys.D))
        {
            playerDirection = Direction.Right;
            flipper = SpriteEffects.FlipHorizontally;
            state = PlayerState.Walking;

            Collider2d = new Rectangle(Collider2d.X + playerSpeed, Collider2d.Y, RpgPlayer.PLAYER_TILESIZE_IN_WORLD, halfDaHeight);
            Position = new Vector2(Collider2d.X, Collider2d.Y);

            interactionBox = new Rectangle(rectangle.X + 64, rectangle.Y, RpgPlayer.PLAYER_TILESIZE_IN_WORLD, RpgPlayer.PLAYER_TILESIZE_IN_WORLD);
        }
        else if (Globals.keyb.IsKeyDown(Keys.Up) || Globals.keyb.IsKeyDown(Keys.W))
        {
            playerDirection = Direction.Up;
            state = PlayerState.Walking;

            Collider2d = new Rectangle(Collider2d.X, Collider2d.Y - playerSpeed, RpgPlayer.PLAYER_TILESIZE_IN_WORLD, halfDaHeight);
            Position = new Vector2(Collider2d.X, Collider2d.Y);

            interactionBox = new Rectangle(rectangle.X, rectangle.Y-64, RpgPlayer.PLAYER_TILESIZE_IN_WORLD, RpgPlayer.PLAYER_TILESIZE_IN_WORLD);
        }
        else if (Globals.keyb.IsKeyDown(Keys.Down) || Globals.keyb.IsKeyDown(Keys.S))
        {
            playerDirection = Direction.Down;
            state = PlayerState.Walking;

            Collider2d = new Rectangle(Collider2d.X, Collider2d.Y + playerSpeed, RpgPlayer.PLAYER_TILESIZE_IN_WORLD, halfDaHeight);
            Position = new Vector2(Collider2d.X, Collider2d.Y);

            interactionBox = new Rectangle(rectangle.X, rectangle.Y + 64, RpgPlayer.PLAYER_TILESIZE_IN_WORLD, RpgPlayer.PLAYER_TILESIZE_IN_WORLD);

        }
        else
            state = PlayerState.Waiting;

        rectangle = new Rectangle(Collider2d.X, Collider2d.Y - halfDaHeight, RpgPlayer.PLAYER_TILESIZE_IN_WORLD, RpgPlayer.PLAYER_TILESIZE_IN_WORLD);

    }
    public void HandleWhatToDraw()
    {
        var origin = new Vector2(0, 0);
        var position = new Vector2(rectangle.X, rectangle.Y);
        var scale = new Vector2(1f, 1f);
        var shadowRectangle = new Rectangle(rectangle.X + 16, rectangle.Y + 24, rectangle.Width, rectangle.Height);

        color = Color.White;

        if (state == PlayerState.Walking)
        {
            switch (playerDirection)
            {
                case (Direction.Down):
                    CURRENT_SPRITE = _walkDown;
                    break;
                case (Direction.Left):
                case (Direction.Right):
                    _walkSide.Effect = flipper;
                    CURRENT_SPRITE = _walkSide;
                    break;
                case (Direction.Up):
                    CURRENT_SPRITE = _walkUp;
                    break;
            }
        }
        else
        {
            switch (playerDirection)
            {
                case (Direction.Down):
                    texture = Atlases.playerAtlas[0];
                    break;
                case (Direction.Left):
                case (Direction.Right):
                    texture = Atlases.playerAtlas[1];
                    break;
                case (Direction.Up):
                    texture = Atlases.playerAtlas[3];
                    break;
            }
        }

    }

    public void DetectCollisionWithNPC(KeyObjective _ko)
    {
        if (_ko == null)
            return;

        if (Collider2d.Intersects(_ko.Collider2d))
        {
            int halfDaHeight = PLAYER_TILESIZE_IN_WORLD / 2;

            switch (playerDirection)
            {
                case Direction.Left:
                    Collider2d = new Rectangle(Collider2d.X + playerSpeed, Collider2d.Y, PLAYER_TILESIZE_IN_WORLD, halfDaHeight);
                    Position = new Vector2(Collider2d.X, Collider2d.Y);
                    return;
                case Direction.Right:
                    Collider2d = new Rectangle(Collider2d.X - playerSpeed, Collider2d.Y, PLAYER_TILESIZE_IN_WORLD, halfDaHeight);
                    Position = new Vector2(Collider2d.X, Collider2d.Y);
                    return;
                case Direction.Up:
                    Collider2d = new Rectangle(Collider2d.X, Collider2d.Y + playerSpeed, PLAYER_TILESIZE_IN_WORLD, halfDaHeight);
                    Position = new Vector2(Collider2d.X, Collider2d.Y + halfDaHeight);
                    return;
                case Direction.Down:
                    Collider2d = new Rectangle(Collider2d.X, Collider2d.Y - playerSpeed, PLAYER_TILESIZE_IN_WORLD, halfDaHeight);
                    Position = new Vector2(Collider2d.X, Collider2d.Y - halfDaHeight);
                    return;
            }

            rectangle = new Rectangle(Collider2d.X, Collider2d.Y - halfDaHeight, PLAYER_TILESIZE_IN_WORLD, PLAYER_TILESIZE_IN_WORLD);

        }
    }

    public void DetectCollision(MapBuilder mapBuilder)
    {
        if (mapBuilder == null)
            return;

        if (state != PlayerState.Walking)
            return;

        foreach (var space in mapBuilder.Spaces)
        {

            if (Collider2d.Intersects(space.rectangle) 
                && space.csvValue == 6)
            {
                int halfDaHeight = PLAYER_TILESIZE_IN_WORLD / 2;
                
                switch (playerDirection)
                {
                    case Direction.Left:
                        Collider2d = new Rectangle(Collider2d.X + playerSpeed, Collider2d.Y, PLAYER_TILESIZE_IN_WORLD, halfDaHeight);
                        Position = new Vector2(Collider2d.X, Collider2d.Y);
                        return;
                    case Direction.Right:
                        Collider2d = new Rectangle(Collider2d.X - playerSpeed, Collider2d.Y, PLAYER_TILESIZE_IN_WORLD, halfDaHeight);
                        Position = new Vector2(Collider2d.X, Collider2d.Y);
                        return;
                    case Direction.Up:
                        Collider2d = new Rectangle(Collider2d.X, Collider2d.Y + playerSpeed, PLAYER_TILESIZE_IN_WORLD, halfDaHeight);
                        Position = new Vector2(Collider2d.X, Collider2d.Y + halfDaHeight);
                        return;
                    case Direction.Down:
                        Collider2d = new Rectangle(Collider2d.X, Collider2d.Y - playerSpeed, PLAYER_TILESIZE_IN_WORLD, halfDaHeight);
                        Position = new Vector2(Collider2d.X, Collider2d.Y - halfDaHeight);
                        return;
                }

                rectangle = new Rectangle(Collider2d.X, Collider2d.Y-halfDaHeight, PLAYER_TILESIZE_IN_WORLD, PLAYER_TILESIZE_IN_WORLD);

            }
        }
    }
}

public class PlayerCombatActor : CombatActors
{

}