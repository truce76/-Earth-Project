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
    class Camera2D
    {
        private float _zoom;
        public float zoom
        {
            get
            {
                return _zoom;
            }
            set
            {
                if (value<=0f)
                {
                    _zoom = 0.1f;
                }
                else
                {
                    _zoom = value;

                }
            }
        }
        public Matrix transform { get; set; }
        public Vector2 position
        {
            get;
            set; 
        }

        public void SetPos(Vector2 pos)
        {
            float leftBarrier = (float)Camera.Width *
                .5f / zoom;
            float rightBarrier = 10000 -
                   (float)Camera.Width * .5f / zoom;
            float topBarrier = 10000 -
                   (float)Camera.Height * .5f / zoom;
            float bottomBarrier = (float)Camera.Height *
                   .5f / zoom;
            Vector2 position2 = pos;
            if (position2.X < leftBarrier)
                position2.X=leftBarrier;
            if (position2.X > rightBarrier)
                position2.X = rightBarrier;
            if (position2.Y > topBarrier)
                position2.Y = topBarrier;
            if (position2.Y < bottomBarrier)
                position2.Y = bottomBarrier;

            position = position2;
        }
        
        public Viewport Camera { get; set; }
        public float rotation { get; set; }
        public Camera2D(Viewport view, float zoom)
        {
            transform = new Matrix();
            this.zoom = zoom;
            rotation = 0.0f;
            Camera = view;
            position = new Vector2(0,0);
        }

        public void Move(Vector2 moved)
        {
            position += moved;
        }

        public Matrix GetTransformation()
        {
            transform =
               Matrix.CreateTranslation(new Vector3(-position.X, -position.Y, 0)) *
               Matrix.CreateRotationZ(rotation) *
               Matrix.CreateScale(new Vector3(zoom, zoom, 1)) *
               Matrix.CreateTranslation(new Vector3(Camera.Width * 0.5f,
                   Camera.Height * 0.5f, 0));

            return transform;
        }
    }
}
