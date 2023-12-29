using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Http;
using System.Net;
using Tesseract;

namespace WinOCR
{

  
    public partial class Form1 : Form
    {
   
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog1.FileName;

                // PictureBox'a resmi yükle
                pictureBox1.ImageLocation = filePath;

                var image = new Bitmap(openFileDialog1.FileName);
                var ocr = new TesseractEngine(@"C:\Users\Özhan Yıldırım\source\repos\WinOCR\packages\Tesseract.5.2.0", "tur");
                var page = ocr.Process(image);
               
                // Extract text from the page
                var text = page.GetText();

                // Print the extracted text
                richTextBox1.Text = text;
            }
        }




    }
}