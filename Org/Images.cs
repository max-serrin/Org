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
        public string currentDirectory { get; set; }
        private DirectoryInfo directoryInfo;
        private List<FileInfo> fileInfos;
        private int fileInfosIndex;

        public List<string> extensions { get; set; }
        public SearchOption searchOption { get; set; }

        public Images(string _currentDirectory = @"C:\", int? _seed = null, string _extensions = "*.jpg, *.jpeg, *.png, *.bmp, *.gif", SearchOption _searchOption = SearchOption.TopDirectoryOnly)
        {
            random = new Random(_seed ?? (int)((DateTime.UtcNow - DateTime.MinValue).TotalMilliseconds % int.MaxValue));
            currentDirectory = _currentDirectory;
            searchOption = _searchOption;
            fileInfos = new List<FileInfo>();
            extensions = Regex.Split(Regex.Replace(_extensions, @"\s+", ""), ",").ToList();
            LoadCurrentDirectory();
        }

        public void LoadCurrentDirectory()
        {
            fileInfos = new List<FileInfo>();
            fileInfosIndex = 0;
            if (currentDirectory != "" && Directory.Exists(currentDirectory))
            {
                directoryInfo = new DirectoryInfo(currentDirectory);
                fileInfos = new List<FileInfo>();
                try
                {
                    foreach (string s in extensions)
                    {
                        fileInfos.AddRange(directoryInfo.EnumerateFiles(s, searchOption).ToList());
                    }
                }
                catch (UnauthorizedAccessException ex)
                {

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
