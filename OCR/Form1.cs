using Patagames.Ocr;
using Patagames.Ocr.Enums;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;
using IronOcr;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;

namespace OCR
{
    public partial class Form1 : Form
    {

        public static string Path="";



        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
        (
            int nLeftRect,     // x-coordinate of upper-left corner
            int nTopRect,      // y-coordinate of upper-left corner
            int nRightRect,    // x-coordinate of lower-right corner
            int nBottomRect,   // y-coordinate of lower-right corner
            int nWidthEllipse, // width of ellipse
            int nHeightEllipse // height of ellipse
        );



        public Form1()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 21, 21));



        }
        public static Bitmap ResizeImage(Image image, int width, int height)
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private bool dragging = false;
        private Point dragCursorPoint;
        private Point dragFormPoint;
        public void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            dragging = true;
            dragCursorPoint = Cursor.Position;
            dragFormPoint = this.Location;
        }

        public void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragging)
            {
                Point dif = Point.Subtract(Cursor.Position, new Size(dragCursorPoint));
                this.Location = Point.Add(dragFormPoint, new Size(dif));
            }
        }

        public void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            dragging = false;
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            OpenFileDialog open_dialog = new OpenFileDialog();
            open_dialog.Filter = "Image Files (*.jpeg; *.png; *.bmp)|*.jpg; *.png; *.bmp";

            if (open_dialog.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = Image.FromFile(open_dialog.FileName);
                Path = open_dialog.FileName;

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        public void AppendTextBox(string value)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action<string>(AppendTextBox), new object[] { value });
                return;
            }
            textBox.Text += value;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (!textBox.Text.Equals("")) { 
            System.Windows.Forms.Clipboard.SetText(textBox.Text);
            }
        }
        public void DOrc(int lang) {

            if (lang == 1) {
                var Ocr = new AdvancedOcr()
                {
                    CleanBackgroundNoise = true,
                    EnhanceContrast = true,
                    EnhanceResolution = true,
                    Language = IronOcr.Languages.English.OcrLanguagePack,
                    Strategy = IronOcr.AdvancedOcr.OcrStrategy.Advanced,
                    ColorSpace = AdvancedOcr.OcrColorSpace.Color,
                    DetectWhiteTextOnDarkBackgrounds = true,
                    InputImageType = AdvancedOcr.InputTypes.Document,
                    RotateAndStraighten = true,
                    ReadBarCodes = true,
                    ColorDepth = 4
                };
                var Results = Ocr.Read(Path);
               AppendTextBox(Results.Text);

              

            } else if (lang ==2)
            {
                var Ocr = new AdvancedOcr()
                {

                    CleanBackgroundNoise = true,
                    EnhanceContrast = true,
                    EnhanceResolution = true,
                    Language = IronOcr.Languages.ChineseSimplified.OcrLanguagePack,
                    Strategy = IronOcr.AdvancedOcr.OcrStrategy.Advanced,
                    ColorSpace = AdvancedOcr.OcrColorSpace.Color,
                    DetectWhiteTextOnDarkBackgrounds = true,
                    InputImageType = AdvancedOcr.InputTypes.Document,
                    RotateAndStraighten = true,
                    ReadBarCodes = true,
                    ColorDepth = 4
                };
                var Results = Ocr.Read(Path);
                textBox.Text = Results.Text;
            }
            else if (lang == 3)
            {
                var Ocr = new AdvancedOcr()
                {

                    CleanBackgroundNoise = true,
                    EnhanceContrast = true,
                    EnhanceResolution = true,
                    Language = IronOcr.Languages.Arabic.OcrLanguagePack,
                    Strategy = IronOcr.AdvancedOcr.OcrStrategy.Advanced,
                    ColorSpace = AdvancedOcr.OcrColorSpace.Color,
                    DetectWhiteTextOnDarkBackgrounds = true,
                    InputImageType = AdvancedOcr.InputTypes.Document,
                    RotateAndStraighten = true,
                    ReadBarCodes = true,
                    ColorDepth = 4
                };
                var Results = Ocr.Read(Path);
                AppendTextBox(Results.Text);
            }
            else if (lang == 4)
            {
                var Ocr = new AdvancedOcr()
                {

                    CleanBackgroundNoise = true,
                    EnhanceContrast = true,
                    EnhanceResolution = true,
                    Language = IronOcr.Languages.French.OcrLanguagePack,
                    Strategy = IronOcr.AdvancedOcr.OcrStrategy.Advanced,
                    ColorSpace = AdvancedOcr.OcrColorSpace.Color,
                    DetectWhiteTextOnDarkBackgrounds = true,
                    InputImageType = AdvancedOcr.InputTypes.Document,
                    RotateAndStraighten = true,
                    ReadBarCodes = true,
                    ColorDepth = 4
                };
                var Results = Ocr.Read(Path);
                AppendTextBox(Results.Text);
            }
          
        }
        public void MOrc(int lang)
        {

            if (lang == 1)
            {
                var Ocr = new AdvancedOcr()
                {
                    CleanBackgroundNoise = false,
                    EnhanceContrast = true,
                    EnhanceResolution = false,
                    Language = IronOcr.Languages.English.OcrLanguagePack,
                    Strategy = IronOcr.AdvancedOcr.OcrStrategy.Advanced,
                    ColorSpace = AdvancedOcr.OcrColorSpace.Color,
                    DetectWhiteTextOnDarkBackgrounds = false,
                    InputImageType = AdvancedOcr.InputTypes.Document,
                    RotateAndStraighten = false,
                    ReadBarCodes = false,
                    ColorDepth = 4
                };
                var Results = Ocr.Read(Path);
                AppendTextBox(Results.Text);

            }
            else if (lang == 2)
            {
                var Ocr = new AdvancedOcr()
                {

                    CleanBackgroundNoise = false,
                    EnhanceContrast = true,
                    EnhanceResolution = false,
                    Language = IronOcr.Languages.ChineseSimplified.OcrLanguagePack,
                    Strategy = IronOcr.AdvancedOcr.OcrStrategy.Advanced,
                    ColorSpace = AdvancedOcr.OcrColorSpace.Color,
                    DetectWhiteTextOnDarkBackgrounds = false,
                    InputImageType = AdvancedOcr.InputTypes.Document,
                    RotateAndStraighten = false,
                    ReadBarCodes = false,
                    ColorDepth = 4

                   
                };
                var Results = Ocr.Read(Path);
                AppendTextBox(Results.Text);
            }
            else if (lang == 3)
            {
                var Ocr = new AdvancedOcr()
                {
                    CleanBackgroundNoise = false,
                    EnhanceContrast = true,
                    EnhanceResolution = false,
                    Language = IronOcr.Languages.Arabic.OcrLanguagePack,
                    Strategy = IronOcr.AdvancedOcr.OcrStrategy.Advanced,
                    ColorSpace = AdvancedOcr.OcrColorSpace.Color,
                    DetectWhiteTextOnDarkBackgrounds = false,
                    InputImageType = AdvancedOcr.InputTypes.Document,
                    RotateAndStraighten = false,
                    ReadBarCodes = false,
                    ColorDepth = 4

                   
                };
                var Results = Ocr.Read(Path);
                AppendTextBox(Results.Text);
            }
            else if (lang == 4)
            {
                var Ocr = new AdvancedOcr()
                {
                    CleanBackgroundNoise = false,
                    EnhanceContrast = true,
                    EnhanceResolution = false,
                    Language = IronOcr.Languages.French.OcrLanguagePack,
                    Strategy = IronOcr.AdvancedOcr.OcrStrategy.Advanced,
                    ColorSpace = AdvancedOcr.OcrColorSpace.Color,
                    DetectWhiteTextOnDarkBackgrounds = false,
                    InputImageType = AdvancedOcr.InputTypes.Document,
                    RotateAndStraighten = false,
                    ReadBarCodes = false,
                    ColorDepth = 4

                   
                };
                var Results = Ocr.Read(Path);
                AppendTextBox(Results.Text);
            }

        }
        public void LOrc(int lang)
        {

            if (lang == 1)
            {
                var Ocr = new AdvancedOcr()
                {

                    CleanBackgroundNoise = true,
                    EnhanceContrast = true,
                    EnhanceResolution = true,
                    Language = IronOcr.Languages.English.OcrLanguagePack,
                    Strategy = IronOcr.AdvancedOcr.OcrStrategy.Advanced,
                    ColorSpace = AdvancedOcr.OcrColorSpace.GrayScale,
                    DetectWhiteTextOnDarkBackgrounds = false,
                    InputImageType = AdvancedOcr.InputTypes.Document,
                    RotateAndStraighten = true,
                    ReadBarCodes = false,
                    ColorDepth = 4

                };
                var Results = Ocr.Read(Path);
                AppendTextBox(Results.Text);

            }
            else if (lang == 2)
            {
                var Ocr = new AdvancedOcr()
                {

                    CleanBackgroundNoise = true,
                    EnhanceContrast = true,
                    EnhanceResolution = true,
                    Language = IronOcr.Languages.ChineseSimplified.OcrLanguagePack,
                    Strategy = IronOcr.AdvancedOcr.OcrStrategy.Advanced,
                    ColorSpace = AdvancedOcr.OcrColorSpace.GrayScale,
                    DetectWhiteTextOnDarkBackgrounds = false,
                    InputImageType = AdvancedOcr.InputTypes.Document,
                    RotateAndStraighten = true,
                    ReadBarCodes = false,
                    ColorDepth = 4


                };
                var Results = Ocr.Read(Path);
                AppendTextBox(Results.Text);
            }
            else if (lang == 3)
            {
                var Ocr = new AdvancedOcr()
                {
                    CleanBackgroundNoise = true,
                    EnhanceContrast = true,
                    EnhanceResolution = true,
                    Language = IronOcr.Languages.Arabic.OcrLanguagePack,
                    Strategy = IronOcr.AdvancedOcr.OcrStrategy.Advanced,
                    ColorSpace = AdvancedOcr.OcrColorSpace.GrayScale,
                    DetectWhiteTextOnDarkBackgrounds = false,
                    InputImageType = AdvancedOcr.InputTypes.Document,
                    RotateAndStraighten = true,
                    ReadBarCodes = false,
                    ColorDepth = 4

                 


                };
                var Results = Ocr.Read(Path);
                AppendTextBox(Results.Text);
            }
            else if (lang == 4)
            {
                var Ocr = new AdvancedOcr()
                {
                    CleanBackgroundNoise = true,
                    EnhanceContrast = true,
                    EnhanceResolution = true,
                    Language = IronOcr.Languages.French.OcrLanguagePack,
                    Strategy = IronOcr.AdvancedOcr.OcrStrategy.Advanced,
                    ColorSpace = AdvancedOcr.OcrColorSpace.GrayScale,
                    DetectWhiteTextOnDarkBackgrounds = false,
                    InputImageType = AdvancedOcr.InputTypes.Document,
                    RotateAndStraighten = true,
                    ReadBarCodes = false,
                    ColorDepth = 4
                
                };
                var Results = Ocr.Read(Path);
                AppendTextBox(Results.Text);
            }

        }
        public void SOrc(int lang)
        {

            if (lang == 1)
            {
                var Ocr = new AdvancedOcr()
                {

                    CleanBackgroundNoise = false,
                    EnhanceContrast = false,
                    EnhanceResolution = false,
                    Language = IronOcr.Languages.English.OcrLanguagePack,
                    Strategy = IronOcr.AdvancedOcr.OcrStrategy.Fast,
                    ColorSpace = AdvancedOcr.OcrColorSpace.GrayScale,
                    DetectWhiteTextOnDarkBackgrounds = false,
                    InputImageType = AdvancedOcr.InputTypes.Document,
                    RotateAndStraighten = false,
                    ReadBarCodes = false,
                    ColorDepth = 4

                };
                var Results = Ocr.Read(Path);
                AppendTextBox(Results.Text);

            }
            else if (lang == 2)
            {
                var Ocr = new AdvancedOcr()
                {
                    CleanBackgroundNoise = false,
                    EnhanceContrast = false,
                    EnhanceResolution = false,
                    Language = IronOcr.Languages.ChineseSimplified.OcrLanguagePack,
                    Strategy = IronOcr.AdvancedOcr.OcrStrategy.Fast,
                    ColorSpace = AdvancedOcr.OcrColorSpace.GrayScale,
                    DetectWhiteTextOnDarkBackgrounds = false,
                    InputImageType = AdvancedOcr.InputTypes.Document,
                    RotateAndStraighten = false,
                    ReadBarCodes = false,
                    ColorDepth = 4

               


                };
                var Results = Ocr.Read(Path);
                AppendTextBox(Results.Text);
            }
            else if (lang == 3)
            {
                var Ocr = new AdvancedOcr()
                {
                    CleanBackgroundNoise = false,
                    EnhanceContrast = false,
                    EnhanceResolution = false,
                    Language = IronOcr.Languages.Arabic.OcrLanguagePack,
                    Strategy = IronOcr.AdvancedOcr.OcrStrategy.Fast,
                    ColorSpace = AdvancedOcr.OcrColorSpace.GrayScale,
                    DetectWhiteTextOnDarkBackgrounds = false,
                    InputImageType = AdvancedOcr.InputTypes.Document,
                    RotateAndStraighten = false,
                    ReadBarCodes = false,
                    ColorDepth = 4





                };
                var Results = Ocr.Read(Path);
                AppendTextBox(Results.Text);
            }
            else if (lang == 4)
            {
                var Ocr = new AdvancedOcr()
                {
                    CleanBackgroundNoise = false,
                    EnhanceContrast = false,
                    EnhanceResolution = false,
                    Language = IronOcr.Languages.French.OcrLanguagePack,
                    Strategy = IronOcr.AdvancedOcr.OcrStrategy.Fast,
                    ColorSpace = AdvancedOcr.OcrColorSpace.GrayScale,
                    DetectWhiteTextOnDarkBackgrounds = false,
                    InputImageType = AdvancedOcr.InputTypes.Document,
                    RotateAndStraighten = false,
                    ReadBarCodes = false,
                    ColorDepth = 4

                

                };
                var Results = Ocr.Read(Path);
                AppendTextBox(Results.Text);
            }

        }
        public  void showGif() {
            loadingGif.Visible = true;
         
        }
        public void GetText() {
            if (!Path.Equals(""))
            {
                if (dr.Checked)
                {
                    if (er.Checked)
                    {



                        DOrc(1);
                    }
                    else if (cr.Checked)
                    {

                        DOrc(2);
                    }
                    else if (ar.Checked)
                    {

                        DOrc(3);
                    }
                    else if (fr.Checked)
                    {

                        DOrc(4);
                    }



                }
                else if (mr.Checked)
                {
                    if (er.Checked)
                    {

                        MOrc(1);
                    }
                    else if (cr.Checked)
                    {

                        MOrc(2);
                    }
                    else if (ar.Checked)
                    {

                        MOrc(3);
                    }
                    else if (fr.Checked)
                    {

                        MOrc(4);
                    }


                }
                else if (lr.Checked)
                {
                    if (er.Checked)
                    {

                        LOrc(1);
                    }
                    else if (cr.Checked)
                    {

                        LOrc(2);
                    }
                    else if (ar.Checked)
                    {

                        LOrc(3);
                    }
                    else if (fr.Checked)
                    {

                        LOrc(4);
                    }


                }
                else if (sr.Checked)
                {
                    if (er.Checked)
                    {

                        SOrc(1);
                    }
                    else if (cr.Checked)
                    {

                        SOrc(2);
                    }
                    else if (ar.Checked)
                    {

                        SOrc(3);
                    }
                    else if (fr.Checked)
                    {

                        SOrc(4);
                    }


                }
               
            }
            else
            {
                MessageBox.Show("You need to select an image", "Warning");
            }

        }
  

        public void show_MouseDown(object sender, MouseEventArgs e)
        {
            textBox.Text = "";
            showGif();
        } 
        public void show_MouseUp(object sender, MouseEventArgs e)
        {
            Task.Run(() => GetText());
        }


        private void textBox_TextChanged(object sender, EventArgs e)
        {
            loadingGif.Visible = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Process.Start("chrome.exe", "https://github.com/it2121");

        }
    }
}

