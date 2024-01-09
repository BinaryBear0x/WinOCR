using System;
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
            if (Clipboard.ContainsImage())
            {
                Image newImage = Clipboard.GetImage();

                pictureBox1.Image = newImage;

                currentImage = newImage;

                if (currentImage != null)
                {
                    // Dil dosyasının bulunduğu dizini belirtin
                    var tessDataPath = Application.StartupPath;

                    var ocr = new TesseractEngine(tessDataPath, "tur");
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

            Thread.Sleep(1000);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog1.FileName;

                pictureBox1.ImageLocation = filePath;

                var image = new Bitmap(openFileDialog1.FileName);

                // Dil dosyasının bulunduğu dizini belirtin
                var tessDataPath = Application.StartupPath;

                var ocr = new TesseractEngine(tessDataPath, "tur");
                var page = ocr.Process(image);

                var text = page.GetText();

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
