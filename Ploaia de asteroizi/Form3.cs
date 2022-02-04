using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Game1
{
    public partial class Game : Form
    {
        string jitDebugging="true";
        int shipSpeed=5 ;
        int roadSpeed ;
        bool shipLeft;
        bool shipRight;
        int trafficSpeed = 5;
        int Score = 0;
        Random rnd = new Random();
        private void Reset()
        {
            trophy.Visible = false;
            button1.Enabled = false;
            explosion.Visible = false;
            trafficSpeed = 4;
            roadSpeed = 4;
            Score = 0;
            player.Left = 161;
            player.Top = 286;
            shipLeft = false;
            shipRight = false;
            AI1.Left = 66;
            AI1.Top = -120;
            AI2.Left = 294;
            AI2.Top = -185;
            roadTrack2.Left = -3;
            roadTrack2.Top = -222;
            roadTrack1.Left = -2;
            roadTrack1.Top = -638;
            timer1.Start();
        }
        public Game()
        {
            InitializeComponent();
            this.KeyDown += moveShip;
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            Reset();
        }
        private void moveShip(object sender, KeyEventArgs e)
        {
            int x;
            x = player.Location.X;
            if (e.KeyCode == Keys.A && player.Left > 0)
            {
                x = x - 10;
                e.SuppressKeyPress = true;
            }
            if (e.KeyCode == Keys.D && player.Left + player.Width < panel1.Width)
            {
                x = x + 10;
                e.SuppressKeyPress = true;
            }
            player.Location = new Point(x, player.Location.Y);
        }

        private void stopShip(object sender, KeyEventArgs e)
        {
            
            if (e.KeyCode == Keys.Left)
            {
                shipLeft = false;
            }
            if (e.KeyCode == Keys.Right)
            {
                shipRight = false;
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            Score++;
            distance.Text = "" + Score;
            roadTrack1.Top += roadSpeed;
            roadTrack2.Top += roadSpeed;
            if (roadTrack1.Top > 630)
            {
                roadTrack1.Top = -630;
            }
            if (roadTrack2.Top > 630)
            {
                roadTrack2.Top = -630;
            }
            if (shipLeft) { player.Left -= shipSpeed; }
            if (shipRight) { player.Left += shipSpeed; }

            if (player.Left < 1)
            {
                shipLeft = false;
            }
            else if (player.Left + player.Width > 380)
            {
                shipRight = false;
            }

            AI1.Top += trafficSpeed;
            AI2.Top += trafficSpeed;

            if (AI1.Top > panel1.Height)
            {
                AI1.Left = rnd.Next(2, 160);
                AI1.Top = rnd.Next(100, 200) * -1;
            }

            if (AI2.Top > panel1.Height)
            {
                AI2.Left = rnd.Next(185, 327);
                AI2.Top = rnd.Next(100, 200) * -1;
            }

            if (player.Bounds.IntersectsWith(AI1.Bounds) || player.Bounds.IntersectsWith(AI2.Bounds))
            {
                gameOver();
            }

            if (Score > 500 && Score < 1000)
            {
                trafficSpeed = 5;
                roadSpeed = 6;
            }

            else if (Score > 1000 && Score < 1250)
            {
                trafficSpeed = 6;
                roadSpeed = 7;
            }

            else if (Score > 1250)
            {
                trafficSpeed = 8;
                roadSpeed = 9;
            }
        }
        
        private void gameOver()
        {
            trophy.Visible = true;
            timer1.Stop();
            button1.Enabled = true;
            explosion.Visible = true;
            player.Controls.Add(explosion);
            explosion.Location = new Point(-8, 5);
            explosion.BackColor = Color.Transparent;
            explosion.BringToFront();
                trophy.ImageLocation = "bronze2.png";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Reset();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Menu f = new Menu();
            f.Show();
        }
        private void Form3_Load(object sender, EventArgs e)
        {

        }
    }
}
