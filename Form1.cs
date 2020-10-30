using System;
using System.Windows.Forms;

namespace TVBlackout
{
    public partial class Form1 : Form
    {
        DateTime TempTimer;
        bool TempShow;
        public Form1()
        {
            InitializeComponent();
            timer1.Interval = 2000;
            timer1.Start();
            notifyIcon1.Visible = true;
            this.ShowInTaskbar = true;
            this.WindowState = FormWindowState.Minimized;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            // Set our form location and bounds
            Left = Top = 0;
            Width = Screen.PrimaryScreen.Bounds.Width;
            Height = Screen.PrimaryScreen.Bounds.Height;
            // If the screen is clicked, a flag is set to temporarily minimize for 10 mins
            if (TempShow)
            {
                this.WindowState = FormWindowState.Minimized;
                if (DateTime.Now >= (TempTimer + TimeSpan.FromMinutes(10)))
                {
                    TempShow = false;                      
                }
                return;
            }
            // Main update loop, check day of week and current hour
            // Maximize blank screen during out of office hours
            switch ((int)DateTime.Now.DayOfWeek)
            {
                case 1:
                case 2:
                case 3:
                case 4:
                case 5:
                    if (DateTime.Now.Hour > 7 && DateTime.Now.Hour < 17) { this.WindowState = FormWindowState.Minimized;  }
                    else { this.WindowState = FormWindowState.Maximized; }        
                    break;
                case 0:
                case 6:
                    this.WindowState = FormWindowState.Maximized;
                    break;
            }
        }

        private void Form1_Click(object sender, EventArgs e)
        {
            TempTimer = DateTime.Now;
            TempShow = true;
        }
    }
}
