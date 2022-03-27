using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TibiaMobileWeb
{
    public static class TextureConvert
    {
        public static Texture2D ChangeColor(this Texture2D texture, Texture2D mask, Color headTo, Color bodyTo, Color legsTo, Color feetsTo, bool force = false)
        {
            if (mask == null)
            {
                return texture;
            }

            Color colorHead = new Color(255, 255, 0);
            Color colorBody = new Color(255, 0, 0);
            Color colorLegs = new Color(0, 255, 0);
            Color colorFeets = new Color(0, 0, 255);

            Color[] combineData = new Color[texture.Width * texture.Height];
            texture.GetData(combineData);

            Color[] maskData = new Color[mask.Width * mask.Height];
            mask.GetData(maskData);

            for (int i = 0; i < maskData.Length; i++)
            {
                if (maskData[i] == colorHead)
                {
                    maskData[i] = headTo;
                }
                else if (maskData[i] == colorBody)
                {
                    maskData[i] = bodyTo;
                }
                else if (maskData[i] == colorLegs)
                {
                    maskData[i] = legsTo;
                }
                else if (maskData[i] == colorFeets)
                {
                    maskData[i] = feetsTo;
                }

                if (maskData[i].A > 0) //Cambiamos el color del pixel
                {
                    int red = (combineData[i].R * maskData[i].R) / 255;
                    int green = (combineData[i].G * maskData[i].G) / 255;
                    int blue = (combineData[i].B * maskData[i].B) / 255;

                    combineData[i] = new Color(red, green, blue);
                }
            }

            texture.SetData(combineData);

            return texture;
        }
    }
}


