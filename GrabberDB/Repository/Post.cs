using GrabberDB.Models;
using Microsoft.Data.Sqlite;
using System;
using System.Drawing;
using System.IO;

namespace GrabberDB.Repository
{
    class Post
    {
        public string dataSource { get; set; }
        public string workingDirectory { get; set; }

        public Post(string dataSource, string workingDirectory = @"G:\2.Anime\1.Grabber\")
        {
            this.dataSource = dataSource;
            this.workingDirectory = workingDirectory;
        }

        public bool CreatePost(string fileName, BooruFile booruFile, bool cut = false)
        {
            try
            {
                if (File.Exists(fileName))
                {
                    var file = new FileInfo(fileName);
                    CreatePost(file, booruFile, cut);
                }

                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }

        public bool CreatePost(FileInfo file, BooruFile booruFile, bool cut = false)
        {
            try
            {
                if (!file.Exists) { return false; }

                booruFile.site = booruFile.site ?? "local";
                booruFile.ext = file.Extension;
                using (var image = Image.FromFile(file.FullName))
                {
                    booruFile.width = image.Width.ToString();
                    booruFile.height = image.Height.ToString();
                }
                var directory = $"{workingDirectory}{(workingDirectory.EndsWith('\\') ? "" : "\\")}images\\{booruFile.site}\\";
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                var newFileName = $"{directory}{booruFile.md5}{booruFile.ext}";

                if (!File.Exists(newFileName))
                {
                    if (cut)
                    {
                        File.Move(file.FullName, newFileName);
                    }
                    else
                    {
                        File.Copy(file.FullName, newFileName);
                    }

                    try
                    {
                        return AddPost(booruFile);
                    }
                    catch (Exception ex)
                    {
                        if (cut)
                        {
                            File.Move(newFileName, file.FullName);
                        }
                        else
                        {
                            File.Delete(newFileName);
                        }

                        throw ex;
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }

        public bool AddPost(BooruFile booruFile)
        {
            try
            {
                using (var connection = new SqliteConnection($"Data Source={dataSource}"))
                {
                    connection.Open();

                    var addPost = connection.CreateCommand();
                    addPost.CommandText =
                    "INSERT INTO posts values('" + (booruFile.md5 ?? Guid.NewGuid().ToString()) +
                        "', '" + (booruFile.site ?? "local") +
                        "', '" + (booruFile.ext ?? "") +
                        "', '" + (booruFile.artist ?? "anonymous") +
                        "', '" + (booruFile.rating ?? "unkown") +
                        "', " + (booruFile.score ?? (-1).ToString()) +
                        ", " + (booruFile.height ?? (-1).ToString()) +
                        ", " + (booruFile.width ?? (-1).ToString()) +
                        ", '" + (booruFile.date ?? DateTime.Now.ToString()) +
                        "', '" + (booruFile.source ?? "") +
                        "', '" + (booruFile.tags ?? "") +
                        "');";

                    //addPost.Parameters.AddWithValue("$md5", booruFile.md5 ?? Guid.NewGuid().ToString());
                    //addPost.Parameters.AddWithValue("$site", booruFile.site ?? "local");
                    //addPost.Parameters.AddWithValue("$ext", booruFile.ext ?? "");
                    //addPost.Parameters.AddWithValue("$artist", booruFile.artist ?? "anonymous");
                    //addPost.Parameters.AddWithValue("$rating", booruFile.rating ?? "unkown");
                    //addPost.Parameters.AddWithValue("$score", booruFile.score ?? (-1).ToString());
                    //addPost.Parameters.AddWithValue("$height", booruFile.height ?? (-1).ToString());
                    //addPost.Parameters.AddWithValue("$width", booruFile.width ?? (-1).ToString());
                    //addPost.Parameters.AddWithValue("$date", booruFile.date ?? DateTime.Now.ToString());
                    //addPost.Parameters.AddWithValue("$source", booruFile.source ?? "");
                    //addPost.Parameters.AddWithValue("$tags", booruFile.tags ?? "");

                    using (var reader = addPost.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var response = reader.GetString(0);

                            Console.WriteLine($"{response}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw ex;
            }

            return true;
        }
    }
}
