using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using System.Diagnostics;

namespace SteamHelper
{
    public partial class Form1 : Form
    {
        string path = Path.GetDirectoryName(Application.ExecutablePath) + "\\gamelist.config";

        static List<games> games;

        
        public Form1()
        {
            InitializeComponent();

games = new List<games>();

            if ((File.Exists(path)))
            {
                using (Stream stream = File.Open(path, FileMode.Open))
                {
                    var bformatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();

                    games = (List<games>)bformatter.Deserialize(stream);


                    for (int i = 0; i < games.Count; i++)
                    {
                        listBox1.Items.Add(games[i].Name);
                        listBox2.Items.Add(games[i].Folder);
                    }

                }
            }
            //MessageBox.Show(path);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            


            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.InitialDirectory = "c:\\";
            openFileDialog1.Filter = "Game Exe (*.exe)|*.exe";
            openFileDialog1.FilterIndex = 0;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string selectedFileName = openFileDialog1.FileName;
                string gamename = Interaction.InputBox("Insert a game name", "Insert game name", "game");
                //MessageBox.Show(gamename);
                //MessageBox.Show(selectedFileName);

                games.Add(new games(selectedFileName, gamename));


                using (Stream stream = File.Open(path, FileMode.Create))
                {
                    var bformatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();

                    bformatter.Serialize(stream, games);
                }
                
    
                listBox1.Items.Add(gamename);
                listBox2.Items.Add(selectedFileName);

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string gameee = listBox2.SelectedItem.ToString();
            Process.Start(gameee);
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            listBox2.SelectedIndex = listBox1.SelectedIndex;
            label1.Text = listBox2.Text;
            label2.Text = listBox1.Text;
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            listBox1.SelectedIndex = listBox2.SelectedIndex;
            if (!listBox2.Text.Equals(""))
            pictureBox1.Image = Icon.ExtractAssociatedIcon(listBox2.Text)?.ToBitmap();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            
        }

        private void button3_Click(object sender, EventArgs e)
        {

            games.RemoveAll(item => listBox2.Text.Contains(item.Folder));
            listBox2.SelectedIndex = listBox1.SelectedIndex;
            listBox1.Items.Remove(listBox1.SelectedItem);
            listBox2.Items.Remove(listBox2.SelectedItem);
            
            using (Stream stream = File.Open(path, FileMode.Create))
            {
                var bformatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();

                bformatter.Serialize(stream, games);
            }
            
        }

        private void label1_Click(object sender, EventArgs e)
        {
            MessageBox.Show(listBox2.Text);
        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            
            
        }
    }
}
