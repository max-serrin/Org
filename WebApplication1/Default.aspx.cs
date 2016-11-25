using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class _Default : Page
    {
        List<string> ls = new List<string>();

        Random rng;

        protected void Page_Load(object sender, EventArgs e)
        {
            rng = new Random();

            ls.Add("http://36.media.tumblr.com/194e0cbae953f48bc19be47806ab10d7/tumblr_nw76b1KUio1uew6mbo1_500.jpg");
            ls.Add("https://40.media.tumblr.com/b07a5e39851261bd53e0458060bc8da3/tumblr_nu3dlg2gA51uew6mbo1_540.jpg");
            ls.Add("https://36.media.tumblr.com/1472680fc7b3f9275768619bd82aa957/tumblr_nufotvCn4i1uew6mbo1_500.jpg");
            ls.Add("https://40.media.tumblr.com/55546c7b67667183583f636f22b4f74d/tumblr_nu3d48ZJPE1uew6mbo1_1280.jpg");
            ls.Add("https://40.media.tumblr.com/2a385dfc70b8c0127faa6fb9cf20ebcb/tumblr_nw7658aDxj1uew6mbo1_1280.jpg");
            ls.Add("http://40.media.tumblr.com/f30d84a9b192275762a8eeeb072570a6/tumblr_nv07odTFO21uew6mbo1_500.jpg");
            ls.Add("https://41.media.tumblr.com/202a72c9a380edab70e006f1cb4ecb82/tumblr_n9jawmrB4i1snjys5o1_1280.jpg");
            ls.Add("https://36.media.tumblr.com/cb418cd0bafe03b4e0773c1b95b333eb/tumblr_nufotijnhc1uew6mbo1_500.jpg");
            ls.Add("https://40.media.tumblr.com/8fb2c919dd1cd0310faba92e0f1311d9/tumblr_nufoth15vw1uew6mbo1_1280.jpg");

            Timer1.Interval = 10000;
            Timer1.Enabled = true;
        }

        protected void Timer1_Tick(object sender, EventArgs e)
        {
            Image1.ImageUrl = ls[rng.Next(ls.Count)];
        }
    }
}