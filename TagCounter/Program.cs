using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading;

namespace TagCounter
{
    class Program
    {
        private static readonly HttpClient client = new HttpClient();

        private static readonly int positiveTagScore = 1;
        private static readonly int negativeTagScore = 1;
        private static readonly string basePath = @"G:\2.Anime\1.Grabber\images\Gelbooru\";
        private static readonly List<string> extensions = new List<string> { "*.jpg", "*.jpeg", "*.png", "*.bmp", "*.gif" };

        private class TagMetadata
        {
            public string tag { get; set; }
            public int total { get; set; }
            public int totalDownloaded { get; set; }
        }

        private class PostMetadata
        {
            public string hash { get; set; }
            public double downloadChance { get; set; }
            public bool downloaded { get; set; }
        }

        static void Main(string[] args)
        {
            var downloadedPosts = JsonSerializer.Deserialize<List<Post>>(File.ReadAllText($"{basePath}downloadedPosts.txt"));
            var rejectedPosts = JsonSerializer.Deserialize<List<Post>>(File.ReadAllText($"{basePath}rejectedPosts.txt"));

            File.WriteAllLines($"{basePath}mltest.txt", downloadedPosts.Select(post => $"{post.hash},{post.tags},true").Concat(rejectedPosts.Select(post => $"{post.hash},{post.tags},false")));
            //try
            //{
            //    System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

            //    var updatePosts = false;

            //    var endingId = "5295808";
            //    var startingId = "5303688";

            //    //List<Post> posts = FetchPostsByUrl();

            //    //List<Post> downloadedPosts = new List<Post>(), rejectedPosts = new List<Post>();
            //    //SplitDownloadedAndRejectedPosts(posts, out downloadedPosts, out rejectedPosts);


            //    //CountTags(downloadedPosts, rejectedPosts);

            //    var downloadedMd5s = FetchDownloadedMd5s();

            //    var downloadedPosts = JsonSerializer.Deserialize<List<Post>>(File.ReadAllText($"{basePath}downloadedPosts.txt"));
            //    var rejectedPosts = JsonSerializer.Deserialize<List<Post>>(File.ReadAllText($"{basePath}rejectedPosts.txt"));
            //    if (updatePosts)
            //    {
            //        var posts = UpdatePostsByIdRange(endingId, startingId);

            //        SplitDownloadedAndRejectedPosts(posts, out downloadedPosts, out rejectedPosts);
            //    }

            //    var tagMetadata = CalculateTagMetadata(downloadedPosts, rejectedPosts, $"{basePath}tagMetadata.txt");

            //    var testPosts = FetchPostsByIdRange(endingId, startingId);
            //    CalculateDownloadChances(tagMetadata, testPosts, downloadedMd5s);
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine($"Exception: {ex}");
            //}
        }

        private static List<string> FetchDownloadedMd5s()
        {
            var baseDirectory = new DirectoryInfo(basePath);

            var files = extensions.AsParallel().SelectMany(extension => baseDirectory.EnumerateFiles(extension, SearchOption.AllDirectories)).ToList();

            var downloadedMd5s = files.Select(file => file.Name.Replace(file.Extension, "")).ToList();

            return downloadedMd5s;
        }

        private static Dictionary<string, TagMetadata> CalculateTagMetadata(List<Post> downloadedPosts, List<Post> rejectedPosts, string tagMetadataPath = null)
        {
            var tagMetadata = new Dictionary<string, TagMetadata>();
            foreach (var post in downloadedPosts)
            {
                var tags = post.tags.Split(" ");

                foreach (var tag in tags)
                {
                    if (tagMetadata.ContainsKey(tag))
                    {
                        tagMetadata[tag].total++;
                        tagMetadata[tag].totalDownloaded++;
                    }
                    else
                    {
                        tagMetadata[tag] = new TagMetadata { tag = tag, total = 1, totalDownloaded = 1 };
                    }
                }
            }

            foreach (var post in rejectedPosts)
            {
                var tags = post.tags.Split(" ");

                foreach (var tag in tags)
                {
                    if (tagMetadata.ContainsKey(tag))
                    {
                        tagMetadata[tag].total++;
                    }
                    else
                    {
                        tagMetadata[tag] = new TagMetadata { tag = tag, total = 1, totalDownloaded = 0 };
                    }
                }
            }

            if (tagMetadataPath != null)
            {
                File.WriteAllLines(tagMetadataPath, tagMetadata.Keys.Select(key => $"{key},{tagMetadata[key].totalDownloaded},{tagMetadata[key].total},{Math.Round(((double)tagMetadata[key].totalDownloaded / tagMetadata[key].total) * 100, 2)}%"));
            }
            Console.WriteLine("Done scoring tags.");
            return tagMetadata;
        }

        private static void CalculateDownloadChances(Dictionary<string, TagMetadata> tagMetadata, List<Post> testPosts, List<string> md5s = null)
        {
            md5s = md5s ?? File.ReadAllLines($"{basePath}md5.txt").ToList();
            //var testPosts = JsonSerializer.Deserialize<List<Post>>(File.ReadAllText($"{basePath}testData_5293828-5294043.txt"));
            var downloadChances = new Dictionary<string, PostMetadata>();
            foreach (var testPost in testPosts)
            {
                downloadChances[testPost.hash] = new PostMetadata
                {
                    hash = testPost.hash,
                    downloadChance = CalculateDownloadChance(tagMetadata, testPost),
                    downloaded = md5s.Contains(testPost.hash)
                };
            }

            File.WriteAllLines($"{basePath}testOutput.txt", downloadChances.Keys.Select(key => $"{key},{downloadChances[key].downloadChance}%,{downloadChances[key].downloaded}"));
            Console.WriteLine("Done calculating test data.");
        }

        private static void UpdateDownloadedAndRejectedPosts()
        {

        }

        private static double CalculateDownloadChance(Dictionary<string, TagMetadata> tagMetadata, Post post)
        {
            double totalTags = 0;
            double percentageTotal = 0;

            foreach (var tag in post.tags.Split(" "))
            {
                if (tagMetadata.ContainsKey(tag))
                {
                    totalTags++;
                    percentageTotal += Math.Round(((double)tagMetadata[tag].totalDownloaded / tagMetadata[tag].total) * 100, 2);
                }
            }

            return Math.Round(percentageTotal / totalTags, 2);
        }

        private static void CountTags(List<Post> downloadedPosts, List<Post> rejectedPosts)
        {
            var tagsScores = new Dictionary<string, int>();
            foreach (var post in downloadedPosts)
            {
                var tags = post.tags.Split(" ");

                foreach (var tag in tags)
                {
                    if (tagsScores.ContainsKey(tag))
                    {
                        tagsScores[tag] += positiveTagScore;
                    }
                    else
                    {
                        tagsScores[tag] = positiveTagScore;
                    }
                }
            }

            foreach (var post in rejectedPosts)
            {
                var tags = post.tags.Split(" ");

                foreach (var tag in tags)
                {
                    if (tagsScores.ContainsKey(tag))
                    {
                        tagsScores[tag] -= negativeTagScore;
                    }
                    else
                    {
                        tagsScores[tag] = -negativeTagScore;
                    }
                }
            }

            File.WriteAllLines($"{basePath}tagScores.txt", tagsScores.Keys.Select(key => $"{key}: {tagsScores[key]}"));
            Console.WriteLine("Done scoring tags.");
        }

        private static void SplitDownloadedAndRejectedPosts(
            List<Post> posts, 
            out List<Post> downloadedPosts, 
            out List<Post> rejectedPosts, 
            string downloadedPostsPath = null, 
            string rejectedPostsPath = null)
        {
            downloadedPostsPath = downloadedPostsPath ?? $"{basePath}downloadedPosts.txt";
            rejectedPostsPath = rejectedPostsPath ?? $"{basePath}rejectedPosts.txt";
            var downloadedPostsPathBackup = $"{downloadedPostsPath}.backup";
            var rejectedPostsPathBackup = $"{rejectedPostsPath}.backup";

            var previousDownloadedPosts = new List<Post>();
            if (File.Exists(downloadedPostsPath))
            {
                previousDownloadedPosts.AddRange(JsonSerializer.Deserialize<List<Post>>(File.ReadAllText(downloadedPostsPath)));
            }

            var previousRejectedPosts = new List<Post>();
            if (File.Exists(downloadedPostsPath))
            {
                previousRejectedPosts.AddRange(JsonSerializer.Deserialize<List<Post>>(File.ReadAllText(rejectedPostsPath)));
            }

            downloadedPosts = new List<Post>();
            rejectedPosts = new List<Post>();

            try
            {
                var downloadedMd5s = File.ReadAllLines($"{basePath}md5.txt").ToList();

                foreach (var post in posts)
                {
                    if (downloadedMd5s.Contains(post.hash))
                    {
                        downloadedPosts.Add(post);
                    }
                    else
                    {
                        rejectedPosts.Add(post);
                    }
                }

                downloadedPosts.AddRange(previousDownloadedPosts);
                rejectedPosts.AddRange(previousRejectedPosts);

                if (File.Exists(downloadedPostsPath))
                {
                    File.Copy(downloadedPostsPath, downloadedPostsPathBackup);
                }

                if (File.Exists(downloadedPostsPath))
                {
                    File.Copy(rejectedPostsPath, rejectedPostsPathBackup);
                }
                File.WriteAllLines(downloadedPostsPath, new string[] { JsonSerializer.Serialize<List<Post>>(downloadedPosts) });
                File.WriteAllLines(rejectedPostsPath, new string[] { JsonSerializer.Serialize<List<Post>>(rejectedPosts) });
                Console.WriteLine("Done sorting posts.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception in {nameof(SplitDownloadedAndRejectedPosts)}: {ex}");
            }
            finally
            {
                if (File.Exists(downloadedPostsPathBackup))
                {
                    File.Delete(downloadedPostsPathBackup);
                }

                if (File.Exists(rejectedPostsPathBackup))
                {
                    File.Delete(rejectedPostsPathBackup);
                }
            }
        }

        private static List<Post> FetchPostsByIdRange(string endingId, string startingId)
        {
            int i = 0;
            var endReached = false;
            var posts = new List<Post>();
            while (!endReached)
            {
                Console.WriteLine($"Fetching posts for page {i}.");
                var requestUrl = $"https://gelbooru.com/index.php?page=dapi&s=post&q=index&limit=100&pid={i}&json=1&tags=id%3A%3E{endingId}"
                    + (startingId == null ? "" : $"%20id%3A%3C%3D{startingId}");

                try
                {
                    var stringTask = client.GetStringAsync(requestUrl).Result;
                    var newPosts = JsonSerializer.Deserialize<List<Post>>(stringTask);
                    if (newPosts.Count() <= 0) { endReached = true; }
                    posts.AddRange(newPosts);
                    Thread.Sleep(10);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Exception in {nameof(FetchPostsByIdRange)} on index {i}: {ex}");
                }

                i++;
            }

            return posts;
        }

        private static List<Post> UpdatePostsByIdRange(string endingId, string startingId = null, string postsPath = null)
        {
            postsPath = postsPath ?? $"{basePath}allPosts.txt";
            var postsPathBackup = $"{postsPath}.backup";

            var previousPosts = new List<Post>();
            if (File.Exists(postsPath))
            {
                previousPosts.AddRange(JsonSerializer.Deserialize<List<Post>>(File.ReadAllText(postsPath)));
            }

            try
            {
                var posts = FetchPostsByIdRange(endingId, startingId);

                posts.AddRange(previousPosts);

                posts = RemoveDuplicatePosts(posts);

                if (File.Exists(postsPath))
                {
                    File.Copy(postsPath, postsPathBackup);
                }

                File.WriteAllLines(postsPath, new string[] { JsonSerializer.Serialize<List<Post>>(posts) });
                Console.WriteLine("Done fetching posts.");
                return posts;
            }
            catch (Exception ex)
            {
                if (File.Exists(postsPathBackup))
                {
                    File.Copy(postsPathBackup, postsPath);
                }
                Console.WriteLine($"Exception in {nameof(UpdatePostsByIdRange)}: {ex}");
            }
            finally
            {
                if (File.Exists(postsPathBackup))
                {
                    File.Delete(postsPathBackup);
                }
            }

            return null;
        }

        private static List<Post> RemoveDuplicatePosts(List<Post> posts)
        {
            posts = posts.GroupBy(post => post.hash).Select(postGroup => postGroup.First()).ToList();
            return posts;
        }

        private static void CountTagsFromMd5s()
        {
            var md5s = File.ReadAllLines($"{basePath}md5.txt");

            var posts = new List<Post>();
            var tagsCount = new Dictionary<string, int>();
            var index = 0;
            foreach (var md5 in md5s)
            {
                index++;
                Console.WriteLine($"Processing {index}/{md5s.Count()}. md5: {md5}.");
                var requestUrl = $"https://gelbooru.com/index.php?page=dapi&s=post&q=index&tags=md5:{md5}&json=1";

                try
                {
                    var stringTask = client.GetStringAsync(requestUrl).Result;
                    //Console.WriteLine(stringTask);
                    stringTask = stringTask.Replace("[", "").Replace("]", "");
                    var post = JsonSerializer.Deserialize<Post>(stringTask);
                    posts.Add(post);

                    CountTags(tagsCount, post);
                    File.WriteAllLines($"{basePath}tags.txt", tagsCount.Keys.Select(key => $"{key}: {tagsCount[key]}"));
                    File.WriteAllLines($"{basePath}posts.txt", new string[] { JsonSerializer.Serialize<List<Post>>(posts) });

                    Thread.Sleep(100);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"md5: {md5} failed. Request Url: {requestUrl}. Exception: {ex}");
                }
            }

            Console.WriteLine("Done.");
        }

        private static void CountTags(Dictionary<string, int> tagsCount, Post post)
        {
            var tags = post.tags.Split(" ");

            foreach (var tag in tags)
            {
                if (tagsCount.ContainsKey(tag))
                {
                    tagsCount[tag]++;
                }
                else
                {
                    tagsCount[tag] = 1;
                }
            }
        }
    }
}
