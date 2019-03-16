using System.IO;

namespace Assets.Scripts.Readers
{
    public static class FileReader
    {
        private static readonly string directory = Directory.GetCurrentDirectory();

        public static string[] ReadFile(string filePath, bool addDirectoryPath)
        {
            var path = filePath;
            var fullPath = addDirectoryPath ? string.Concat(directory, path) : path;
            var fileContent = File.ReadAllLines(fullPath);

            return fileContent;
        }
    }
}
