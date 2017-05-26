using Labyrinth.classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Labyrinth
{
    public partial class Form1 : Form
    {
        private CGame Game;
        public Form1()
        {
            InitializeComponent();
            Game = new CGame();
        }
        public bool play = false;
        public void button1_Click(object sender, EventArgs e)
        {
            play = true;
            button1.Visible = false;
            Game.Start();
            pictureBox1.BackgroundImage = Game.Field.Img;
            pictureBox1.BackgroundImageLayout = ImageLayout.None;
            DrawingBox();
        }
        private void DrawingBox()
        {
            //Thread.Sleep(1000);
            pictureBox1.Controls.Clear();
            PictureBox pictureBox2 = new PictureBox
            {
                Size = pictureBox1.Size,
                Location = new Point(0, 0),
                BackColor = Color.Transparent,
                Image = Game.Field.BlackHoles
            };
            pictureBox1.Controls.Add(pictureBox2); // добавление слоя в родительский контейнер
            pictureBox2.Controls.Clear();
            PictureBox pictureBox3 = new PictureBox
            {
                Size = pictureBox2.Size,
                Location = new Point(0, 0),
                BackColor = Color.Transparent,
                Image = Game.Curs
            };
            pictureBox2.Controls.Add(pictureBox3);
        }
        private void Esc(object sender, EventArgs e)
        {
            Close();
        }
        public void nextcell()
        {
            //label1.Text = (string.Format("X: {0}, Y: {1}",Game.Cursor.coordx, Game.Cursor.coordy));
            switch (Game.Field.Get(Game.Cursor.coordy, Game.Cursor.coordx).type)
            {
                case EType.Portal:
                    pictureBox1.Update();
                    Thread.Sleep(500);
                    Game.port();
                    DrawingBox();
                    break;
                case EType.BlackHole:
                    pictureBox1.Update();
                    Thread.Sleep(500);
                    Game.bh();
                    DrawingBox();
                    break;
                case EType.Fin:
                    pictureBox1.Update();
                    Thread.Sleep(500);
                    Game.fin();
                    DrawingBox();
                    win();
                    break;
                case EType.Strt:
                    pictureBox1.Update();
                    Thread.Sleep(500);
                    Game.strt();
                    DrawingBox();
                    break;
                case EType.Exit:
                    win();
                    break;
                default:
                    break;
            }                
        }
        public void win()
        {
            MessageBox.Show("Победа!");
            play = false;
        }
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.F2)
            {
                play = true;
                button1.Visible = false;
                Game.Start();
                pictureBox1.BackgroundImage = Game.Field.Img;
                pictureBox1.BackgroundImageLayout = ImageLayout.None;
                DrawingBox();
            }
            if (e.KeyCode == Keys.Escape)
                Close();
            if (play)
            {
                switch (e.KeyCode)
                {
                    case Keys.A:
                    case Keys.NumPad4:
                    case Keys.Left:
                        {
                            Game.left();
                            DrawingBox();
                            nextcell();
                        }
                        break;
                    case Keys.W:
                    case Keys.NumPad8:
                    case Keys.Up:
                        {
                            Game.top();
                            DrawingBox();
                            nextcell();
                        }
                        break;
                    case Keys.S:
                    case Keys.NumPad5:
                    case Keys.Down:
                        {
                            Game.bottom();
                            DrawingBox();
                            nextcell();
                        }
                        break;
                    case Keys.D:
                    case Keys.NumPad6:
                    case Keys.Right:
                        {
                            Game.right();
                            DrawingBox();
                            nextcell();
                        }
                        break;
                }
            }   
        }
        private void Form1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Down:
                case Keys.Up:
                case Keys.Left:
                case Keys.Right:
                    e.IsInputKey = true;
                    break;
            }
        }
    }
}
