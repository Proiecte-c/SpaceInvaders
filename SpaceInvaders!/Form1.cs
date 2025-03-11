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
        int wallspeed = 50;
        int wallDirection = 1;
        int enemySpeed;
        int score = 0;
        int enemyBulletTimer = 2000;
        int bosshealth = 5;
        long tprev;

        private string difficulty;
        private int invNum;
        private bool power;
        private bool walls;

        PictureBox BossEnemy;
        PictureBox[] Invaders;
        PictureBox[] Super;

        bool shooting;
        bool isGameOver;
        public Form1(string selectedDifficulty, int invadersNumber, bool obstacles, bool powerUp)
        {
            InitializeComponent();
            difficulty = selectedDifficulty;
            invNum = invadersNumber;
            power = powerUp;
            walls = obstacles;
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
                if (x is PictureBox && ((string)x.Tag == "Invaders" || (string)x.Tag == "Boss"))
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
                                if ((string)x.Tag == "Invaders")
                                {
                                    this.Controls.Remove(x);
                                    score++;
                                }
                                else if ((string)x.Tag == "Boss")
                                {
                                    bosshealth--;
                                    if (bosshealth == 0)
                                    {
                                        this.Controls.Remove(x);
                                    }
                                }
                                this.Controls.Remove(y);
                                txtScore.Text = "Score : " + score;
                                if (difficulty == "Hard" && score == invNum && BossEnemy == null)
                                {
                                    spawnBoss();
                                }
                                if (score == invNum && difficulty != "Hard" || difficulty == "Hard" && bosshealth == 0)
                                {
                                    gameOver("You win!!!");
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
                    x.Top -= Convert.ToInt32(bulletspeed * deltaT);
                    if (x.Top < 15)
                    {
                        this.Controls.Remove(x);
                        shooting = false;
                    }
                }

                if (x is PictureBox && (string)x.Tag == "enemyBullet")
                {
                    x.Top += Convert.ToInt32(bulletspeed * deltaT);
                    if (x.Top > 620) this.Controls.Remove(x);
                    if (x.Bounds.IntersectsWith(player.Bounds))
                    {
                        this.Controls.Remove(x);
                        gameOver("Game Over");
                    }
                }
            }
            foreach (Control x in this.Controls)
            {
                if (x is Label && (string)x.Tag == "Wall")
                {

                    if (wallDirection == 1)
                    {
                        x.Left += Convert.ToInt32(wallspeed * deltaT);
                        if (x.Right > 735)
                        {
                            wallDirection = 0;
                        }
                    }
                    else
                    {
                        x.Left -= Convert.ToInt32(wallspeed * deltaT);
                        if (x.Left < 15)
                        {
                            wallDirection = 1;
                        }
                    }
                    foreach (Control y in this.Controls)
                    {
                        if (y is PictureBox && (string)y.Tag == "Bullet")
                        {
                            if (x.Bounds.IntersectsWith(y.Bounds))
                            {
                                this.Controls.Remove(y);
                                shooting = false;
                            }
                        }
                    }
                }
            }
            enemyBulletTimer -= Convert.ToInt32(deltaT * 1000);
            if (enemyBulletTimer <= 0)
            {
                makeBullet("enemyBullet");
                enemyBulletTimer = (difficulty == "Easy") ? 2000 : (difficulty == "Medium") ? 1500 : 1000;
            }
            tprev = aux;

        }

        private void spawnBoss()
        {
            BossEnemy = new PictureBox();
            BossEnemy.Size = new Size(120, 100);
            BossEnemy.Image = Properties.Resources.boss;
            BossEnemy.Top = 5;
            BossEnemy.Tag = "Boss";
            BossEnemy.Left = 50;
            BossEnemy.SizeMode = PictureBoxSizeMode.StretchImage;
            this.Controls.Add(BossEnemy);
            BossEnemy.BringToFront();
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
                if (power)
                    makeSuperBullet();
                else
                    makeBullet("Bullet");
            }
            if (e.KeyCode == Keys.Enter && isGameOver == true)
            {
                removeAll();
                Application.Restart();
            }
        }

        private void makeInvaders()
        {
            Invaders = new PictureBox[invNum];
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

        private void makeWalls()
        {
            Label wall = new Label();
            wall.AutoSize = false;
            wall.Size = new Size(237, 15);
            wall.Location = new Point(330, 343);
            wall.BackColor = Color.White;
            wall.Tag = "Wall";
            this.Controls.Add(wall);
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
            if (walls)
                makeWalls();

            makeInvaders();

            tprev = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();

            gameTimer.Start();

        }
        private void gameOver(string message)
        {
            isGameOver = true;
            gameTimer.Stop();
            txtScore.Text = "Score: " + score;
            txtMessage.Text = message;
            txtMessage.BorderStyle = BorderStyle.FixedSingle;
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
                    if ((string)x.Tag == "bullet" || (string)x.Tag == "enemyBullet"|| (string)x.Tag == "Boss")
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
            bullet.SizeMode = PictureBoxSizeMode.CenterImage;
            this.Controls.Add(bullet);
            bullet.BringToFront();
        }

        private void makeSuperBullet()
        {
            Super = new PictureBox[3];
            for (int i = 0; i < 3; i++)
            {
                Super[i] = new PictureBox();
                Super[i].Image = Properties.Resources.Bullet;
                Super[i].Size = new Size(10, 20);
                Super[i].Tag = "Bullet";
                Super[i].Top = player.Top - 20;
                Super[i].BringToFront();
                Super[i].SizeMode = PictureBoxSizeMode.CenterImage;
            }
            Super[0].Left = player.Left + player.Width / 2 - 25;
            Super[1].Left = player.Left + player.Width / 2;
            Super[2].Left = player.Left + player.Width / 2 + 25;
            this.Controls.Add(Super[0]);
            this.Controls.Add(Super[1]);
            this.Controls.Add(Super[2]);
        }
    }
}
