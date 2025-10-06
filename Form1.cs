using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.IO;
using System.Windows.Forms;

namespace osu_gen_cs
{
    public partial class Form1 : Form
    {
        private string fontPath = "";
        private string outputFolder = "";

        private Button btnFont;
        private Button btnFolder;
        private Button btnExport;
        private Label lblFont;
        private Label lblFolder;
        private CheckBox chkSD;
        private CheckBox chkHD;

        public Form1()
        {
            Init();
        }

        private void Init()
        {
            this.btnFont = new Button();
            this.btnFolder = new Button();
            this.btnExport = new Button();
            this.lblFont = new Label();
            this.lblFolder = new Label();
            this.chkSD = new CheckBox();
            this.chkHD = new CheckBox();

            this.SuspendLayout();

            // Select Font
            this.btnFont.Location = new Point(10, 10);
            this.btnFont.Size = new Size(100, 30);
            this.btnFont.Text = "Select Font";
            this.btnFont.Click += BtnFont_Click;

            // Select Folder
            this.btnFolder.Location = new Point(120, 10);
            this.btnFolder.Size = new Size(100, 30);
            this.btnFolder.Text = "Select Folder";
            this.btnFolder.Click += BtnFolder_Click;

            // Export
            this.btnExport.Location = new Point(230, 10);
            this.btnExport.Size = new Size(100, 30);
            this.btnExport.Text = "Export";
            this.btnExport.Click += BtnExport_Click;

            // Labels
            this.lblFont.Location = new Point(10, 50);
            this.lblFont.Size = new Size(800, 20);
            this.lblFont.Text = "Font: (none selected)";

            this.lblFolder.Location = new Point(10, 70);
            this.lblFolder.Size = new Size(800, 20);
            this.lblFolder.Text = "Output Folder: (none selected)";

            // Checkboxes
            this.chkSD.Location = new Point(10, 100);
            this.chkSD.Text = "Export normal (SD)";
            this.chkSD.Checked = true;

            this.chkHD.Location = new Point(150, 100);
            this.chkHD.Text = "Export @2x (HD)";

            // Form
            this.ClientSize = new Size(600, 140);
            this.Controls.Add(this.btnFont);
            this.Controls.Add(this.btnFolder);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.lblFont);
            this.Controls.Add(this.lblFolder);
            this.Controls.Add(this.chkSD);
            this.Controls.Add(this.chkHD);
            this.Text = "osu! Font Exporter";
            this.ResumeLayout(false);
        }

        private void BtnFont_Click(object sender, EventArgs e)
        {
            using var ofd = new OpenFileDialog();
            ofd.Filter = "Font Files (*.ttf;*.otf)|*.ttf;*.otf";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                fontPath = ofd.FileName;
                lblFont.Text = "Font: " + fontPath;
            }
        }

        private void BtnFolder_Click(object sender, EventArgs e)
        {
            using var fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                outputFolder = fbd.SelectedPath;
                lblFolder.Text = "Output Folder: " + outputFolder;
            }
        }

        private void BtnExport_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(fontPath) || string.IsNullOrEmpty(outputFolder))
            {
                MessageBox.Show("Please select font and folder first!");
                return;
            }

            var exportMap = new Dictionary<char, string[]>
            {
                ['0'] = new[] { "default-0.png", "combo-0.png", "score-0.png" },
                ['1'] = new[] { "default-1.png", "combo-1.png", "score-1.png" },
                ['2'] = new[] { "default-2.png", "combo-2.png", "score-2.png" },
                ['3'] = new[] { "default-3.png", "combo-3.png", "score-3.png" },
                ['4'] = new[] { "default-4.png", "combo-4.png", "score-4.png" },
                ['5'] = new[] { "default-5.png", "combo-5.png", "score-5.png" },
                ['6'] = new[] { "default-6.png", "combo-6.png", "score-6.png" },
                ['7'] = new[] { "default-7.png", "combo-7.png", "score-7.png" },
                ['8'] = new[] { "default-8.png", "combo-8.png", "score-8.png" },
                ['9'] = new[] { "default-9.png", "combo-9.png", "score-9.png" },
                [','] = new[] { "score-comma.png" },
                ['.'] = new[] { "score-dot.png" },
                ['x'] = new[] { "score-x.png", "combo-x.png" },
            };

            int baseFontSize = 64;

            using var pfc = new PrivateFontCollection();
            pfc.AddFontFile(fontPath);
            var family = pfc.Families[0];

            foreach (var kv in exportMap)
            {
                if (chkSD.Checked)
                {
                    var bmp = RenderGlyph(family, kv.Key, baseFontSize);
                    foreach (var name in kv.Value)
                    {
                        bmp.Save(Path.Combine(outputFolder, name), ImageFormat.Png);
                    }
                    bmp.Dispose();
                }

                if (chkHD.Checked)
                {
                    var bmp2x = RenderGlyph(family, kv.Key, baseFontSize * 2);
                    foreach (var name in kv.Value)
                    {
                        var hdName = Path.GetFileNameWithoutExtension(name) + "@2x.png";
                        bmp2x.Save(Path.Combine(outputFolder, hdName), ImageFormat.Png);
                    }
                    bmp2x.Dispose();
                }
            }

            MessageBox.Show("Export complete!");
        }

        private static Bitmap RenderGlyph(FontFamily family, char c, int size)
        {
            string text = c.ToString();
            using var font = new Font(family, size, FontStyle.Regular, GraphicsUnit.Pixel);

            // Measure text
            using var tmp = new Bitmap(1, 1);
            SizeF textSize;
            using (var g = Graphics.FromImage(tmp))
            {
                g.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
                textSize = g.MeasureString(text, font, PointF.Empty, StringFormat.GenericTypographic);
            }

            // Render
            var bmp = new Bitmap((int)Math.Ceiling(textSize.Width * 2), (int)Math.Ceiling(textSize.Height * 2), PixelFormat.Format32bppArgb);
            using (var g = Graphics.FromImage(bmp))
            {
                g.Clear(Color.Transparent);
                g.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
                g.DrawString(text, font, Brushes.White, 0, 0, StringFormat.GenericTypographic);
            }

            return TrimBitmap(bmp);
        }

        private static Bitmap TrimBitmap(Bitmap source)
        {
            int minX = source.Width, minY = source.Height, maxX = 0, maxY = 0;
            bool hasAlpha = false;

            for (int y = 0; y < source.Height; y++)
            {
                for (int x = 0; x < source.Width; x++)
                {
                    var c = source.GetPixel(x, y);
                    if (c.A > 0)
                    {
                        hasAlpha = true;
                        if (x < minX) minX = x;
                        if (y < minY) minY = y;
                        if (x > maxX) maxX = x;
                        if (y > maxY) maxY = y;
                    }
                }
            }

            if (!hasAlpha)
                return new Bitmap(1, 1);

            int w = maxX - minX + 1;
            int h = maxY - minY + 1;
            var rect = new Rectangle(minX, minY, w, h);
            return source.Clone(rect, PixelFormat.Format32bppArgb);
        }
    }
}
