using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Media;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpaceInvaders_
{

    public partial class Form2 : Form
    {
        private SoundPlayer gameSound;

        public Form2()
        {
            InitializeComponent();

            gameSound=new SoundPlayer("C:\\Users\\vladf\\source\\repos\\Ahbreh\\SpaceInvaders-\\SpaceInvaders!\\Resources\\game music.wav");
        }
        private void StartGame(string difficulty, int invNum)
        {
            this.Hide();
            gameSound.Stop();
            string PlayerName = textBox2.Text;
            Form1 gameForm = new Form1(difficulty, invNum, checkBox1.Checked, checkBox2.Checked, PlayerName);
            gameForm.ShowDialog();
            this.Close();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
           gameSound.PlayLooping();
        }

        private void btnEasy_Click_1(object sender, EventArgs e)
        {
            if (textBox2.Text == "")
            {
                MessageBox.Show("Nu ai completat caseta de nume");
                return;
            }
            if (textBox1.Text == "")
            {
                MessageBox.Show("Nu ai introdus un numar de inamici");
                return;
            }
            if (Regex.IsMatch(textBox2.Text, @"^[0-9!@#$%^&*()<>?/\|=+%-]+$"))
            {
                MessageBox.Show("Trebuie sa introduci un nume valid");
                return;
            }
            if (Regex.IsMatch(textBox1.Text, @"^[a-zA-Z!@#$%^&*()<>?/\|=+%-]+$"))
            {
                MessageBox.Show("Trebuie sa introduci un numar intre 1 si 9");
                return;
            }
            int invNum = Convert.ToInt32(textBox1.Text);
            if (invNum < 1 || invNum > 9)
            {
                MessageBox.Show("Nu ai introdus un numar valid de inamici.");
                return;
            }
            else
                StartGame("Easy", invNum);
        }

        private void btnMedium_Click_1(object sender, EventArgs e)
        {
            if (textBox2.Text == "")
            {
                MessageBox.Show("Nu ai completat caseta de nume");
                return;
            }
            if (textBox1.Text == "")
            {
                MessageBox.Show("Nu ai introdus un numar de inamici");
                return;
            }
            if (Regex.IsMatch(textBox2.Text, @"^[0-9!@#$%^&*()<>?/\|=+%-]+$"))
            {
                MessageBox.Show("Trebuie sa introduci un nume valid");
                return;
            }
            if (Regex.IsMatch(textBox1.Text, @"^[a-zA-Z!@#$%^&*()<>?/\|=+%-]+$"))
            {
                MessageBox.Show("Trebuie sa introduci un numar intre 1 si 9");
                return;
            }
            int invNum = Convert.ToInt32(textBox1.Text);
            if (invNum < 1 || invNum > 9)
            {
                MessageBox.Show("Nu ai introdus un numar valid de inamici.");
            }
            else
                StartGame("Medium", invNum);
        }

        private void btnHard_Click_1(object sender, EventArgs e)
        {
            if (textBox2.Text == "")
            {
                MessageBox.Show("Nu ai completat caseta de nume");
                return;
            }
            if (textBox1.Text == "")
            {
                MessageBox.Show("Nu ai introdus un numar de inamici");
                return;
            }
            if (Regex.IsMatch(textBox2.Text, @"^[0-9!@#$%^&*()<>?/\|=+%-]+$"))
            {
                MessageBox.Show("Trebuie sa introduci un nume valid");
                return;
            }
            if (Regex.IsMatch(textBox1.Text, @"^[a-zA-Z!@#$%^&*()<>?/\|=+%-]+$"))
            {
                MessageBox.Show("Trebuie sa introduci un numar intre 1 si 9");
                return;
            }
            int invNum = Convert.ToInt32(textBox1.Text);
            if (invNum < 1 || invNum > 9)
            {
                MessageBox.Show("Nu ai introdus un numar valid de inamici.");

            }
            else
                StartGame("Hard", invNum);
        }


        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}
