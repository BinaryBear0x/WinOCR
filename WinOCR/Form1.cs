using System;
using System.Collections;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using Tesseract;

namespace WinOCR
{
    public partial class Form1 : Form
    {
        private Image currentImage;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           
        }


        private void ClipboardWatcher()
        {
           
                // Panoyu kontrol et
                if (Clipboard.ContainsImage())
                {
                    Image newImage = Clipboard.GetImage();

                  
                        // PictureBox'a yeni görseli ekle
                        pictureBox1.Image = newImage;

                        // Yeni görseli işleme alabilir veya kaydedebilirsiniz.
                        currentImage = newImage;

                        // OCR işlemi
                        if (currentImage != null)
                        {
                            var ocr = new TesseractEngine(@"C:\Users\Özhan Yıldırım\source\repos\WinOCR\packages\Tesseract.5.2.0", "tur");
                            var page = ocr.Process((Bitmap)currentImage);

                            // Extract text from the page
                            var text = page.GetText();

                            // Print the extracted text
                            richTextBox1.Invoke((MethodInvoker)delegate
                            {
                                richTextBox1.Text = text;
                            });
                        }
                    }
                

                // Bir süre bekleme (örnekte 1 saniye)
                Thread.Sleep(1000);
            
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

        private void button2_Click(object sender, EventArgs e)
        {
            // Clipboard'ı izlemek için bir thread başlatma
            Thread clipboardThread = new Thread(ClipboardWatcher);
            clipboardThread.SetApartmentState(ApartmentState.STA);
            clipboardThread.Start();
        }
    }
}
