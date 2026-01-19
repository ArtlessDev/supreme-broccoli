using JairLib.InputHandler;
using JairLib.TileGenerators;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Graphics;
using System.Diagnostics;
using System.Timers;

namespace JairLib;
public class BasePlayer : AnyObject
{
    public static Rectangle frameStartRectangle;
    public static int PLAYER_TILESIZE_IN_WORLD = 128;
    public SpriteEffects flipper { get; set; }
    public Direction playerDirection { get; set; }
    public PlayerState state { get; set; }
    public Vector3 vectorPosition { get; set; }

    public void AnimatePlayerIdle(GameTime gameTime)
    {

        var deltaTime = (float)gameTime.TotalGameTime.Milliseconds;
        //could make methods to handle animations in this sort of way
        if (deltaTime < 500)
        {
            texture = Globals.playerPrototypeAtlas[0];
            Debug.WriteLine($"{deltaTime}");
        }
        else
        {
            texture = Globals.playerPrototypeAtlas[1];
            Debug.WriteLine($"{deltaTime}");
        }
    }


    public void AnimatePlayerMoving(GameTime gameTime)
    {
        texture = Globals.playerPrototypeAtlas[0];

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

        texture = Globals.playerPrototypeAtlas[0]; //blue
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
        HandleGravity();
        HandleJump();

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
            texture = Globals.playerPrototypeAtlas[0]; //blue
            playerDirection = Direction.Left;
            var adjustedX = (int)(rectangle.X - playerCurrentSpeed);
            
            rectangle = new Rectangle(adjustedX, rectangle.Y, PLAYER_TILESIZE_IN_WORLD, PLAYER_TILESIZE_IN_WORLD);
        }
        if (BasicInputs.HandleKeyDown(Keys.Right, Keys.D))
        {
            //flipper = SpriteEffects.FlipHorizontally;
            playerDirection = Direction.Right;
            texture = Globals.playerPrototypeAtlas[0]; //blue
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
    public Vector3 playerAltitude { get; set; }

    public PlayerOverworld()
    {
        identifier = "player";
        //texture = Globals.atlas[2 - '0'];
        texture = Globals.playerPrototypeAtlas[1]; //blue
        rectangle = new Rectangle((int)Globals.STARTING_POSITION.X, (int)Globals.STARTING_POSITION.Y, PLAYER_TILESIZE_IN_WORLD, PLAYER_TILESIZE_IN_WORLD);
        color = Color.White;
        flipper = SpriteEffects.None;
        state = PlayerState.Waiting;
        playerSpeed = 3;
        playerAltitude = new Vector3(0, 0, 7);
    }
    public void Update(GameTime gameTime, MapBuilder mapBuilder)
    {
        if (state == PlayerState.Dead)
        {
            PlayerReset();
        }
        else
        {
            DiagonalMovement(mapBuilder);
            DetectCollision(mapBuilder);
            HandleGravity(gameTime, mapBuilder);
            CheckStateForColor();
        }
        
    }

    public void PlayerReset()
    {
        texture = Globals.playerPrototypeAtlas[3]; //idk lol

        if (Globals.keyb.WasKeyPressed(Keys.R))
        {
            rectangle = new Rectangle((int)Globals.STARTING_POSITION.X, (int)Globals.STARTING_POSITION.Y, PLAYER_TILESIZE_IN_WORLD, PLAYER_TILESIZE_IN_WORLD);
            playerAltitude = new Vector3(0, 0, 7);
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
            if(state!=PlayerState.Freefall)
                state = PlayerState.Walking;
            //texture = Globals.gameTilePrototypeAtlas[1]; //blue
            playerDirection = Direction.Left;
        }
        if (Globals.keyb.IsKeyDown(Keys.Right) || Globals.keyb.IsKeyDown(Keys.D))
        {
            flipper = SpriteEffects.FlipHorizontally;
            rectangle = new Rectangle(rectangle.X + playerSpeed, rectangle.Y, PLAYER_TILESIZE_IN_WORLD, PLAYER_TILESIZE_IN_WORLD);
            if(state!=PlayerState.Freefall)
                state = PlayerState.Walking;
            playerDirection = Direction.Right;
            //texture = Globals.gameTilePrototypeAtlas[1]; //blue

        }
        if (Globals.keyb.IsKeyDown(Keys.Up) || Globals.keyb.IsKeyDown(Keys.W))
        {
            rectangle = new Rectangle(rectangle.X, rectangle.Y - playerSpeed, PLAYER_TILESIZE_IN_WORLD, PLAYER_TILESIZE_IN_WORLD);
            if(state!=PlayerState.Freefall)
                state = PlayerState.Walking;
            playerDirection = Direction.Up;
            //texture = Globals.gameTilePrototypeAtlas[1]; //blue

        }
        if (Globals.keyb.IsKeyDown(Keys.Down) || Globals.keyb.IsKeyDown(Keys.S))
        {
            rectangle = new Rectangle(rectangle.X, rectangle.Y + playerSpeed, PLAYER_TILESIZE_IN_WORLD, PLAYER_TILESIZE_IN_WORLD);
            if(state!=PlayerState.Freefall)
                state = PlayerState.Walking;
            //texture = Globals.gameTilePrototypeAtlas[1]; //blue
            playerDirection = Direction.Down;
        }
        if (!Globals.keyb.WasAnyKeyJustDown() && state != PlayerState.Freefall)
        {
            state = PlayerState.Waiting;
        }

        if (Globals.keyb.WasKeyPressed(Keys.Space))
        {
            //texture = Globals.gameTilePrototypeAtlas[6]; //yellow
            playerAltitude = new Vector3(playerAltitude.X, playerAltitude.Y, playerAltitude.Z + 50);
            state = PlayerState.Freefall;
            return;
        }
        
    }
    
    public void GridMovement(MapBuilder mapBuilder)
    {
        var precheck = rectangle;

        if (Globals.keyb.IsKeyDown(Keys.Left) || Globals.keyb.IsKeyDown(Keys.A))
        {
            flipper = SpriteEffects.None;
            rectangle = new Rectangle(rectangle.X - playerSpeed, rectangle.Y, PLAYER_TILESIZE_IN_WORLD, PLAYER_TILESIZE_IN_WORLD);            
            if(state!=PlayerState.Freefall)
                state = PlayerState.Walking;
            //texture = Globals.gameTilePrototypeAtlas[1]; //blue
            playerDirection = Direction.Left;
        }
        else if (Globals.keyb.IsKeyDown(Keys.Right) || Globals.keyb.IsKeyDown(Keys.D))
        {
            flipper = SpriteEffects.FlipHorizontally;
            rectangle = new Rectangle(rectangle.X + playerSpeed, rectangle.Y, PLAYER_TILESIZE_IN_WORLD, PLAYER_TILESIZE_IN_WORLD);
            if(state!=PlayerState.Freefall)
                state = PlayerState.Walking;
            playerDirection = Direction.Right;
            //texture = Globals.gameTilePrototypeAtlas[1]; //blue

        }
        else if (Globals.keyb.IsKeyDown(Keys.Up) || Globals.keyb.IsKeyDown(Keys.W))
        {
            rectangle = new Rectangle(rectangle.X, rectangle.Y - playerSpeed, PLAYER_TILESIZE_IN_WORLD, PLAYER_TILESIZE_IN_WORLD);
            if(state!=PlayerState.Freefall)
                state = PlayerState.Walking;
            playerDirection = Direction.Up;
            //texture = Globals.gameTilePrototypeAtlas[1]; //blue

        }
        else if (Globals.keyb.IsKeyDown(Keys.Down) || Globals.keyb.IsKeyDown(Keys.S))
        {
            rectangle = new Rectangle(rectangle.X, rectangle.Y + playerSpeed, PLAYER_TILESIZE_IN_WORLD, PLAYER_TILESIZE_IN_WORLD);
            if(state!=PlayerState.Freefall)
                state = PlayerState.Walking;
            //texture = Globals.gameTilePrototypeAtlas[1]; //blue
            playerDirection = Direction.Down;
        }
        else if (!Globals.keyb.WasAnyKeyJustDown() && state != PlayerState.Freefall)
        {
            state = PlayerState.Waiting;
        }

        if (Globals.keyb.WasKeyPressed(Keys.Space))
        {
            //texture = Globals.gameTilePrototypeAtlas[6]; //yellow
            playerAltitude = new Vector3(playerAltitude.X, playerAltitude.Y, playerAltitude.Z + 50);
            state = PlayerState.Freefall;
            return;
        }
        
    }

    public void CheckStateForColor()
    {
        switch (state)
        {
            case PlayerState.Walking: 
                texture = Globals.playerPrototypeAtlas[1]; //white
                break;
            case PlayerState.Jumping: 
                texture = Globals.playerPrototypeAtlas[6]; //yellow
                break;
            case PlayerState.Freefall: 
                texture = Globals.playerPrototypeAtlas[7]; //red
                break;
            case PlayerState.Waiting:
                texture = Globals.playerPrototypeAtlas[8]; //orange
                break;
        }
    }



    //this is a bit much should probably refactor to offload tothe tilespaces 
    public void HandleGravity(GameTime gameTime, MapBuilder map)
    {
        Debug.WriteLine(playerAltitude.Z);
        foreach (var mapEntry in map.Spaces)
        {
            //handles for when the player is above pit zones so somewhere where the player could 'fall into the void'
            if (state != PlayerState.Jumping 
                && mapEntry.isPit 
                && rectangle.Intersects(mapEntry.rectangle)
                )
            {
                if (playerAltitude.Z > mapEntry.altitude.Z)
                {
                    //Debug.WriteLine("PIT");
                    playerAltitude = new Vector3(playerAltitude.X, playerAltitude.Y, playerAltitude.Z - 1f);
                    break;
                }
                if(mapEntry.altitude.Z == playerAltitude.Z)
                {
                    state = PlayerState.Dead;
                    return;
                }
            }

            //this gatekeeps the player from getting onto a platform being under it
            if (mapEntry.spaceType == TileSpaceType.Walkable
                && rectangle.Intersects(mapEntry.rectangle) 
                && (state != PlayerState.Jumping || state != PlayerState.Freefall)
                )
            {
                if (playerAltitude.Z > mapEntry.altitude.Z)
                {
                    playerAltitude = new Vector3(0,0, playerAltitude.Z - 1f);

                }
                else if (playerAltitude.Z <= mapEntry.altitude.Z)
                {
                    //player will land and restet to waiting
                    state = PlayerState.Waiting;
                    playerAltitude = mapEntry.altitude;
                }

                break;
            }
            
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
                    && space.spaceType == TileSpaceType.Walkable
                    && playerAltitude.Z < space.altitude.Z)
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
