using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace GosArchive.Model
{
    public class Converter2
    {
        public async Task ConvertToImage(string fileLink)
        {
            try
            {
                string tortf = ReadPdfFile(fileLink);
                RichTextBox rtf = new RichTextBox();
                rtf.Document.Blocks.Clear();
                rtf.Document.Blocks.Add(new Paragraph(new Run(tortf)));
                string path = System.IO.Path.GetDirectoryName(fileLink);

                if (!Directory.Exists(path))
                {
                    DirectoryInfo di = Directory.CreateDirectory(path);
                }
                Spire.Pdf.PdfDocument pdf = new Spire.Pdf.PdfDocument();

                await Task.Run(() =>
                {
                    pdf.LoadFromFile(fileLink);
                    System.Drawing.Size size = new System.Drawing.Size(650, 1080);
                    for (int i = 0; i <= pdf.Pages.Count; i++)
                    {
                        try
                        {
                            var source = pdf.SaveAsImage(i);
                            Bitmap bitmapImage = (Bitmap) source; 
                            var result = new Bitmap(Resize(bitmapImage, size, ImageFormat.Png));
                            result.Save(path + @"\page" + i.ToString() + ".png", ImageFormat.Png);
                        }
                        catch
                        {

                        }
                    }
                });
            }
            catch (Exception r)
            {
                MessageBox.Show(r.Message);
            }
        }

        public static Bitmap Resize(Bitmap imgPhoto, System.Drawing.Size objSize, ImageFormat enuType)
        {
            int sourceWidth = imgPhoto.Width;
            int sourceHeight = imgPhoto.Height;
            int sourceX = 0;
            int sourceY = 0;

            int destX = 0;
            int destY = 0;
            int destWidth = objSize.Width;
            int destHeight = objSize.Height;

            Bitmap bmPhoto;
            if (enuType == ImageFormat.Png)
                bmPhoto = new Bitmap(destWidth, destHeight, PixelFormat.Format32bppArgb);
            else if (enuType == ImageFormat.Gif)
                bmPhoto = new Bitmap(destWidth, destHeight); //PixelFormat.Format8bppIndexed should be the right value for a GIF, but will throw an error with some GIF images so it's not safe to specify.
            else
                bmPhoto = new Bitmap(destWidth, destHeight, PixelFormat.Format24bppRgb);

            //For some reason the resolution properties will be 96, even when the source image is different, so this matching does not appear to be reliable.
            //bmPhoto.SetResolution(imgPhoto.HorizontalResolution, imgPhoto.VerticalResolution);

            //If you want to override the default 96dpi resolution do it here
            //bmPhoto.SetResolution(72, 72);

            Graphics grPhoto = Graphics.FromImage(bmPhoto);
            grPhoto.InterpolationMode = InterpolationMode.HighQualityBicubic;
            grPhoto.DrawImage(imgPhoto,
                new Rectangle(destX, destY, destWidth, destHeight),
                new Rectangle(sourceX, sourceY, sourceWidth, sourceHeight),
                GraphicsUnit.Pixel);

            grPhoto.Dispose();
            return bmPhoto;
        }


        public string ReadPdfFile(object Filename)
        {
            PdfReader reader2 = new PdfReader((string)Filename);
            string strText = string.Empty;

            for (int page = 1; page <= reader2.NumberOfPages; page++)
            {
                ITextExtractionStrategy its = new iTextSharp.text.pdf.parser.SimpleTextExtractionStrategy();
                PdfReader reader = new PdfReader((string)Filename);
                String s = PdfTextExtractor.GetTextFromPage(reader, page, its);
                s = Encoding.UTF8.GetString(ASCIIEncoding.Convert(Encoding.Default, Encoding.UTF8, Encoding.Default.GetBytes(s)));
                strText = strText + s;
                reader.Close();
            }
            return strText;
        }
    }
}
