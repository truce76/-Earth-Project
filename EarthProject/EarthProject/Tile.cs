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
    class Tile
    {
        public Texture2D texture { get; set; }
        public Rectangle bound { get; set; }


        public Tile()
        {
            this.bound = new Rectangle();

        }
        public Tile(Texture2D texture, Rectangle rect)
        {
            this.texture = texture;
            bound = rect;

        } 
    }
}
