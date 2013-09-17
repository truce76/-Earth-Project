using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;


namespace EarthProject
{  
    
    enum Action
        {
            ACTION_IDLE = 0,
            ACTION_MOVING = 1
        }

    class AnimatedTexture
    {
     
        public int frame { get; set; }
        public int totalframe { get; set; }
        public float framesec { get; set; }
        public Texture2D texture { get; set; }
        public double elapsed { get; set; }
        public Rectangle hitbox { get; set; }
        public Action action { get; set; }
        public Point newcoord { get; set; }
        public AnimatedTexture(int framesec, int totalframe, Texture2D texture,Rectangle hitbox, int frame = 0 )
        {
           
            this.frame=frame;
            this.framesec=(1f/framesec);
            this.texture = texture;
            this.totalframe = totalframe;
            this.elapsed = 0;
            this.hitbox = hitbox;
            this.newcoord = new Point(0, 0);

        }
        public void Update(GameTime elapse)
        {

            this.elapsed += elapse.ElapsedGameTime.TotalSeconds;

            if (newcoord.X != 0 && newcoord.Y != 0)
            {
                this.Move(newcoord.X, newcoord.Y);
            }
            if (elapsed>=framesec)
            {
              
                frame++;
                if (frame>totalframe)
                {
                    frame = 0;
                }
                elapsed = 0;
            }
        }
        public void Move(int x, int y)
        {
            if (this.hitbox.X != x || this.hitbox.Y != y)
            {
                if (this.action== Action.ACTION_IDLE)
                {
                    this.action = Action.ACTION_MOVING;
                }
               
                int movex= 0;
                int movey = 0;
                if (x>this.hitbox.X)
                {
                    movex++;
                }
                else
                {
                    movex--;
                }
                if (y>this.hitbox.Y)
                {
                    movey++;
                }
                else
                {
                    movey--;
                }

                this.hitbox = new Rectangle(this.hitbox.X + movex, this.hitbox.Y + movey, this.hitbox.Width, this.hitbox.Height);
            }
            else
            {
                this.action = Action.ACTION_IDLE;
                this.newcoord = new Point(0, 0);
            }
        }
        public void Draw(SpriteBatch spritebatch)
        {
            Rectangle source = new Rectangle(frame*30,0,30,25);
            spritebatch.Draw(texture, hitbox, source, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 1);
        }

        public bool Outofbounds(Terrain terrain )
        {
            int x = this.hitbox.X;
            int y = this.hitbox.Y;
            int last = terrain.terrain.Count-1;
            Tile firstT = terrain.terrain[0];
            Tile lastT= terrain.terrain[last];
            if  ( x<=firstT.bound.X || 
                  x>=(lastT.bound.X + lastT.bound.Width)  || 
                  y<=firstT.bound.Y ||
                  y>=(lastT.bound.Y + lastT.bound.Height)
                )
            {
                return true;
            }
           

            return false;
        }
    }
}
