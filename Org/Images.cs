using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Org
{
    class Images : ITraversable<FileInfo>
    {
        private Random random;
        public string CurrentDirectory { get; set; }
        private DirectoryInfo directoryInfo;
        private List<FileInfo> fileInfos;

        public List<string> Extensions { get; set; }
        public SearchOption SearchOption { get; set; }

        public Images(string _currentDirectory = @"C:\", int? _seed = null, string _extensions = "*.jpg, *.jpeg, *.png, *.bmp, *.gif", SearchOption _searchOption = SearchOption.TopDirectoryOnly)
        {
            random = new Random(_seed ?? (int)((DateTime.UtcNow - DateTime.MinValue).TotalMilliseconds % int.MaxValue));
            CurrentDirectory = _currentDirectory;
            SearchOption = _searchOption;
            fileInfos = new List<FileInfo>();
            Extensions = Regex.Split(Regex.Replace(_extensions, @"\s+", ""), ",").ToList();
            LoadCurrentDirectory();
        }

        public void LoadCurrentDirectory()
        {
            fileInfos = new List<FileInfo>();
            if (CurrentDirectory != "" && Directory.Exists(CurrentDirectory))
            {
                directoryInfo = new DirectoryInfo(CurrentDirectory);
                fileInfos = new List<FileInfo>();
                try
                {
                    foreach (string s in Extensions)
                    {
                        fileInfos.AddRange(directoryInfo.EnumerateFiles(s, SearchOption).ToList());
                    }
                }
                catch (UnauthorizedAccessException ex)
                {
                    Log.AddLogMessage(Log.LogLevels.Warning, "Unauthorized folder access. Exception: " + ex.Message);
                }
            }
        }

        public FileInfo GetRandomImage()
        {
            return fileInfos[random.Next(fileInfos.Count)];
        }

        private List<int> GetRandomizedIndexes()
        {
            List<int> indexList = new List<int>();
            for (int index = 0; index < fileInfos.Count; index++)
            {
                indexList.Add(index);
            }
            MyExtensions.Shuffle(indexList, random);
            return indexList;
        }

        public ITraverser<FileInfo> GetTraverser()
        {
            return new Traverser<FileInfo>(fileInfos);
        }

        public ITraverser<FileInfo> GetTraverser(int size)
        {
            return new Traverser<FileInfo>(fileInfos, size);
        }

        public ITraverser<FileInfo> GetRandomizedTraverser()
        {
            return new Traverser<FileInfo>(fileInfos, GetRandomizedIndexes());
        }

        public ITraverser<FileInfo> GetRandomizedTraverser(int size)
        {
            return new Traverser<FileInfo>(fileInfos, GetRandomizedIndexes(), size);
        }
    }
}
