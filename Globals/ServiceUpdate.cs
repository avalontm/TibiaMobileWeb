using System.IO;
using System.Threading.Tasks;
using System;
using Google.Protobuf.Reflection;
using TibiaMobileWeb.Models;
using System.Collections.Generic;
using System.Text.Json;
using System.Diagnostics;

namespace TibiaMobileWeb
{
    public static class ServiceUpdate
    {
        public static string Content = Path.Combine("wwwroot", "updates", "content");
        public static string Output = Path.Combine("wwwroot", "updates", "update.json");

        static string Version = "1.0.25";
        static string Host = "http://192.168.100.2/update/file?url=";

        public static async Task Generate()
        {
            // Obtener todos los archivos de las subcarpetas
            var files = Directory.GetFiles(Path.Combine(Content), "*.*", SearchOption.AllDirectories);

            var items = new List<FileItemModel>();

            foreach (string file in files)
            {
                string relativePath = file.Substring(Content.Length + 1); // Obtener la ruta relativa
                relativePath = relativePath.Replace("\\", "/");

                string checksum = await GetMD5Checksum(relativePath);

                Debug.WriteLine($"relativePath: {relativePath}");
                items.Add(new FileItemModel
                {
                    crc32 = checksum,
                    file = relativePath
                });
            }

            var fileUpdateModel = new FileUpdateModel
            {
                version = Version,
                host = Host,
                items = items.ToArray()
            };

            // Serializar el objeto a JSON
            var json = JsonSerializer.Serialize(fileUpdateModel, new JsonSerializerOptions { WriteIndented = true });

            // Guardar el JSON en un archivo
            await File.WriteAllTextAsync(Output, json);
        }

        public static async Task<string> GetMD5Checksum(string filename)
        {
            using (var md5 = System.Security.Cryptography.MD5.Create())
            {
                using (var stream = System.IO.File.OpenRead(Path.Combine(Content, filename)))
                {
                    var hash = md5.ComputeHash(stream);
                    return BitConverter.ToString(hash).Replace("-", "");
                }
            }
        }
    }
}
