namespace VCNDSLayout
{
    partial class FormEditor
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormEditor));
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.importToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.layoutsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.organizeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panelPreview = new System.Windows.Forms.Panel();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.showFullPreviewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.savePreviewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBoxUpper = new System.Windows.Forms.GroupBox();
            this.panelUpperRight = new System.Windows.Forms.Panel();
            this.buttonUpperBackground = new System.Windows.Forms.Button();
            this.panelUpperStand = new System.Windows.Forms.Panel();
            this.labelUpperSize = new System.Windows.Forms.Label();
            this.panelUpperLeft = new System.Windows.Forms.Panel();
            this.panelUpperPin = new System.Windows.Forms.Panel();
            this.labelUpperScale = new System.Windows.Forms.Label();
            this.labelUpperY = new System.Windows.Forms.Label();
            this.labelUpperX = new System.Windows.Forms.Label();
            this.numericUpDownUpperX = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownUpperScale = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownUpperY = new System.Windows.Forms.NumericUpDown();
            this.groupBoxLower = new System.Windows.Forms.GroupBox();
            this.buttonLowerBackground = new System.Windows.Forms.Button();
            this.panelLowerRight = new System.Windows.Forms.Panel();
            this.labelLowerSize = new System.Windows.Forms.Label();
            this.panelLowerPin = new System.Windows.Forms.Panel();
            this.labelLowerScale = new System.Windows.Forms.Label();
            this.panelLowerStand = new System.Windows.Forms.Panel();
            this.labelLowerY = new System.Windows.Forms.Label();
            this.labelLowerX = new System.Windows.Forms.Label();
            this.panelLowerLeft = new System.Windows.Forms.Panel();
            this.numericUpDownLowerX = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownLowerScale = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownLowerY = new System.Windows.Forms.NumericUpDown();
            this.checkBoxOnlyIntegerScales = new System.Windows.Forms.CheckBox();
            this.buttonBackground = new System.Windows.Forms.Button();
            this.comboBoxLayout = new System.Windows.Forms.ComboBox();
            this.labelLayout = new System.Windows.Forms.Label();
            this.groupBoxLayout = new System.Windows.Forms.GroupBox();
            this.buttonSwapNDSScreens = new System.Windows.Forms.Button();
            this.checkBoxLanguages = new System.Windows.Forms.CheckBox();
            this.labelLanguage = new System.Windows.Forms.Label();
            this.comboBoxLanguage = new System.Windows.Forms.ComboBox();
            this.labelName = new System.Windows.Forms.Label();
            this.labelDescription = new System.Windows.Forms.Label();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.textBoxDescription = new System.Windows.Forms.TextBox();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.comboBoxTarget = new System.Windows.Forms.ComboBox();
            this.labelTarget = new System.Windows.Forms.Label();
            this.comboBoxPad = new System.Windows.Forms.ComboBox();
            this.comboBoxDRC = new System.Windows.Forms.ComboBox();
            this.comboBoxButtons = new System.Windows.Forms.ComboBox();
            this.groupBoxRotations = new System.Windows.Forms.GroupBox();
            this.labelButtons = new System.Windows.Forms.Label();
            this.labelDRC = new System.Windows.Forms.Label();
            this.labelPad = new System.Windows.Forms.Label();
            this.menuStrip.SuspendLayout();
            this.contextMenuStrip.SuspendLayout();
            this.groupBoxUpper.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownUpperX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownUpperScale)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownUpperY)).BeginInit();
            this.groupBoxLower.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownLowerX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownLowerScale)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownLowerY)).BeginInit();
            this.groupBoxLayout.SuspendLayout();
            this.groupBoxRotations.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.layoutsToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(803, 24);
            this.menuStrip.TabIndex = 0;
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.toolStripSeparator1,
            this.importToolStripMenuItem,
            this.exportToolStripMenuItem,
            this.toolStripSeparator2,
            this.closeToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
            this.newToolStripMenuItem.Text = "New";
            this.newToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
            this.saveAsToolStripMenuItem.Text = "Save as...";
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(118, 6);
            // 
            // importToolStripMenuItem
            // 
            this.importToolStripMenuItem.Name = "importToolStripMenuItem";
            this.importToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
            this.importToolStripMenuItem.Text = "Import";
            this.importToolStripMenuItem.Click += new System.EventHandler(this.importToolStripMenuItem_Click);
            // 
            // exportToolStripMenuItem
            // 
            this.exportToolStripMenuItem.Name = "exportToolStripMenuItem";
            this.exportToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
            this.exportToolStripMenuItem.Text = "Export";
            this.exportToolStripMenuItem.Click += new System.EventHandler(this.exportToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(118, 6);
            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            this.closeToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
            this.closeToolStripMenuItem.Text = "Close";
            this.closeToolStripMenuItem.Click += new System.EventHandler(this.closeToolStripMenuItem_Click);
            // 
            // layoutsToolStripMenuItem
            // 
            this.layoutsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.organizeToolStripMenuItem,
            this.optionsToolStripMenuItem});
            this.layoutsToolStripMenuItem.Name = "layoutsToolStripMenuItem";
            this.layoutsToolStripMenuItem.Size = new System.Drawing.Size(60, 20);
            this.layoutsToolStripMenuItem.Text = "Layouts";
            // 
            // organizeToolStripMenuItem
            // 
            this.organizeToolStripMenuItem.Name = "organizeToolStripMenuItem";
            this.organizeToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
            this.organizeToolStripMenuItem.Text = "Organize";
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
            this.optionsToolStripMenuItem.Text = "Options";
            this.optionsToolStripMenuItem.Click += new System.EventHandler(this.optionsToolStripMenuItem_Click);
            // 
            // panelPreview
            // 
            this.panelPreview.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(42)))));
            this.panelPreview.ContextMenuStrip = this.contextMenuStrip;
            this.panelPreview.Location = new System.Drawing.Point(131, 19);
            this.panelPreview.Name = "panelPreview";
            this.panelPreview.Size = new System.Drawing.Size(640, 360);
            this.panelPreview.TabIndex = 1;
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showFullPreviewToolStripMenuItem,
            this.savePreviewToolStripMenuItem});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(168, 48);
            // 
            // showFullPreviewToolStripMenuItem
            // 
            this.showFullPreviewToolStripMenuItem.Name = "showFullPreviewToolStripMenuItem";
            this.showFullPreviewToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.showFullPreviewToolStripMenuItem.Text = "Show full preview";
            this.showFullPreviewToolStripMenuItem.Click += new System.EventHandler(this.showFullPreviewToolStripMenuItem_Click);
            // 
            // savePreviewToolStripMenuItem
            // 
            this.savePreviewToolStripMenuItem.Name = "savePreviewToolStripMenuItem";
            this.savePreviewToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.savePreviewToolStripMenuItem.Text = "Save preview...";
            this.savePreviewToolStripMenuItem.Click += new System.EventHandler(this.savePreviewToolStripMenuItem_Click);
            // 
            // groupBoxUpper
            // 
            this.groupBoxUpper.Controls.Add(this.panelUpperRight);
            this.groupBoxUpper.Controls.Add(this.buttonUpperBackground);
            this.groupBoxUpper.Controls.Add(this.panelUpperStand);
            this.groupBoxUpper.Controls.Add(this.labelUpperSize);
            this.groupBoxUpper.Controls.Add(this.panelUpperLeft);
            this.groupBoxUpper.Controls.Add(this.panelUpperPin);
            this.groupBoxUpper.Controls.Add(this.labelUpperScale);
            this.groupBoxUpper.Controls.Add(this.labelUpperY);
            this.groupBoxUpper.Controls.Add(this.labelUpperX);
            this.groupBoxUpper.Controls.Add(this.numericUpDownUpperX);
            this.groupBoxUpper.Controls.Add(this.numericUpDownUpperScale);
            this.groupBoxUpper.Controls.Add(this.numericUpDownUpperY);
            this.groupBoxUpper.Location = new System.Drawing.Point(6, 19);
            this.groupBoxUpper.Name = "groupBoxUpper";
            this.groupBoxUpper.Size = new System.Drawing.Size(117, 149);
            this.groupBoxUpper.TabIndex = 17;
            this.groupBoxUpper.TabStop = false;
            this.groupBoxUpper.Text = "Upper";
            // 
            // panelUpperRight
            // 
            this.panelUpperRight.BackgroundImage = global::VCNDSLayout.Properties.Resources.Right;
            this.panelUpperRight.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.panelUpperRight.Location = new System.Drawing.Point(55, 19);
            this.panelUpperRight.Name = "panelUpperRight";
            this.panelUpperRight.Size = new System.Drawing.Size(23, 23);
            this.panelUpperRight.TabIndex = 16;
            this.panelUpperRight.Click += new System.EventHandler(this.panelUpperRight_Click);
            // 
            // buttonUpperBackground
            // 
            this.buttonUpperBackground.Location = new System.Drawing.Point(86, 121);
            this.buttonUpperBackground.Name = "buttonUpperBackground";
            this.buttonUpperBackground.Size = new System.Drawing.Size(24, 23);
            this.buttonUpperBackground.TabIndex = 28;
            this.buttonUpperBackground.Text = "...";
            this.buttonUpperBackground.UseVisualStyleBackColor = true;
            this.buttonUpperBackground.Click += new System.EventHandler(this.buttonUpperBackground_Click);
            // 
            // panelUpperStand
            // 
            this.panelUpperStand.BackgroundImage = global::VCNDSLayout.Properties.Resources.Stand;
            this.panelUpperStand.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.panelUpperStand.Location = new System.Drawing.Point(32, 19);
            this.panelUpperStand.Name = "panelUpperStand";
            this.panelUpperStand.Size = new System.Drawing.Size(23, 23);
            this.panelUpperStand.TabIndex = 15;
            this.panelUpperStand.Click += new System.EventHandler(this.panelUpperStand_Click);
            // 
            // labelUpperSize
            // 
            this.labelUpperSize.AutoSize = true;
            this.labelUpperSize.Location = new System.Drawing.Point(6, 127);
            this.labelUpperSize.Name = "labelUpperSize";
            this.labelUpperSize.Size = new System.Drawing.Size(48, 13);
            this.labelUpperSize.TabIndex = 25;
            this.labelUpperSize.Text = "256x192";
            // 
            // panelUpperLeft
            // 
            this.panelUpperLeft.BackgroundImage = global::VCNDSLayout.Properties.Resources.Left;
            this.panelUpperLeft.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.panelUpperLeft.Location = new System.Drawing.Point(9, 19);
            this.panelUpperLeft.Name = "panelUpperLeft";
            this.panelUpperLeft.Size = new System.Drawing.Size(23, 23);
            this.panelUpperLeft.TabIndex = 14;
            this.panelUpperLeft.Click += new System.EventHandler(this.panelUpperLeft_Click);
            // 
            // panelUpperPin
            // 
            this.panelUpperPin.BackgroundImage = global::VCNDSLayout.Properties.Resources.Pins;
            this.panelUpperPin.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.panelUpperPin.Location = new System.Drawing.Point(86, 19);
            this.panelUpperPin.Name = "panelUpperPin";
            this.panelUpperPin.Size = new System.Drawing.Size(23, 19);
            this.panelUpperPin.TabIndex = 13;
            this.panelUpperPin.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelUpperPin_MouseDown);
            // 
            // labelUpperScale
            // 
            this.labelUpperScale.AutoSize = true;
            this.labelUpperScale.Location = new System.Drawing.Point(6, 100);
            this.labelUpperScale.Name = "labelUpperScale";
            this.labelUpperScale.Size = new System.Drawing.Size(37, 13);
            this.labelUpperScale.TabIndex = 12;
            this.labelUpperScale.Text = "Scale:";
            // 
            // labelUpperY
            // 
            this.labelUpperY.AutoSize = true;
            this.labelUpperY.Location = new System.Drawing.Point(6, 74);
            this.labelUpperY.Name = "labelUpperY";
            this.labelUpperY.Size = new System.Drawing.Size(17, 13);
            this.labelUpperY.TabIndex = 1;
            this.labelUpperY.Text = "Y:";
            // 
            // labelUpperX
            // 
            this.labelUpperX.AutoSize = true;
            this.labelUpperX.Location = new System.Drawing.Point(6, 48);
            this.labelUpperX.Name = "labelUpperX";
            this.labelUpperX.Size = new System.Drawing.Size(17, 13);
            this.labelUpperX.TabIndex = 0;
            this.labelUpperX.Text = "X:";
            // 
            // numericUpDownUpperX
            // 
            this.numericUpDownUpperX.Location = new System.Drawing.Point(49, 46);
            this.numericUpDownUpperX.Maximum = new decimal(new int[] {
            2240,
            0,
            0,
            0});
            this.numericUpDownUpperX.Minimum = new decimal(new int[] {
            960,
            0,
            0,
            -2147483648});
            this.numericUpDownUpperX.Name = "numericUpDownUpperX";
            this.numericUpDownUpperX.Size = new System.Drawing.Size(60, 20);
            this.numericUpDownUpperX.TabIndex = 9;
            this.numericUpDownUpperX.ValueChanged += new System.EventHandler(this.numericUpDownUpperX_ValueChanged);
            // 
            // numericUpDownUpperScale
            // 
            this.numericUpDownUpperScale.DecimalPlaces = 5;
            this.numericUpDownUpperScale.Increment = new decimal(new int[] {
            3125,
            0,
            0,
            327680});
            this.numericUpDownUpperScale.Location = new System.Drawing.Point(49, 98);
            this.numericUpDownUpperScale.Maximum = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.numericUpDownUpperScale.Name = "numericUpDownUpperScale";
            this.numericUpDownUpperScale.Size = new System.Drawing.Size(60, 20);
            this.numericUpDownUpperScale.TabIndex = 11;
            this.numericUpDownUpperScale.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownUpperScale.ValueChanged += new System.EventHandler(this.numericUpDownUpperScale_ValueChanged);
            // 
            // numericUpDownUpperY
            // 
            this.numericUpDownUpperY.Location = new System.Drawing.Point(49, 72);
            this.numericUpDownUpperY.Maximum = new decimal(new int[] {
            1680,
            0,
            0,
            0});
            this.numericUpDownUpperY.Minimum = new decimal(new int[] {
            960,
            0,
            0,
            -2147483648});
            this.numericUpDownUpperY.Name = "numericUpDownUpperY";
            this.numericUpDownUpperY.Size = new System.Drawing.Size(60, 20);
            this.numericUpDownUpperY.TabIndex = 10;
            this.numericUpDownUpperY.ValueChanged += new System.EventHandler(this.numericUpDownUpperY_ValueChanged);
            // 
            // groupBoxLower
            // 
            this.groupBoxLower.Controls.Add(this.buttonLowerBackground);
            this.groupBoxLower.Controls.Add(this.panelLowerRight);
            this.groupBoxLower.Controls.Add(this.labelLowerSize);
            this.groupBoxLower.Controls.Add(this.panelLowerPin);
            this.groupBoxLower.Controls.Add(this.labelLowerScale);
            this.groupBoxLower.Controls.Add(this.panelLowerStand);
            this.groupBoxLower.Controls.Add(this.labelLowerY);
            this.groupBoxLower.Controls.Add(this.labelLowerX);
            this.groupBoxLower.Controls.Add(this.panelLowerLeft);
            this.groupBoxLower.Controls.Add(this.numericUpDownLowerX);
            this.groupBoxLower.Controls.Add(this.numericUpDownLowerScale);
            this.groupBoxLower.Controls.Add(this.numericUpDownLowerY);
            this.groupBoxLower.Location = new System.Drawing.Point(6, 174);
            this.groupBoxLower.Name = "groupBoxLower";
            this.groupBoxLower.Size = new System.Drawing.Size(117, 149);
            this.groupBoxLower.TabIndex = 18;
            this.groupBoxLower.TabStop = false;
            this.groupBoxLower.Text = "Lower";
            // 
            // buttonLowerBackground
            // 
            this.buttonLowerBackground.Location = new System.Drawing.Point(86, 121);
            this.buttonLowerBackground.Name = "buttonLowerBackground";
            this.buttonLowerBackground.Size = new System.Drawing.Size(24, 23);
            this.buttonLowerBackground.TabIndex = 29;
            this.buttonLowerBackground.Text = "...";
            this.buttonLowerBackground.UseVisualStyleBackColor = true;
            this.buttonLowerBackground.Click += new System.EventHandler(this.buttonLowerBackground_Click);
            // 
            // panelLowerRight
            // 
            this.panelLowerRight.BackgroundImage = global::VCNDSLayout.Properties.Resources.Right;
            this.panelLowerRight.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.panelLowerRight.Location = new System.Drawing.Point(55, 19);
            this.panelLowerRight.Name = "panelLowerRight";
            this.panelLowerRight.Size = new System.Drawing.Size(23, 23);
            this.panelLowerRight.TabIndex = 16;
            this.panelLowerRight.Click += new System.EventHandler(this.panelLowerRight_Click);
            // 
            // labelLowerSize
            // 
            this.labelLowerSize.AutoSize = true;
            this.labelLowerSize.Location = new System.Drawing.Point(6, 127);
            this.labelLowerSize.Name = "labelLowerSize";
            this.labelLowerSize.Size = new System.Drawing.Size(48, 13);
            this.labelLowerSize.TabIndex = 26;
            this.labelLowerSize.Text = "256x192";
            // 
            // panelLowerPin
            // 
            this.panelLowerPin.BackgroundImage = global::VCNDSLayout.Properties.Resources.Pins;
            this.panelLowerPin.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.panelLowerPin.Location = new System.Drawing.Point(86, 19);
            this.panelLowerPin.Name = "panelLowerPin";
            this.panelLowerPin.Size = new System.Drawing.Size(23, 19);
            this.panelLowerPin.TabIndex = 13;
            this.panelLowerPin.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelLowerPin_MouseDown);
            // 
            // labelLowerScale
            // 
            this.labelLowerScale.AutoSize = true;
            this.labelLowerScale.Location = new System.Drawing.Point(6, 100);
            this.labelLowerScale.Name = "labelLowerScale";
            this.labelLowerScale.Size = new System.Drawing.Size(37, 13);
            this.labelLowerScale.TabIndex = 12;
            this.labelLowerScale.Text = "Scale:";
            // 
            // panelLowerStand
            // 
            this.panelLowerStand.BackgroundImage = global::VCNDSLayout.Properties.Resources.Stand;
            this.panelLowerStand.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.panelLowerStand.Location = new System.Drawing.Point(32, 19);
            this.panelLowerStand.Name = "panelLowerStand";
            this.panelLowerStand.Size = new System.Drawing.Size(23, 23);
            this.panelLowerStand.TabIndex = 15;
            this.panelLowerStand.Click += new System.EventHandler(this.panelLowerStand_Click);
            // 
            // labelLowerY
            // 
            this.labelLowerY.AutoSize = true;
            this.labelLowerY.Location = new System.Drawing.Point(6, 74);
            this.labelLowerY.Name = "labelLowerY";
            this.labelLowerY.Size = new System.Drawing.Size(17, 13);
            this.labelLowerY.TabIndex = 1;
            this.labelLowerY.Text = "Y:";
            // 
            // labelLowerX
            // 
            this.labelLowerX.AutoSize = true;
            this.labelLowerX.Location = new System.Drawing.Point(6, 48);
            this.labelLowerX.Name = "labelLowerX";
            this.labelLowerX.Size = new System.Drawing.Size(17, 13);
            this.labelLowerX.TabIndex = 0;
            this.labelLowerX.Text = "X:";
            // 
            // panelLowerLeft
            // 
            this.panelLowerLeft.BackgroundImage = global::VCNDSLayout.Properties.Resources.Left;
            this.panelLowerLeft.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.panelLowerLeft.Location = new System.Drawing.Point(9, 19);
            this.panelLowerLeft.Name = "panelLowerLeft";
            this.panelLowerLeft.Size = new System.Drawing.Size(23, 23);
            this.panelLowerLeft.TabIndex = 14;
            this.panelLowerLeft.Click += new System.EventHandler(this.panelLowerLeft_Click);
            // 
            // numericUpDownLowerX
            // 
            this.numericUpDownLowerX.Location = new System.Drawing.Point(49, 46);
            this.numericUpDownLowerX.Maximum = new decimal(new int[] {
            2240,
            0,
            0,
            0});
            this.numericUpDownLowerX.Minimum = new decimal(new int[] {
            960,
            0,
            0,
            -2147483648});
            this.numericUpDownLowerX.Name = "numericUpDownLowerX";
            this.numericUpDownLowerX.Size = new System.Drawing.Size(60, 20);
            this.numericUpDownLowerX.TabIndex = 9;
            this.numericUpDownLowerX.ValueChanged += new System.EventHandler(this.numericUpDownLowerX_ValueChanged);
            // 
            // numericUpDownLowerScale
            // 
            this.numericUpDownLowerScale.DecimalPlaces = 5;
            this.numericUpDownLowerScale.Increment = new decimal(new int[] {
            3125,
            0,
            0,
            327680});
            this.numericUpDownLowerScale.Location = new System.Drawing.Point(49, 98);
            this.numericUpDownLowerScale.Maximum = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.numericUpDownLowerScale.Name = "numericUpDownLowerScale";
            this.numericUpDownLowerScale.Size = new System.Drawing.Size(60, 20);
            this.numericUpDownLowerScale.TabIndex = 11;
            this.numericUpDownLowerScale.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownLowerScale.ValueChanged += new System.EventHandler(this.numericUpDownLowerScale_ValueChanged);
            // 
            // numericUpDownLowerY
            // 
            this.numericUpDownLowerY.Location = new System.Drawing.Point(49, 72);
            this.numericUpDownLowerY.Maximum = new decimal(new int[] {
            1680,
            0,
            0,
            0});
            this.numericUpDownLowerY.Minimum = new decimal(new int[] {
            960,
            0,
            0,
            -2147483648});
            this.numericUpDownLowerY.Name = "numericUpDownLowerY";
            this.numericUpDownLowerY.Size = new System.Drawing.Size(60, 20);
            this.numericUpDownLowerY.TabIndex = 10;
            this.numericUpDownLowerY.ValueChanged += new System.EventHandler(this.numericUpDownLowerY_ValueChanged);
            // 
            // checkBoxOnlyIntegerScales
            // 
            this.checkBoxOnlyIntegerScales.AutoSize = true;
            this.checkBoxOnlyIntegerScales.Location = new System.Drawing.Point(669, 33);
            this.checkBoxOnlyIntegerScales.Name = "checkBoxOnlyIntegerScales";
            this.checkBoxOnlyIntegerScales.Size = new System.Drawing.Size(115, 17);
            this.checkBoxOnlyIntegerScales.TabIndex = 21;
            this.checkBoxOnlyIntegerScales.Text = "Only integer scales";
            this.checkBoxOnlyIntegerScales.UseVisualStyleBackColor = true;
            this.checkBoxOnlyIntegerScales.Visible = false;
            // 
            // buttonBackground
            // 
            this.buttonBackground.Location = new System.Drawing.Point(48, 356);
            this.buttonBackground.Name = "buttonBackground";
            this.buttonBackground.Size = new System.Drawing.Size(75, 23);
            this.buttonBackground.TabIndex = 22;
            this.buttonBackground.Text = "Background";
            this.buttonBackground.UseVisualStyleBackColor = true;
            this.buttonBackground.Click += new System.EventHandler(this.buttonBackground_Click);
            // 
            // comboBoxLayout
            // 
            this.comboBoxLayout.FormattingEnabled = true;
            this.comboBoxLayout.Location = new System.Drawing.Point(64, 31);
            this.comboBoxLayout.Name = "comboBoxLayout";
            this.comboBoxLayout.Size = new System.Drawing.Size(320, 21);
            this.comboBoxLayout.TabIndex = 23;
            this.comboBoxLayout.SelectedIndexChanged += new System.EventHandler(this.comboBoxLayout_SelectedIndexChanged);
            // 
            // labelLayout
            // 
            this.labelLayout.AutoSize = true;
            this.labelLayout.Location = new System.Drawing.Point(16, 34);
            this.labelLayout.Name = "labelLayout";
            this.labelLayout.Size = new System.Drawing.Size(42, 13);
            this.labelLayout.TabIndex = 24;
            this.labelLayout.Text = "Layout:";
            // 
            // groupBoxLayout
            // 
            this.groupBoxLayout.Controls.Add(this.buttonSwapNDSScreens);
            this.groupBoxLayout.Controls.Add(this.panelPreview);
            this.groupBoxLayout.Controls.Add(this.buttonBackground);
            this.groupBoxLayout.Controls.Add(this.groupBoxUpper);
            this.groupBoxLayout.Controls.Add(this.groupBoxLower);
            this.groupBoxLayout.Location = new System.Drawing.Point(13, 58);
            this.groupBoxLayout.Name = "groupBoxLayout";
            this.groupBoxLayout.Size = new System.Drawing.Size(777, 386);
            this.groupBoxLayout.TabIndex = 27;
            this.groupBoxLayout.TabStop = false;
            // 
            // buttonSwapNDSScreens
            // 
            this.buttonSwapNDSScreens.Location = new System.Drawing.Point(6, 329);
            this.buttonSwapNDSScreens.Name = "buttonSwapNDSScreens";
            this.buttonSwapNDSScreens.Size = new System.Drawing.Size(117, 23);
            this.buttonSwapNDSScreens.TabIndex = 35;
            this.buttonSwapNDSScreens.Text = "Swap NDS screens";
            this.buttonSwapNDSScreens.UseVisualStyleBackColor = true;
            this.buttonSwapNDSScreens.Click += new System.EventHandler(this.buttonSwapNDSScreens_Click);
            // 
            // checkBoxLanguages
            // 
            this.checkBoxLanguages.AutoSize = true;
            this.checkBoxLanguages.Location = new System.Drawing.Point(669, 505);
            this.checkBoxLanguages.Name = "checkBoxLanguages";
            this.checkBoxLanguages.Size = new System.Drawing.Size(106, 17);
            this.checkBoxLanguages.TabIndex = 28;
            this.checkBoxLanguages.Text = "For all languages";
            this.checkBoxLanguages.UseVisualStyleBackColor = true;
            this.checkBoxLanguages.Visible = false;
            this.checkBoxLanguages.CheckedChanged += new System.EventHandler(this.checkBoxLanguages_CheckedChanged);
            // 
            // labelLanguage
            // 
            this.labelLanguage.AutoSize = true;
            this.labelLanguage.Location = new System.Drawing.Point(16, 506);
            this.labelLanguage.Name = "labelLanguage";
            this.labelLanguage.Size = new System.Drawing.Size(58, 13);
            this.labelLanguage.TabIndex = 29;
            this.labelLanguage.Text = "Language:";
            // 
            // comboBoxLanguage
            // 
            this.comboBoxLanguage.FormattingEnabled = true;
            this.comboBoxLanguage.Items.AddRange(new object[] {
            "Default",
            "Deutsch",
            "English (USA)",
            "English (EUR)",
            "French (USA)",
            "French (EUR)",
            "Italian",
            "Japanese",
            "Nederlands",
            "Portuguese (USA)",
            "Portuguese (EUR)",
            "Russian",
            "Spanish (USA)",
            "Spanish (EUR)"});
            this.comboBoxLanguage.Location = new System.Drawing.Point(85, 503);
            this.comboBoxLanguage.Name = "comboBoxLanguage";
            this.comboBoxLanguage.Size = new System.Drawing.Size(121, 21);
            this.comboBoxLanguage.TabIndex = 30;
            this.comboBoxLanguage.SelectedIndexChanged += new System.EventHandler(this.comboBoxLanguage_SelectedIndexChanged);
            // 
            // labelName
            // 
            this.labelName.AutoSize = true;
            this.labelName.Location = new System.Drawing.Point(16, 533);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(38, 13);
            this.labelName.TabIndex = 31;
            this.labelName.Text = "Name:";
            // 
            // labelDescription
            // 
            this.labelDescription.AutoSize = true;
            this.labelDescription.Location = new System.Drawing.Point(16, 559);
            this.labelDescription.Name = "labelDescription";
            this.labelDescription.Size = new System.Drawing.Size(63, 13);
            this.labelDescription.TabIndex = 32;
            this.labelDescription.Text = "Description:";
            // 
            // textBoxName
            // 
            this.textBoxName.Location = new System.Drawing.Point(85, 530);
            this.textBoxName.MaxLength = 60;
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(699, 20);
            this.textBoxName.TabIndex = 33;
            this.textBoxName.TextChanged += new System.EventHandler(this.textBoxName_TextChanged);
            // 
            // textBoxDescription
            // 
            this.textBoxDescription.Location = new System.Drawing.Point(85, 556);
            this.textBoxDescription.Multiline = true;
            this.textBoxDescription.Name = "textBoxDescription";
            this.textBoxDescription.Size = new System.Drawing.Size(699, 40);
            this.textBoxDescription.TabIndex = 34;
            this.textBoxDescription.TextChanged += new System.EventHandler(this.textBoxDescription_TextChanged);
            // 
            // comboBoxTarget
            // 
            this.comboBoxTarget.FormattingEnabled = true;
            this.comboBoxTarget.Items.AddRange(new object[] {
            "TV",
            "GamePad"});
            this.comboBoxTarget.Location = new System.Drawing.Point(437, 31);
            this.comboBoxTarget.Name = "comboBoxTarget";
            this.comboBoxTarget.Size = new System.Drawing.Size(72, 21);
            this.comboBoxTarget.TabIndex = 36;
            this.comboBoxTarget.SelectedIndexChanged += new System.EventHandler(this.comboBoxTarget_SelectedIndexChanged);
            // 
            // labelTarget
            // 
            this.labelTarget.AutoSize = true;
            this.labelTarget.Location = new System.Drawing.Point(390, 34);
            this.labelTarget.Name = "labelTarget";
            this.labelTarget.Size = new System.Drawing.Size(41, 13);
            this.labelTarget.TabIndex = 37;
            this.labelTarget.Text = "Target:";
            // 
            // comboBoxPad
            // 
            this.comboBoxPad.FormattingEnabled = true;
            this.comboBoxPad.Items.AddRange(new object[] {
            "0",
            "90",
            "270"});
            this.comboBoxPad.Location = new System.Drawing.Point(41, 19);
            this.comboBoxPad.Name = "comboBoxPad";
            this.comboBoxPad.Size = new System.Drawing.Size(52, 21);
            this.comboBoxPad.TabIndex = 38;
            this.comboBoxPad.SelectedIndexChanged += new System.EventHandler(this.comboBoxPad_SelectedIndexChanged);
            // 
            // comboBoxDRC
            // 
            this.comboBoxDRC.FormattingEnabled = true;
            this.comboBoxDRC.Items.AddRange(new object[] {
            "0",
            "90",
            "270"});
            this.comboBoxDRC.Location = new System.Drawing.Point(148, 19);
            this.comboBoxDRC.Name = "comboBoxDRC";
            this.comboBoxDRC.Size = new System.Drawing.Size(52, 21);
            this.comboBoxDRC.TabIndex = 39;
            this.comboBoxDRC.SelectedIndexChanged += new System.EventHandler(this.comboBoxDRC_SelectedIndexChanged);
            // 
            // comboBoxButtons
            // 
            this.comboBoxButtons.FormattingEnabled = true;
            this.comboBoxButtons.Items.AddRange(new object[] {
            "0",
            "90",
            "270"});
            this.comboBoxButtons.Location = new System.Drawing.Point(268, 19);
            this.comboBoxButtons.Name = "comboBoxButtons";
            this.comboBoxButtons.Size = new System.Drawing.Size(52, 21);
            this.comboBoxButtons.TabIndex = 40;
            this.comboBoxButtons.SelectedIndexChanged += new System.EventHandler(this.comboBoxButtons_SelectedIndexChanged);
            // 
            // groupBoxRotations
            // 
            this.groupBoxRotations.Controls.Add(this.labelButtons);
            this.groupBoxRotations.Controls.Add(this.comboBoxButtons);
            this.groupBoxRotations.Controls.Add(this.labelDRC);
            this.groupBoxRotations.Controls.Add(this.comboBoxDRC);
            this.groupBoxRotations.Controls.Add(this.labelPad);
            this.groupBoxRotations.Controls.Add(this.comboBoxPad);
            this.groupBoxRotations.Location = new System.Drawing.Point(12, 450);
            this.groupBoxRotations.Name = "groupBoxRotations";
            this.groupBoxRotations.Size = new System.Drawing.Size(326, 47);
            this.groupBoxRotations.TabIndex = 41;
            this.groupBoxRotations.TabStop = false;
            this.groupBoxRotations.Text = "Rotations";
            // 
            // labelButtons
            // 
            this.labelButtons.AutoSize = true;
            this.labelButtons.Location = new System.Drawing.Point(216, 22);
            this.labelButtons.Name = "labelButtons";
            this.labelButtons.Size = new System.Drawing.Size(46, 13);
            this.labelButtons.TabIndex = 2;
            this.labelButtons.Text = "Buttons:";
            // 
            // labelDRC
            // 
            this.labelDRC.AutoSize = true;
            this.labelDRC.Location = new System.Drawing.Point(109, 22);
            this.labelDRC.Name = "labelDRC";
            this.labelDRC.Size = new System.Drawing.Size(33, 13);
            this.labelDRC.TabIndex = 1;
            this.labelDRC.Text = "DRC:";
            // 
            // labelPad
            // 
            this.labelPad.AutoSize = true;
            this.labelPad.Location = new System.Drawing.Point(6, 22);
            this.labelPad.Name = "labelPad";
            this.labelPad.Size = new System.Drawing.Size(29, 13);
            this.labelPad.TabIndex = 0;
            this.labelPad.Text = "Pad:";
            // 
            // FormEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(803, 609);
            this.Controls.Add(this.groupBoxRotations);
            this.Controls.Add(this.labelTarget);
            this.Controls.Add(this.textBoxDescription);
            this.Controls.Add(this.comboBoxTarget);
            this.Controls.Add(this.textBoxName);
            this.Controls.Add(this.labelDescription);
            this.Controls.Add(this.labelName);
            this.Controls.Add(this.comboBoxLanguage);
            this.Controls.Add(this.labelLanguage);
            this.Controls.Add(this.checkBoxLanguages);
            this.Controls.Add(this.labelLayout);
            this.Controls.Add(this.checkBoxOnlyIntegerScales);
            this.Controls.Add(this.comboBoxLayout);
            this.Controls.Add(this.menuStrip);
            this.Controls.Add(this.groupBoxLayout);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip;
            this.MaximizeBox = false;
            this.Name = "FormEditor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "VCNDSLayout Editor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormEditor_FormClosing);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.contextMenuStrip.ResumeLayout(false);
            this.groupBoxUpper.ResumeLayout(false);
            this.groupBoxUpper.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownUpperX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownUpperScale)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownUpperY)).EndInit();
            this.groupBoxLower.ResumeLayout(false);
            this.groupBoxLower.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownLowerX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownLowerScale)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownLowerY)).EndInit();
            this.groupBoxLayout.ResumeLayout(false);
            this.groupBoxRotations.ResumeLayout(false);
            this.groupBoxRotations.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.Panel panelPreview;
        private System.Windows.Forms.GroupBox groupBoxUpper;
        private System.Windows.Forms.Panel panelUpperRight;
        private System.Windows.Forms.Panel panelUpperStand;
        private System.Windows.Forms.Panel panelUpperLeft;
        private System.Windows.Forms.Panel panelUpperPin;
        private System.Windows.Forms.Label labelUpperScale;
        private System.Windows.Forms.Label labelUpperY;
        private System.Windows.Forms.Label labelUpperX;
        private System.Windows.Forms.NumericUpDown numericUpDownUpperX;
        private System.Windows.Forms.NumericUpDown numericUpDownUpperScale;
        private System.Windows.Forms.NumericUpDown numericUpDownUpperY;
        private System.Windows.Forms.GroupBox groupBoxLower;
        private System.Windows.Forms.Panel panelLowerRight;
        private System.Windows.Forms.Panel panelLowerStand;
        private System.Windows.Forms.Panel panelLowerLeft;
        private System.Windows.Forms.Panel panelLowerPin;
        private System.Windows.Forms.Label labelLowerScale;
        private System.Windows.Forms.Label labelLowerY;
        private System.Windows.Forms.Label labelLowerX;
        private System.Windows.Forms.NumericUpDown numericUpDownLowerX;
        private System.Windows.Forms.NumericUpDown numericUpDownLowerScale;
        private System.Windows.Forms.NumericUpDown numericUpDownLowerY;
        private System.Windows.Forms.CheckBox checkBoxOnlyIntegerScales;
        private System.Windows.Forms.Button buttonBackground;
        private System.Windows.Forms.ComboBox comboBoxLayout;
        private System.Windows.Forms.Label labelLayout;
        private System.Windows.Forms.Label labelUpperSize;
        private System.Windows.Forms.Label labelLowerSize;
        private System.Windows.Forms.GroupBox groupBoxLayout;
        private System.Windows.Forms.Button buttonUpperBackground;
        private System.Windows.Forms.Button buttonLowerBackground;
        private System.Windows.Forms.CheckBox checkBoxLanguages;
        private System.Windows.Forms.Label labelLanguage;
        private System.Windows.Forms.ComboBox comboBoxLanguage;
        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.Label labelDescription;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.TextBox textBoxDescription;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem layoutsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem organizeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.Button buttonSwapNDSScreens;
        private System.Windows.Forms.ComboBox comboBoxTarget;
        private System.Windows.Forms.Label labelTarget;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem showFullPreviewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem savePreviewToolStripMenuItem;
        private System.Windows.Forms.ComboBox comboBoxPad;
        private System.Windows.Forms.ComboBox comboBoxDRC;
        private System.Windows.Forms.ComboBox comboBoxButtons;
        private System.Windows.Forms.GroupBox groupBoxRotations;
        private System.Windows.Forms.Label labelButtons;
        private System.Windows.Forms.Label labelDRC;
        private System.Windows.Forms.Label labelPad;
    }
}

