using GrabberDB.Models;
using GrabberDB.Repository;
using System;
using System.Collections.Generic;
using System.IO;

namespace GrabberDB
{
    class Program
    {
        static void Main(string[] args)
        {
            // -i="C:\\Files\\img.jpg"
            // -md5=612807eb63aedbf2488597aa3b59aa2c
            // -site=gelbooru
            // -ext=jpg
            // -artist=anonymous
            // -rating=questionable
            // -score=13
            // -width=100
            // -height=100
            // -date=12/12/12T12:00:00.0000Z
            // -source="http_pixiv.com..."
            // -tags="general:wet;general:bikini"
            // --cut

            try
            {
                var cut = false;
                var fileOnly = false;
                var parameters = new List<string>
                {
                    "i", "md5", "site", "ext", "artist", "rating", "score", "width", "height", "date", "source", "tags", "dir"
                };

                var argumentsDictionary = new Dictionary<string, string>();

                foreach (var arg in args)
                {
                    if (arg == "--cut")
                    {
                        cut = true;
                    }
                    else
                    {
                        foreach (var parameter in parameters)
                        {
                            var key = $"-{parameter}=";
                            if (arg.StartsWith(key))
                            {
                                argumentsDictionary[parameter] = arg.Substring(key.Length);
                            }
                        }
                    }
                }

                var file = new FileInfo(args[0]);
                Post post = null;
                if (file.Extension == ".db")
                {
                    post = new Post(args[0], argumentsDictionary.ContainsKey("dir") ? argumentsDictionary["dir"] : "G:\\2.Anime\\1.Grabber\\");
                }
                else
                {
                    fileOnly = true;
                    post = new Post("G:\\2.Anime\\1.Grabber\\grabber.db", "G:\\2.Anime\\1.Grabber\\");
                }

                if (post == null) return;

                var booruFile = new BooruFile
                {
                    md5 = argumentsDictionary.ContainsKey("md5") ? argumentsDictionary["md5"] : Guid.NewGuid().ToString(),
                    site = argumentsDictionary.ContainsKey("site") ? argumentsDictionary["site"] : "local",
                    ext = argumentsDictionary.ContainsKey("ext") ? argumentsDictionary["ext"] : "",
                    artist = argumentsDictionary.ContainsKey("artist") ? argumentsDictionary["artist"] : "anonymous",
                    rating = argumentsDictionary.ContainsKey("rating") ? argumentsDictionary["rating"] : "unkown",
                    score = argumentsDictionary.ContainsKey("score") ? argumentsDictionary["score"] : "-1",
                    width = argumentsDictionary.ContainsKey("width") ? argumentsDictionary["width"] : "-1",
                    height = argumentsDictionary.ContainsKey("height") ? argumentsDictionary["height"] : "-1",
                    date = argumentsDictionary.ContainsKey("date") ? argumentsDictionary["date"] : DateTime.Now.ToString(),
                    source = argumentsDictionary.ContainsKey("source") ? argumentsDictionary["source"] : "",
                    tags = argumentsDictionary.ContainsKey("tags") ? argumentsDictionary["tags"] : ""
                };

                Console.WriteLine(booruFile.md5);

                if (argumentsDictionary.ContainsKey("i"))
                {
                    post.CreatePost(argumentsDictionary["i"], booruFile, cut);
                }
                else if (fileOnly)
                {
                    post.CreatePost(file, booruFile, true);
                }
                else
                {
                    post.AddPost(booruFile);
                }

                Console.WriteLine("End");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
