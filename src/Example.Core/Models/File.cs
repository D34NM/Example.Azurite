namespace Example.Core.Models
{
    public sealed class File
    {
        private readonly string _path;

        public File(string filePath)
        {
            _path = filePath;
        }

        public static implicit operator string(File file) => file._path;
    }
}