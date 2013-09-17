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
    class Terrain
    {

        public List<Tile> terrain { get; set; }
        public int width { get; set; }
        public int height { get; set; }
        public Terrain()
        {
            terrain = new List<Tile>();
            width = 0;
            height = 0;
        }
        public Terrain(int width,int height)
        {
            terrain = new List<Tile>();
            this.width = width;
            this.height = height;
        }

        public void generer(EarthProject game)
        { 

            Random r = new Random();
            int size = EarthProject.TILE_SIZE;
           
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {

                    Tile herbe = new Tile(game.Content.Load<Texture2D>("grass"), new Rectangle(i * size, j * size, size, size));
                    terrain.Add(herbe);
                }
            }

            for (int i = 0; i < width; i++)
            {
                Tile bord = new Tile(game.Content.Load<Texture2D>("grassBord"), new Rectangle(size * i, height * size, size, size));
                terrain.Add(bord);
                
            }

            terrain = terrain.OrderBy(ts => ts.bound.X).ToList();

        }
    }
}
