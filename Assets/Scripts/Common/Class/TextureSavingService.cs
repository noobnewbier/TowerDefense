using System.IO;
using UnityEngine;

namespace Common.Class
{
    [CreateAssetMenu(menuName = "ScriptableService/TextureSaving")]
    public class TextureSavingService : ScriptableObject
    {
        [SerializeField] private string pathName;

        public void SaveTexture(Texture2D texture, string fileName)
        {
            var path =
                $"{Directory.GetParent(Application.dataPath)}{Path.DirectorySeparatorChar}{pathName}{Path.DirectorySeparatorChar}";
            Directory.CreateDirectory(path);

            File.WriteAllBytes(FindAppropriateFilename(path, fileName), texture.EncodeToPNG());
        }

        private static string FindAppropriateFilename(string path, string filename)
        {
            var count = 1;
            const string extension = ".png";
            var finalPath = $"{path}{filename}{extension}";

            while (File.Exists(finalPath)) finalPath = $"{path}{filename}({count++}){extension}";

            return finalPath;
        }
    }
}