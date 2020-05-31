using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace VCNDSLayout
{
    public partial class FormOptions : Form
    {
        public int Bilinear;
        public int RenderScale;
        public int PixelArtUpscaler;
        public int Brightness;
        public bool FoldOnPause;
        public int FoldOnResumeFadeFromBlackDuration;
        public int FoldOnPauseTimeout;

        private BufferedGraphics Preview;
        private Bitmap PreviewImg;

        public FormOptions()
        {
            InitializeComponent();

            Bilinear = 0;
            RenderScale = 1;
            PixelArtUpscaler = 0;
            Brightness = 100;
            FoldOnPause = false;
            FoldOnResumeFadeFromBlackDuration = 1000;
            FoldOnPauseTimeout = 3000;

            string previewImgPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "VCNDSLayoutEditor", "Preview.png");
            PreviewImg = new Bitmap(previewImgPath);
        }

        private void FormBrightness_Load(object sender, EventArgs e)
        {
            if (Bilinear >= numericUpDownBilinear.Minimum && Bilinear <= numericUpDownBilinear.Maximum)
                numericUpDownBilinear.Value = Bilinear;
            if (RenderScale >= numericUpDownRenderScale.Minimum && RenderScale <= numericUpDownRenderScale.Maximum)
                numericUpDownRenderScale.Value = RenderScale;
            if (PixelArtUpscaler >= numericUpDownPixelArtUpscaler.Minimum && PixelArtUpscaler <= numericUpDownPixelArtUpscaler.Maximum)
                numericUpDownPixelArtUpscaler.Value = PixelArtUpscaler;
            if (Brightness >= trackBarBrightness.Minimum && Brightness <= trackBarBrightness.Maximum)
                trackBarBrightness.Value = Brightness;
            checkBoxFoldOnPause.Checked = FoldOnPause;
            if (FoldOnResumeFadeFromBlackDuration >= numericUpDownResumeFadeFromBlackDuration.Minimum && FoldOnResumeFadeFromBlackDuration <= numericUpDownResumeFadeFromBlackDuration.Maximum)
                numericUpDownResumeFadeFromBlackDuration.Value = FoldOnResumeFadeFromBlackDuration;
            if (FoldOnPauseTimeout >= numericUpDownPauseTimeout.Minimum && FoldOnPauseTimeout <= numericUpDownPauseTimeout.Maximum)
                numericUpDownPauseTimeout.Value = FoldOnPauseTimeout;

            labelBrightnessValue.Text = trackBarBrightness.Value.ToString() + "%";

            int alpha = (int)((100.0 - trackBarBrightness.Value) / 100.0 * 255.0);

            Bitmap img = new Bitmap(panelPreview.Width, panelPreview.Height);
            panelPreview.BackgroundImage = img;
            Preview = BufferedGraphicsManager.Current.Allocate(Graphics.FromImage(img), new Rectangle(0, 0, panelPreview.Width, panelPreview.Height));
            Preview.Graphics.DrawImage(PreviewImg, 0, 0, panelPreview.Width, panelPreview.Height);
            Preview.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(alpha, 0, 0, 0)), 0, 0, panelPreview.Width, panelPreview.Height);
            Preview.Render();
            panelPreview.Refresh();
        }

        private void trackBar_Scroll(object sender, EventArgs e)
        {
            labelBrightnessValue.Text = trackBarBrightness.Value.ToString() + "%";

            int alpha = (int)((100.0 - trackBarBrightness.Value) / 100.0 * 255.0);

            if (panelPreview.BackgroundImage != null)
            {
                panelPreview.BackgroundImage.Dispose();
                panelPreview.BackgroundImage = null;
                panelPreview.Refresh();
            }

            Preview.Dispose();
            Preview = BufferedGraphicsManager.Current.Allocate(panelPreview.CreateGraphics(), new Rectangle(0, 0, panelPreview.Width, panelPreview.Height));

            Preview.Graphics.DrawImage(PreviewImg, 0, 0, panelPreview.Width, panelPreview.Height);
            Preview.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(alpha, 0, 0, 0)), 0, 0, panelPreview.Width, panelPreview.Height);
            Preview.Render();
        }

        private void buttonAccept_Click(object sender, EventArgs e)
        {
            Bilinear = (int)numericUpDownBilinear.Value;
            RenderScale = (int)numericUpDownRenderScale.Value;
            PixelArtUpscaler = (int)numericUpDownPixelArtUpscaler.Value;
            Brightness = trackBarBrightness.Value;
            FoldOnPause = checkBoxFoldOnPause.Checked;
            FoldOnResumeFadeFromBlackDuration = (int)numericUpDownResumeFadeFromBlackDuration.Value;
            FoldOnPauseTimeout = (int)numericUpDownPauseTimeout.Value;

            Preview.Dispose();
            PreviewImg.Dispose();
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
