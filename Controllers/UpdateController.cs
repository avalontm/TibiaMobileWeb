using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using System.Diagnostics;
using System.IO;

namespace TibiaMobileWeb.Controllers
{
    public class UpdateController : Controller
    {
        public ActionResult File(string url)
        {
            string root = Path.GetFullPath("wwwroot");
            // Combina la ruta raíz con la URL para obtener la ruta completa del archivo
            string file = Path.Combine(root, "updates", "content", url.Replace("/", Path.DirectorySeparatorChar.ToString()));

            if (!System.IO.File.Exists(file))
            {
                Debug.WriteLine($"Not exist File: {file}");
                return new NotFoundResult();
            }

            byte[] bytes = System.IO.File.ReadAllBytes(file);

            // Determina el tipo MIME del archivo
            var provider = new FileExtensionContentTypeProvider();
            if (!provider.TryGetContentType(file, out var mimeType))
            {
                mimeType = "application/octet-stream";
            }

            return File(bytes, mimeType, Path.GetFileName(file));
        }
    }
}
