namespace AdventOfCode
{
    public class FilePath
    {
        public string Name { get; set; }
        public bool IsFolder { get; set; }
        public List<FilePath> Files { get; set; }
        public FilePath? Parent { get; set; }

        private int? _size;
        public int? Size
        {
            get
            {
                if (IsFolder)
                {
                    return Files.Sum(f => f.Size);
                }

                return _size;
            }

            set
            {
                if (!IsFolder) { _size = value; }
            }
        }

        public FilePath(string name, bool isFolder)
        {
            Name = name;
            IsFolder = isFolder;
            Parent = null;

            Files = new List<FilePath>();
        }

        public void AddFile(FilePath path)
        {
            if (IsFolder)
            {
                path.Parent = this;
                Files.Add(path);
            }
        }

        public void Print(string indent = "")
        {
            if (!IsFolder)
            {
                Console.WriteLine($"{indent} - {Name} (file, size={Size})");
            }
            else
            {
                Console.WriteLine($"{indent} - {Name} (dir)");
                foreach (FilePath file in Files)
                {
                    file.Print($"  {indent}");
                }
            }
        }
    }
}