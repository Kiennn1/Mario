using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        bool goLeft, goRight, jumping, hasKey;

        int jumpSpeed = 10;
        int force = 8;
        int score = 0;

        int playerSpeed = 10;
        int backgroundSpeed = 8;




        public Form1()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox27_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox26_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox24_Click(object sender, EventArgs e)
        {

        }

        private void MainTimerEvent(object sender, EventArgs e)
        {
            txtScore.Text = "Điểm: " + score;
            player.Top += jumpSpeed;

            if (goLeft == true && player.Left > 60)
            {
                player.Left -= playerSpeed;
            }
            if (goRight == true && player.Left + (player.Width + 60) < this.ClientSize.Width)
            {
                player.Left += playerSpeed;
            }


            if (goLeft == true && background.Left < 0)
            {
                background.Left += backgroundSpeed;
                MoveGameElements("forward");
            }

            if (goRight == true && background.Left > -1360)
            {
                background.Left -= backgroundSpeed;
                MoveGameElements("back");
            }

            if (jumping == true)
            {
                jumpSpeed = -12;
                force -= 1;
            }
            else
            {
                jumpSpeed = 12;
            }

            if (jumping == true && force < 0)
            {
                jumping = false;
            }

            foreach (Control x in this.Controls)
            {

                if (x is PictureBox && (string)x.Tag == "platfrom")
                {

                    if (player.Bounds.IntersectsWith(x.Bounds) && jumping == false)
                    {
                        force = 8;
                        player.Top = x.Top - player.Height;
                        jumpSpeed = 0;
                    }

                    x.BringToFront();

                }
                if (x is PictureBox && (string)x.Tag == "coin")
                {
                    if(player.Bounds.IntersectsWith (x.Bounds) && x.Visible == true)
                    {
                        x.Visible = false;
                        score += 1;
                    }
                }
            }

            if (player.Bounds.IntersectsWith(key.Bounds))
            {
                key.Visible = false;
                hasKey = true;
            }
            if( player.Bounds.IntersectsWith(door.Bounds) && hasKey == true)
            {
                door.Image = Properties.Resources.door_open;
                GameTimer.Stop();
                MessageBox.Show("bạn đã hoàn thành " + Environment.NewLine + "ấn vào OK để chơi lại");
                RestartGame();
            }

            if (player.Top + player.Height > this.ClientSize.Height)
            {
                GameTimer.Stop();
                MessageBox.Show("Bạn đã thua " + Environment.NewLine + "ấn vào OK để chơi lại");
                RestartGame();
            }
        }

        private void KeyIsDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Left)
            {
                goLeft = true;
            }
            if (e.KeyCode == Keys.Right)
            {
                goRight = true;
            }
            if (e.KeyCode == Keys.Space && jumping == false)
            {
                jumping = true;
            }


        }


        private void KeyIsUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                goLeft = false;
            }
            if(e.KeyCode == Keys.Right)
            {
                goRight = false;
            }
            if (jumping == false)
            {
                jumping = false;
            }
        }

        private void background_Click(object sender, EventArgs e)
        {

        }

        private void CloseGame(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void RestartGame()
        {
            Form1 newWindow = new Form1();
            newWindow.Show();
            this.Hide();
        }

        private void MoveGameElements(string direction)
        {
            foreach (Control x in this.Controls)
            {
                if (x is PictureBox && (string)x.Tag == "platfrom" || x is PictureBox && (string)x.Tag == "coin" || x is PictureBox && (string)x.Tag == "key" || x is PictureBox && (string)x.Tag == "door")
                {

                    if (direction == "back")
                    {
                        x.Left -= backgroundSpeed;
                    }
                    if (direction == "forward")
                    {
                        x.Left += backgroundSpeed;
                    }

                    
                }
            }
        }

        }
}
