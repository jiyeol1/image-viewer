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
using System.Drawing.Drawing2D;


namespace _0719_1
{
    public partial class Form1 : Form
    {
        private Point LastPoint;
        private Bitmap img;
        private double ratio = 1.0F;
        private Point imgPoint;
        private Rectangle imgRect;
        private Point clickPoint;
        String file_path = "";
        string img_path;
        public Form1()
        {
            InitializeComponent();
            ///휠 이벤트 선언
            pictureBox1.MouseWheel += new MouseEventHandler(pictureBox1_MouseWheel);


            imgPoint = new Point(pictureBox1.Width / 2, pictureBox1.Height / 2);
            imgRect = new Rectangle(0, 0, pictureBox1.Width, pictureBox1.Height);

            clickPoint = imgPoint;

            pictureBox1.Invalidate();
        }
        private void button1_Click(object sender, EventArgs e)
        {

            openFileDialog1.InitialDirectory = "C:\\";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
                file_path = openFileDialog1.FileName;

            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.Image = (Bitmap)Image.FromFile(file_path);

            img_path = file_path;


        }
       private void button4_Click(object sender, EventArgs e)
        {
        pictureBox1.Image.RotateFlip(RotateFlipType.RotateNoneFlipX);
            pictureBox1.Refresh();
        }
        private void pictureBox1_MouseWheel(object sender, MouseEventArgs e)
        {
           
            int lines = e.Delta * SystemInformation.MouseWheelScrollLines / 120;
            PictureBox pb = (PictureBox)sender;

            if (lines > 0)
            {
                ratio *= 1.1F;
                if (ratio > 100.0) ratio = 100.0f;

                imgRect.Width = (int)Math.Round(pictureBox1.Width * ratio);
                imgRect.Height = (int)Math.Round(pictureBox1.Height * ratio);
                imgRect.X = -(int)Math.Round(1.1F * (imgPoint.X - imgRect.X) - imgPoint.X);
                imgRect.Y = -(int)Math.Round(1.1F * (imgPoint.Y - imgRect.Y) - imgPoint.Y);
            }
            else if (lines < 0)
            {
                ratio *= 0.9F;
                if (ratio < 1) ratio = 1;

                imgRect.Width = (int)Math.Round(pictureBox1.Width * ratio);
                imgRect.Height = (int)Math.Round(pictureBox1.Height * ratio);
                imgRect.X = -(int)Math.Round(0.9F * (imgPoint.X - imgRect.X) - imgPoint.X);
                imgRect.Y = -(int)Math.Round(0.9F * (imgPoint.Y - imgRect.Y) - imgPoint.Y);
            }

            if (imgRect.X > 0) imgRect.X = 0;
            if (imgRect.Y > 0) imgRect.Y = 0;
            if (imgRect.X + imgRect.Width < pictureBox1.Width) imgRect.X = pictureBox1.Width - imgRect.Width;
            if (imgRect.Y + imgRect.Height < pictureBox1.Height) imgRect.Y = pictureBox1.Height - imgRect.Height;
            pictureBox1.Invalidate();
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {


        }



        private void pictureBox1_Paint_1(object sender, PaintEventArgs e)
        {

            if (pictureBox1.Image != null)
            {
                e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;

                e.Graphics.DrawImage(pictureBox1.Image, imgRect);
                pictureBox1.Focus();
            }
        }

        private void pictureBox1_MouseDown_1(object sender, MouseEventArgs e)
        {

            if (e.Button == MouseButtons.Left)
            {
                clickPoint = new Point(e.X, e.Y);
            }
            pictureBox1.Invalidate();
        }


        private void pictureBox1_MouseMove_1(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                imgRect.X = imgRect.X + (int)Math.Round((double)(e.X - clickPoint.X) / 5);
                if (imgRect.X >= 0) imgRect.X = 0;
                if (Math.Abs(imgRect.X) >= Math.Abs(imgRect.Width - pictureBox1.Width))
                    imgRect.X = -(imgRect.Width - pictureBox1.Width);
                imgRect.Y = imgRect.Y + (int)Math.Round((double)(e.Y - clickPoint.Y) / 5);
                if (imgRect.Y >= 0) imgRect.Y = 0;
                if (Math.Abs(imgRect.Y) >= Math.Abs(imgRect.Height - pictureBox1.Height)) imgRect.Y = -(imgRect.Height - pictureBox1.Height);
            }
            else
            {
                LastPoint = e.Location;
            }

            pictureBox1.Invalidate();
        }
        private void pictureBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            pictureBox1.Image.RotateFlip(RotateFlipType.Rotate90FlipNone);
    
            pictureBox1.Refresh();
        }


        private void button2_Click_1(object sender, EventArgs e)
        {
            img = new Bitmap((Bitmap)pictureBox1.Image);
            Color col;
            int Average;
            
            //x 는 image의 width
            //y 는 image의 hediht
            for (int x = 0; x < img.Width; x++)
            {
                for (int y = 0; y < img.Height; y++)
                {

                    col = img.GetPixel(x, y);
                    Average = (col.R + col.G+ col.B) / 3;

                    col = Color.FromArgb(Average, Average, Average);
                    img.SetPixel(x, y, col);
                }
            }

            pictureBox1.Image = img;



        }
       
        private void button3_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Title = "다른 이름으로 저장";
            dlg.DefaultExt = "jpg";
            dlg.Filter = "JPEG (*.jpg)|*.jpg|Bitmap (*.bmp)|*.bmp|GIF (*.gif)|*.gif";
            dlg.FilterIndex = 0;

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image.Save(dlg.FileName);
            }
        }
        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
           
         
            pictureBox1.Image = (Bitmap)Image.FromFile(img_path);
            
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label1_Click(object sender, EventArgs e)
        {
         
        }

        
    }
}

