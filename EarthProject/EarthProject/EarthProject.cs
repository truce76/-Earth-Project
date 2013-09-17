using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace EarthProject
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class EarthProject : Microsoft.Xna.Framework.Game
    {
        #region const
        /// <summary>
        /// size of a tile
        /// </summary>
        public const int TILE_SIZE = 50;


        #endregion const

        #region privatevar
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch; 
        Terrain terre;
        Camera2D cam;
        int previousScroll;
        Song mysong;
        AnimatedTexture butterfly;
        
        int i = 10;
        #endregion privatevar

        public EarthProject()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
         //   graphics.ToggleFullScreen();
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            this.IsMouseVisible = true;
            base.Initialize();
            cam = new Camera2D(this.GraphicsDevice.Viewport, 0.5f);
             
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            terre = new Terrain(40, 10);
            terre.generer(this);
            mysong = Content.Load<Song>("Toes and Water_0");
            MediaPlayer.Play(mysong);
            butterfly = new AnimatedTexture(13, 9, Content.Load<Texture2D>("butterfly"), new Rectangle(10,10,20,20));
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            MediaPlayer.Stop();
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Exit();



            //Adjust zoom if the mouse wheel has moved
            if (Mouse.GetState().ScrollWheelValue > previousScroll)
                cam.zoom += 0.5f;
            else if (Mouse.GetState().ScrollWheelValue < previousScroll)
                cam.zoom -= 0.5f;

            previousScroll = Mouse.GetState().ScrollWheelValue;

            // Move the cam when the arrow keys are pressed
            Vector2 movement = Vector2.Zero;
            Viewport vp = this.GraphicsDevice.Viewport;

            if (Keyboard.GetState().IsKeyDown(Keys.Q))
                movement.X--;
            if (Keyboard.GetState().IsKeyDown(Keys.D))
                movement.X++;
            if (Keyboard.GetState().IsKeyDown(Keys.Z))
                movement.Y--;
            if (Keyboard.GetState().IsKeyDown(Keys.S))
                movement.Y++;

            cam.Move(movement * 20);

            if (MediaPlayer.State == MediaState.Stopped)
            {
                MediaPlayer.Play(mysong);
            }
            if (Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                Matrix mat = this.cam.GetTransformation();

                float X = ((Mouse.GetState().X - (mat.Translation.X)) / this.cam.zoom) - (butterfly.hitbox.Width / 2);

                float Y = ((Mouse.GetState().Y - (mat.Translation.Y)) / this.cam.zoom) - (butterfly.hitbox.Height / 2); 

                butterfly.newcoord = new Point((int)X,(int)Y);
            }
            butterfly.Update(gameTime);
            base.Update(gameTime);

         
        }
        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

          spriteBatch.Begin(SpriteSortMode.Deferred,BlendState.AlphaBlend,SamplerState.LinearClamp,DepthStencilState.None,RasterizerState.CullCounterClockwise,null,cam.GetTransformation());

            foreach (Tile tile in terre.terrain)
            {
              
            spriteBatch.Draw(tile.texture,tile.bound, null,Color.White,0f,Vector2.Zero,SpriteEffects.None,1);
           
            }
            butterfly.Draw(spriteBatch);
            if (Mouse.GetState().LeftButton== ButtonState.Pressed)
            {
                spriteBatch.Draw(Content.Load<Texture2D>("Default"), new Rectangle(Mouse.GetState().X, Mouse.GetState().Y, 15, 15),null, Color.White,0f,Vector2.Zero,SpriteEffects.None,0);
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
