using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpaceInvaders_
{
    public partial class Form1 : Form
    {
        bool goLeft, goRight;
        int playerSpeed = 12;
        int enemySpeed = 5;
        int score = 0;
        int enemyBulletTimer = 300;
        Random rnd = new Random();

        PictureBox[] Invaders = new PictureBox[5];

        bool shooting;
        bool isGameOver;
        public Form1()
        {
            InitializeComponent();
            gameSetup();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void mainGameTimer(object sender, EventArgs e)
        {
            txtScore.Text = "Score: " + score;
            if (goLeft)
            {
                player.Left -= playerSpeed;
            }
            if (goRight)
            {
                player.Left += playerSpeed;
            }
            enemyBulletTimer -= 10;
            if (enemyBulletTimer < 1)
            {
                enemyBulletTimer = 300;
                int invader_number = rnd.Next(Invaders.Length);
                if (this.Controls.Contains(Invaders[invader_number]))
                    makeBullet("enemyBullet",invader_number);
            }
            foreach (Control x in this.Controls)
            {
                if (x is PictureBox && (string)x.Tag == "Invaders")
                {
                    x.Left += enemySpeed;
                    if (x.Left > 730)
                    {
                        x.Top += 65;
                        x.Left = -80;
                    }
                    if (x.Bounds.IntersectsWith(player.Bounds))
                    {
                        gameOver("Game Over");
                    }
                    foreach (Control y in this.Controls)
                    {
                        if (y is PictureBox && (string)y.Tag == "Bullet")
                        {
                            if (y.Bounds.IntersectsWith(x.Bounds))
                            {
                                this.Controls.Remove(x);
                                this.Controls.Remove(y);
                                score += 1;
                                shooting = false;
                            }
                        }
                    }
                }
                if (x is PictureBox && (string)x.Tag == "Bullet")
                {
                    x.Top -= 20;
                    if (x.Top < 15)
                    {
                        this.Controls.Remove(x);
                        shooting = false;
                    }
                }
                if (x is PictureBox && (string)x.Tag == "enemyBullet")
                {
                    x.Top += 20;
                    if (x.Top > 620)
                    {
                        this.Controls.Remove(x);
                    }
                    if (x.Bounds.IntersectsWith(player.Bounds))
                    {
                        this.Controls.Remove(x);
                        gameOver("Game Over");
                    }
                }
            }
            if (score > 8)
            {
                enemySpeed = 12;
            }
            if (score == Invaders.Length)
            {
                gameOver("You Won!!!");
            }
        }

        private void keyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                goLeft = true;
            }
            if (e.KeyCode == Keys.Right)
            {
                goRight = true;
            }
        }

        private void keyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                goLeft = false;
            }
            if (e.KeyCode == Keys.Right)
            {
                goRight = false;
            }
            if (e.KeyCode == Keys.Space && shooting == false)
            {
                shooting = true;
                makeBullet("Bullet",0);
            }
            if (e.KeyCode == Keys.Enter && isGameOver == true)
            {
                removeAll();
                gameSetup();
            }
        }

        private void makeInvaders()
        {
            int left = 0;
            for (int i = 0; i < Invaders.Length; i++)
            {
                Invaders[i] = new PictureBox();
                Invaders[i].Size = new Size(60, 50);
                Invaders[i].Image = Properties.Resources.Invader;
                Invaders[i].Top = 5;
                Invaders[i].Tag = "Invaders";
                Invaders[i].Left = left;
                Invaders[i].SizeMode = PictureBoxSizeMode.StretchImage;
                this.Controls.Add(Invaders[i]);
                left = left - 80;
            }
        }

        private void gameSetup()
        {
            txtScore.Text = "Score: 0";
            score = 0;
            isGameOver = false;
            enemyBulletTimer = 300;
            enemySpeed = 5;
            shooting = false;
            makeInvaders();
            gameTimer.Start();
        }
        private void gameOver(string message)
        {
            isGameOver = true;
            gameTimer.Stop();
            txtScore.Text = "Score: " + score;
            txtMessage.Text = message;
        }

        private void removeAll()
        {
            foreach (PictureBox i in Invaders)
            {
                this.Controls.Remove(i);
            }
            foreach (Control x in this.Controls)
            {
                if (x is PictureBox)
                {
                    if ((string)x.Tag == "bullet" || (string)x.Tag == "sadBullet")
                    {
                        this.Controls.Remove(x);
                    }
                }
            }
        }

        private void player_Click(object sender, EventArgs e)
        {

        }

        private void makeBullet(string bulletTag, int x)
        {
            PictureBox bullet = new PictureBox();
            bullet.Image = Properties.Resources.Bullet;
            bullet.Size = new Size(10, 20);
            bullet.Tag = bulletTag;
            bullet.Left = player.Left + player.Width / 2;
            if ((string)bullet.Tag == "Bullet")
            {
                bullet.Top = player.Top - 20;
            }
            else if ((string)bullet.Tag == "enemyBullet")
            {
                bullet.Location = Invaders[x].Location;
            }
            bullet.SizeMode = PictureBoxSizeMode.CenterImage;
            this.Controls.Add(bullet);
            bullet.BringToFront();
        }
    }
}
