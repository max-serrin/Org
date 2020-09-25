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
using mshtml;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DL
{
    public partial class Form1 : Form
    {
        // Delimiters
        char[] delS = new char[] { '[', ']' };
        char[] delC = new char[] { '{', '}' };

        // Start path
        string path = @"C:\";

        // Image extension
        string ext = "jpg";

        List<string> websiteList;

        Boolean autoDLFlag;

        // Download Counter
        int dlcount = 0, dlcount_CoedCherry = 0, dlcount_Tumblr = 0;
        List<int> dlcounts;

        // Background workers for downloading
        private BackgroundWorker bw = new BackgroundWorker();
        BackgroundWorker worker;

        public Form1()
        {
            InitializeComponent();


            // TAB - Fusker
            if (File.Exists(@"C:\Users\Mark\Desktop\New folder\dl.txt"))
                websiteList = File.ReadAllLines(@"C:\Users\Mark\Desktop\New folder\dl.txt").ToList();

            // Setup background work
            bw.WorkerReportsProgress = true;
            bw.WorkerSupportsCancellation = true;
            bw.DoWork += new DoWorkEventHandler(bw_DoWork);
            bw.ProgressChanged += new ProgressChangedEventHandler(bw_ProgressChanged);
            bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_RunWorkerCompleted);

            if (websiteList?.Count > 0)
                bAutoDL.Enabled = true;
            autoDLFlag = false;


            // TAB - CoedCherry
        }

        /*
         *  
         */
        private void bDownload_Click(object sender, EventArgs e)
        {
            // Download/Cancel
            if (bDownload.Text == "Download")
            {
                // Set busy
                if (bw.IsBusy != true)
                {
                    bDownload.Text = "Cancel";

                    // Start downloading
                    bw.RunWorkerAsync();
                }
            }
            else
            {
                if (bw.WorkerSupportsCancellation == true)
                {
                    // Cancel downloading
                    bw.CancelAsync();
                    bDownload.Text = "Download";
                    MessageBox.Show("Canceled.\nDownloaded " + dlcount + " images.");
                }
            }
        }

        /*
         *
         */
        private void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (!worker.CancellationPending)
            {
                if (!autoDLFlag)
                {
                    MessageBox.Show("Done!\nDownloaded " + dlcount + " images.");
                    tbWebsite.Focus();
                    tbWebsite.SelectAll();
                }
                else
                {
                    string m = "Done!";
                    for (int i = 0; i < dlcounts.Count; i++)
                        m += "\n" + websiteList[i] + ": " + dlcounts[i].ToString();
                    MessageBox.Show(m);
                    autoDLFlag = false;
                }

                bDownload.Text = "Download";
            }
        }

        /*
         *  
         */
        private void bw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this.ldCount.Text = (e.ProgressPercentage.ToString());
        }

        /*
         *
         */
        private void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            // Store background worker
            worker = sender as BackgroundWorker;
            dlcounts = new List<int>();

            if (autoDLFlag)
                foreach (string s in websiteList)
                {
                    string[] d = getData(s);
                    dlcounts.Add(work(@"C:\Users\Mark\Desktop\New folder\" + d[0] + "\\", d[1] + d[2]));
                }
            else
                work(tbPath.Text + "\\" + tbPath2.Text + "\\", tbURL.Text + tbURL2.Text);
        }

        private int work(string p, string u)
        {
            // Set path
            path = p;
            
            // Setup variables
            string url = u;
            List<string> ls = new List<string>(); // In-progress url, built up by pieces
            List<Point> lp = new List<Point>(); // Save fusker limits
            List<int> ld = new List<int>(); // Padding for each fusker

            dlcount = 0;

            // Get file save directory, create it if it doesn't exist
            string dir = path.Substring(0, path.LastIndexOf(@"\") + 1);
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            // Look for fusker chars
            if (url.Contains('[') && url.Contains(']'))
            {
                // Split up url by fusker delimiter
                string[] al = url.Split(delS);


                // Begin initial loop
                for (int i = 0; i < al.Count(); i++)
                {
                    ls.Add(al[i]);
                    i++;

                    if (i < al.Count())
                    {
                        // Split fusker limits
                        string[] num = al[i].Split('-');
                        ld.Add(Math.Min(num[0].Length, num[1].Length));
                        int x, y;
                        int.TryParse(num[0], out x);
                        int.TryParse(num[1], out y);
                        lp.Add(new Point(x, y));
                    }
                }

                func(ls[0], "", ls, lp, ld, 0);
            }
            else
            {
                // No fusker so just download the file
                string file = path + "1" + "." + ext;
                string _ext;
                if (ext == "jpg")
                    _ext = "jpeg";
                else
                    _ext = ext;

                if (DownloadRemoteImageFile(url, file, _ext))
                    worker.ReportProgress(++dlcount);
            }

            return dlcount;
        }

        /*
         *  Recursive downloading for fusker
         */
        private void func(string s, string d, List<string> ls, List<Point> lp, List<int> ld, int depth)
        {
            Boolean cancel_recursion = false;
            if (!worker.CancellationPending && !cancel_recursion)
            {
                if (depth == lp.Count())
                {
                    string file = path + d + "." + ext;
                    if (!File.Exists(file))
                    {
                        string _ext;
                        if (ext == "jpg")
                            _ext = "jpeg";
                        else
                            _ext = ext;

                        if (DownloadRemoteImageFile(s, file, _ext))
                            worker.ReportProgress(++dlcount);
                        else
                        {
                            if (autoDLFlag)
                                cancel_recursion = true;
                            else
                                bw.CancelAsync();
                        }
                    }

                    return;
                }
                for (int i = lp[depth].X; i <= lp[depth].Y; i++)
                {
                    string a = "";
                    for (int j = i.ToString().Length; j < ld[depth]; j++)
                    {
                        a += "0";
                    }
                    a += i.ToString();
                    func(s + a + ls[depth + 1], d + i.ToString() + "-", ls, lp, ld, depth + 1);
                }
            }
        }

        /*
         *  Download image
         */
        private static bool DownloadRemoteImageFile(string uri, string fileName, string i_ext)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            request.Timeout = 5000;
            HttpWebResponse response;
            try
            {
                response = (HttpWebResponse)request.GetResponse();
            }
            catch (Exception e)
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
                response.ContentType.EndsWith(i_ext, StringComparison.OrdinalIgnoreCase))
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

        /*
         *
         */
        private void bSetPath_Click(object sender, EventArgs e)
        {
            if (fbd.ShowDialog() == DialogResult.OK)
                tbPath.Text = fbd.SelectedPath + @"\";
        }

        private void tbURL_Click(object sender, EventArgs e)
        {
            tbURL.SelectAll();
        }

        private void tbPath2_Click(object sender, EventArgs e)
        {
            tbPath2.SelectAll();
        }

        private void tbWebsite_Click(object sender, EventArgs e)
        {
            tbWebsite.SelectAll();
        }

        private void bAuto_Click(object sender, EventArgs e)
        {
            if (tbWebsite.Text != "")
            {
                string[] s = getData(tbWebsite.Text);
                // Auto Fill entries
                tbPath2.Text = s[0];
                tbURL.Text = s[1];
                tbURL2.Text = s[2];

                if (tbPath.Text.Equals(""))
                    tbPath.Text = @"C:\Users\Mark\Desktop\New folder";
            }
        }

        private string[] getData(string site)
        {
            string[] ret = new string[3];
            using (WebClient client = new WebClient())
            {
                //Encoding gb = Encoding.GetEncoding("gb2312");
                //client.Encoding = gb;
                // Get page
                string data = client.DownloadString(site);
                List<string> dlist = data.Split('\n').ToList();

                // Get encoding
                int i;
                for (i = 0; i < dlist.Count; i++)
                    if (dlist[i].Contains("charset"))
                        break;
                string charset = dlist[i];
                List<string> charset_break = charset.Split('"').ToList();
                charset = charset_break[3];
                charset_break = charset.Split('=').ToList();
                charset = charset_break[1];

                // Redownload page with encoding
                Encoding enc = Encoding.GetEncoding(charset);
                client.Encoding = enc;
                data = client.DownloadString(site);

                // Get album title
                dlist = data.Split('\n').ToList();
                for (; i < dlist.Count; i++)
                    if (dlist[i].Contains("<h1>"))
                        break;
                string title = dlist[i];
                List<string> title_break = title.Split('>').ToList();
                title = title_break[2];
                title_break = title.Split('<').ToList();
                title = title_break[0];

                // Get fusker link for album
                for (; i < dlist.Count; i++)
                    if (dlist[i].Contains("img src="))
                        break;
                string image = dlist[i];
                List<string> image_break = image.Split('\'').ToList();
                image = image_break[5];
                string image_first = image.Substring(0, image.LastIndexOf("/") + 1);

                string image_last = image.Substring(image.LastIndexOf("/") + 1);
                string num = image_last.Substring(0, 2);
                if (num.Equals("01"))
                    image_last = "[" + image_last.Substring(0, 2) + "-99]" + image_last.Substring(2);
                else if (num.Equals("1."))
                    image_last = "[" + image_last.Substring(0, 1) + "-99]" + image_last.Substring(1);
                else if (num.Equals("00"))
                    image_last = image_last.Substring(0, 2) + "[" + image_last.Substring(2, 2) + "-99]" + image_last.Substring(4);
                else
                    image_last = "[" + image_last.Substring(0, 2) + "-99]" + image_last.Substring(2);

                ret[0] = title;
                ret[1] = image_first;
                ret[2] = image_last;
            }

            return ret;
        }

        private void bAutoDL_Click(object sender, EventArgs e)
        {
            autoDLFlag = true;

            websiteList = File.ReadAllLines(@"C:\Users\Mark\Desktop\New folder\dl.txt").ToList<string>();

            // Set busy
            if (bw.IsBusy != true)
            {
                bDownload.Text = "Cancel";

                // Start downloading
                bw.RunWorkerAsync();
            }
        }

        private void buttonDownload_CoedCherry_Click(object sender, EventArgs e)
        {
            dlcount_CoedCherry = 0;
            string modelName = textBoxModelName_CoedCherry.Text.Trim();
            string id = textBoxId_CoedCherry.Text.Trim();

            List<Tuple<string, string>> galleries = new List<Tuple<string,string>>();
            using (WebClient client = new WebClient())
            {
                foreach (HtmlElement link in GetHtmlDocument(
                    client.DownloadString(@"https://www.coedcherry.com/models/" + (id.Length > 0 ? modelName + "?id=" + id : modelName))).Links)
                {
                    if (link.OuterHtml.Contains("galleries") && ReferenceEquals(link.OuterText, null))
                    {
                        int indexTitle = link.OuterHtml.IndexOf("title=") + "title=".Length + 1;
                        int lengthTitle = link.OuterHtml.IndexOf("\" class") - indexTitle;
                        if (lengthTitle < 0)
                        {
                            lengthTitle = link.OuterHtml.IndexOf("' class") - indexTitle;
                        }
                        int indexGalleries = link.OuterHtml.IndexOf(".coedcherry.com/") + ".coedcherry.com/".Length;
                        int lengthGalleries = link.OuterHtml.IndexOf("/th180") - indexGalleries;
                        galleries.Add(new Tuple<string, string>(
                            link.OuterHtml.Substring(indexTitle, lengthTitle), 
                            link.OuterHtml.Substring(indexGalleries, lengthGalleries)));
                    }
                }
            }

            foreach (Tuple<string,string> gallery in galleries)
            {
                string fileLink = @"https://content4.coedcherry.com/" + gallery.Item2 + "/";
                string saveDirectory = @"C:\Temp\" + modelName + @"\" + gallery.Item1 + @"\";
                saveDirectory = saveDirectory.Replace('"', '\'');
                if (!Directory.Exists(saveDirectory))
                {
                    Directory.CreateDirectory(saveDirectory);
                }

                using (WebClient client = new WebClient())
                {
                    List<string> galleryLinks = new List<string>();
                    string url = @"https://www.coedcherry.com/models/" + modelName + "/galleries/" + gallery.Item2.Substring(gallery.Item2.IndexOf("/") + 1);
                    foreach (HtmlElement link in GetHtmlDocument(client.DownloadString(url)).Links)
                    {
                        if (link.OuterHtml.Contains("content") && ReferenceEquals(link.OuterText, null))
                        {
                            int indexContent = link.OuterHtml.IndexOf("<A href=\"") + "<A href=\"".Length;
                            int lengthContent = link.OuterHtml.IndexOf("\" target") - indexContent;
                            galleryLinks.Add(link.OuterHtml.Substring(indexContent, lengthContent));
                        }
                    }
                    foreach (string galleryLink in galleryLinks)
                    {
                        string fileName = galleryLink.Substring(galleryLink.LastIndexOf("/") + 1);
                        if(DownloadRemoteImageFile(
                            @"https://www.coedcherry.com" + galleryLink,
                            saveDirectory + fileName,
                            "jpeg"))
                        {
                            downloadCount_CoedCherry.Text = (++dlcount_CoedCherry).ToString();
                            downloadCount_CoedCherry.Refresh();
                        }

                    }
                }
            }
            downloadCount_CoedCherry.Text += " done.";
        }

        private void buttonDownload_Tumblr_Click(object sender, EventArgs e)
        {
            dlcount_Tumblr = 0;
            string api_prefix = @"https://api.tumblr.com/v2/blog/";
            string siteName = textBoxLink_Tumblr.Text + ".tumblr.com";
            //string api_key = @"api_key=fuiKNFp9vQFvjLNvx4sUwti4Yb5yGutBN4Xh10LXZhhRKjWlV4";
            //NB6MviShLWaTpod8ZdeFWh8NJctrruANFZ79xcRg9UTwqkrHLg
            //np1J08dGSk6PoIesda028qGbGKz4CqjdKCbGfxN39LFDxb9gbC
            string api_key = @"api_key=6x2rD9bLRuKXeIMdzRKGP0QkLHnTWKlI8zFdJ5DmXxSdXSnjHN";
            int offsetStart = (int)numericOffset_Tumblr.Value;
            int totalPosts = 0;
            //List<string> downloadUrls = new List<string>();
            List<string> exceptionUrls = new List<string>();
            string saveDirectory = @"C:\Temp\" + siteName + @"\";
            if (!Directory.Exists(saveDirectory))
            {
                Directory.CreateDirectory(saveDirectory);
            }

            using (WebClient client = new WebClient())
            {
                string json = client.DownloadString(api_prefix + siteName + "/posts/photo?offset=0&limit=1&" + api_key);
                dynamic o = JsonConvert.DeserializeObject(json);
                totalPosts = Convert.ToInt32(o.response.total_posts.Value);
            }

            for (int offset = offsetStart; offset < totalPosts; offset += 20)
            {
                numericOffset_Tumblr.Value = offset;
                numericOffset_Tumblr.Refresh();
                using (WebClient client = new WebClient())
                {
                    string json = client.DownloadString(api_prefix + siteName + "/posts/photo?offset=" + offset.ToString() + "&limit=20&" + api_key);
                    dynamic o = JsonConvert.DeserializeObject(json);
                    foreach (dynamic post in o.response.posts)
                    {
                        foreach (dynamic photo in post.photos)
                        {
                            string fileLink = photo.original_size.url.Value;
                            string fileName = fileLink.Substring(fileLink.LastIndexOf("/") + 1);
                            //if (DownloadRemoteImageFile(
                            //        fileLink,
                            //        saveDirectory + fileName,
                            //        "jpeg"))
                            //{
                            //    downloadCount_Tumblr.Text = (++dlcount_Tumblr).ToString();
                            //    downloadCount_Tumblr.Refresh();
                            //}
                            try
                            {
                                client.DownloadFile(fileLink, saveDirectory + fileName);
                                downloadCount_Tumblr.Text = (++dlcount_Tumblr).ToString();
                                downloadCount_Tumblr.Refresh();
                            }
                            catch (Exception ex)
                            {
                                if (!ex.Message.Contains("404"))
                                    exceptionUrls.Add(fileLink);
                            }
                            //System.Threading.Thread.Sleep(1000);
                        }
                    }
                }
            }

            //string site = @"http://" + siteName + @"/archive/";

            //using (WebClient client = new WebClient())
            //{
            //    string link = site + year.ToString() + "/" + month.ToString() + "?before_time=" + beforeTime.ToString();
            //    string data = client.DownloadString(link);
            //    if (data.Contains("<!-- START CONTENT -->"))
            //    {
            //        int contentIndex = data.IndexOf("<!-- START CONTENT -->");
            //        int contentLength = data.IndexOf("<!-- END CONTENT -->") - contentIndex;
            //        data = data.Substring(contentIndex, contentLength);
            //        List<string> splitData = data.Split(new string[] { "data-imageurl=\"" }, StringSplitOptions.None).ToList();
            //        splitData.RemoveAt(0);
            //        List<string> dataLinks = new List<string>();
            //        foreach (string split in splitData)
            //        {
            //            string sizeFixSplit = split.Replace("_250.jpg", "_1280.jpg");
            //            sizeFixSplit = sizeFixSplit.Substring(0, sizeFixSplit.IndexOf("\""));

            //            string fileName = sizeFixSplit.Substring(sizeFixSplit.LastIndexOf("/") + 1);
            //            if (DownloadRemoteImageFile(
            //                    sizeFixSplit,
            //                    saveDirectory + fileName,
            //                    "jpeg"))
            //            {
            //                downloadCount_Tumblr.Text = (++dlcount_Tumblr).ToString();
            //                downloadCount_Tumblr.Refresh();
            //            }
            //        }
            //    }
            //}
        }

        public System.Windows.Forms.HtmlDocument GetHtmlDocument(string html)
        {
            WebBrowser browser = new WebBrowser();
            browser.ScriptErrorsSuppressed = true;
            browser.DocumentText = html;
            browser.Document.OpenNew(true);
            browser.Document.Write(html);
            browser.Refresh();
            return browser.Document;
        }

        private void buttonDownload_trishdavis9_Click(object sender, EventArgs e)
        {
            //FileStream data = File.Open(@"C:\Users\mouri\OneDrive\Projects\Visual Studio\Org\DL\Resources\trishdavis9.html", FileMode.Open);
            List<string> exceptionLinks = new List<string>();
            
            string saveDirectory = @"C:\Temp\trishdavis9\";
            if (!Directory.Exists(saveDirectory))
            {
                Directory.CreateDirectory(saveDirectory);
            }

            using (WebClient client = new WebClient())
            {
                HtmlDocument doc = GetHtmlDocument(File.ReadAllText(@"C:\Users\mouri\OneDrive\Projects\Visual Studio\Org\DL\Resources\trishdavis9.html"));
                foreach (HtmlElement link in doc.Links)
                {
                    if (link.OuterHtml.Contains("data-big-photo=\""))
                    {
                        string partialLink = link.OuterHtml.Substring(link.OuterHtml.IndexOf("data-big-photo=\"") + "data-big-photo=\"".Length);
                        string fileLink = partialLink.Substring(0, partialLink.IndexOf('"'));
                        string fileName = fileLink.Substring(fileLink.LastIndexOf("/") + 1);
                        try
                        {
                            client.DownloadFile(fileLink, saveDirectory + fileName);
                        }
                        catch (Exception ex)
                        {
                            exceptionLinks.Add(fileLink);
                        }
                    }
                }
            }
        }

        private void buttonDownload_tdavis_Click(object sender, EventArgs e)
        {
            List<string> exceptionLinks = new List<string>();

            string saveDirectory = @"C:\Temp\tdavis\";
            if (!Directory.Exists(saveDirectory))
            {
                Directory.CreateDirectory(saveDirectory);
            }

            using (WebClient client = new WebClient())
            {
                string data = File.ReadAllText(@"C:\Users\mouri\OneDrive\Projects\Visual Studio\Org\DL\Resources\tdavis.html");
                foreach (string split in data.Split(new string[] { "background-image:url(" }, StringSplitOptions.None))
                {
                    if (split.StartsWith("/cdn"))
                    {
                        string fileLink = "http://www.jhphawaii.com" + split.Substring(0, split.IndexOf("-11.jpg")) + "-4.jpg";
                        string fileName = fileLink.Substring(fileLink.LastIndexOf("/") + 1);
                        try
                        {
                            client.DownloadFile(fileLink, saveDirectory + fileName);
                        }
                        catch (Exception ex)
                        {
                            exceptionLinks.Add(fileLink);
                        }
                    }
                }
            }
        }
    }
}