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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        private void StartGame(string difficulty, int invNum)
        {
            this.Hide();
            Form1 gameForm = new Form1(difficulty, invNum,checkBox1.Checked,checkBox2.Checked);
            gameForm.ShowDialog();
            this.Close();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void btnEasy_Click_1(object sender, EventArgs e)
        {
            int invNum = Convert.ToInt32(textBox1.Text);
            if (invNum < 1 || invNum > 9)
            {
                MessageBox.Show("Nu ai introdus un numar valid de inamici.");
            }
            else
                StartGame("Easy", invNum);
        }

        private void btnMedium_Click_1(object sender, EventArgs e)
        {
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
            int invNum = Convert.ToInt32(textBox1.Text);
            if (invNum < 1 || invNum > 9)
            {
                MessageBox.Show("Nu ai introdus un numar valid de inamici.");

            }
            else
                StartGame("Hard", invNum);
        }
    }
}
