using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp6
{
    public partial class Form1 : Form
    {
        Random r = new Random();
        List<PictureBox> pictureBoxList = new List<PictureBox>();
        List<Point> locationList = new List<Point>();
        List<int> sneknum = new List<int>();
        PictureBox pictureBoxN = new PictureBox();
        Point lastFoodLocation = new Point();
        int dir = 0;
        int length1 = 0;
        bool appending = false;


        public Form1()
        {
            InitializeComponent();
        }

        public void MoveSnake(List<PictureBox> pictureboxlist, List<int> sneknum, int direction)
        {
            length1++;
            locationList.Clear();
            for (int i = pictureboxlist.Count()-1; i > 0; i--)
            {
                pictureboxlist[i].Location = pictureboxlist[i - 1].Location;
                sneknum[i] = sneknum[i - 1];
                pictureboxlist[i].Image = (Image)(Properties.Resources.ResourceManager.GetObject("snek" + sneknum[i].ToString()));
                pictureboxlist[i].Size = new Size(25, 25);
                locationList.Add(pictureBoxList[i].Location);
            }
            if (pictureboxlist.Count() == length1 && appending == true)
            {
                PictureBox pictureBoxN = new PictureBox();
                pictureBoxN.Image = (Image)(Properties.Resources.ResourceManager.GetObject("snek" + sneknum[sneknum.Count()-1].ToString()));
                pictureBoxN.Location = pictureboxlist.Last().Location;
                pictureBoxN.BackgroundImageLayout = ImageLayout.Stretch;
                pictureboxlist.Add(pictureBoxN);
                sneknum.Add(direction);
                Controls.Add(pictureBoxN);
                appending = false;
            }
            switch (direction % 4)
            {
                case 0:
                    pictureBox1.Left += 24;
                    break;
                case 1:
                    pictureBox1.Top -= 24;
                    break;
                case 2:
                    pictureBox1.Left -= 24;
                    break;
                case 3:
                    pictureBox1.Top += 24;
                    break;
            }
            pictureBox1.Image = (Image)(Properties.Resources.ResourceManager.GetObject("snekhead" + direction.ToString()));
            sneknum[0] = direction;
            if (locationList.Contains(pictureBox1.Location) || (pictureBox1.Top < 0) || (pictureBox1.Top >= 720) || (pictureBox1.Left < 0) || (pictureBox1.Left >= 720))
            {
                Die();
            }
            else if (pictureBox1.Location == pictureBox2.Location)
            {
                pictureBox2.Location = new Point(r.Next(0, 29) * 24, r.Next(0, 29) * 24);
                while (locationList.Contains(pictureBox2.Location))
                {
                    pictureBox2.Location = new Point(r.Next(0, 29) * 24, r.Next(0, 29) * 24);
                }
                lastFoodLocation = pictureBox2.Location;
                length1 = 1;
                appending = true;
                label1.Text = (pictureboxlist.Count() - 1).ToString();
            }
        }

        private void Die()
        {
            timer1.Enabled = false;
            button1.Show();
            button2.Hide();
            pictureBox1.Hide();
            pictureBox2.Hide();
            foreach (PictureBox p in pictureBoxList)
            {
                p.Hide();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Die();
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((e.KeyChar == (char)Keys.D) || (e.KeyChar == (char)100)) && (dir != 2 ))
            {
                dir = 0;
            }
            else if (((e.KeyChar == (char)Keys.W) || (e.KeyChar == (char)119)) && (dir != 3))
            {
                dir = 1;
            }
            else if (((e.KeyChar == (char)Keys.A) || (e.KeyChar == (char)97)) && (dir != 0))
            {
                dir = 2;
            }
            else if (((e.KeyChar == (char)Keys.S) || (e.KeyChar == (char)115)) && (dir != 1))
            {
                dir = 3;
            }
            else if (e.KeyChar == (char)Keys.Escape)
            {
                timer1.Enabled = timer1.Enabled ^ true;
                button2.Visible = button2.Visible ^ true;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            MoveSnake(pictureBoxList, sneknum, dir);
        }

        private void button1_Click(object sender, EventArgs e)
        {            
            label1.Text = "0";
            button2.Hide();
            pictureBox1.Show();
            pictureBox2.Show();
            pictureBoxList.Clear();
            pictureBoxList.Add(pictureBox1);
            sneknum.Add(dir);
            PictureBox pictureBoxN = new PictureBox();
            pictureBoxN.ImageLocation = "snek0.png";
            pictureBoxN.Location = pictureBoxList.Last().Location;
            pictureBoxN.BackgroundImageLayout = ImageLayout.Stretch;
            pictureBoxList.Add(pictureBoxN);
            sneknum.Add(dir);
            Controls.Add(pictureBoxN);
            pictureBox1.Location = new Point(r.Next(10, 20) * 24, r.Next(10, 20) * 24);
            pictureBox2.Location = new Point(r.Next(0, 29) * 24, r.Next(0, 29) * 24);
            button1.Hide();
            timer1.Enabled = true;
        }

    }
}
