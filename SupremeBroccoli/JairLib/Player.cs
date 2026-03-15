using JairLib.InputHandler;
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
    public static Rectangle frameStartRectangle;
    public static int PLAYER_TILESIZE_IN_WORLD = 128;
    public SpriteEffects flipper { get; set; }
    public Direction playerDirection { get; set; }
    public PlayerState state { get; set; }

    #region Stats
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
    #endregion Stats

    public void AnimatePlayerIdle(GameTime gameTime)
    {

        var deltaTime = (float)gameTime.TotalGameTime.Milliseconds;
        //could make methods to handle animations in this sort of way
        if (deltaTime < 500)
        {
            texture = Atlases.tilesetAtlas[0];
            Debug.WriteLine($"{deltaTime}");
        }
        else
        {
            texture = Atlases.tilesetAtlas[1];
            Debug.WriteLine($"{deltaTime}");
        }
    }


    public void AnimatePlayerMoving(GameTime gameTime)
    {
        texture = Atlases.tilesetAtlas[0];

        var deltaTime = (float)gameTime.TotalGameTime.Milliseconds;

        if (deltaTime == 0)
        {
            state = PlayerState.Waiting;

        }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        //spriteBatch.Draw(texture, rectangle, color);
        var rotation = 0f;
        var origin = new Vector2(0, 0);
        var position = new Vector2(rectangle.X, rectangle.Y);
        var scale = new Vector2(1f, 1f);

        spriteBatch.Draw(texture, position, color, rotation, origin, scale, flipper, 1f);

    }
}

public class PlayerPlatformer : BasePlayer
{
    public static System.Timers.Timer timer = new System.Timers.Timer(200f);
    public float playerTopSpeed { get; set; }
    public float playerCurrentSpeed { get; set; }

    public PlayerPlatformer()
    {
        identifier = "player";

        texture = Atlases.tilesetAtlas[0]; //blue
        rectangle = new Rectangle((int)Globals.STARTING_POSITION.X, (int)Globals.STARTING_POSITION.Y, PLAYER_TILESIZE_IN_WORLD, PLAYER_TILESIZE_IN_WORLD);
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

public class PlayerOverworld : BasePlayer
{
    public int playerSpeed { get; set; }

    public PlayerOverworld()
    {
        identifier = "player";
        texture = Atlases.tilesetAtlas[3]; //blue
        int startposx = 0;//Globals.TileSize * (Globals.mapWidth / 2);
        int startposy = 0;// Globals.TileSize * (int)(Globals.mapHeight * .95);
        rectangle = new Rectangle(startposx, startposy, PLAYER_TILESIZE_IN_WORLD, PLAYER_TILESIZE_IN_WORLD);
        Position = new(startposx, startposy);

        color = Color.White;
        flipper = SpriteEffects.None;
        state = PlayerState.Waiting;
        playerSpeed = Globals.TileSize;
    }
    public void Update(GameTime gameTime, MapBuilder mapBuilder)
    {
        GridMovement(mapBuilder);
            //DiagonalMovement(mapBuilder);
            //DetectCollision(mapBuilder);
            //HandleGravity(gameTime, mapBuilder);
            //CheckStateForColor();
        
        
    }

    public void PlayerReset()
    {
        texture = Atlases.tilesetAtlas[3]; //idk lol

        if (Globals.keyb.WasKeyPressed(Keys.R))
        {
            rectangle = new Rectangle((int)Globals.STARTING_POSITION.X, (int)Globals.STARTING_POSITION.Y, PLAYER_TILESIZE_IN_WORLD, PLAYER_TILESIZE_IN_WORLD);
            state = PlayerState.Waiting;

        }

        return;
    }

    public void DiagonalMovement(MapBuilder mapBuilder)
    {
        var precheck = rectangle;

        if (Globals.keyb.IsKeyDown(Keys.Left) || Globals.keyb.IsKeyDown(Keys.A))
        {
            flipper = SpriteEffects.None;
            rectangle = new Rectangle(rectangle.X - playerSpeed, rectangle.Y, PLAYER_TILESIZE_IN_WORLD, PLAYER_TILESIZE_IN_WORLD);
            Position = new(rectangle.X, rectangle.Y);
            
            playerDirection = Direction.Left;
        }
        if (Globals.keyb.IsKeyDown(Keys.Right) || Globals.keyb.IsKeyDown(Keys.D))
        {
            flipper = SpriteEffects.FlipHorizontally;
            rectangle = new Rectangle(rectangle.X + playerSpeed, rectangle.Y, PLAYER_TILESIZE_IN_WORLD, PLAYER_TILESIZE_IN_WORLD);
            Position = new(rectangle.X, rectangle.Y);
            playerDirection = Direction.Right;

        }
        if (Globals.keyb.IsKeyDown(Keys.Up) || Globals.keyb.IsKeyDown(Keys.W))
        {
            rectangle = new Rectangle(rectangle.X, rectangle.Y - playerSpeed, PLAYER_TILESIZE_IN_WORLD, PLAYER_TILESIZE_IN_WORLD);
            Position = new(rectangle.X, rectangle.Y);

        }
        if (Globals.keyb.IsKeyDown(Keys.Down) || Globals.keyb.IsKeyDown(Keys.S))
        {
            rectangle = new Rectangle(rectangle.X, rectangle.Y + playerSpeed, PLAYER_TILESIZE_IN_WORLD, PLAYER_TILESIZE_IN_WORLD);
            Position = new(rectangle.X, rectangle.Y);
            playerDirection = Direction.Down;
        }
    }
    
    public void GridMovement(MapBuilder mapBuilder)
    {
        var precheck = rectangle;

        if (Globals.keyb.WasKeyPressed(Keys.Left) || Globals.keyb.WasKeyPressed(Keys.A))
        {
            flipper = SpriteEffects.None;
            rectangle = new Rectangle(rectangle.X - playerSpeed, rectangle.Y, PLAYER_TILESIZE_IN_WORLD, PLAYER_TILESIZE_IN_WORLD);
            Position = new Vector2(rectangle.X, rectangle.Y);

            playerDirection = Direction.Left;
        }
        else if (Globals.keyb.WasKeyPressed(Keys.Right) || Globals.keyb.WasKeyPressed(Keys.D))
        {
            flipper = SpriteEffects.FlipHorizontally;
            rectangle = new Rectangle(rectangle.X + playerSpeed, rectangle.Y, PLAYER_TILESIZE_IN_WORLD, PLAYER_TILESIZE_IN_WORLD);
            Position = new Vector2(rectangle.X, rectangle.Y);
            
            playerDirection = Direction.Right;
        }
        else if (Globals.keyb.WasKeyPressed(Keys.Up) || Globals.keyb.WasKeyPressed(Keys.W))
        {
            rectangle = new Rectangle(rectangle.X, rectangle.Y - playerSpeed, PLAYER_TILESIZE_IN_WORLD, PLAYER_TILESIZE_IN_WORLD);
            Position = new Vector2(rectangle.X, rectangle.Y); 
         
            playerDirection = Direction.Up;
        }
        else if (Globals.keyb.WasKeyPressed(Keys.Down) || Globals.keyb.WasKeyPressed(Keys.S))
        {
            rectangle = new Rectangle(rectangle.X, rectangle.Y + playerSpeed, PLAYER_TILESIZE_IN_WORLD, PLAYER_TILESIZE_IN_WORLD);
            Position = new Vector2(rectangle.X, rectangle.Y);
         
            playerDirection = Direction.Down;
        }
    }

    public void CheckStateForColor()
    {
        switch (state)
        {
            case PlayerState.Walking: 
                texture = Atlases.tilesetAtlas[1]; //white
                break;
            case PlayerState.Jumping: 
                texture = Atlases.tilesetAtlas[6]; //yellow
                break;
            case PlayerState.Freefall: 
                texture = Atlases.tilesetAtlas[7]; //red
                break;
            case PlayerState.Waiting:
                texture = Atlases.tilesetAtlas[8]; //orange
                break;
        }
    }

    public void DetectCollision(MapBuilder mapBuilder)
    {
        if (state == PlayerState.Walking && mapBuilder != null)
        {
            foreach (var space in mapBuilder.Spaces)
            {

                //this makes the player not allowed to get onto a platform if they miss a jump
                if (space.rectangle.Intersects(rectangle) 
                    && space.csvValue !=6)
                {
                    switch (playerDirection)
                    {
                        case Direction.Left:
                            rectangle = new Rectangle(rectangle.X + playerSpeed, rectangle.Y, PLAYER_TILESIZE_IN_WORLD, PLAYER_TILESIZE_IN_WORLD);
                            Position = new Vector2(rectangle.X, rectangle.Y);
                            return;
                        case Direction.Right:
                            rectangle = new Rectangle(rectangle.X - playerSpeed, rectangle.Y, PLAYER_TILESIZE_IN_WORLD, PLAYER_TILESIZE_IN_WORLD);
                            Position = new Vector2(rectangle.X, rectangle.Y);
                            return;
                        case Direction.Up:
                            rectangle = new Rectangle(rectangle.X, rectangle.Y + playerSpeed, PLAYER_TILESIZE_IN_WORLD, PLAYER_TILESIZE_IN_WORLD);
                            Position = new Vector2(rectangle.X, rectangle.Y);
                            return;
                        case Direction.Down:
                            rectangle = new Rectangle(rectangle.X, rectangle.Y + playerSpeed, PLAYER_TILESIZE_IN_WORLD, PLAYER_TILESIZE_IN_WORLD);
                            Position = new Vector2(rectangle.X, rectangle.Y);
                            return;
                    }
                }

                //this is for walls
                if (space.rectangle.Intersects(rectangle) && space.isCollidable)
                {
                    switch (playerDirection)
                    {
                        case Direction.Left:
                            rectangle = new Rectangle(rectangle.X + playerSpeed, rectangle.Y, PLAYER_TILESIZE_IN_WORLD, PLAYER_TILESIZE_IN_WORLD);
                            return;
                        case Direction.Right:
                            rectangle = new Rectangle(rectangle.X - playerSpeed, rectangle.Y, PLAYER_TILESIZE_IN_WORLD, PLAYER_TILESIZE_IN_WORLD);
                            return;
                        case Direction.Up:
                            rectangle = new Rectangle(rectangle.X, rectangle.Y + playerSpeed, PLAYER_TILESIZE_IN_WORLD, PLAYER_TILESIZE_IN_WORLD);
                            return;
                        case Direction.Down:
                            rectangle = new Rectangle(rectangle.X, rectangle.Y + playerSpeed, PLAYER_TILESIZE_IN_WORLD, PLAYER_TILESIZE_IN_WORLD);
                            return;
                    }
                }

                
            }
        }
    }
}
