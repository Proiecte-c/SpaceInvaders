using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace SpaceInvaders_
{
    public partial class Form3 : Form
    {
        private string highScoreFile = "highscore.txt";
        private TableLayoutPanel tableLayoutPanel = new TableLayoutPanel();

        public Form3(int score, string Name)
        {
            InitializeComponent();
            SaveHighScore (score, Name);
        }



        private void SaveHighScore(int scores, string Name)
        {
            List<(string Name, int Score)> highScores = File.Exists(highScoreFile)
         ? File.ReadAllLines(highScoreFile)
             .Select(line => line.Split(' '))
             .Where(parts => parts.Length == 2 && int.TryParse(parts[1], out _))
             .Select(parts => (parts[0], int.Parse(parts[1])))
             .ToList()
         : new List<(string, int)>();

            highScores.Add((Name, scores));
            highScores = highScores.OrderByDescending(entry => entry.Score).Take(5).ToList();

            File.WriteAllLines(highScoreFile, highScores.Select(hs => hs.Name + " " + hs.Score));

            lblHighScore.Text = "Top 5 scoruri";

            Label[] nameLabels = { label2, label4, label6, label8, label10 };
            Label[] scoreLabels = { label3, label5, label7, label9, label11 };

            for (int i = 0; i < nameLabels.Length; i++)
            {
                if (i < highScores.Count)
                {
                    nameLabels[i].Text = highScores[i].Name;
                    scoreLabels[i].Text = highScores[i].Score.ToString();
                }
                else
                {
                    nameLabels[i].Text = "";
                    scoreLabels[i].Text = "";
                }
            }
        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }

        private void btnResetScores_Click(object sender, EventArgs e)
        {
            File.WriteAllText(highScoreFile, "0");
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
