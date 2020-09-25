using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using XperiCode.JpegMetadata;

namespace GetRandomImage
{
    public class RandomImageList
    {

        // Global Variables
        public string folderPath;
        
        private Random rng;                             // Random number generator used to shuffle and get random images from a list.
        private DirectoryInfo di;                       // Main directory loaded
        private List<FileInfo> fi;                      // Master file list
        
        public List<string> ext;                        // Extensions to search
        public List<string> tagWhitelist;
        public List<string> tagBlacklist;
        public Boolean useWhitelist, useBlacklist;
        public string fileNameFilter;
        public int pos;                                 // Index for image list
        public SearchOption so;                         // Search option (to search sub directories or not)

        /// <summary>
        /// Initialize the random image list to the given directory p.
        /// searchAll determines if all directories within the given directory will also be loaded.
        /// searchAll is required due to possible issues of hidden lower directories not having permission and me not wanting to deal with it (TODO).
        /// </summary>
        /// <param name="p"></param>
        /// <param name="searchAll"></param>
        public RandomImageList(string p, Boolean searchAll)
        {
            // Init Random
            rng = new Random();

            folderPath = p;

            // Create the list of extensions
            ext = new List<string> { "*.jpg", "*.jpeg", "*.gif", "*.png", "*.bmp" };
            tagWhitelist = new List<string> { "Figure" };
            tagBlacklist = new List<string> { "Explicit" };

            // Set Search Option to search all directories
            if (searchAll)
                so = SearchOption.AllDirectories;
            else
                so = SearchOption.TopDirectoryOnly;

            fi = setFileInfo(folderPath);
        }

        public int Count { get { return fi.Count; } }

        /// <summary>
        /// Re-randomize the list. 
        /// Fully reloads the directory so new search options, files and directories will be processed.
        /// </summary>
        public void Reload()
        {
            fi = setFileInfo(folderPath);
        }

        /// <summary>
        /// Sets a new directory to path p then performs a reload.
        /// </summary>
        /// <param name="p"></param>
        public void Reload(string p)
        {
            folderPath = p;
            Reload();
        }

        public void Reload(List<FileInfo> fileList)
        {
            folderPath = null;
            di = null;
            fi = setFileInfo(fileList);
        }

        /// <summary>
        /// Shuffle the image list and reset the position.
        /// </summary>
        public void Randomize()
        {
            MyExtensions.Shuffle(fi, rng);
            pos = -1;
        }

        /// <summary>
        /// Get a random image from the list.
        /// This can be used to grab a random image without randomizing the list.
        /// </summary>
        /// <returns></returns>
        public FileInfo getRandom()
        {
            if (fi.Count > 0)
                return fi[rng.Next(0, fi.Count)];
            else
                return null;
        }

        /// <summary>
        /// Increments the position counter by one (looping) and returns the image at that position.
        /// </summary>
        /// <returns></returns>
        public FileInfo getNext()
        {
            if (fi?.Count > 0)
            {
                if (pos == fi.Count - 1)
                    pos = -1;
                return fi[++pos];
            }

            return null;
        }

        /// <summary>
        /// Decements the position counter by one (looping) and returns the image at that position.
        /// </summary>
        /// <returns></returns>
        public FileInfo getPrevious()
        {
            if (fi?.Count > 0)
            {
                if (pos == 0)
                    pos = fi.Count;
                return fi[--pos];
            }

            return null;
        }

        /// <summary>
        /// Returns the next x images from the randomized list (looping).
        /// x cannot be greater than the number of existing images in the list.
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public List<FileInfo> getNextX(int x)
        {
            if (x >= fi.Count) x = fi.Count - 1;
            List<FileInfo> ret = new List<FileInfo>();
            for (int i = 0; i < x; i++)
                ret.Add(getNext());
            return ret;
        }

        /// <summary>
        /// Returns x random images from the list (without randomizing it), no repeats.
        /// x cannot be greater than the number of existing images in the list.
        /// (TODO cleanup, probably ineffecient and verbose)
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public List<FileInfo> getRandomX(int x)
        {
            if (x >= fi.Count) x = fi.Count - 1;
            List<FileInfo> ret = new List<FileInfo>();
            FileInfo[] fi_ = new FileInfo[fi.Count];
            fi.CopyTo(fi_);
            MyExtensions.Shuffle(fi_, rng);
            ret = ret.ToList();
            if (x < fi.Count) ret.RemoveRange(x, fi.Count - 1);
            return ret;
        }

        /// <summary>
        /// Gets all files in a directory with the given extensions
        /// </summary>
        private List<FileInfo> setFileInfo(string folderpath)
        {
            List<FileInfo> fi = new List<FileInfo>();
            if (folderpath != "" && Directory.Exists(folderpath))
            {
                di = new DirectoryInfo(folderpath);
                fi = new List<FileInfo>();

                //if (useWhitelist || useBlacklist)
                //{
                //    foreach (var f in ext.Where(t => t == "*.jpg" || t == "*.jpeg").SelectMany(e => (di.EnumerateFiles(e, so))))
                //    {
                //        try
                //        {
                //            var v = new JpegMetadataAdapter(f.FullName).Metadata;
                //            if (!((!tagWhitelist.Any(t => v.Keywords.Contains(t)) && useWhitelist) || (tagBlacklist.Any(t => v.Keywords.Contains(t)) && useBlacklist)))
                //            //if (tagWhitelist.Any(t => v.Keywords.Contains(t)) && !tagBlacklist.Any(t => v.Keywords.Contains(t)))
                //                fi.Add(f);
                //        }
                //        catch { }
                //    }
                //    fi.AddRange(ext.Where(t => t != "*.jpg" && t != "*.jpeg").SelectMany(e => (di.EnumerateFiles(e, so))));
                //}
                //else
                //{
                    fi.AddRange(ext.SelectMany(e => (di.EnumerateFiles(e, so))));
                //}

                MyExtensions.Shuffle(fi, rng);
                pos = -1;
            }
            else
            {
                di = null;
                fi = null;
            }

            return fi;
        }

        private List<FileInfo> setFileInfo(List<FileInfo> fi)
        {
            MyExtensions.Shuffle(fi, rng);
            pos = -1;

            return fi;
        }
    }

    /// <summary>
    /// Helper extension for shuffling a list.
    /// (I believe I got this code on Stack Overflow)
    /// </summary>
    public static class MyExtensions
    {
        public static void Shuffle<T>(this IList<T> list, Random rnd)
        {
            for (var i = 0; i < list.Count; i++)
                Swap(list, i, rnd.Next(i, list.Count));
        }

        public static void Swap<T>(this IList<T> list, int i, int j)
        {
            var temp = list[i];
            list[i] = list[j];
            list[j] = temp;
        }
    }
}
