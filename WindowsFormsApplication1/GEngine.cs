using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace WindowsFormsApplication1
{
    class GEngine
    {
        private Graphics drawHandle;
        private Thread renderThread;

        public GEngine(Graphics g)
        {
            drawHandle = g;
            
        }

        public void init()
        {
            renderThread = new Thread(new ThreadStart(render));
            renderThread.Start();
        }

        public void stop()
        {
            renderThread.Abort();
        }

        private void render()
        {
            int framesRendered = 0;
            long startTime = Environment.TickCount;

            while (true)
            {
                drawHandle.FillRectangle(new SolidBrush(Color.Aqua), 0, 0, 50, 50);

                //Bitmap frame = new Bitmap();

                //Benchmarking
                framesRendered++;
                if (Environment.TickCount >= startTime + 1000)
                {
                    Console.WriteLine("GEngine: " + framesRendered + "fps");
                    framesRendered = 0;
                    startTime = Environment.TickCount;
                }
            }
        }
    }
}
