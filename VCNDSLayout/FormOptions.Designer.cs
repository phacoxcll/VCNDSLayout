namespace VCNDSLayout
{
    partial class FormOptions
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormOptions));
            this.trackBarBrightness = new System.Windows.Forms.TrackBar();
            this.labelBrightnessValue = new System.Windows.Forms.Label();
            this.panelPreview = new System.Windows.Forms.Panel();
            this.buttonAccept = new System.Windows.Forms.Button();
            this.checkBoxFoldOnPause = new System.Windows.Forms.CheckBox();
            this.numericUpDownBilinear = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownRenderScale = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownPixelArtUpscaler = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownResumeFadeFromBlackDuration = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownPauseTimeout = new System.Windows.Forms.NumericUpDown();
            this.labelBilinear = new System.Windows.Forms.Label();
            this.labelRenderScale = new System.Windows.Forms.Label();
            this.labelPixelArtUpscaler = new System.Windows.Forms.Label();
            this.labelResumeFadeFromBlackDuration = new System.Windows.Forms.Label();
            this.labelPauseTimeout = new System.Windows.Forms.Label();
            this.labelBrightness = new System.Windows.Forms.Label();
            this.groupBox3DRendering = new System.Windows.Forms.GroupBox();
            this.groupBoxDisplay = new System.Windows.Forms.GroupBox();
            this.groupBoxArguments = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarBrightness)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownBilinear)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRenderScale)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPixelArtUpscaler)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownResumeFadeFromBlackDuration)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPauseTimeout)).BeginInit();
            this.groupBox3DRendering.SuspendLayout();
            this.groupBoxDisplay.SuspendLayout();
            this.groupBoxArguments.SuspendLayout();
            this.SuspendLayout();
            // 
            // trackBarBrightness
            // 
            this.trackBarBrightness.Location = new System.Drawing.Point(71, 45);
            this.trackBarBrightness.Maximum = 100;
            this.trackBarBrightness.Name = "trackBarBrightness";
            this.trackBarBrightness.Size = new System.Drawing.Size(293, 45);
            this.trackBarBrightness.TabIndex = 0;
            this.trackBarBrightness.TickFrequency = 5;
            this.trackBarBrightness.Scroll += new System.EventHandler(this.trackBar_Scroll);
            // 
            // labelBrightnessValue
            // 
            this.labelBrightnessValue.AutoSize = true;
            this.labelBrightnessValue.Location = new System.Drawing.Point(370, 53);
            this.labelBrightnessValue.Name = "labelBrightnessValue";
            this.labelBrightnessValue.Size = new System.Drawing.Size(33, 13);
            this.labelBrightnessValue.TabIndex = 1;
            this.labelBrightnessValue.Text = "100%";
            // 
            // panelPreview
            // 
            this.panelPreview.Location = new System.Drawing.Point(6, 77);
            this.panelPreview.Name = "panelPreview";
            this.panelPreview.Size = new System.Drawing.Size(400, 225);
            this.panelPreview.TabIndex = 2;
            // 
            // buttonAccept
            // 
            this.buttonAccept.Location = new System.Drawing.Point(349, 427);
            this.buttonAccept.Name = "buttonAccept";
            this.buttonAccept.Size = new System.Drawing.Size(75, 23);
            this.buttonAccept.TabIndex = 3;
            this.buttonAccept.Text = "OK";
            this.buttonAccept.UseVisualStyleBackColor = true;
            this.buttonAccept.Click += new System.EventHandler(this.buttonAccept_Click);
            // 
            // checkBoxFoldOnPause
            // 
            this.checkBoxFoldOnPause.AutoSize = true;
            this.checkBoxFoldOnPause.Location = new System.Drawing.Point(9, 19);
            this.checkBoxFoldOnPause.Name = "checkBoxFoldOnPause";
            this.checkBoxFoldOnPause.Size = new System.Drawing.Size(93, 17);
            this.checkBoxFoldOnPause.TabIndex = 4;
            this.checkBoxFoldOnPause.Text = "Fold on pause";
            this.checkBoxFoldOnPause.UseVisualStyleBackColor = true;
            // 
            // numericUpDownBilinear
            // 
            this.numericUpDownBilinear.Location = new System.Drawing.Point(85, 19);
            this.numericUpDownBilinear.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownBilinear.Name = "numericUpDownBilinear";
            this.numericUpDownBilinear.Size = new System.Drawing.Size(46, 20);
            this.numericUpDownBilinear.TabIndex = 5;
            // 
            // numericUpDownRenderScale
            // 
            this.numericUpDownRenderScale.Location = new System.Drawing.Point(85, 45);
            this.numericUpDownRenderScale.Maximum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numericUpDownRenderScale.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownRenderScale.Name = "numericUpDownRenderScale";
            this.numericUpDownRenderScale.Size = new System.Drawing.Size(46, 20);
            this.numericUpDownRenderScale.TabIndex = 6;
            this.numericUpDownRenderScale.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // numericUpDownPixelArtUpscaler
            // 
            this.numericUpDownPixelArtUpscaler.Location = new System.Drawing.Point(102, 19);
            this.numericUpDownPixelArtUpscaler.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownPixelArtUpscaler.Name = "numericUpDownPixelArtUpscaler";
            this.numericUpDownPixelArtUpscaler.Size = new System.Drawing.Size(46, 20);
            this.numericUpDownPixelArtUpscaler.TabIndex = 7;
            // 
            // numericUpDownResumeFadeFromBlackDuration
            // 
            this.numericUpDownResumeFadeFromBlackDuration.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numericUpDownResumeFadeFromBlackDuration.Location = new System.Drawing.Point(211, 42);
            this.numericUpDownResumeFadeFromBlackDuration.Maximum = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            this.numericUpDownResumeFadeFromBlackDuration.Name = "numericUpDownResumeFadeFromBlackDuration";
            this.numericUpDownResumeFadeFromBlackDuration.Size = new System.Drawing.Size(46, 20);
            this.numericUpDownResumeFadeFromBlackDuration.TabIndex = 9;
            this.numericUpDownResumeFadeFromBlackDuration.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            // 
            // numericUpDownPauseTimeout
            // 
            this.numericUpDownPauseTimeout.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numericUpDownPauseTimeout.Location = new System.Drawing.Point(211, 68);
            this.numericUpDownPauseTimeout.Maximum = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            this.numericUpDownPauseTimeout.Name = "numericUpDownPauseTimeout";
            this.numericUpDownPauseTimeout.Size = new System.Drawing.Size(46, 20);
            this.numericUpDownPauseTimeout.TabIndex = 10;
            this.numericUpDownPauseTimeout.Value = new decimal(new int[] {
            3000,
            0,
            0,
            0});
            // 
            // labelBilinear
            // 
            this.labelBilinear.AutoSize = true;
            this.labelBilinear.Location = new System.Drawing.Point(6, 21);
            this.labelBilinear.Name = "labelBilinear";
            this.labelBilinear.Size = new System.Drawing.Size(44, 13);
            this.labelBilinear.TabIndex = 11;
            this.labelBilinear.Text = "Bilinear:";
            // 
            // labelRenderScale
            // 
            this.labelRenderScale.AutoSize = true;
            this.labelRenderScale.Location = new System.Drawing.Point(6, 47);
            this.labelRenderScale.Name = "labelRenderScale";
            this.labelRenderScale.Size = new System.Drawing.Size(73, 13);
            this.labelRenderScale.TabIndex = 12;
            this.labelRenderScale.Text = "Render scale:";
            // 
            // labelPixelArtUpscaler
            // 
            this.labelPixelArtUpscaler.AutoSize = true;
            this.labelPixelArtUpscaler.Location = new System.Drawing.Point(6, 21);
            this.labelPixelArtUpscaler.Name = "labelPixelArtUpscaler";
            this.labelPixelArtUpscaler.Size = new System.Drawing.Size(90, 13);
            this.labelPixelArtUpscaler.TabIndex = 13;
            this.labelPixelArtUpscaler.Text = "Pixel art upscaler:";
            // 
            // labelResumeFadeFromBlackDuration
            // 
            this.labelResumeFadeFromBlackDuration.AutoSize = true;
            this.labelResumeFadeFromBlackDuration.Location = new System.Drawing.Point(6, 44);
            this.labelResumeFadeFromBlackDuration.Name = "labelResumeFadeFromBlackDuration";
            this.labelResumeFadeFromBlackDuration.Size = new System.Drawing.Size(199, 13);
            this.labelResumeFadeFromBlackDuration.TabIndex = 15;
            this.labelResumeFadeFromBlackDuration.Text = "Fold on resume fade from black duration:";
            // 
            // labelPauseTimeout
            // 
            this.labelPauseTimeout.AutoSize = true;
            this.labelPauseTimeout.Location = new System.Drawing.Point(6, 70);
            this.labelPauseTimeout.Name = "labelPauseTimeout";
            this.labelPauseTimeout.Size = new System.Drawing.Size(114, 13);
            this.labelPauseTimeout.TabIndex = 16;
            this.labelPauseTimeout.Text = "Fold on pause timeout:";
            // 
            // labelBrightness
            // 
            this.labelBrightness.AutoSize = true;
            this.labelBrightness.Location = new System.Drawing.Point(6, 53);
            this.labelBrightness.Name = "labelBrightness";
            this.labelBrightness.Size = new System.Drawing.Size(59, 13);
            this.labelBrightness.TabIndex = 17;
            this.labelBrightness.Text = "Brightness:";
            // 
            // groupBox3DRendering
            // 
            this.groupBox3DRendering.Controls.Add(this.labelBilinear);
            this.groupBox3DRendering.Controls.Add(this.numericUpDownBilinear);
            this.groupBox3DRendering.Controls.Add(this.numericUpDownRenderScale);
            this.groupBox3DRendering.Controls.Add(this.labelRenderScale);
            this.groupBox3DRendering.Location = new System.Drawing.Point(12, 12);
            this.groupBox3DRendering.Name = "groupBox3DRendering";
            this.groupBox3DRendering.Size = new System.Drawing.Size(140, 75);
            this.groupBox3DRendering.TabIndex = 18;
            this.groupBox3DRendering.TabStop = false;
            this.groupBox3DRendering.Text = "3DRendering";
            // 
            // groupBoxDisplay
            // 
            this.groupBoxDisplay.Controls.Add(this.panelPreview);
            this.groupBoxDisplay.Controls.Add(this.labelPixelArtUpscaler);
            this.groupBoxDisplay.Controls.Add(this.numericUpDownPixelArtUpscaler);
            this.groupBoxDisplay.Controls.Add(this.labelBrightness);
            this.groupBoxDisplay.Controls.Add(this.trackBarBrightness);
            this.groupBoxDisplay.Controls.Add(this.labelBrightnessValue);
            this.groupBoxDisplay.Location = new System.Drawing.Point(12, 112);
            this.groupBoxDisplay.Name = "groupBoxDisplay";
            this.groupBoxDisplay.Size = new System.Drawing.Size(412, 309);
            this.groupBoxDisplay.TabIndex = 19;
            this.groupBoxDisplay.TabStop = false;
            this.groupBoxDisplay.Text = "Display";
            // 
            // groupBoxArguments
            // 
            this.groupBoxArguments.Controls.Add(this.checkBoxFoldOnPause);
            this.groupBoxArguments.Controls.Add(this.numericUpDownResumeFadeFromBlackDuration);
            this.groupBoxArguments.Controls.Add(this.numericUpDownPauseTimeout);
            this.groupBoxArguments.Controls.Add(this.labelResumeFadeFromBlackDuration);
            this.groupBoxArguments.Controls.Add(this.labelPauseTimeout);
            this.groupBoxArguments.Location = new System.Drawing.Point(158, 12);
            this.groupBoxArguments.Name = "groupBoxArguments";
            this.groupBoxArguments.Size = new System.Drawing.Size(266, 98);
            this.groupBoxArguments.TabIndex = 20;
            this.groupBoxArguments.TabStop = false;
            this.groupBoxArguments.Text = "Arguments";
            // 
            // FormOptions
            // 
            this.AcceptButton = this.buttonAccept;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(436, 461);
            this.Controls.Add(this.groupBoxArguments);
            this.Controls.Add(this.groupBoxDisplay);
            this.Controls.Add(this.groupBox3DRendering);
            this.Controls.Add(this.buttonAccept);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(452, 500);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(452, 500);
            this.Name = "FormOptions";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Options";
            this.Load += new System.EventHandler(this.FormBrightness_Load);
            ((System.ComponentModel.ISupportInitialize)(this.trackBarBrightness)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownBilinear)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRenderScale)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPixelArtUpscaler)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownResumeFadeFromBlackDuration)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPauseTimeout)).EndInit();
            this.groupBox3DRendering.ResumeLayout(false);
            this.groupBox3DRendering.PerformLayout();
            this.groupBoxDisplay.ResumeLayout(false);
            this.groupBoxDisplay.PerformLayout();
            this.groupBoxArguments.ResumeLayout(false);
            this.groupBoxArguments.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TrackBar trackBarBrightness;
        private System.Windows.Forms.Label labelBrightnessValue;
        private System.Windows.Forms.Panel panelPreview;
        private System.Windows.Forms.Button buttonAccept;
        private System.Windows.Forms.CheckBox checkBoxFoldOnPause;
        private System.Windows.Forms.NumericUpDown numericUpDownBilinear;
        private System.Windows.Forms.NumericUpDown numericUpDownRenderScale;
        private System.Windows.Forms.NumericUpDown numericUpDownPixelArtUpscaler;
        private System.Windows.Forms.NumericUpDown numericUpDownResumeFadeFromBlackDuration;
        private System.Windows.Forms.NumericUpDown numericUpDownPauseTimeout;
        private System.Windows.Forms.Label labelBilinear;
        private System.Windows.Forms.Label labelRenderScale;
        private System.Windows.Forms.Label labelPixelArtUpscaler;
        private System.Windows.Forms.Label labelResumeFadeFromBlackDuration;
        private System.Windows.Forms.Label labelPauseTimeout;
        private System.Windows.Forms.Label labelBrightness;
        private System.Windows.Forms.GroupBox groupBox3DRendering;
        private System.Windows.Forms.GroupBox groupBoxDisplay;
        private System.Windows.Forms.GroupBox groupBoxArguments;
    }
}