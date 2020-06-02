using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Gladiator5.Desktop
{
    public class SpriteAtlas
    {
        public static Texture2D ball;
        public static Texture2D star1;
        public static Texture2D spark5;
        public static Texture2D spark4;
        public static Texture2D twirl2;
        public static Texture2D smoke1;
        public static Texture2D star7;

        public static Texture2D circle2;
        public static Texture2D circle5;
        public static Texture2D light3;

        public static Texture2D scorch3;
        public static Texture2D scorch2;
        public static Texture2D flame4;

        public static Texture2D space;
        public static Texture2D metal;

        public static Texture2D whiteSquare;


        public static Texture2D FilterGrayscale(Texture2D tex)
        {
            //initialize a texture
            Texture2D texture = tex;

            //the array holds the color for each pixel in the texture
            //Color[] data = new Color[tex.Width * tex.Height];
            Color[] src = new Color[tex.Width * tex.Height];
            tex.GetData(src);

            for (int pixel = 0; pixel < src.Length; pixel++)
                src[pixel].A = src[pixel].R;

            //set the color
            texture.SetData(src);

            return texture;
        }

    }
}
