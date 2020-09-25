using IWshRuntimeLibrary;
using System;
using System.IO;
using System.Linq;

namespace CopyRandomFile
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            WshShell shell = new WshShell();
            var wsh = new IWshShell_Class();

            int count = 1;
            var sourcePath = "G:\\2.Anime\\pixivutil";
            var targetPath = "C:\\Users\\mouri\\Downloads\\test\\___";
            Random rnd = new Random();

            var parentDirectory = new DirectoryInfo(sourcePath);
            foreach (var directory in parentDirectory.EnumerateDirectories())
            {
                if (directory.FullName == targetPath) continue;
                FileInfo[] randomFiles = directory
                    .GetFiles("*.*", SearchOption.AllDirectories)
                    .Where(file => !file.Name.Contains("folder"))
                    .OrderByDescending(x => x.Name)
                    .Take(20)
                    .OrderBy(x => rnd.Next())
                    .Take(count)
                    .ToArray();

                foreach (FileInfo file in randomFiles)
                {
                    string targetFile = Path.Combine(targetPath, file.Name);
                    //Console.WriteLine("copy " + file.FullName + " -> " + targetFile);
                    //file.CopyTo(targetFile);
                    //IWshRuntimeLibrary.IWshShortcut shortcut = wsh.CreateShortcut(targetFile + ".lnk") as IWshRuntimeLibrary.IWshShortcut;
                    //string shortcutAddress = targetFile + @".lnk";
                    //IWshShortcut shortcut = (IWshShortcut)shell.CreateShortcut(shortcutAddress);
                    //shortcut.TargetPath = file.FullName;
                    //shortcut.Save();

                    var shortcutName = file.Name + ".lnk";
                    // Create empty .lnk file
                    string path = System.IO.Path.Combine(targetPath, shortcutName);
                    System.IO.File.WriteAllBytes(path, new byte[0]);
                    // Create a ShellLinkObject that references the .lnk file
                    Shell32.Shell shl = new Shell32.Shell();
                    Shell32.Folder dir = shl.NameSpace(targetPath);
                    Shell32.FolderItem itm = dir.Items().Item(shortcutName);
                    Shell32.ShellLinkObject lnk = (Shell32.ShellLinkObject)itm.GetLink;
                    // Set the .lnk file properties
                    lnk.Path = file.FullName;
                    lnk.Save(path);
                }
            }
        }
    }
}
