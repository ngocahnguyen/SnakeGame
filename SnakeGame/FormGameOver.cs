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

namespace SnakeGame
{
    
    public partial class FormGameOver : Form
    {
      
        public int Score { get; set; }
        public FormGameOver(int score)
        {
            InitializeComponent();
            Score = score;
            int highScore = HighScoreManager.GetHighScore();
            
            if (score > highScore)
            {
                HighScoreManager.SaveHighScore(score);
                highScore = score;
                label3.Text = $"High Score: {score}";
                label1.Hide();
                label2.Hide();
            }
            else
            {
                label2.Text = $"{score}";
                label3.Text = $"High Score: {highScore}";
            }

            
        }

        private void btnRestart_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Retry;
            this.Close();
        }
       
        private void btnExit_Click(object sender, EventArgs e)
        {
            HighScoreManager.ResetHighScore();
            this.DialogResult = DialogResult.Abort;
            Application.Exit();
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            FormStart newformStart = new FormStart();
            newformStart.Show();
            this.Close();
        }
    }
}
