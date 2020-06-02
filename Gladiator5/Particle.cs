using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Gladiator5.Desktop
{
    public class Particle
    {
        public Vector2 scale { get; set; }
        Vector2 origin;
        public Vector2 position { get; set; }
        public Vector2 direction { get; set; }
        public float mass { get; set; }
        public float speed { get; set; }
        public float rotation { get; set; }
        public float maxSpeed { get; set; }
        Texture2D texture;
        public Color color { get; set; }
        public int lifetime { get; set; }
        public Rectangle colisionBox { get; set; }
        public bool decay;
        public int flag { get; set; }
        public bool flaged { get; set; }
        public int value { get; set;}



        public Particle(int x,int y,float w,float h,float spd,Texture2D tex,Color c,int life)
        {
            texture = tex;
            position = new Vector2(x, y);
            scale = new Vector2(w, h);
            origin = new Vector2(texture.Width / 2, texture.Height / 2);
            speed = spd;
            direction = new Vector2(0, 0);
            color = c;
            lifetime = life;
            mass = life;
            colisionBox = new Rectangle((int)(position.X-scale.X/2), (int)(position.Y - scale.Y/2), (int)scale.X, (int)scale.Y);

        }

        public void update(GameTime gameTime)
        {
            if(lifetime > 0)
            {
                float delta = (float)gameTime.ElapsedGameTime.TotalSeconds;
                position = Vector2.Add(position, new Vector2(direction.X * delta, direction.Y * delta));

                colisionBox = new Rectangle((int)(position.X - scale.X/2), (int)(position.Y - scale.Y/2), (int)scale.X, (int)scale.Y);
                speed *= 0.99f;
                if(decay)
                    lifetime--;
            }
        }

        public void updatePositon(Vector2 pos)
        {
            position = pos;
            colisionBox = new Rectangle((int)(position.X - scale.X/2), (int)(position.Y - scale.Y/2), (int)scale.X, (int)scale.Y);

        }

        public bool isOutOfBounds()
        {
            return position.X < 0 || position.X > Game1.windowWidth ||
            position.Y < 0 || position.Y > Game1.windowHeight;
              
        }

        public float angleTo(Particle p)
        {
            return (float)Math.Atan2(p.position.Y - position.Y, p.position.X - position.X);
        }



        public bool colidesWith(Particle p)
        {
            return p.colisionBox.Intersects(colisionBox);
        }

        public void draw(ref SpriteBatch spriteBatch)
        {
            /*
            spriteBatch.Draw(SpriteAtlas.whiteSquare,
                             new Rectangle((int)(colisionBox.X),
                                            (int)(colisionBox.Y), (int)colisionBox.Width, (int)colisionBox.Height),
                             color);
            */
            if (isOutOfBounds())
                return;

            if (lifetime <= 0)
                return;

            /*
            spriteBatch.Draw(texture,
                             new Rectangle((int)(position.X - scale.X / 2), 
                                            (int)(position.Y - scale.Y/2), (int)scale.X, (int)scale.Y),
                             color * (1f - 1/lifetime));
            */


            spriteBatch.Draw(texture,
                             position,
                             null,
                             color * (1.0f - 1.0f / lifetime),
                             rotation,
                             origin,
                             new Vector2(scale.X / texture.Width,scale.Y / texture.Height),
                             SpriteEffects.None,
                             0f);

        }
    }
}
