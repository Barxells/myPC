using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

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
            Regex list = new Regex(string.Format("{0}~(.*?)\r", CPU), RegexOptions.IgnoreCase);
            string file = File.ReadAllText(@"../../CPU.list");
            MatchCollection listmatch = list.Matches(file);
            Match match = listmatch[0];
            GroupCollection group = match.Groups;
            int CPUscore = int.Parse(group[0].Value.Split('~')[1].Replace(",", ""));

            // GPU
            string GPU = textBox4.Text;
            // Check if string is a number
            int n;
            bool isNumeric = int.TryParse(GPU, out n);
            int GPUscore;
            if (isNumeric)
            {
                int v = int.Parse(GPU);
                GPUscore = v;
            }
            else
            {
                Regex GPUlist = new Regex(string.Format("{0}~(.*?)\n", GPU), RegexOptions.IgnoreCase);
                string GPUfile = File.ReadAllText(@"../../GPU.list");
                MatchCollection GPUlistmatch = GPUlist.Matches(GPUfile);
                Match GPUmatch = GPUlistmatch[0];
                GroupCollection GPUgroup = GPUmatch.Groups;
                GPUscore = int.Parse(GPUgroup[0].Value.Split('~')[1].Replace(",", ""));
            }

            // Storage
            int GB = int.Parse(textBox3.Text);

            // Work out and display score
            int Score = ((RAM * (RAMType * 75)) + CPUscore + GPUscore) / 100;
            label5.Text = "Score: " + Score.ToString();

            // Set progress bar to 50%
            progressBar1.Value = 50;

            // Give a general "speed"
            if (Score > 200)
            {
                label6.Text = "Overkill";
            }
            else if (Score > 100)
            {
                label6.Text = "Very Powerful";
            }
            else if (Score > 70)
            {
                label6.Text = "Powerful";
            }
            else if (Score > 40)
            {
                label6.Text = "Good";
            }
            else
            {
                label6.Text = "Slow";
            }

            // Hide progress bar and show labels
            progressBar1.Value = 100;
            label5.Visible = true;
            label6.Visible = true;
            progressBar1.Visible = false;
        }

        private void gsGO_Click(object sender, EventArgs e)
        {
            string RAM = gsRAM.Text;
            string RAMType = gsRAMType.Text;

            // CPU
            string CPU = gsCPU.Text;
            // Check if string is a number
            int m;
            bool isCPUNumeric = int.TryParse(CPU, out m);
            if (isCPUNumeric)
            {
            }
            else
            {
                Regex list = new Regex(string.Format("{0}~(.*?)\r", CPU), RegexOptions.IgnoreCase);
                string file = File.ReadAllText(@"../../CPU.list");
                MatchCollection listmatch = list.Matches(file);
                Match match = listmatch[0];
                GroupCollection group = match.Groups;
                CPU = group[0].Value.Split('~')[1].Replace(",", "");

            }

            // GPU
            string GPU = gsGPU.Text;
            // Check if string is a number
            int n;
            bool isNumeric = int.TryParse(GPU, out n);
            int GPUscore;
            if (isNumeric)
            {
                
            }
            else
            {
                Regex GPUlist = new Regex(string.Format("{0}~(.*?)\n", GPU), RegexOptions.IgnoreCase);
                string GPUfile = File.ReadAllText(@"../../GPU.list");
                MatchCollection GPUlistmatch = GPUlist.Matches(GPUfile);
                Match GPUmatch = GPUlistmatch[0];
                GroupCollection GPUgroup = GPUmatch.Groups;
                GPU = GPUgroup[0].Value.Split('~')[1].Replace(",", "");
            }

            string Storage = gsGB.Text;

            Dictionary<string, string[]> gamesRec = new Dictionary<string, string[]>();
            gamesRec.Add("Minecraft: Java Edition", new string[] { "8", "3", "2214", "343", "100" });
            gamesRec.Add("Grand Theft Auto V", new string[] { "8", "3", "4678", "3998", "150" });
            /// Add more games later
            foreach (KeyValuePair<string, string[]> game in gamesRec)
            {
                string gameName = game.Key;
                int gameRAM = int.Parse(game.Value[0]);
                int gameRAMType = int.Parse(game.Value[1].Replace("DDR", ""));
                int gameCPU = int.Parse(game.Value[2]);
                int gameGPU = int.Parse(game.Value[3]);
                int gameStorage = int.Parse(game.Value[4]);
                int gameCompatibilityScore; // >100 is great, 100 is good, 70-100 is okay, 40-70 is playable, 10-40 is VERY slow, <10 is just unplayable
                int gameRAMScore = 0;
                int gameCPUScore = 0;
                int gameGPUScore = 0;
                if (gameRAM <= int.Parse(RAM))
                {
                    gameRAMScore = (int.Parse(RAM) / gameRAM) * 100;
                }
                else
                {
                    gameRAMScore = (int.Parse(RAM) / gameRAM) * 100;
                }
                if (gameCPU <= int.Parse(CPU))
                {
                    gameCPUScore = (int.Parse(CPU) / gameCPU) * 100;
                }
                else
                {
                    gameCPUScore = (int.Parse(CPU) / gameCPU) * 100;
                }
                if (gameGPU <= int.Parse(GPU))
                {
                    gameGPUScore = (int.Parse(GPU) / gameGPU) * 100;
                }
                else
                {
                    gameGPUScore = (int.Parse(GPU) / gameGPU) * 100;
                }
                gameCompatibilityScore = ((gameRAMScore * 2) + gameCPUScore + (gameGPUScore * 3)) / 6;
                gsText.Text += gameName + " - " + gameCompatibilityScore + "%\r\n";
            }
            gsPanel.Visible = false;
            gsReset.Visible = true;
        }

        private void gsReset_Click(object sender, EventArgs e)
        {
            gsPanel.Visible = true;
            gsReset.Visible = false;
            gsText.Text = "";
            gsRAM.Text = "";
            gsRAMType.Text = "";
            gsCPU.Text = "";
            gsGPU.Text = "";
            gsGB.Text = "";
        }
    }
}
