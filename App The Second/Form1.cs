using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace App_The_Second
{
    public partial class Form1 : Form
    {
        // ((R*250)+C+G)/100
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            progressBar1.Visible = true;
            
            int RAM = int.Parse(textBox1.Text);
            int RAMType = int.Parse(listBox1.Text.Last().ToString());
            int CPU = int.Parse(textBox2.Text);
            int GPU = int.Parse(textBox3.Text);
            int GB = int.Parse(textBox4.Text);
            int Score = ((RAM * (RAMType * 75)) + CPU + GPU) / 100;
            label5.Text = "Score: " + Score.ToString();
            progressBar1.Value = 50;
            if (Score > 200)
            {
                label6.Text = "Overkill";
            } else if (Score > 100)
            {
                label6.Text = "Very Powerful";
            } else if (Score > 70)
            {
                label6.Text = "Powerful";
            } else if (Score > 40)
            {
                label6.Text = "Good";
            } else
            {
                label6.Text = "Slow";
            }
            progressBar1.Value = 100;
            label5.Visible = true;
            label6.Visible = true;
            progressBar1.Visible = false;
        }
    }
}
