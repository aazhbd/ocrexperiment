using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Handwriting
{
    public partial class Form1 : Form
    {
        Bitmap img;
        String str;
        int x, h, w;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog of = new OpenFileDialog();
            of.ShowDialog();
            str = of.FileName;
            if (str.Length == 0) return;
            img = (Bitmap) Image.FromFile(str);
            x = 1;
            pictureBox1.Image = img;
            h = img.Height;
            w = img.Width;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Color c;
            int i, j, cr, cg, cb;

            if (x == 0)
            {
                OpenFileDialog of = new OpenFileDialog();
                of.ShowDialog();
                string str = of.FileName;
                if (str.Length == 0) return;
                img = (Bitmap) Image.FromFile(str);
                x = 1;
                pictureBox1.Image = img;
                h = img.Height;
                w = img.Width;
            }

            for (i = 0; i < h; i++)
            {
                for (j = 0; j < w; j++)
                {
                    c = img.GetPixel(j, i);
                    cr = c.R;
                    cg = c.G;
                    cb = c.B;

                    if ((cr != 255 && cg != 255 && cb != 255) && (cr != 0 && cg != 0 && cb != 0))
                    {
                        img.SetPixel(j, i, Color.Black);
                    }
                }
            }

            pictureBox2.Image = img;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Color c;
            int i, j, cr, cg, cb, sx, sy, right, down, diagonal;

            sx = 0;
            sy = 0;

            for (i = 0; i < w; i++)
            {
                for (j = 0; j < h; j++)
                {
                    c = img.GetPixel(i, j);
                    cr = c.R;
                    cb = c.B;
                    cg = c.G;

                    if (cr == 0 && cb == 0 && cg == 0)
                    {
                        sx = i;
                        sy = j;
                        break;
                    }
                }
            }

            //run right...

            for (i = sx; i < w; i++)
            {
                c = img.GetPixel(i, sy);
                cr = c.R;
                cb = c.B;
                cg = c.G;

                if (cr == 255 && cb == 255 && cg == 255) break;
            }

            right = i;

            // run down...

            for (i = sy; i < h; i++)
            {
                c = img.GetPixel(sx, i);
                cr = c.R;
                cb = c.B;
                cg = c.G;

                if (cr == 255 && cb == 255 && cg == 255) break;
            }

            down = i;

            // run diagonal...

            i = sx;
            j = sy;
            diagonal = 0;

            while (i < w && j < h)
            {
                c = img.GetPixel(i, j);
                cr = c.R;
                cb = c.B;
                cg = c.G;

                if (cr == 255 && cb == 255 && cg == 255) break;
                diagonal++;
                i++;
                j++;
            }

            if (right > down && right > diagonal)
            {
                MessageBox.Show("Horizontal Line");
            }
            else if (down > diagonal && down > right)
            {
                MessageBox.Show("Vertical Line");
            }
            else if(diagonal > right && diagonal > down)
            {
                MessageBox.Show("Diagonal Line");
            }
        }
    }
}