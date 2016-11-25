using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.IO;

namespace JJDL
{
    public partial class Form1 : Form
    {
        string dir;
        bool exists;

        private BackgroundWorker bw = new BackgroundWorker();

        public Form1()
        {
            InitializeComponent();

            bw.WorkerReportsProgress = true;
            bw.WorkerSupportsCancellation = true;
            bw.DoWork += new DoWorkEventHandler(bw_DoWork);
            bw.ProgressChanged += new ProgressChangedEventHandler(bw_ProgressChanged);
            bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_RunWorkerCompleted);
        }

        private void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;

            string girl = textBox1.Text;
            string base_url = textBox2.Text;
            dir = @"C:\Users\Mark\Pictures\Uplay\" + girl + @"\";

            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            exists = true;
            int dlcount = 0;
            int excount = 0;

            for (int i = 1; exists; i++)
            {
                for (int j = 1; j <= 12 & exists; j++)
                {
                    string f = @"C:\Users\Mark\Pictures\Uplay\" + girl + @"\" + girl + "_" + i + "-" + j + ".jpg";
                    if (!File.Exists(f) && exists)
                    {
                        exists = DownloadRemoteImageFile(base_url + girl + "/" + i + "/" + girl + "-" + j + ".jpg", f);
                        if (exists)
                            worker.ReportProgress(++dlcount);
                    }
                    else
                    {
                        //lscount.Text = (++excount).ToString();
                        ++excount;
                    }

                    
                }
            }

            MessageBox.Show("Done.\nDownloaded " + dlcount + " images.\n" + excount + " images already existed.");
        }

        private void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

        }

        private void bw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this.ldcount.Text = (e.ProgressPercentage.ToString());
        }

        private static bool DownloadRemoteImageFile(string uri, string fileName)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            HttpWebResponse response;
            try
            {
                response = (HttpWebResponse)request.GetResponse();
            }
            catch (Exception)
            {
                return false;
            }

            // Check that the remote file was found. The ContentType
            // check is performed since a request for a non-existent
            // image file might be redirected to a 404-page, which would
            // yield the StatusCode "OK", even though the image was not
            // found.
            if ((response.StatusCode == HttpStatusCode.OK ||
                response.StatusCode == HttpStatusCode.Moved ||
                response.StatusCode == HttpStatusCode.Redirect) &&
                response.ContentType.StartsWith("image", StringComparison.OrdinalIgnoreCase) &&
                !response.ContentType.EndsWith("gif", StringComparison.OrdinalIgnoreCase))
            {
                // if the remote file was found, download it
                using (Stream inputStream = response.GetResponseStream())

                using (Stream outputStream = File.OpenWrite(fileName))
                {
                    byte[] buffer = new byte[4096];
                    int bytesRead;
                    do
                    {
                        bytesRead = inputStream.Read(buffer, 0, buffer.Length);
                        outputStream.Write(buffer, 0, bytesRead);
                    } while (bytesRead != 0);
                }
                return true;
            }
            else
                return false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (bw.IsBusy != true)
                bw.RunWorkerAsync();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (bw.WorkerSupportsCancellation == true)
                bw.CancelAsync();
        }
    }
}
