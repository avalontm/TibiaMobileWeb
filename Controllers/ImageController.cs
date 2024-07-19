using Microsoft.AspNetCore.Mvc;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using System.IO;
using TMFormat.Formats;

namespace TibiaMobileWeb.Controllers
{
    public class ImageController : Controller
    {
        public ActionResult Outfit(int look, int head, int body, int legs, int feet)
        {
            byte[] bytes;
            string root = Path.GetFullPath("wwwroot");
            string data = Path.Combine(root, "creatures", $"chr_{look}.tmc");

            TMCreature model = TMCreature.Load(data);

            if (model == null)
            {
                return new NotFoundResult();
            }

            int frames = model.dirs[2].sprites[0].textures.Count;

            byte[] _texture = model.dirs[2].sprites[0].textures[0];
            byte[] _mask = model.dirs[2].sprites[0].masks[0];

            Color colorHead = new Color(new Rgba32(255, 255, 0));
            Color colorBody = new Color(new Rgba32(255, 0, 0));
            Color colorLegs = new Color(new Rgba32(0, 255, 0));
            Color colorFeets = new Color(new Rgba32(0, 0, 255));

            using (Image<Rgba32> texture = Image.Load<Rgba32>(_texture)) // load up source images
            {
                using (Image<Rgba32> mask = Image.Load<Rgba32>(_mask))
                {
                    for (int x = 0; x < texture.Width; x++)
                    {
                        for (int y = 0; y < texture.Height; y++)
                        {
                            if (mask[x, y].A > 0) //Cambiamos el color del pixel
                            {
                                if (mask[x, y].ToHex() == colorHead.ToHex())
                                {
                                    mask[x, y] = Colors.colors[head].color;
                                }
                                else if (mask[x, y].ToHex() == colorBody.ToHex())
                                {
                                    mask[x, y] = Colors.colors[body].color;
                                }
                                else if (mask[x, y].ToHex() == colorLegs.ToHex())
                                {
                                    mask[x, y] = Colors.colors[legs].color;
                                }
                                else if (mask[x, y].ToHex() == colorFeets.ToHex())
                                {
                                    mask[x, y] = Colors.colors[feet].color;
                                }

                                byte red = (byte)((texture[x, y].R * mask[x, y].R) / 255);
                                byte green = (byte)((texture[x, y].G * mask[x, y].G) / 255);
                                byte blue = (byte)((texture[x, y].B * mask[x, y].B) / 255);

                                Rgba32 combine = new Rgba32(red, green, blue);

                                texture[x, y] = combine;
                            }

                        }

                    }

                    using (var memoryStream = new MemoryStream())
                    {
                        texture.SaveAsGif(memoryStream);
                        bytes = memoryStream.ToArray();
                        memoryStream.Flush();
                    }
                }
            }

            return File(bytes, "image/gif");
        }

    }
}
