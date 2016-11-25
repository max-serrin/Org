using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Rating
{
    public partial class Form1 : Form
    {
        // Declare variables
        string root = "C:\\"; // Where to work from
        List<FileInfo> fi; // Store files in directory
        List<FileInfo> rated; // Organize them by index = rating

        // RNG
        Random rng;

        // Temps to store across functions
        FileInfo oldfi;
        int oldindex;

        // Index positions for the binary search
        int index, imin, imax;

        public Form1()
        {
            InitializeComponent();

            // Set default path
            folderBrowserDialog1.SelectedPath = root;

            // Initialize RNG
            rng = new Random();
        }

        private void openDirectoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Open folder dialog
            if (folderBrowserDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                // Store the folder as the new root
                root = folderBrowserDialog1.SelectedPath;

                // Get all the image files in the folder (not recursive)
                DirectoryInfo di = new DirectoryInfo(root);
                fi = di.EnumerateFiles("*.jpg").ToList();
                fi.AddRange(di.EnumerateFiles("*.jpeg").ToList());
                fi.AddRange(di.EnumerateFiles("*.png").ToList());
                fi.AddRange(di.EnumerateFiles("*.bmp").ToList());
                fi.AddRange(di.EnumerateFiles("*.gif").ToList());

                // Initialize a new rated list to store into
                rated = new List<FileInfo>();

                // Check to see if there is any "save data"
                DirectoryInfo dir = new DirectoryInfo(root + "\\rated");
                if (dir.Exists && dir.EnumerateFiles().Count() > 0)
                {
                    // Load any files in the save folder
                    rated = dir.EnumerateFiles("*.*").ToList();

                    // Move them to a temp folder (to prevent overriding issues)
                    for (int i = 0; i < rated.Count; i++)
                    {
                        if (!Directory.Exists(root + "\\temp"))
                        {
                            Directory.CreateDirectory(root + "\\temp");
                        }

                        File.Move(rated[i].FullName, root + "\\temp\\" + rated[i].Name);
                    }

                    // Re-grab the files (there's probably a better way to do this... TODO)
                    dir = new DirectoryInfo(root + "\\temp");
                    List<FileInfo> templist = dir.EnumerateFiles("*.*").ToList();

                    // Clear the list
                    rated.Clear();

                    // Organize the rated list by checking file name and adding it only when its name matches the index
                    for (int i = 0; rated.Count < templist.Count || i > templist.Count * 2; i++)
                    {
                        foreach (FileInfo f in templist)
                        {
                            if (f.Name.Remove(f.Name.IndexOf("_")) == i.ToString())
                            {
                                rated.Add(f);
                                break;
                            }
                        }
                    }
                }
                else
                {
                    // Grab a random image and store it in our temp variable
                    int temp = rng.Next(0, fi.Count);
                    FileInfo tempfi = fi[temp];

                    // Remove the image and place it in the rated list for our first image
                    fi.RemoveAt(temp);
                    rated.Add(tempfi);
                }

                // Proceed to process images and move them accordingly
                NextBatch();
            }
        }

        private void NextBatch()
        {
            // While there's work to do...
            if (fi.Count > 0)
            {
                // Grab a random image and store it in a temporary variable
                oldindex = rng.Next(0, fi.Count);
                oldfi = fi[oldindex];

                // Display the image
                pictureBox1.ImageLocation = oldfi.FullName;

                // Get the middle image in the rated list and display it
                index = (int)Math.Floor((double)(rated.Count / 2));
                pictureBox2.ImageLocation = rated[index].FullName;

                // Set min and max indexes
                imin = 0;
                imax = rated.Count - 1;
            }
            else
            {
                // Blankout the picture boxes
                pictureBox1.ImageLocation = "";
                pictureBox2.ImageLocation = "";

                // Move the images into the /rated/ folder and name them based on their rating 
                for (int i = 0; i < rated.Count; i++)
                {
                    if (!Directory.Exists(root + "\\rated"))
                    {
                        Directory.CreateDirectory(root + "\\rated");
                    }

                    File.Move(rated[i].FullName, root + "\\rated\\" + i.ToString() + "_" + rated[i].Extension);
                }

                // Delete the temp folder (if it exists)
                DirectoryInfo dir = new DirectoryInfo(root + "\\temp");
                if (dir.Exists)
                {
                    Directory.Delete(root + "\\temp", true);
                }

                // Close the program to prevent any problems... (TODO fix so this isn't required)
                this.Close();
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            // Essentially returning a "greater than" in a binary search
            // Increase min index to exclude everything up to and including the last shown image on the left side
            imin = index + 1;
            
            // Check to see if we've gone over
            if (imin > imax)
            {
                // Move the image into the appropriate place in the rated array and start a new batch
                AddAtIndex(imin);
                NextBatch();
            }
            else
            {
                // Get a new index halfway between imin and imax and display the image
                index = imin + (int)Math.Floor((double)((imax - imin) / 2.0));
                pictureBox2.ImageLocation = rated[index].FullName;
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            // Essentially returning a "less than" in a binary search
            // Decrease max index to exclude everything up to and including the last shown image on the right side
            imax = index - 1;

            // Check to see if we've gone over
            if (imax < imin)
            {
                // Move the image into the appropriate place in the rated array and start a new batch
                AddAtIndex(imin);
                NextBatch();
            }
            else
            {
                // Get a new index halfway between imin and imax and display the image
                index = imin + (int)Math.Floor((double)((imax - imin) / 2.0));
                pictureBox2.ImageLocation = rated[index].FullName;
            }
        }

        private void AddAtIndex(int index)
        {
            // Remove the old image and place insert it into the appropriate place in the rated list
            fi.RemoveAt(oldindex);
            rated.Insert(index, oldfi);
        }

        private void saveProgressToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Blankout the picture boxes
            pictureBox1.ImageLocation = "";
            pictureBox2.ImageLocation = "";

            // Move the images into the /rated_save/ folder (for later processing, the program will later look for
            // and grab any files in this folder and add them automatically to the rated list) and name them based on their rating
            for (int i = 0; i < rated.Count; i++)
            {
                if (!Directory.Exists(root + "\\rated"))
                {
                    Directory.CreateDirectory(root + "\\rated");
                }

                File.Move(rated[i].FullName, root + "\\rated\\" + i.ToString() + "_" + rated[i].Extension);
            }

            // Delete the temp folder (if it exists)
            DirectoryInfo dir = new DirectoryInfo(root + "\\temp");
            if (dir.Exists)
            {
                Directory.Delete(root + "\\temp", true);
            }

            // Quit (until I find a better way)
            this.Close();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Move any files still in the temp folder back to the rated_save folder
            DirectoryInfo dir = new DirectoryInfo(root + "\\temp");
            if (dir.Exists && dir.EnumerateFiles().Count() > 0)
            {
                // Load any files in the temp folder
                rated = dir.EnumerateFiles("*.*").ToList();

                // Move them back to the save folder folder (so the program can grab them next time it's opened)
                for (int i = 0; i < rated.Count; i++)
                {
                    if (!Directory.Exists(root + "\\rated"))
                    {
                        Directory.CreateDirectory(root + "\\rated");
                    }

                    File.Move(rated[i].FullName, root + "\\rated\\" + rated[i].Name);
                }

                // Delete the temp folder
                Directory.Delete(root + "\\temp", true);
            }
        }

        private void skipToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NextBatch();
        }

        private void removeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists(root + "\\remove"))
            {
                Directory.CreateDirectory(root + "\\remove");
            }

            // Remove the file from the list and move it into the remove folder
            fi.RemoveAt(oldindex);
            File.Move(oldfi.FullName, root + "\\remove\\" + oldfi.Name);

            NextBatch();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Remove the file from the list and delete it
            fi.RemoveAt(oldindex);
            File.Delete(oldfi.FullName);

            NextBatch();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
