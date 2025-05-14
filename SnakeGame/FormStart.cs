using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SnakeGame
{
    public partial class FormStart : Form
    {
        
        public FormStart()
        {
            InitializeComponent();
        }
        //click vào nút exit thì close FormStart/thoát Game
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormStart_Load(object sender, EventArgs e)
        {
            int highScore = HighScoreManager.GetHighScore();
            label3.Text = $"{highScore}";
        }
 

        private void btnSpeed_Click(object sender, EventArgs e)
        {
            FormSpeed formSpeed = new FormSpeed();
            formSpeed.Show();
            this.Hide();
        }
    }
}
