namespace AdventOfCode
{
    public class _2022Day07 : _2022Day
    {
        private readonly FilePath FileSystem;
        private readonly List<int> FolderSizes;

        public _2022Day07() : base("Day07")
        {
            FileSystem = ExtractFileSystem();
            FolderSizes = GetFoldersList(FileSystem);
        }

        public override void Part1()
        {
            base.Part1();

            int totalFolderSize = FolderSizes
                .Where(f => f <= 100000)
                .Sum()
            ;

            Console.WriteLine($"Total size of the small directories: {totalFolderSize}\n");
        }

        public override void Part2()
        {
            base.Part2();

            int spaceToFree = (FileSystem.Size ?? 0) - 40000000;

            int smallestDirectoryToDelete = Convert.ToInt32(FolderSizes.Where(f => f >= spaceToFree).Min());

            Console.WriteLine($"Smallest directory to delete: {smallestDirectoryToDelete}\n");
        }

        #region Private methods
        private FilePath ExtractFileSystem()
        {
            FilePath system = new("/", true);
            FilePath? currentLocation = system;
            int currentLine = 1;

            while (currentLine < Input.Length)
            {
                string[] output = Input[currentLine].Split(' ');

                switch (output[0])
                {
                    case "$": // command
                        if (output[1] == "cd") // the other command is ls, which does nothing to the current folder
                        {
                            if (output[2] != "..")
                            {
                                currentLocation = currentLocation?.Files.Find(f => f.Name == output[2]) ?? currentLocation;
                            }
                            else
                            {
                                currentLocation = currentLocation?.Parent ?? currentLocation;
                            }
                        }
                        break;

                    case "dir": // directory
                        FilePath folder = new(output[1], true);
                        currentLocation?.AddFile(folder);
                        break;

                    default: // file
                        FilePath file = new(output[1], false)
                        {
                            Size = Convert.ToInt32(output[0])
                        };
                        currentLocation?.AddFile(file);
                        break;
                }

                currentLine++;
            }

            return system;
        }

        private static List<int> GetFoldersList(FilePath path)
        {
            List<int> list = new();

            if (!path.IsFolder)
            {
                return list;
            }

            list.Add(path.Size ?? 0);

            foreach (FilePath file in path.Files)
            {
                list = list.Concat(GetFoldersList(file)).ToList();
            }

            return list;
        }
        #endregion
    }
}