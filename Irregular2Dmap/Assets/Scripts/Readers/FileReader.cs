using System.IO;

namespace Assets.Scripts.Readers
{
    public static class FileReader
    {
        private static readonly string directory = Directory.GetCurrentDirectory();

        public static string[] ReadFile(string filePath)
        {
            var path = filePath;
            var fullPath = string.Concat(directory, path);
            var fileContent = File.ReadAllLines(fullPath);

            return fileContent;
        }
    }
}
