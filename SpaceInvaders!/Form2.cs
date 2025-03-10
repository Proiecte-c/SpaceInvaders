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
        private void StartGame(string difficulty)
        {
            this.Hide();
            Form1 gameForm = new Form1(difficulty);
            gameForm.ShowDialog();
            this.Close();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
           
        }

        private void btnEasy_Click_1(object sender, EventArgs e)
        {
            StartGame("Easy");
        }

        private void btnMedium_Click_1(object sender, EventArgs e)
        {
            StartGame("Medium");
        }

        private void btnHard_Click_1(object sender, EventArgs e)
        {
            StartGame("Hard");
        }
    }
}
