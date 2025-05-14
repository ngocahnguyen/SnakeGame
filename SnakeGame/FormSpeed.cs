using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SnakeGame
{
    public partial class FormSpeed : Form
    {
        public SpeedLevel SelectedSpeed { get; private set; }
        public FormSpeed()
        {
            InitializeComponent();
        }

        private void btnSlow_Click(object sender, EventArgs e)
        {
            SelectedSpeed = SpeedLevel.Slow;
            FormGame formgame = new FormGame(SelectedSpeed);
            formgame.Show();
            this.Hide();
           
        }

        private void btnMedium_Click(object sender, EventArgs e)
        {
            SelectedSpeed = SpeedLevel.Medium;
            FormGame formgame = new FormGame(SelectedSpeed);
            formgame.Show();
            this.Hide();
        }

        private void btnFast_Click(object sender, EventArgs e)
        {
            SelectedSpeed = SpeedLevel.Fast;
            FormGame formgame = new FormGame(SelectedSpeed);
            formgame.Show();
            this.Hide();
        }
    }
}
