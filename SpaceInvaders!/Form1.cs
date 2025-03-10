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
        int playerDirection = 0;
        int playerSpeed = 200;
        int bulletspeed = 600;
        int enemySpeed;
        int score = 0;
        int enemyBulletTimer = 2000;
        int bosshealth = 5;
        long tprev;

        private string difficulty;

        PictureBox BossEnemy;
        PictureBox[] Invaders;

        bool shooting;
        bool isGameOver;
        public Form1(string selectedDifficulty)
        {
            InitializeComponent();
            difficulty = selectedDifficulty;
            gamesetup();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }


        private void gameLoop(object sender, EventArgs e)
        {
            long aux = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();

            double deltaT = (aux - tprev) / 1000.0;

            player.Left += Convert.ToInt32(deltaT * playerSpeed * playerDirection);

            foreach (Control x in this.Controls)
            {
                if (x is PictureBox && (string)x.Tag == "Invaders")
                {
                    x.Left += Convert.ToInt32(enemySpeed * deltaT);
                    if (x.Left > 730)
                    {
                        x.Top += 65;
                        x.Left = -80;
                    }
                    if (x.Bounds.IntersectsWith(player.Bounds))
                        gameOver("Game Over");
                    foreach (Control y in this.Controls)
                    {
                        if (y is PictureBox && (string)y.Tag == "Bullet")
                        {
                            if (y.Bounds.IntersectsWith(x.Bounds))
                            {
                                this.Controls.Remove(x);
                                this.Controls.Remove(y);
                                score++;
                                txtScore.Text = "score : " + score;
                              
                                if (score == 5)
                                {
                                    if (difficulty == "Hard")
                                    {
                                        spawnBoss();
                                    }
                                    else
                                        gameOver("winner");
                                }
                                shooting = false;
                            }
                        }
                    }
                }
            

            }
            foreach (Control x in this.Controls)
            {
                if (x is PictureBox && (string)x.Tag == "Bullet")
                {
                    x.Top -= Convert.ToInt32 (bulletspeed * deltaT);
                    if (x.Top < 15)
                    {
                        this.Controls.Remove(x);
                        shooting = false;
                    }
                }

                if (x is PictureBox && (string)x.Tag == "enemyBullet")
                {
                    x.Top += Convert.ToInt32 (bulletspeed * deltaT);
                    if (x.Top > 620) this.Controls.Remove(x);
                    if (x.Bounds.IntersectsWith(player.Bounds))
                    {
                        this.Controls.Remove(x);
                        gameOver("Game Over");
                    }
                }
            }
            enemyBulletTimer -= Convert.ToInt32(deltaT * 1000);
            if (enemyBulletTimer <= 0)
            {
                makeBullet("enemyBullet");
                enemyBulletTimer = (difficulty == "Easy") ? 2000 : (difficulty == "Medium") ? 1500 : 1000;
            }

            if (difficulty == "Hard" && score > 5 && BossEnemy == null)
            {
                MessageBox.Show("ok");
                spawnBoss();
            }

        tprev = aux;
           
        }

        private void spawnBoss()
        {
            BossEnemy = new PictureBox();
            BossEnemy.Size = new Size(60, 50);
            BossEnemy.Image = Properties.Resources.boss;
            BossEnemy.Top = 5;
            BossEnemy.Tag = "Boss";
            BossEnemy.Left = 50;
            BossEnemy.SizeMode = PictureBoxSizeMode.StretchImage;
            this.Controls.Add(BossEnemy);
        }
        private void keyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                playerDirection = -1;
            }
            if (e.KeyCode == Keys.Right)
            {
                playerDirection = 1;
            }
        }

        private void keyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                playerDirection = 0;
            }
            if (e.KeyCode == Keys.Right)
            {
                playerDirection = 0;
            }
            if (e.KeyCode == Keys.Space && shooting == false)
            {
                shooting = true;
                makeBullet("Bullet");
            }
            if (e.KeyCode == Keys.Enter && isGameOver == true)
            {
                removeAll();
                gamesetup();
            }
        }

        private void makeInvaders()
        {
            Invaders = new PictureBox[5];
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

        private void gamesetup()
        {
            txtScore.Text = "Score: 0";
            score = 0;
            isGameOver = false;
            shooting = false;
            enemyBulletTimer = 300;
            bosshealth = 5;

            switch (difficulty)
            {
                case "Easy":
                    enemySpeed = 200;
                    gameTimer.Interval = 25;

                    break;
                case "Medium":
                    enemySpeed = 400;
                    gameTimer.Interval = 25;
                    break;
                case "Hard":
                    enemySpeed = 400;
                    gameTimer.Interval = 25;
                    bosshealth = 5;
                    break;
            }



            makeInvaders();

            tprev = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds (); 

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

        private void makeBullet(string bulletTag)
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
                bullet.Top = -100;
            }
            bullet.SizeMode = PictureBoxSizeMode.StretchImage;
            this.Controls.Add(bullet);
            bullet.BringToFront();
        }
    }
}
