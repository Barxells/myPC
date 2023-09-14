using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using static System.Net.Mime.MediaTypeNames;

namespace App_The_Second
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Make Progress Bar Visible
            progressBar1.Visible = true;

            // RAM
            int RAM = int.Parse(textBox1.Text);
            int RAMType = int.Parse(listBox1.Text.Last().ToString());

            // CPU
            string CPU = textBox2.Text;
            Regex list = new Regex(string.Format("{0}~(.*?)$", CPU), RegexOptions.IgnoreCase);
            MatchCollection listmatch = list.Matches(File.ReadAllText(@"../../CPU.list"));
            Match match = listmatch[0];
            GroupCollection group = match.Groups;
            int CPUscore = int.Parse(group[0].Value.Split('~')[1]);

            // GPU
            string GPU = textBox3.Text;
            Regex GPUlist = new Regex(string.Format("{0}~(.*?)$", GPU), RegexOptions.IgnoreCase);
            MatchCollection GPUlistmatch = GPUlist.Matches(File.ReadAllText(@"../../GPU.list"));
            Match GPUmatch = GPUlistmatch[0];
            GroupCollection GPUgroup = GPUmatch.Groups;
            int GPUscore = int.Parse(GPUgroup[0].Value.Split('~')[1]);

            // Storage
            int GB = int.Parse(textBox4.Text);

            // Work out and display score
            int Score = ((RAM * (RAMType * 75)) + CPUscore + GPUscore) / 100;
            label5.Text = "Score: " + Score.ToString();

            // Set progress bar to 50%
            progressBar1.Value = 50;

            // Give a general "speed"
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

            // Hide progress bar and show labels
            progressBar1.Value = 100;
            label5.Visible = true;
            label6.Visible = true;
            progressBar1.Visible = false;
        }
    }
}
