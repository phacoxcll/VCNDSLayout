using System;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.IO.Compression;

namespace VCNDSLayout
{
    public partial class FormEditor : Form
    {
        public const string Release = "alpha 2"; //CllVersionReplace "stability major"

        private string VCNDSLayoutEditorDataPath;
        private string CurrentFilePath;

        private Configuration Config;

        private BufferedGraphics Preview;

        private Bitmap BackgroundTV;
        private Bitmap BackgroundGamePad;
        private string BackgroundTVPath;
        private string BackgroundGamePadPath;

        private Bitmap UpperImage;
        private Bitmap LowerImage;

        private int CurrentLayout;
        private Target CurrentTarget;

        private enum Target : int
        {
            TV = 0,
            GamePad = 1
        }

        private enum AlignmentHorizontal
        {
            Left = 0,
            Center = 1,
            Right = 2
        }

        private enum AlignmentVertical
        {
            Top = 0x00,
            Middle = 0x10,
            Bottom = 0x20
        }

        private enum Alignment2D
        {
            TopLeft = AlignmentVertical.Top | AlignmentHorizontal.Left,
            TopCenter = AlignmentVertical.Top | AlignmentHorizontal.Center,
            TopRight = AlignmentVertical.Top | AlignmentHorizontal.Right,
            MiddleLeft = AlignmentVertical.Middle | AlignmentHorizontal.Left,
            MiddleCenter = AlignmentVertical.Middle | AlignmentHorizontal.Center,
            MiddleRight = AlignmentVertical.Middle | AlignmentHorizontal.Right,
            BottomLeft = AlignmentVertical.Bottom | AlignmentHorizontal.Left,
            BottomCenter = AlignmentVertical.Bottom | AlignmentHorizontal.Center,
            BottomRight = AlignmentVertical.Bottom | AlignmentHorizontal.Right
        }

        private struct DSScreen
        {
            public PointF _location;
            private double _rad;
            private PointF _base;
            private Alignment2D _pin;
            public Alignment2D Pin
            {
                set
                {
                    _pin = value;
                    switch (value)
                    {
                        case Alignment2D.TopLeft:
                            _base = new Point(0, 0);
                            break;
                        case Alignment2D.TopCenter:
                            _base = new Point(128, 0);
                            break;
                        case Alignment2D.TopRight:
                            _base = new Point(256, 0);
                            break;
                        case Alignment2D.MiddleLeft:
                            _base = new Point(0, 96);
                            break;
                        case Alignment2D.MiddleCenter:
                            _base = new Point(128, 96);
                            break;
                        case Alignment2D.MiddleRight:
                            _base = new Point(256, 96);
                            break;
                        case Alignment2D.BottomLeft:
                            _base = new Point(0, 192);
                            break;
                        case Alignment2D.BottomCenter:
                            _base = new Point(128, 192);
                            break;
                        case Alignment2D.BottomRight:
                            _base = new Point(256, 192);
                            break;
                        default:
                            _base = new Point(0, 0);
                            break;
                    }
                }
                get { return _pin; }
            }
            public PointF Translate;
            public float Scale;
            public float Angle
            {
                set { _rad = value * Math.PI / 180.0; }
                get { return (float)(_rad * 180.0 / Math.PI); }
            }
            public float PinX
            {
                get
                {
                    return (float)(
                        (_base.X * Math.Cos(_rad) - _base.Y * Math.Sin(_rad)) * Scale
                        + _location.X + Translate.X);
                }
            }
            public float PinY
            {
                get
                {
                    return (float)(
                        (_base.X * Math.Sin(_rad) + _base.Y * Math.Cos(_rad)) * Scale
                        + _location.Y + Translate.Y);
                }
            }
            public PointF OffsetLocation
            {
                get
                {
                    return new PointF(
                        (float)((_base.X * Math.Cos(_rad) - _base.Y * Math.Sin(_rad)) * Scale + _location.X),
                        (float)((_base.X * Math.Sin(_rad) + _base.Y * Math.Cos(_rad)) * Scale + _location.Y));
                }
            }
            public float Width
            {
                get { return 256.0f * Scale; }
            }
            public float Height
            {
                get { return 192.0f * Scale; }
            }
            public bool IsEmpty
            {
                get { return Scale == 0; }
            }

            public DSScreen(int x, int y, float scale, float angle)
            {
                _location = PointF.Empty;
                _rad = angle * Math.PI / 180.0;
                _base = PointF.Empty;
                _pin = Alignment2D.TopLeft;
                Translate = new PointF(x, y);
                Scale = scale;
            }

            public Matrix GetTransform(PointF offsetLocation, float rescale)
            {
                _location.X = (float)(offsetLocation.X - (_base.X * Math.Cos(_rad) - _base.Y * Math.Sin(_rad)) * Scale);
                _location.Y = (float)(offsetLocation.Y - (_base.X * Math.Sin(_rad) + _base.Y * Math.Cos(_rad)) * Scale);
                return new Matrix(
                    (float)(Math.Cos(_rad) * Scale) * rescale, (float)(Math.Sin(_rad) * Scale) * rescale,
                    (float)(-Math.Sin(_rad) * Scale) * rescale, (float)(Math.Cos(_rad) * Scale) * rescale,
                    (_location.X + Translate.X) * rescale, (_location.Y + Translate.Y) * rescale);
            }

            public readonly static DSScreen Empty = new DSScreen(0, 0, 0.0F, 0.0F);
        }

        private struct DSLayout
        {
            public DSTarget Tv;
            public DSTarget Drc;
            public string Name;
            public string Description;

            public DSLayout(DSTarget tv, DSTarget drc, string name, string description)
            {
                Tv = tv;
                Drc = drc;
                Name = name;
                Description = description;
            }

            public DSLayout(DSTarget tv, DSTarget drc)
            {
                Tv = tv;
                Drc = drc;
                Name = "";
                Description = "";
            }
        }

        private struct DSTarget
        {
            public DSScreen Upper;
            public DSScreen Lower;
            public string Background;

            public DSTarget(DSScreen upper, DSScreen lower, string background)
            {
                Upper = upper;
                Lower = lower;
                Background = background;
            }

            public DSTarget(DSScreen upper, DSScreen lower)
            {
                Upper = upper;
                Lower = lower;
                Background = "";
            }

            public static DSTarget Tv_1_1 = new DSTarget(
                new DSScreen(512, 106, 1.0f, 0),
                new DSScreen(512, 381, 1.0f, 0));
            public static DSTarget Tv_2_1 = new DSTarget(
                new DSScreen(384, 48, 2.0f, 0),
                new DSScreen(512, 480, 1.0f, 0));
            public static DSTarget Tv_1_2 = new DSTarget(
                new DSScreen(512, 48, 1.0f, 0),
                new DSScreen(384, 288, 2.0f, 0));
            public static DSTarget Tv_3_0 = new DSTarget(
                new DSScreen(256, 72, 3.0f, 0),
                new DSScreen(0, 0, 0, 0));

            public static DSTarget Tv_2_2 = new DSTarget(
                new DSScreen(85, 168, 2.0f, 0),
                new DSScreen(682, 168, 2.0f, 0));
            public static DSTarget Tv_2_2_Left = new DSTarget(
                new DSScreen(216, 616, 2.0f, 270.0f),
                new DSScreen(680, 616, 2.0f, 270.0f));
            public static DSTarget Tv_2_2_Right = new DSTarget(
                new DSScreen(1064, 104, 2.0f, 90.0f),
                new DSScreen(600, 104, 2.0f, 90.0f));

            public static DSTarget Tv_3_1_Top = new DSTarget(
                new DSScreen(110, 72, 3.0f, 0),
                new DSScreen(914, 72, 1.0f, 0));
            public static DSTarget Tv_3_1_Center = new DSTarget(
                new DSScreen(110, 72, 3.0f, 0),
                new DSScreen(914, 264, 1.0f, 0));
            public static DSTarget Tv_3_1_Bottom = new DSTarget(
                new DSScreen(110, 72, 3.0f, 0),
                new DSScreen(914, 456, 1.0f, 0));

            public static DSTarget Tv_1_3_Top = new DSTarget(
                new DSScreen(110, 72, 1.0f, 0),
                new DSScreen(402, 72, 3.0f, 0));
            public static DSTarget Tv_1_3_Center = new DSTarget(
                new DSScreen(110, 264, 1.0f, 0),
                new DSScreen(402, 72, 3.0f, 0));
            public static DSTarget Tv_1_3_Bottom = new DSTarget(
                new DSScreen(110, 456, 1.0f, 0),
                new DSScreen(402, 72, 3.0f, 0));

            public static DSTarget Tv_3_2_Top = new DSTarget(
                new DSScreen(0, 72, 3.0f, 0),
                new DSScreen(768, 72, 2.0f, 0));
            public static DSTarget Tv_3_2_Center = new DSTarget(
                new DSScreen(0, 72, 3.0f, 0),
                new DSScreen(768, 168, 2.0f, 0));
            public static DSTarget Tv_3_2_Bottom = new DSTarget(
                new DSScreen(0, 72, 3.0f, 0),
                new DSScreen(768, 264, 2.0f, 0));

            public static DSTarget Tv_2_3_Top = new DSTarget(
                new DSScreen(0, 72, 2.0f, 0),
                new DSScreen(512, 72, 3.0f, 0));
            public static DSTarget Tv_2_3_Center = new DSTarget(
                new DSScreen(0, 168, 2.0f, 0),
                new DSScreen(512, 72, 3.0f, 0));
            public static DSTarget Tv_2_3_Bottom = new DSTarget(
                new DSScreen(0, 264, 2.0f, 0),
                new DSScreen(512, 72, 3.0f, 0));

            public static DSTarget Tv_1d7_1d7 = new DSTarget(
                new DSScreen(428, 11, 1.65625f, 0),
                new DSScreen(428, 391, 1.65625f, 0));
            public static DSTarget Tv_2d7_1 = new DSTarget(
                new DSScreen(292, 2, 2.71875f, 0),
                new DSScreen(512, 526, 1.0f, 0));
            public static DSTarget Tv_1_2d7 = new DSTarget(
                new DSScreen(512, 2, 1.0f, 0),
                new DSScreen(292, 196, 2.71875f, 0));
            public static DSTarget Tv_3d6_0 = new DSTarget(
                new DSScreen(176, 12, 3.625f, 0),
                new DSScreen(0, 0, 0, 0));

            public static DSTarget Tv_2d5_2d5 = new DSTarget(
                new DSScreen(0, 120, 2.5f, 0),
                new DSScreen(640, 120, 2.5f, 0));
            public static DSTarget Tv_2d7_2d7_Left = new DSTarget(
                new DSScreen(64, 708, 2.71875f, 270.0f),
                new DSScreen(694, 708, 2.71875f, 270.0f));
            public static DSTarget Tv_2d7_2d7_Right = new DSTarget(
                new DSScreen(1216, 12, 2.71875f, 90.0f),
                new DSScreen(586, 12, 2.71875f, 90.0f));

            public static DSTarget Tv_3d6_1_Top = new DSTarget(
                new DSScreen(32, 12, 3.625f, 0),
                new DSScreen(992, 12, 1.0f, 0));
            public static DSTarget Tv_3d6_1_Center = new DSTarget(
                new DSScreen(32, 12, 3.625f, 0),
                new DSScreen(992, 264, 1.0f, 0));
            public static DSTarget Tv_3d6_1_Bottom = new DSTarget(
                new DSScreen(32, 12, 3.625f, 0),
                new DSScreen(992, 516, 1.0f, 0));

            public static DSTarget Tv_1_3d6_Top = new DSTarget(
                new DSScreen(32, 12, 1.0f, 0),
                new DSScreen(320, 12, 3.625f, 0));
            public static DSTarget Tv_1_3d6_Center = new DSTarget(
                new DSScreen(32, 264, 1.0f, 0),
                new DSScreen(320, 12, 3.625f, 0));
            public static DSTarget Tv_1_3d6_Bottom = new DSTarget(
                new DSScreen(32, 516, 1.0f, 0),
                new DSScreen(320, 12, 3.625f, 0));

            public static DSTarget[] Targets = {
                Tv_1_1,
                Tv_2_1,
                Tv_1_2,
                Tv_3_0,

                Tv_1d7_1d7,
                Tv_2d7_1,
                Tv_1_2d7,
                Tv_3d6_0,

                Tv_2_2,
                Tv_2_2_Left,
                Tv_2_2_Right,

                Tv_2d5_2d5,
                Tv_2d7_2d7_Left,
                Tv_2d7_2d7_Right,

                Tv_3_1_Top,
                Tv_3_1_Center,
                Tv_3_1_Bottom,

                Tv_3d6_1_Top,
                Tv_3d6_1_Center,
                Tv_3d6_1_Bottom,

                Tv_1_3_Top,
                Tv_1_3_Center,
                Tv_1_3_Bottom,

                Tv_1_3d6_Top,
                Tv_1_3d6_Center,
                Tv_1_3d6_Bottom,

                Tv_3_2_Top,
                Tv_3_2_Center,
                Tv_3_2_Bottom,
                Tv_2_3_Top,
                Tv_2_3_Center,
                Tv_2_3_Bottom
            };

            public static DSTarget Drc_1_1_V = new DSTarget(
                new DSScreen(299, 16, 1.0f, 0),
                new DSScreen(299, 283, 1.0f, 0));
            public static DSTarget Drc_2_0 = new DSTarget(
                new DSScreen(171, 48, 2.0f, 0),
                new DSScreen(0, 0, 0, 0));

            public static DSTarget Drc_1_1_H = new DSTarget(
                new DSScreen(114, 144, 1.0f, 0),
                new DSScreen(484, 144, 1.0f, 0));
            public static DSTarget Drc_1_1_Left = new DSTarget(
                new DSScreen(205, 368, 1.0f, 270.0f),
                new DSScreen(457, 368, 1.0f, 270.0f));
            public static DSTarget Drc_1_1_Right = new DSTarget(
                new DSScreen(649, 112, 1.0f, 90.0f),
                new DSScreen(397, 112, 1.0f, 90.0f));

            public static DSTarget Drc_2_1_Top = new DSTarget(
                new DSScreen(30, 48, 2.0f, 0),
                new DSScreen(568, 48, 1.0f, 0));
            public static DSTarget Drc_2_1_Center = new DSTarget(
                new DSScreen(30, 48, 2.0f, 0),
                new DSScreen(568, 144, 1.0f, 0));
            public static DSTarget Drc_2_1_Bottom = new DSTarget(
                new DSScreen(30, 48, 2.0f, 0),
                new DSScreen(568, 240, 1.0f, 0));

            public static DSTarget Drc_1_2_Top = new DSTarget(
                new DSScreen(30, 48, 1.0f, 0),
                new DSScreen(312, 48, 2.0f, 0));
            public static DSTarget Drc_1_2_Center = new DSTarget(
                new DSScreen(30, 144, 1.0f, 0),
                new DSScreen(312, 48, 2.0f, 0));
            public static DSTarget Drc_1_2_Bottom = new DSTarget(
                new DSScreen(30, 240, 1.0f, 0),
                new DSScreen(312, 48, 2.0f, 0));

            public static DSTarget Drc_1d3_1d3 = new DSTarget(
                new DSScreen(267, 0, 1.25f, 0),
                new DSScreen(267, 240, 1.25f, 0));
            public static DSTarget Drc_1d5_1 = new DSTarget(
                new DSScreen(235, 0, 1.5f, 0),
                new DSScreen(299, 288, 1.0f, 0));
            public static DSTarget Drc_1_1d5 = new DSTarget(
                new DSScreen(299, 0, 1.0f, 0),
                new DSScreen(235, 192, 1.5f, 0));
            public static DSTarget Drc_2d5_0 = new DSTarget(
                new DSScreen(107, 0, 2.5f, 0),
                new DSScreen(0, 0, 0, 0));

            public static DSTarget Drc_1d7_1d7 = new DSTarget(
                new DSScreen(2, 81, 1.65625f, 0),
                new DSScreen(428, 81, 1.65625f, 0));
            public static DSTarget Drc_1d9_1d9_Left = new DSTarget(
                new DSScreen(28, 480, 1.875f, 270.0f),
                new DSScreen(466, 480, 1.875f, 270.0f));
            public static DSTarget Drc_1d9_1d9_Right = new DSTarget(
                new DSScreen(826, 0, 1.875f, 90.0f),
                new DSScreen(388, 0, 1.875f, 90.0f));

            public static DSTarget Drc_2d3_1_Top = new DSTarget(
                new DSScreen(2, 18, 2.3125f, 0),
                new DSScreen(596, 18, 1.0f, 0));
            public static DSTarget Drc_2d3_1_Center = new DSTarget(
                new DSScreen(2, 18, 2.3125f, 0),
                new DSScreen(596, 144, 1.0f, 0));
            public static DSTarget Drc_2d3_1_Bottom = new DSTarget(
                new DSScreen(2, 18, 2.3125f, 0),
                new DSScreen(596, 270, 1.0f, 0));

            public static DSTarget Drc_1_2d3_Top = new DSTarget(
                new DSScreen(2, 18, 1.0f, 0),
                new DSScreen(260, 18, 2.3125f, 0));
            public static DSTarget Drc_1_2d3_Center = new DSTarget(
                new DSScreen(2, 144, 1.0f, 0),
                new DSScreen(260, 18, 2.3125f, 0));
            public static DSTarget Drc_1_2d3_Bottom = new DSTarget(
                new DSScreen(2, 270, 1.0f, 0),
                new DSScreen(260, 18, 2.3125f, 0));

            public static DSTarget[] TargetsD = {
                Drc_1_1_V,
                Drc_2_0,

                Drc_1d3_1d3,
                Drc_1d5_1,
                Drc_1_1d5,
                Drc_2d5_0,

                Drc_1_1_H,
                Drc_1_1_Left,
                Drc_1_1_Right,

                Drc_1d7_1d7,
                Drc_1d9_1d9_Left,
                Drc_1d9_1d9_Right,

                Drc_2_1_Top,
                Drc_2_1_Center,
                Drc_2_1_Bottom,

                Drc_2d3_1_Top,
                Drc_2d3_1_Center,
                Drc_2d3_1_Bottom,

                Drc_1_2_Top,
                Drc_1_2_Center,
                Drc_1_2_Bottom,

                Drc_1_2d3_Top,
                Drc_1_2d3_Center,
                Drc_1_2d3_Bottom,
            };
        }

        private DSScreen Upper;
        private DSScreen Lower;

        private bool UpdateUpperX;
        private bool UpdateUpperY;
        private bool UpdateLowerX;
        private bool UpdateLowerY;

        private bool UpdateControls;

        public FormEditor()
        {
            InitializeComponent();

            VCNDSLayoutEditorDataPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "VCNDSLayoutEditor");

            if (!Directory.Exists(VCNDSLayoutEditorDataPath))
                Directory.CreateDirectory(VCNDSLayoutEditorDataPath);

            comboBoxLanguage.SelectedIndex = 0;

            Config = new Configuration();

            /*for (int i = 0; i < DSTarget.Targets.Length; ++i)
            {
                listViewDefaultLayouts.Items.Add(new ListViewItem(i.ToString()));
            }*/

            BackgroundTV = null;
            BackgroundGamePad = null;
            BackgroundTVPath = null;
            BackgroundGamePadPath = null;
            UpperImage = null;
            LowerImage = null;

            UpdateUpperX = true;
            UpdateUpperY = true;
            UpdateLowerX = true;
            UpdateLowerY = true;

            UpdateUpperRotationButtons();
            UpdateLowerRotationButtons();
            UpdateUpperPin();
            UpdateLowerPin();

            for (int i = 0; i < Config.Layouts.Length; i++)
                comboBoxLayout.Items.Add(Config.Layouts[i].Name.Default);

            CurrentLayout = 0;
            CurrentTarget = Target.TV;

            UpdateControls = true;
            comboBoxLayout.SelectedIndex = 0;
            comboBoxTarget.SelectedIndex = 0;
            UpdateControls = false;

            groupBoxLayout.Text = comboBoxLayout.Items[CurrentLayout].ToString() + " - " + (string)comboBoxTarget.SelectedItem;
            UpdateText();
            LoadLayoutScreen();
            UpdatePreview();

            this.Text = "VCNDSLayout Editor " + Release + " :: New";
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult save = DialogResult.No;
            if (this.Text.EndsWith("*"))
                save = MessageBox.Show("Do you want to save the current layout?", "Save?", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

            if (save == DialogResult.Yes)
                SaveCurrentFile();

            if (save != DialogResult.Cancel)
            {
                comboBoxLanguage.SelectedIndex = 0;

                Config = new Configuration();

                if (BackgroundTV != null)
                {
                    BackgroundTV.Dispose();
                    BackgroundTV = null;
                }

                if (BackgroundGamePad != null)
                {
                    BackgroundGamePad.Dispose();
                    BackgroundGamePad = null;
                }

                BackgroundTVPath = null;
                BackgroundGamePadPath = null;
                UpperImage = null;
                LowerImage = null;

                UpdateUpperX = true;
                UpdateUpperY = true;
                UpdateLowerX = true;
                UpdateLowerY = true;

                UpdateUpperRotationButtons();
                UpdateLowerRotationButtons();
                UpdateUpperPin();
                UpdateLowerPin();

                for (int i = 0; i < Config.Layouts.Length; i++)
                    comboBoxLayout.Items.Add(Config.Layouts[i].Name.Default);

                this.Text = "VCNDSLayout Editor " + Release + " :: New";

                CurrentLayout = 0;
                CurrentTarget = Target.TV;

                UpdateControls = true;
                comboBoxLayout.SelectedIndex = 0;
                comboBoxTarget.SelectedIndex = 0;
                UpdateControls = false;

                groupBoxLayout.Text = comboBoxLayout.Items[CurrentLayout].ToString() + " - " + (string)comboBoxTarget.SelectedItem;
                UpdateText();
                LoadLayoutScreen();
                UpdatePreview();
                Changed();
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult save = DialogResult.No;
            if (this.Text.EndsWith("*"))
                save = MessageBox.Show("Do you want to save the current layout?", "Save?", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

            if (save == DialogResult.Yes)
                SaveCurrentFile();

            openFileDialog.FileName = "";
            openFileDialog.Filter = "Layout for Wii U NDS Virtual Console|*.luds";
            if (save != DialogResult.Cancel && openFileDialog.ShowDialog() == DialogResult.OK)
            {
                if (BackgroundTV != null)
                {
                    BackgroundTV.Dispose();
                    BackgroundTV = null;
                }

                if (BackgroundGamePad != null)
                {
                    BackgroundGamePad.Dispose();
                    BackgroundGamePad = null;
                }

                string lastConfig = Path.Combine(VCNDSLayoutEditorDataPath, "LastConfig");
                if (Directory.Exists(lastConfig))
                    Directory.Delete(lastConfig, true);

                ZipFile.ExtractToDirectory(openFileDialog.FileName, lastConfig);

                comboBoxLayout.Items.Clear();

                StreamReader sr = null;

                try
                {
                    sr = File.OpenText(Path.Combine(lastConfig, "config.json"));
                    Cll.JSON.SyntacticAnalyzer syn = new Cll.JSON.SyntacticAnalyzer(sr);
                    Cll.JSON.Element config = syn.Run();
                    sr.Close();
                    
                    Config = new Configuration(config);

                    foreach (Layout layout in Config.Layouts)
                    {
                        layout.BackgroundTV.Resource = Path.Combine(lastConfig, Path.GetFileName(layout.BackgroundTV.Resource));
                        layout.BackgroundGamePad.Resource = Path.Combine(lastConfig, Path.GetFileName(layout.BackgroundGamePad.Resource));
                    }

                    for (int i = 0; i < Config.Layouts.Length; i++)
                    {
                        Config.Layouts[i].Name.Default = ((Cll.JSON.String)config.Value.GetValue("strings").GetValue("Default").GetValue(Config.Layouts[i].NameID)).Value;
                        Config.Layouts[i].Description.Default = ((Cll.JSON.String)config.Value.GetValue("strings").GetValue("Default").GetValue(Config.Layouts[i].DescriptionID)).Value;
                        Config.Layouts[i].Name.Deutsch = ((Cll.JSON.String)config.Value.GetValue("strings").GetValue("Deutsch").GetValue(Config.Layouts[i].NameID)).Value;
                        Config.Layouts[i].Description.Deutsch = ((Cll.JSON.String)config.Value.GetValue("strings").GetValue("Deutsch").GetValue(Config.Layouts[i].DescriptionID)).Value;
                        Config.Layouts[i].Name.EnglishUSA = ((Cll.JSON.String)config.Value.GetValue("strings").GetValue("EnglishUSA").GetValue(Config.Layouts[i].NameID)).Value;
                        Config.Layouts[i].Description.EnglishUSA = ((Cll.JSON.String)config.Value.GetValue("strings").GetValue("EnglishUSA").GetValue(Config.Layouts[i].DescriptionID)).Value;
                        Config.Layouts[i].Name.EnglishEUR = ((Cll.JSON.String)config.Value.GetValue("strings").GetValue("EnglishEUR").GetValue(Config.Layouts[i].NameID)).Value;
                        Config.Layouts[i].Description.EnglishEUR = ((Cll.JSON.String)config.Value.GetValue("strings").GetValue("EnglishEUR").GetValue(Config.Layouts[i].DescriptionID)).Value;
                        Config.Layouts[i].Name.FrenchUSA = ((Cll.JSON.String)config.Value.GetValue("strings").GetValue("FrenchUSA").GetValue(Config.Layouts[i].NameID)).Value;
                        Config.Layouts[i].Description.FrenchUSA = ((Cll.JSON.String)config.Value.GetValue("strings").GetValue("FrenchUSA").GetValue(Config.Layouts[i].DescriptionID)).Value;
                        Config.Layouts[i].Name.FrenchEUR = ((Cll.JSON.String)config.Value.GetValue("strings").GetValue("FrenchEUR").GetValue(Config.Layouts[i].NameID)).Value;
                        Config.Layouts[i].Description.FrenchEUR = ((Cll.JSON.String)config.Value.GetValue("strings").GetValue("FrenchEUR").GetValue(Config.Layouts[i].DescriptionID)).Value;
                        Config.Layouts[i].Name.Italian = ((Cll.JSON.String)config.Value.GetValue("strings").GetValue("Italian").GetValue(Config.Layouts[i].NameID)).Value;
                        Config.Layouts[i].Description.Italian = ((Cll.JSON.String)config.Value.GetValue("strings").GetValue("Italian").GetValue(Config.Layouts[i].DescriptionID)).Value;
                        Config.Layouts[i].Name.Japanese = ((Cll.JSON.String)config.Value.GetValue("strings").GetValue("Japanese").GetValue(Config.Layouts[i].NameID)).Value;
                        Config.Layouts[i].Description.Japanese = ((Cll.JSON.String)config.Value.GetValue("strings").GetValue("Japanese").GetValue(Config.Layouts[i].DescriptionID)).Value;
                        Config.Layouts[i].Name.Nederlands = ((Cll.JSON.String)config.Value.GetValue("strings").GetValue("Nederlands").GetValue(Config.Layouts[i].NameID)).Value;
                        Config.Layouts[i].Description.Nederlands = ((Cll.JSON.String)config.Value.GetValue("strings").GetValue("Nederlands").GetValue(Config.Layouts[i].DescriptionID)).Value;
                        Config.Layouts[i].Name.PortugueseUSA = ((Cll.JSON.String)config.Value.GetValue("strings").GetValue("PortugueseUSA").GetValue(Config.Layouts[i].NameID)).Value;
                        Config.Layouts[i].Description.PortugueseUSA = ((Cll.JSON.String)config.Value.GetValue("strings").GetValue("PortugueseUSA").GetValue(Config.Layouts[i].DescriptionID)).Value;
                        Config.Layouts[i].Name.PortugueseEUR = ((Cll.JSON.String)config.Value.GetValue("strings").GetValue("PortugueseEUR").GetValue(Config.Layouts[i].NameID)).Value;
                        Config.Layouts[i].Description.PortugueseEUR = ((Cll.JSON.String)config.Value.GetValue("strings").GetValue("PortugueseEUR").GetValue(Config.Layouts[i].DescriptionID)).Value;
                        Config.Layouts[i].Name.Russian = ((Cll.JSON.String)config.Value.GetValue("strings").GetValue("Russian").GetValue(Config.Layouts[i].NameID)).Value;
                        Config.Layouts[i].Description.Russian = ((Cll.JSON.String)config.Value.GetValue("strings").GetValue("Russian").GetValue(Config.Layouts[i].DescriptionID)).Value;
                        Config.Layouts[i].Name.SpanishUSA = ((Cll.JSON.String)config.Value.GetValue("strings").GetValue("SpanishUSA").GetValue(Config.Layouts[i].NameID)).Value;
                        Config.Layouts[i].Description.SpanishUSA = ((Cll.JSON.String)config.Value.GetValue("strings").GetValue("SpanishUSA").GetValue(Config.Layouts[i].DescriptionID)).Value;
                        Config.Layouts[i].Name.SpanishEUR = ((Cll.JSON.String)config.Value.GetValue("strings").GetValue("SpanishEUR").GetValue(Config.Layouts[i].NameID)).Value;
                        Config.Layouts[i].Description.SpanishEUR = ((Cll.JSON.String)config.Value.GetValue("strings").GetValue("SpanishEUR").GetValue(Config.Layouts[i].DescriptionID)).Value;
                        comboBoxLayout.Items.Add(Config.Layouts[i].Name.Default);
                    }

                    CurrentLayout = 0;
                    CurrentTarget = Target.TV;

                    UpdateControls = true;
                    comboBoxLayout.SelectedIndex = 0;
                    comboBoxTarget.SelectedIndex = 0;
                    UpdateControls = false;

                    groupBoxLayout.Text = comboBoxLayout.Items[CurrentLayout].ToString() + " - " + (string)comboBoxTarget.SelectedItem;
                    UpdateText();
                    LoadLayoutScreen();
                    UpdatePreview();

                    CurrentFilePath = openFileDialog.FileName;
                    this.Text = "VCNDSLayout Editor " + Release + " :: " + Path.GetFileNameWithoutExtension(openFileDialog.FileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    if (sr != null)
                        sr.Close();
                }
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveCurrentFile();
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog.FileName = "";
            saveFileDialog.Filter = "Layout for Wii U NDS Virtual Console|*.luds";
            if (comboBoxLayout.Items.Count != 0 && saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                SaveFile(saveFileDialog.FileName);
                CurrentFilePath = saveFileDialog.FileName;
                this.Text = "VCNDSLayout Editor " + Release + " :: " + Path.GetFileNameWithoutExtension(saveFileDialog.FileName);
            }
        }

        private void SaveCurrentFile()
        {
            if (File.Exists(CurrentFilePath))
            {
                SaveFile(CurrentFilePath);
                this.Text = "VCNDSLayout Editor " + Release + " :: " + Path.GetFileNameWithoutExtension(CurrentFilePath);
            }
            else
            {
                saveFileDialog.FileName = "";
                saveFileDialog.Filter = "Layout for Wii U NDS Virtual Console|*.luds";
                if (comboBoxLayout.Items.Count != 0 && saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    SaveFile(saveFileDialog.FileName);
                    CurrentFilePath = saveFileDialog.FileName;
                    this.Text = "VCNDSLayout Editor " + Release + " :: " + Path.GetFileNameWithoutExtension(saveFileDialog.FileName);
                }
            }
        }

        private void SaveFile(string filename)
        {
            SaveLayoutScreen();

            if (BackgroundTV != null)
            {
                BackgroundTV.Dispose();
                BackgroundTV = null;
            }

            if (BackgroundGamePad != null)
            {
                BackgroundGamePad.Dispose();
                BackgroundGamePad = null;
            }

            string lastConfig = Path.Combine(VCNDSLayoutEditorDataPath, "LastConfig");
            if (!Directory.Exists(lastConfig))
                Directory.CreateDirectory(lastConfig);

            for (int i = 0; i < Config.Layouts.Length; i++)
            {
                Bitmap bgDest;
                if (Config.Layouts[i].BackgroundTV.Resource != null &&
                    File.Exists(Config.Layouts[i].BackgroundTV.Resource))
                {
                    Bitmap bgSource = new Bitmap(Config.Layouts[i].BackgroundTV.Resource);
                    bgDest = new Bitmap(bgSource, 1280, 720);
                    bgSource.Dispose();
                }
                else
                {
                    bgDest = new Bitmap(1280, 720);
                    Graphics g = Graphics.FromImage(bgDest);
                    g.Clear(Color.FromArgb(16, 16, 16));
                    g.Dispose();
                }
                string dest = Path.Combine(lastConfig, "tv" + (i + 1).ToString("00") + ".png");
                bgDest.Save(dest, System.Drawing.Imaging.ImageFormat.Png);
                bgDest.Dispose();

                if (Config.Layouts[i].BackgroundGamePad.Resource != null &&
                    File.Exists(Config.Layouts[i].BackgroundGamePad.Resource))
                {
                    Bitmap bgSource = new Bitmap(Config.Layouts[i].BackgroundGamePad.Resource);
                    bgDest = new Bitmap(bgSource, 854, 480);
                    bgSource.Dispose();
                }
                else
                {
                    bgDest = new Bitmap(854, 480);
                    Graphics g = Graphics.FromImage(bgDest);
                    g.Clear(Color.FromArgb(16, 16, 16));
                    g.Dispose();
                }
                dest = Path.Combine(lastConfig, "drc" + (i + 1).ToString("00") + ".png");
                bgDest.Save(dest, System.Drawing.Imaging.ImageFormat.Png);
                bgDest.Dispose();
            }

            StreamWriter sw = File.CreateText(Path.Combine(lastConfig, "config.json"));
            sw.Write(Config.GetExtended().ToString(""));
            sw.Close();

            if (File.Exists(filename))
                File.Delete(filename);
            ZipFile.CreateFromDirectory(lastConfig, filename);
        }

        private void importToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult save = DialogResult.No;
            if (this.Text.EndsWith("*"))
                save = MessageBox.Show("Do you want to save the current layout?", "Save?", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

            if (save == DialogResult.Yes)
                SaveCurrentFile();

            if (save != DialogResult.Cancel && folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                comboBoxLayout.Items.Clear();

                StreamReader sr = null;

                Cll.JSON.Element df = null;
                Cll.JSON.Element de = null;
                Cll.JSON.Element enUSA = null;
                Cll.JSON.Element enEUR = null;
                Cll.JSON.Element frUSA = null;
                Cll.JSON.Element frEUR = null;
                Cll.JSON.Element it = null;
                Cll.JSON.Element ja = null;
                Cll.JSON.Element nl = null;
                Cll.JSON.Element ptUSA = null;
                Cll.JSON.Element ptEUR = null;
                Cll.JSON.Element ru = null;
                Cll.JSON.Element esUSA = null;
                Cll.JSON.Element esEUR = null;

                try
                {
                    ValidateBase(folderBrowserDialog.SelectedPath);

                    string contentPath = Path.Combine(folderBrowserDialog.SelectedPath, "content", "0010");
                    string texturesPath = Path.Combine(contentPath, "assets", "textures");
                    string stringsPath = Path.Combine(contentPath, "data", "strings");

                    sr = File.OpenText(Path.Combine(contentPath, "configuration_cafe.json"));
                    Cll.JSON.SyntacticAnalyzer syn = new Cll.JSON.SyntacticAnalyzer(sr);
                    Cll.JSON.Element config = syn.Run();
                    sr.Close();

                    Config = new Configuration(config);

                    foreach (Layout layout in Config.Layouts)
                    {
                        layout.BackgroundTV.Resource = Path.Combine(texturesPath, Path.GetFileName(layout.BackgroundTV.Resource));
                        layout.BackgroundGamePad.Resource = Path.Combine(texturesPath, Path.GetFileName(layout.BackgroundGamePad.Resource));
                    }

                    if (File.Exists(Path.Combine(stringsPath, "strings.json")))
                    {
                        sr = File.OpenText(Path.Combine(stringsPath, "strings.json"));
                        syn = new Cll.JSON.SyntacticAnalyzer(sr);
                        df = syn.Run();
                        sr.Close();
                        for (int i = 0; i < Config.Layouts.Length; i++)
                        {
                            Config.Layouts[i].Name.Default = ((Cll.JSON.String)df.Value.GetValue("strings").GetValue(Config.Layouts[i].NameID)).Value;
                            Config.Layouts[i].Description.Default = ((Cll.JSON.String)df.Value.GetValue("strings").GetValue(Config.Layouts[i].DescriptionID)).Value;
                            comboBoxLayout.Items.Add(Config.Layouts[i].Name.Default);
                        }
                    }

                    if (File.Exists(Path.Combine(stringsPath, "de", "strings.json")))
                    {
                        sr = File.OpenText(Path.Combine(stringsPath, "de", "strings.json"));
                        syn = new Cll.JSON.SyntacticAnalyzer(sr);
                        de = syn.Run();
                        sr.Close();
                        for (int i = 0; i < Config.Layouts.Length; i++)
                        {
                            Config.Layouts[i].Name.Deutsch = ((Cll.JSON.String)de.Value.GetValue("strings").GetValue(Config.Layouts[i].NameID)).Value;
                            Config.Layouts[i].Description.Deutsch = ((Cll.JSON.String)de.Value.GetValue("strings").GetValue(Config.Layouts[i].DescriptionID)).Value;
                        }
                    }

                    if (File.Exists(Path.Combine(stringsPath, "en", "strings.json")))
                    {
                        sr = File.OpenText(Path.Combine(stringsPath, "en", "strings.json"));
                        syn = new Cll.JSON.SyntacticAnalyzer(sr);
                        enUSA = syn.Run();
                        sr.Close();
                        for (int i = 0; i < Config.Layouts.Length; i++)
                        {
                            Config.Layouts[i].Name.EnglishUSA = ((Cll.JSON.String)enUSA.Value.GetValue("strings").GetValue(Config.Layouts[i].NameID)).Value;
                            Config.Layouts[i].Description.EnglishUSA = ((Cll.JSON.String)enUSA.Value.GetValue("strings").GetValue(Config.Layouts[i].DescriptionID)).Value;
                        }
                    }

                    if (File.Exists(Path.Combine(stringsPath, "eur_en", "strings.json")))
                    {
                        sr = File.OpenText(Path.Combine(stringsPath, "eur_en", "strings.json"));
                        syn = new Cll.JSON.SyntacticAnalyzer(sr);
                        enEUR = syn.Run();
                        sr.Close();
                        for (int i = 0; i < Config.Layouts.Length; i++)
                        {
                            Config.Layouts[i].Name.EnglishEUR = ((Cll.JSON.String)enEUR.Value.GetValue("strings").GetValue(Config.Layouts[i].NameID)).Value;
                            Config.Layouts[i].Description.EnglishEUR = ((Cll.JSON.String)enEUR.Value.GetValue("strings").GetValue(Config.Layouts[i].DescriptionID)).Value;
                        }
                    }

                    if (File.Exists(Path.Combine(stringsPath, "usa_fr", "strings.json")))
                    {
                        sr = File.OpenText(Path.Combine(stringsPath, "usa_fr", "strings.json"));
                        syn = new Cll.JSON.SyntacticAnalyzer(sr);
                        frUSA = syn.Run();
                        sr.Close();
                        for (int i = 0; i < Config.Layouts.Length; i++)
                        {
                            Config.Layouts[i].Name.FrenchUSA = ((Cll.JSON.String)frUSA.Value.GetValue("strings").GetValue(Config.Layouts[i].NameID)).Value;
                            Config.Layouts[i].Description.FrenchUSA = ((Cll.JSON.String)frUSA.Value.GetValue("strings").GetValue(Config.Layouts[i].DescriptionID)).Value;
                        }
                    }

                    if (File.Exists(Path.Combine(stringsPath, "fr", "strings.json")))
                    {
                        sr = File.OpenText(Path.Combine(stringsPath, "fr", "strings.json"));
                        syn = new Cll.JSON.SyntacticAnalyzer(sr);
                        frEUR = syn.Run();
                        sr.Close();
                        for (int i = 0; i < Config.Layouts.Length; i++)
                        {
                            Config.Layouts[i].Name.FrenchEUR = ((Cll.JSON.String)frEUR.Value.GetValue("strings").GetValue(Config.Layouts[i].NameID)).Value;
                            Config.Layouts[i].Description.FrenchEUR = ((Cll.JSON.String)frEUR.Value.GetValue("strings").GetValue(Config.Layouts[i].DescriptionID)).Value;
                        }
                    }

                    if (File.Exists(Path.Combine(stringsPath, "it", "strings.json")))
                    {
                        sr = File.OpenText(Path.Combine(stringsPath, "it", "strings.json"));
                        syn = new Cll.JSON.SyntacticAnalyzer(sr);
                        it = syn.Run();
                        sr.Close();
                        for (int i = 0; i < Config.Layouts.Length; i++)
                        {
                            Config.Layouts[i].Name.Italian = ((Cll.JSON.String)it.Value.GetValue("strings").GetValue(Config.Layouts[i].NameID)).Value;
                            Config.Layouts[i].Description.Italian = ((Cll.JSON.String)it.Value.GetValue("strings").GetValue(Config.Layouts[i].DescriptionID)).Value;
                        }
                    }

                    if (File.Exists(Path.Combine(stringsPath, "ja", "strings.json")))
                    {
                        sr = File.OpenText(Path.Combine(stringsPath, "ja", "strings.json"));
                        syn = new Cll.JSON.SyntacticAnalyzer(sr);
                        ja = syn.Run();
                        sr.Close();
                        for (int i = 0; i < Config.Layouts.Length; i++)
                        {
                            Config.Layouts[i].Name.Japanese = ((Cll.JSON.String)ja.Value.GetValue("strings").GetValue(Config.Layouts[i].NameID)).Value;
                            Config.Layouts[i].Description.Japanese = ((Cll.JSON.String)ja.Value.GetValue("strings").GetValue(Config.Layouts[i].DescriptionID)).Value;
                        }
                    }

                    if (File.Exists(Path.Combine(stringsPath, "nl", "strings.json")))
                    {
                        sr = File.OpenText(Path.Combine(stringsPath, "nl", "strings.json"));
                        syn = new Cll.JSON.SyntacticAnalyzer(sr);
                        nl = syn.Run();
                        sr.Close();
                        for (int i = 0; i < Config.Layouts.Length; i++)
                        {
                            Config.Layouts[i].Name.Nederlands = ((Cll.JSON.String)nl.Value.GetValue("strings").GetValue(Config.Layouts[i].NameID)).Value;
                            Config.Layouts[i].Description.Nederlands = ((Cll.JSON.String)nl.Value.GetValue("strings").GetValue(Config.Layouts[i].DescriptionID)).Value;
                        }
                    }

                    if (File.Exists(Path.Combine(stringsPath, "usa_pt", "strings.json")))
                    {
                        sr = File.OpenText(Path.Combine(stringsPath, "usa_pt", "strings.json"));
                        syn = new Cll.JSON.SyntacticAnalyzer(sr);
                        ptUSA = syn.Run();
                        sr.Close();
                        for (int i = 0; i < Config.Layouts.Length; i++)
                        {
                            Config.Layouts[i].Name.PortugueseUSA = ((Cll.JSON.String)ptUSA.Value.GetValue("strings").GetValue(Config.Layouts[i].NameID)).Value;
                            Config.Layouts[i].Description.PortugueseUSA = ((Cll.JSON.String)ptUSA.Value.GetValue("strings").GetValue(Config.Layouts[i].DescriptionID)).Value;
                        }
                    }

                    if (File.Exists(Path.Combine(stringsPath, "pt", "strings.json")))
                    {
                        sr = File.OpenText(Path.Combine(stringsPath, "pt", "strings.json"));
                        syn = new Cll.JSON.SyntacticAnalyzer(sr);
                        ptEUR = syn.Run();
                        sr.Close();
                        for (int i = 0; i < Config.Layouts.Length; i++)
                        {
                            Config.Layouts[i].Name.PortugueseEUR = ((Cll.JSON.String)ptEUR.Value.GetValue("strings").GetValue(Config.Layouts[i].NameID)).Value;
                            Config.Layouts[i].Description.PortugueseEUR = ((Cll.JSON.String)ptEUR.Value.GetValue("strings").GetValue(Config.Layouts[i].DescriptionID)).Value;
                        }
                    }

                    if (File.Exists(Path.Combine(stringsPath, "ru", "strings.json")))
                    {
                        sr = File.OpenText(Path.Combine(stringsPath, "ru", "strings.json"));
                        syn = new Cll.JSON.SyntacticAnalyzer(sr);
                        ru = syn.Run();
                        sr.Close();
                        for (int i = 0; i < Config.Layouts.Length; i++)
                        {
                            Config.Layouts[i].Name.Russian = ((Cll.JSON.String)ru.Value.GetValue("strings").GetValue(Config.Layouts[i].NameID)).Value;
                            Config.Layouts[i].Description.Russian = ((Cll.JSON.String)ru.Value.GetValue("strings").GetValue(Config.Layouts[i].DescriptionID)).Value;
                        }
                    }

                    if (File.Exists(Path.Combine(stringsPath, "usa_es", "strings.json")))
                    {
                        sr = File.OpenText(Path.Combine(stringsPath, "usa_es", "strings.json"));
                        syn = new Cll.JSON.SyntacticAnalyzer(sr);
                        esUSA = syn.Run();
                        sr.Close();
                        for (int i = 0; i < Config.Layouts.Length; i++)
                        {
                            Config.Layouts[i].Name.SpanishUSA = ((Cll.JSON.String)esUSA.Value.GetValue("strings").GetValue(Config.Layouts[i].NameID)).Value;
                            Config.Layouts[i].Description.SpanishUSA = ((Cll.JSON.String)esUSA.Value.GetValue("strings").GetValue(Config.Layouts[i].DescriptionID)).Value;
                        }
                    }

                    if (File.Exists(Path.Combine(stringsPath, "es", "strings.json")))
                    {
                        sr = File.OpenText(Path.Combine(stringsPath, "es", "strings.json"));
                        syn = new Cll.JSON.SyntacticAnalyzer(sr);
                        esEUR = syn.Run();
                        sr.Close();
                        for (int i = 0; i < Config.Layouts.Length; i++)
                        {
                            Config.Layouts[i].Name.SpanishEUR = ((Cll.JSON.String)esEUR.Value.GetValue("strings").GetValue(Config.Layouts[i].NameID)).Value;
                            Config.Layouts[i].Description.SpanishEUR = ((Cll.JSON.String)esEUR.Value.GetValue("strings").GetValue(Config.Layouts[i].DescriptionID)).Value;
                        }
                    }

                    Config.Brightness = 100;

                    CurrentLayout = 0;
                    CurrentTarget = Target.TV;

                    UpdateControls = true;
                    comboBoxLayout.SelectedIndex = 0;
                    comboBoxTarget.SelectedIndex = 0;
                    UpdateControls = false;

                    groupBoxLayout.Text = comboBoxLayout.Items[CurrentLayout].ToString() + " - " + (string)comboBoxTarget.SelectedItem;
                    UpdateText();
                    LoadLayoutScreen();
                    UpdatePreview();

                    this.Text = "VCNDSLayout Editor " + Release + " :: " + Path.GetFileName(folderBrowserDialog.SelectedPath);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    if (sr != null)
                        sr.Close();
                }
            }
        }

        private void exportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (comboBoxLayout.Items.Count != 0 && folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                SaveLayoutScreen();

                StreamWriter sw = null;
                StreamReader sr = null;

                Cll.JSON.Element df = null;
                Cll.JSON.Element de = null;
                Cll.JSON.Element enUSA = null;
                Cll.JSON.Element enEUR = null;
                Cll.JSON.Element frUSA = null;
                Cll.JSON.Element frEUR = null;
                Cll.JSON.Element it = null;
                Cll.JSON.Element ja = null;
                Cll.JSON.Element nl = null;
                Cll.JSON.Element ptUSA = null;
                Cll.JSON.Element ptEUR = null;
                Cll.JSON.Element ru = null;
                Cll.JSON.Element esUSA = null;
                Cll.JSON.Element esEUR = null;

                try
                {
                    ValidateBase(folderBrowserDialog.SelectedPath);
                    Cll.JSON.SyntacticAnalyzer syn;
                    string contentPath = Path.Combine(folderBrowserDialog.SelectedPath, "content", "0010");
                    string texturesPath = Path.Combine(contentPath, "assets", "textures");
                    string stringsPath = Path.Combine(contentPath, "data", "strings");

                    for (int i = 0; i < Config.Layouts.Length; i++)
                    {
                        Bitmap bgDest;
                        if (Config.Layouts[i].BackgroundTV.Resource != null &&
                            File.Exists(Config.Layouts[i].BackgroundTV.Resource))
                        {
                            Bitmap bgSource = new Bitmap(Config.Layouts[i].BackgroundTV.Resource);
                            bgDest = new Bitmap(bgSource, 1280, 720);
                            bgSource.Dispose();
                        }
                        else
                        {
                            bgDest = new Bitmap(1280, 720);
                            Graphics g = Graphics.FromImage(bgDest);
                            g.Clear(Color.FromArgb(16, 16, 16));
                            g.Dispose();
                        }
                        string dest = Path.Combine(texturesPath, "tv" + (i + 1).ToString("00") + ".png");
                        bgDest.Save(dest, System.Drawing.Imaging.ImageFormat.Png);
                        bgDest.Dispose();

                        if (Config.Layouts[i].BackgroundGamePad.Resource != null &&
                            File.Exists(Config.Layouts[i].BackgroundGamePad.Resource))
                        {
                            Bitmap bgSource = new Bitmap(Config.Layouts[i].BackgroundGamePad.Resource);
                            bgDest = new Bitmap(bgSource, 854, 480);
                            bgSource.Dispose();
                        }
                        else
                        {
                            bgDest = new Bitmap(854, 480);
                            Graphics g = Graphics.FromImage(bgDest);
                            g.Clear(Color.FromArgb(16, 16, 16));
                            g.Dispose();
                        }
                        dest = Path.Combine(texturesPath, "drc" + (i + 1).ToString("00") + ".png");
                        bgDest.Save(dest, System.Drawing.Imaging.ImageFormat.Png);
                        bgDest.Dispose();
                    }

                    if (File.Exists(Path.Combine(stringsPath, "strings.json")))
                    {
                        sr = File.OpenText(Path.Combine(stringsPath, "strings.json"));
                        syn = new Cll.JSON.SyntacticAnalyzer(sr);
                        df = syn.Run();
                        sr.Close();
                        for (int i = 0; i < Config.Layouts.Length; i++)
                        {
                            df.Value.GetValue("strings").SetValue(Config.Layouts[i].NameID, new Cll.JSON.String(Config.Layouts[i].Name.Default));
                            df.Value.GetValue("strings").SetValue(Config.Layouts[i].DescriptionID, new Cll.JSON.String(Config.Layouts[i].Description.Default));
                        }
                        sw = File.CreateText(Path.Combine(stringsPath, "strings.json"));
                        sw.Write(df.ToString(""));
                        sw.Close();
                    }

                    if (File.Exists(Path.Combine(stringsPath, "de", "strings.json")))
                    {
                        sr = File.OpenText(Path.Combine(stringsPath, "de", "strings.json"));
                        syn = new Cll.JSON.SyntacticAnalyzer(sr);
                        de = syn.Run();
                        sr.Close();
                        for (int i = 0; i < Config.Layouts.Length; i++)
                        {
                            de.Value.GetValue("strings").SetValue(Config.Layouts[i].NameID, new Cll.JSON.String(Config.Layouts[i].Name.Deutsch));
                            de.Value.GetValue("strings").SetValue(Config.Layouts[i].DescriptionID, new Cll.JSON.String(Config.Layouts[i].Description.Deutsch));
                        }
                        sw = File.CreateText(Path.Combine(stringsPath, "de", "strings.json"));
                        sw.Write(de.ToString(""));
                        sw.Close();
                    }

                    if (File.Exists(Path.Combine(stringsPath, "en", "strings.json")))
                    {
                        sr = File.OpenText(Path.Combine(stringsPath, "en", "strings.json"));
                        syn = new Cll.JSON.SyntacticAnalyzer(sr);
                        enUSA = syn.Run();
                        sr.Close();
                        for (int i = 0; i < Config.Layouts.Length; i++)
                        {
                            enUSA.Value.GetValue("strings").SetValue(Config.Layouts[i].NameID, new Cll.JSON.String(Config.Layouts[i].Name.EnglishUSA));
                            enUSA.Value.GetValue("strings").SetValue(Config.Layouts[i].DescriptionID, new Cll.JSON.String(Config.Layouts[i].Description.EnglishUSA));
                        }
                        sw = File.CreateText(Path.Combine(stringsPath, "en", "strings.json"));
                        sw.Write(enUSA.ToString(""));
                        sw.Close();
                    }

                    if (File.Exists(Path.Combine(stringsPath, "eur_en", "strings.json")))
                    {
                        sr = File.OpenText(Path.Combine(stringsPath, "eur_en", "strings.json"));
                        syn = new Cll.JSON.SyntacticAnalyzer(sr);
                        enEUR = syn.Run();
                        sr.Close();
                        for (int i = 0; i < Config.Layouts.Length; i++)
                        {
                            enEUR.Value.GetValue("strings").SetValue(Config.Layouts[i].NameID, new Cll.JSON.String(Config.Layouts[i].Name.EnglishEUR));
                            enEUR.Value.GetValue("strings").SetValue(Config.Layouts[i].DescriptionID, new Cll.JSON.String(Config.Layouts[i].Description.EnglishEUR));
                        }
                        sw = File.CreateText(Path.Combine(stringsPath, "eur_en", "strings.json"));
                        sw.Write(enEUR.ToString(""));
                        sw.Close();
                    }

                    if (File.Exists(Path.Combine(stringsPath, "usa_fr", "strings.json")))
                    {
                        sr = File.OpenText(Path.Combine(stringsPath, "usa_fr", "strings.json"));
                        syn = new Cll.JSON.SyntacticAnalyzer(sr);
                        frUSA = syn.Run();
                        sr.Close();
                        for (int i = 0; i < Config.Layouts.Length; i++)
                        {
                            frUSA.Value.GetValue("strings").SetValue(Config.Layouts[i].NameID, new Cll.JSON.String(Config.Layouts[i].Name.FrenchUSA));
                            frUSA.Value.GetValue("strings").SetValue(Config.Layouts[i].DescriptionID, new Cll.JSON.String(Config.Layouts[i].Description.FrenchUSA));
                        }
                        sw = File.CreateText(Path.Combine(stringsPath, "usa_fr", "strings.json"));
                        sw.Write(frUSA.ToString(""));
                        sw.Close();
                    }

                    if (File.Exists(Path.Combine(stringsPath, "fr", "strings.json")))
                    {
                        sr = File.OpenText(Path.Combine(stringsPath, "fr", "strings.json"));
                        syn = new Cll.JSON.SyntacticAnalyzer(sr);
                        frEUR = syn.Run();
                        sr.Close();
                        for (int i = 0; i < Config.Layouts.Length; i++)
                        {
                            frEUR.Value.GetValue("strings").SetValue(Config.Layouts[i].NameID, new Cll.JSON.String(Config.Layouts[i].Name.FrenchEUR));
                            frEUR.Value.GetValue("strings").SetValue(Config.Layouts[i].DescriptionID, new Cll.JSON.String(Config.Layouts[i].Description.FrenchEUR));
                        }
                        sw = File.CreateText(Path.Combine(stringsPath, "fr", "strings.json"));
                        sw.Write(frEUR.ToString(""));
                        sw.Close();
                    }

                    if (File.Exists(Path.Combine(stringsPath, "it", "strings.json")))
                    {
                        sr = File.OpenText(Path.Combine(stringsPath, "it", "strings.json"));
                        syn = new Cll.JSON.SyntacticAnalyzer(sr);
                        it = syn.Run();
                        sr.Close();
                        for (int i = 0; i < Config.Layouts.Length; i++)
                        {
                            it.Value.GetValue("strings").SetValue(Config.Layouts[i].NameID, new Cll.JSON.String(Config.Layouts[i].Name.Italian));
                            it.Value.GetValue("strings").SetValue(Config.Layouts[i].DescriptionID, new Cll.JSON.String(Config.Layouts[i].Description.Italian));
                        }
                        sw = File.CreateText(Path.Combine(stringsPath, "it", "strings.json"));
                        sw.Write(it.ToString(""));
                        sw.Close();
                    }

                    if (File.Exists(Path.Combine(stringsPath, "ja", "strings.json")))
                    {
                        sr = File.OpenText(Path.Combine(stringsPath, "ja", "strings.json"));
                        syn = new Cll.JSON.SyntacticAnalyzer(sr);
                        ja = syn.Run();
                        sr.Close();
                        for (int i = 0; i < Config.Layouts.Length; i++)
                        {
                            ja.Value.GetValue("strings").SetValue(Config.Layouts[i].NameID, new Cll.JSON.String(Config.Layouts[i].Name.Japanese));
                            ja.Value.GetValue("strings").SetValue(Config.Layouts[i].DescriptionID, new Cll.JSON.String(Config.Layouts[i].Description.Japanese));
                        }
                        sw = File.CreateText(Path.Combine(stringsPath, "ja", "strings.json"));
                        sw.Write(ja.ToString(""));
                        sw.Close();
                    }

                    if (File.Exists(Path.Combine(stringsPath, "nl", "strings.json")))
                    {
                        sr = File.OpenText(Path.Combine(stringsPath, "nl", "strings.json"));
                        syn = new Cll.JSON.SyntacticAnalyzer(sr);
                        nl = syn.Run();
                        sr.Close();
                        for (int i = 0; i < Config.Layouts.Length; i++)
                        {
                            nl.Value.GetValue("strings").SetValue(Config.Layouts[i].NameID, new Cll.JSON.String(Config.Layouts[i].Name.Nederlands));
                            nl.Value.GetValue("strings").SetValue(Config.Layouts[i].DescriptionID, new Cll.JSON.String(Config.Layouts[i].Description.Nederlands));
                        }
                        sw = File.CreateText(Path.Combine(stringsPath, "nl", "strings.json"));
                        sw.Write(nl.ToString(""));
                        sw.Close();
                    }

                    if (File.Exists(Path.Combine(stringsPath, "usa_pt", "strings.json")))
                    {
                        sr = File.OpenText(Path.Combine(stringsPath, "usa_pt", "strings.json"));
                        syn = new Cll.JSON.SyntacticAnalyzer(sr);
                        ptUSA = syn.Run();
                        sr.Close();
                        for (int i = 0; i < Config.Layouts.Length; i++)
                        {
                            ptUSA.Value.GetValue("strings").SetValue(Config.Layouts[i].NameID, new Cll.JSON.String(Config.Layouts[i].Name.PortugueseUSA));
                            ptUSA.Value.GetValue("strings").SetValue(Config.Layouts[i].DescriptionID, new Cll.JSON.String(Config.Layouts[i].Description.PortugueseUSA));
                        }
                        sw = File.CreateText(Path.Combine(stringsPath, "usa_pt", "strings.json"));
                        sw.Write(ptUSA.ToString(""));
                        sw.Close();
                    }

                    if (File.Exists(Path.Combine(stringsPath, "pt", "strings.json")))
                    {
                        sr = File.OpenText(Path.Combine(stringsPath, "pt", "strings.json"));
                        syn = new Cll.JSON.SyntacticAnalyzer(sr);
                        ptEUR = syn.Run();
                        sr.Close();
                        for (int i = 0; i < Config.Layouts.Length; i++)
                        {
                            ptEUR.Value.GetValue("strings").SetValue(Config.Layouts[i].NameID, new Cll.JSON.String(Config.Layouts[i].Name.PortugueseEUR));
                            ptEUR.Value.GetValue("strings").SetValue(Config.Layouts[i].DescriptionID, new Cll.JSON.String(Config.Layouts[i].Description.PortugueseEUR));
                        }
                        sw = File.CreateText(Path.Combine(stringsPath, "pt", "strings.json"));
                        sw.Write(ptEUR.ToString(""));
                        sw.Close();
                    }

                    if (File.Exists(Path.Combine(stringsPath, "ru", "strings.json")))
                    {
                        sr = File.OpenText(Path.Combine(stringsPath, "ru", "strings.json"));
                        syn = new Cll.JSON.SyntacticAnalyzer(sr);
                        ru = syn.Run();
                        sr.Close();
                        for (int i = 0; i < Config.Layouts.Length; i++)
                        {
                            ru.Value.GetValue("strings").SetValue(Config.Layouts[i].NameID, new Cll.JSON.String(Config.Layouts[i].Name.Russian));
                            ru.Value.GetValue("strings").SetValue(Config.Layouts[i].DescriptionID, new Cll.JSON.String(Config.Layouts[i].Description.Russian));
                        }
                        sw = File.CreateText(Path.Combine(stringsPath, "ru", "strings.json"));
                        sw.Write(ru.ToString(""));
                        sw.Close();
                    }

                    if (File.Exists(Path.Combine(stringsPath, "usa_es", "strings.json")))
                    {
                        sr = File.OpenText(Path.Combine(stringsPath, "usa_es", "strings.json"));
                        syn = new Cll.JSON.SyntacticAnalyzer(sr);
                        esUSA = syn.Run();
                        sr.Close();
                        for (int i = 0; i < Config.Layouts.Length; i++)
                        {
                            esUSA.Value.GetValue("strings").SetValue(Config.Layouts[i].NameID, new Cll.JSON.String(Config.Layouts[i].Name.SpanishUSA));
                            esUSA.Value.GetValue("strings").SetValue(Config.Layouts[i].DescriptionID, new Cll.JSON.String(Config.Layouts[i].Description.SpanishUSA));
                        }
                        sw = File.CreateText(Path.Combine(stringsPath, "usa_es", "strings.json"));
                        sw.Write(esUSA.ToString(""));
                        sw.Close();
                    }
                    
                    if (File.Exists(Path.Combine(stringsPath, "es", "strings.json")))
                    {
                        sr = File.OpenText(Path.Combine(stringsPath, "es", "strings.json"));
                        syn = new Cll.JSON.SyntacticAnalyzer(sr);
                        esEUR = syn.Run();
                        sr.Close();
                        for (int i = 0; i < Config.Layouts.Length; i++)
                        {
                            esEUR.Value.GetValue("strings").SetValue(Config.Layouts[i].NameID, new Cll.JSON.String(Config.Layouts[i].Name.SpanishEUR));
                            esEUR.Value.GetValue("strings").SetValue(Config.Layouts[i].DescriptionID, new Cll.JSON.String(Config.Layouts[i].Description.SpanishEUR));
                        }
                        sw = File.CreateText(Path.Combine(stringsPath, "en", "strings.json"));
                        sw.Write(esEUR.ToString(""));
                        sw.Close();
                    }

                    sw = File.CreateText(Path.Combine(contentPath, "configuration_cafe.json"));
                    sw.Write(Config.GetJSON().ToString(""));
                    sw.Close();

                    MessageBox.Show("Success!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    if (sw != null)
                        sw.Close();
                    if (sr != null)
                        sr.Close();
                }
            }
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void FormEditor_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult save = DialogResult.No;
            if (this.Text.EndsWith("*"))
                save = MessageBox.Show("Do you want to save the current layout?", "Save?", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

            if (save == DialogResult.Yes)
                SaveCurrentFile();

            if (save == DialogResult.Cancel)
                e.Cancel = true;
        }

        private void LoadLayoutScreen()
        {
            if (comboBoxLayout.Items.Count != 0)
            {
                string resource;
                if (CurrentTarget == Target.GamePad)
                    resource = Config.Layouts[CurrentLayout].BackgroundGamePad.Resource;
                else
                    resource = Config.Layouts[CurrentLayout].BackgroundTV.Resource;

                if (CurrentTarget == Target.GamePad)
                {
                    if (BackgroundGamePad != null)
                    {
                        BackgroundGamePad.Dispose();
                        BackgroundGamePad = null;
                    }
                    BackgroundGamePadPath = resource;
                    if (File.Exists(resource))
                        BackgroundGamePad = new Bitmap(resource);
                    else
                    {
                        BackgroundGamePad = new Bitmap(854, 480);
                        Graphics g = Graphics.FromImage(BackgroundGamePad);
                        g.Clear(Color.FromArgb(16, 16, 16));
                        g.Dispose();
                    }
                }
                else
                {
                    if (BackgroundTV != null)
                    {
                        BackgroundTV.Dispose();
                        BackgroundTV = null;
                    }
                    BackgroundTVPath = resource;
                    if (File.Exists(resource))
                        BackgroundTV = new Bitmap(resource);
                    else
                    {
                        BackgroundTV = new Bitmap(1280, 720);
                        Graphics g = Graphics.FromImage(BackgroundTV);
                        g.Clear(Color.FromArgb(16, 16, 16));
                        g.Dispose();
                    }
                }

                Layout.ScreenStruct screenUpper;
                if (CurrentTarget == Target.GamePad)
                    screenUpper = Config.Layouts[CurrentLayout].ScreenUpperGamePad;
                else
                    screenUpper = Config.Layouts[CurrentLayout].ScreenUpperTV;
                if (screenUpper.Rotation == 90)
                {
                    Upper.Scale = screenUpper.Size.Height / 256.0F;
                    Upper.Translate.X = screenUpper.Position.X;
                    Upper.Translate.Y = screenUpper.Position.Y + Upper.Width;
                    Upper.Angle = 270.0F;
                }
                else if (screenUpper.Rotation == 270)
                {
                    Upper.Scale = screenUpper.Size.Height / 256.0F;
                    Upper.Translate.X = screenUpper.Position.X + Upper.Height;
                    Upper.Translate.Y = screenUpper.Position.Y;
                    Upper.Angle = 90.0F;
                }
                else
                {
                    Upper.Scale = screenUpper.Size.Width / 256.0F;
                    Upper.Translate.X = screenUpper.Position.X;
                    Upper.Translate.Y = screenUpper.Position.Y;
                    Upper.Angle = 0.0F;
                }
                Upper.Pin = Alignment2D.TopLeft;

                Layout.ScreenStruct screenLower;
                if (CurrentTarget == Target.GamePad)
                    screenLower = Config.Layouts[CurrentLayout].ScreenLowerGamePad;
                else
                    screenLower = Config.Layouts[CurrentLayout].ScreenLowerTV;
                if (screenLower.Rotation == 90)
                {
                    Lower.Scale = screenLower.Size.Height / 256.0F;
                    Lower.Translate.X = screenLower.Position.X;
                    Lower.Translate.Y = screenLower.Position.Y + Lower.Width;
                    Lower.Angle = 270.0F;
                }
                else if (screenLower.Rotation == 270)
                {
                    Lower.Scale = screenLower.Size.Height / 256.0F;
                    Lower.Translate.X = screenLower.Position.X + Lower.Height;
                    Lower.Translate.Y = screenLower.Position.Y;
                    Lower.Angle = 90.0F;
                }
                else
                {
                    Lower.Scale = screenLower.Size.Width / 256.0F;
                    Lower.Translate.X = screenLower.Position.X;
                    Lower.Translate.Y = screenLower.Position.Y;
                    Lower.Angle = 0.0F;
                }
                Lower.Pin = Alignment2D.TopLeft;


                switch (Config.Layouts[CurrentLayout].PadRotation)
                {
                    case 90:
                        comboBoxPad.SelectedItem = "90";
                        break;
                    case 270:
                        comboBoxPad.SelectedItem = "270";
                        break;
                    default:
                        comboBoxPad.SelectedItem = "0";
                        break;
                }

                switch (Config.Layouts[CurrentLayout].DRCRotation)
                {
                    case 90:
                        comboBoxDRC.SelectedItem = "90";
                        break;
                    case 270:
                        comboBoxDRC.SelectedItem = "270";
                        break;
                    default:
                        comboBoxDRC.SelectedItem = "0";
                        break;
                }

                switch (Config.Layouts[CurrentLayout].ButtonsRotation)
                {
                    case 90:
                        comboBoxButtons.SelectedItem = "90";
                        break;
                    case 270:
                        comboBoxButtons.SelectedItem = "270";
                        break;
                    default:
                        comboBoxButtons.SelectedItem = "0";
                        break;
                }

                UpdateControls = true;
                numericUpDownUpperX.Value = (decimal)Upper.Translate.X;
                numericUpDownUpperY.Value = (decimal)Upper.Translate.Y;
                numericUpDownUpperScale.Value = (decimal)Upper.Scale;
                numericUpDownLowerX.Value = (decimal)Lower.Translate.X;
                numericUpDownLowerY.Value = (decimal)Lower.Translate.Y;
                numericUpDownLowerScale.Value = (decimal)Lower.Scale;
                UpdateControls = false;

                UpdateUpperPin();
                UpdateLowerPin();
                UpdateUpperRotationButtons();
                UpdateLowerRotationButtons();
                labelUpperSize.Text = Upper.Width.ToString() + "x" + Upper.Height.ToString();
                labelLowerSize.Text = Lower.Width.ToString() + "x" + Lower.Height.ToString();
            }
        }

        private void SaveLayoutScreen()
        {
            if (comboBoxLayout.Items.Count != 0)
            {
                if (CurrentTarget == Target.GamePad)
                    Config.Layouts[CurrentLayout].BackgroundGamePad.Resource = BackgroundGamePadPath;
                else
                    Config.Layouts[CurrentLayout].BackgroundTV.Resource = BackgroundTVPath;

                Layout.ScreenStruct screenUpper;
                if (CurrentTarget == Target.GamePad)
                    screenUpper = Config.Layouts[CurrentLayout].ScreenUpperGamePad;
                else
                    screenUpper = Config.Layouts[CurrentLayout].ScreenUpperTV;
                if (Upper.Angle == 270.0F)
                {
                    screenUpper.Size.Width = (int)(192.0F * Upper.Scale);
                    screenUpper.Size.Height = (int)(256.0F * Upper.Scale);
                    screenUpper.Position.X = (int)Upper.Translate.X;
                    screenUpper.Position.Y = (int)(Upper.Translate.Y - Upper.Width);
                    screenUpper.Rotation = 90;
                }
                else if (Upper.Angle == 90.0F)
                {
                    screenUpper.Size.Width = (int)(192.0F * Upper.Scale);
                    screenUpper.Size.Height = (int)(256.0F * Upper.Scale);
                    screenUpper.Position.X = (int)(Upper.Translate.X - Upper.Height);
                    screenUpper.Position.Y = (int)Upper.Translate.Y;
                    screenUpper.Rotation = 270;
                }
                else
                {
                    screenUpper.Size.Width = (int)(256.0F * Upper.Scale);
                    screenUpper.Size.Height = (int)(192.0F * Upper.Scale);
                    screenUpper.Position.X = (int)Upper.Translate.X;
                    screenUpper.Position.Y = (int)Upper.Translate.Y;
                    screenUpper.Rotation = 0;
                }
                if (CurrentTarget == Target.GamePad)
                    Config.Layouts[CurrentLayout].ScreenUpperGamePad = screenUpper;
                else
                    Config.Layouts[CurrentLayout].ScreenUpperTV = screenUpper;

                Layout.ScreenStruct screenLower;
                if (CurrentTarget == Target.GamePad)
                    screenLower = Config.Layouts[CurrentLayout].ScreenLowerGamePad;
                else
                    screenLower = Config.Layouts[CurrentLayout].ScreenLowerTV;
                if (Lower.Angle == 270.0F)
                {
                    screenLower.Size.Width = (int)(192.0F * Lower.Scale);
                    screenLower.Size.Height = (int)(256.0F * Lower.Scale);
                    screenLower.Position.X = (int)Lower.Translate.X;
                    screenLower.Position.Y = (int)(Lower.Translate.Y - Lower.Width);
                    screenLower.Rotation = 90;
                }
                else if (Lower.Angle == 90.0F)
                {
                    screenLower.Size.Width = (int)(192.0F * Lower.Scale);
                    screenLower.Size.Height = (int)(256.0F * Lower.Scale);
                    screenLower.Position.X = (int)(Lower.Translate.X - Lower.Height);
                    screenLower.Position.Y = (int)Lower.Translate.Y;
                    screenLower.Rotation = 270;
                }
                else
                {
                    screenLower.Size.Width = (int)(256.0F * Lower.Scale);
                    screenLower.Size.Height = (int)(192.0F * Lower.Scale);
                    screenLower.Position.X = (int)Lower.Translate.X;
                    screenLower.Position.Y = (int)Lower.Translate.Y;
                    screenLower.Rotation = 0;
                }
                if (CurrentTarget == Target.GamePad)
                    Config.Layouts[CurrentLayout].ScreenLowerGamePad = screenLower;
                else
                    Config.Layouts[CurrentLayout].ScreenLowerTV = screenLower;
            }
        }

        private void ValidateBase(string path)
        {
            string[] folders = {
                path + "\\content\\0010\\assets",
                path + "\\content\\0010\\assets\\textures",
                path + "\\content\\0010\\data",
                path + "\\content\\0010\\data\\strings"
            };

            string[] files = {
                path + "\\code\\app.xml",
                path + "\\code\\cos.xml",
                path + "\\code\\hachihachi_ntr.rpx",
                path + "\\content\\0010\\configuration_cafe.json",
                path + "\\content\\0010\\rom.zip",
                path + "\\meta\\iconTex.tga",
                path + "\\meta\\bootTvTex.tga",
                path + "\\meta\\bootDrcTex.tga",
                path + "\\meta\\meta.xml"
            };

            ValidateBase(folders, files);
        }

        private void ValidateBase(string[] folders, string[] files)
        {
            StringBuilder strBuilder = new StringBuilder();

            bool valid = true;
            foreach (string folder in folders)
            {
                if (!Directory.Exists(folder))
                {
                    strBuilder.AppendLine("This folder is missing: \"" + folder + "\"");
                    valid = false;
                }
            }

            foreach (string file in files)
            {
                if (!File.Exists(file))
                {
                    strBuilder.AppendLine("This file is missing: \"" + file + "\"");
                    valid = false;
                }
            }

            if (!valid)
                throw new Exception(strBuilder.ToString());
        }

        private void comboBoxLayout_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!UpdateControls)
            {
                SaveLayoutScreen();
                CurrentLayout = comboBoxLayout.SelectedIndex;
                groupBoxLayout.Text = comboBoxLayout.Items[CurrentLayout].ToString() + " - " + (string)comboBoxTarget.SelectedItem;
                UpdateText();
                LoadLayoutScreen();
                UpdatePreview();
            }
        }

        private void comboBoxTarget_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxLayout.Items.Count != 0 && !UpdateControls)
            {
                SaveLayoutScreen();
                CurrentTarget = comboBoxTarget.SelectedIndex == 1 ? Target.GamePad : Target.TV;
                groupBoxLayout.Text = comboBoxLayout.Items[CurrentLayout].ToString() + " - " + (string)comboBoxTarget.SelectedItem;
                LoadLayoutScreen();
                UpdatePreview();
            }
        }

        private void checkBoxLanguages_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxLanguages.Checked)
                comboBoxLanguage.Enabled = false;
            else
                comboBoxLanguage.Enabled = true;
        }

        private void comboBoxLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateText();
        }

        private void UpdateText()
        {
            if (Config != null)
            {
                switch ((string)comboBoxLanguage.SelectedItem)
                {
                    case "Deutsch":
                        textBoxName.Text = Config.Layouts[CurrentLayout].Name.Deutsch;
                        textBoxDescription.Text = Config.Layouts[CurrentLayout].Description.Deutsch;
                        break;
                    case "English (USA)":
                        textBoxName.Text = Config.Layouts[CurrentLayout].Name.EnglishUSA;
                        textBoxDescription.Text = Config.Layouts[CurrentLayout].Description.EnglishUSA;
                        break;
                    case "English (EUR)":
                        textBoxName.Text = Config.Layouts[CurrentLayout].Name.EnglishEUR;
                        textBoxDescription.Text = Config.Layouts[CurrentLayout].Description.EnglishEUR;
                        break;
                    case "French (USA)":
                        textBoxName.Text = Config.Layouts[CurrentLayout].Name.FrenchUSA;
                        textBoxDescription.Text = Config.Layouts[CurrentLayout].Description.FrenchUSA;
                        break;
                    case "French (EUR)":
                        textBoxName.Text = Config.Layouts[CurrentLayout].Name.FrenchEUR;
                        textBoxDescription.Text = Config.Layouts[CurrentLayout].Description.FrenchEUR;
                        break;
                    case "Italian":
                        textBoxName.Text = Config.Layouts[CurrentLayout].Name.Italian;
                        textBoxDescription.Text = Config.Layouts[CurrentLayout].Description.Italian;
                        break;
                    case "Japanese":
                        textBoxName.Text = Config.Layouts[CurrentLayout].Name.Japanese;
                        textBoxDescription.Text = Config.Layouts[CurrentLayout].Description.Japanese;
                        break;
                    case "Nederlands":
                        textBoxName.Text = Config.Layouts[CurrentLayout].Name.Nederlands;
                        textBoxDescription.Text = Config.Layouts[CurrentLayout].Description.Nederlands;
                        break;
                    case "Portuguese (USA)":
                        textBoxName.Text = Config.Layouts[CurrentLayout].Name.PortugueseUSA;
                        textBoxDescription.Text = Config.Layouts[CurrentLayout].Description.PortugueseUSA;
                        break;
                    case "Portuguese (EUR)":
                        textBoxName.Text = Config.Layouts[CurrentLayout].Name.PortugueseEUR;
                        textBoxDescription.Text = Config.Layouts[CurrentLayout].Description.PortugueseEUR;
                        break;
                    case "Russian":
                        textBoxName.Text = Config.Layouts[CurrentLayout].Name.Russian;
                        textBoxDescription.Text = Config.Layouts[CurrentLayout].Description.Russian;
                        break;
                    case "Spanish (USA)":
                        textBoxName.Text = Config.Layouts[CurrentLayout].Name.SpanishUSA;
                        textBoxDescription.Text = Config.Layouts[CurrentLayout].Description.SpanishUSA;
                        break;
                    case "Spanish (EUR)":
                        textBoxName.Text = Config.Layouts[CurrentLayout].Name.SpanishEUR;
                        textBoxDescription.Text = Config.Layouts[CurrentLayout].Description.SpanishEUR;
                        break;
                    default:
                        textBoxName.Text = Config.Layouts[CurrentLayout].Name.Default;
                        textBoxDescription.Text = Config.Layouts[CurrentLayout].Description.Default;
                        break;
                }
            }
        }

        /*private void listViewDefaultLayouts_DrawItem(object sender, DrawListViewItemEventArgs e)
        {
            if ((e.State & ListViewItemStates.Selected) != 0)
            {
                e.Graphics.DrawImage(Thumb(Color.FromArgb(64, 64, 64), e.ItemIndex), e.Bounds);
            }
            else
            {
                e.Graphics.DrawImage(Thumb(Color.FromArgb(32, 32, 32), e.ItemIndex), e.Bounds);
            }
        }*/

        /*private Bitmap Thumb(Color background, int index)
        {
            Bitmap bmp = new Bitmap(80, 45);

            Graphics g = Graphics.FromImage(bmp);
            g.PixelOffsetMode = PixelOffsetMode.Half;
            //g.InterpolationMode = InterpolationMode.NearestNeighbor;
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.CompositingMode = CompositingMode.SourceOver;
            g.CompositingQuality = CompositingQuality.HighQuality;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

            Font font = new Font("Arial", 128.0F, FontStyle.Regular, GraphicsUnit.Point);

            //Tv  1.0f / 16.0f
            //Drc 3.0f / 32.0f
            g.Clear(background);

            if (!DSTarget.Targets[index].Upper.IsEmpty)
            {
                g.ResetTransform();

                PointF offsetUpperLocation = DSTarget.Targets[index].Upper.OffsetLocation;
                g.Transform = DSTarget.Targets[index].Upper.GetTransform(offsetUpperLocation, 1.0f / 16.0f);

                g.FillRectangle(new SolidBrush(Color.FromArgb(32, 64, 128)), 0, 0, 256, 192);
                SizeF upperSize = g.MeasureString("U", font);
                g.DrawString("U", font, Brushes.Black, (256 - upperSize.Width) / 2, (192 - upperSize.Height) / 2);
            }

            if (!DSTarget.Targets[index].Lower.IsEmpty)
            {
                g.ResetTransform();

                PointF offsetLowerLocation = DSTarget.Targets[index].Lower.OffsetLocation;
                g.Transform = DSTarget.Targets[index].Lower.GetTransform(offsetLowerLocation, 1.0f / 16.0f);

                g.FillRectangle(new SolidBrush(Color.FromArgb(128, 32, 32)), 0, 0, 256, 192);
                SizeF lowerSize = g.MeasureString("L", font);
                g.DrawString("L", font, Brushes.Black, (256 - lowerSize.Width) / 2, (192 - lowerSize.Height) / 2);
            }

            return bmp;
        }*/

        private void UpdatePreview()
        {
            if (panelPreview.BackgroundImage != null)
            {
                panelPreview.BackgroundImage.Dispose();
                panelPreview.BackgroundImage = null;
                GC.Collect();
            }

            Bitmap img = new Bitmap(640, 360);
            panelPreview.BackgroundImage = img;
            Preview = BufferedGraphicsManager.Current.Allocate(Graphics.FromImage(img), new Rectangle(0, 0, 640, 360));

            GeneratePreview(640, 360, CurrentTarget == Target.GamePad ? 0.75F : 0.5F);

            panelPreview.Refresh();
        }

        private void UpdatePreviewFast()
        {
            Changed();
            if (panelPreview.BackgroundImage != null)
            {
                panelPreview.BackgroundImage.Dispose();
                panelPreview.BackgroundImage = null;
                panelPreview.Refresh();
            }

            Preview.Dispose();
            Preview = BufferedGraphicsManager.Current.Allocate(panelPreview.CreateGraphics(), new Rectangle(0, 0, 640, 360));

            GeneratePreview(640, 360, CurrentTarget == Target.GamePad ? 0.75F : 0.5F);
        }

        private void SavePreview(string path)
        {
            int width = CurrentTarget == Target.GamePad ? 854 : 1280;
            int height = CurrentTarget == Target.GamePad ? 480 : 720;

            Bitmap img =  new Bitmap(width, height);
            Preview = BufferedGraphicsManager.Current.Allocate(Graphics.FromImage(img), new Rectangle(0, 0, width, height));

            GeneratePreview(width, height, 1.0F);

            img.Save(path, System.Drawing.Imaging.ImageFormat.Png);
            img.Dispose();
        }

        private void GeneratePreview(int width, int height, float scale)
        {
            if (Config.Bilinear == 1)
                Preview.Graphics.InterpolationMode = InterpolationMode.Bilinear;
            else
                Preview.Graphics.InterpolationMode = InterpolationMode.NearestNeighbor;

            Font font = new Font("Arial", 32.0F, FontStyle.Regular, GraphicsUnit.Point);

            if (CurrentTarget == Target.GamePad)
            {
                if (BackgroundGamePad == null)
                    Preview.Graphics.Clear(Color.FromArgb(16, 16, 16));
                else
                    Preview.Graphics.DrawImage(BackgroundGamePad, 0, 0, width, height);
            }
            else
            {
                if (BackgroundTV == null)
                    Preview.Graphics.Clear(Color.FromArgb(16, 16, 16));
                else
                    Preview.Graphics.DrawImage(BackgroundTV, 0, 0, width, height);
            }

            if (!Lower.IsEmpty)
            {
                Preview.Graphics.ResetTransform();

                PointF lowerOffsetLocation = Lower.OffsetLocation;
                Preview.Graphics.Transform = Lower.GetTransform(lowerOffsetLocation, scale);

                if (LowerImage == null)
                {
                    Preview.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(128, 32, 32)), 0, 0, 256, 192);
                    SizeF lowerSize = Preview.Graphics.MeasureString("Lower", font);
                    Preview.Graphics.DrawString("Lower", font, Brushes.Black, (256 - lowerSize.Width) / 2, (192 - lowerSize.Height) / 2);
                }
                else
                    Preview.Graphics.DrawImage(LowerImage, 0, 0, 256, 192);
            }

            if (!Upper.IsEmpty)
            {
                Preview.Graphics.ResetTransform();

                PointF upperOffsetLocation = Upper.OffsetLocation;
                Preview.Graphics.Transform = Upper.GetTransform(upperOffsetLocation, scale);

                if (UpperImage == null)
                {
                    Preview.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(32, 64, 128)), 0, 0, 256, 192);
                    SizeF upperSize = Preview.Graphics.MeasureString("Upper", font);
                    Preview.Graphics.DrawString("Upper", font, Brushes.Black, (256 - upperSize.Width) / 2, (192 - upperSize.Height) / 2);
                }
                else
                    Preview.Graphics.DrawImage(UpperImage, 0, 0, 256, 192);
            }

            Preview.Render();
        }


        private void panelUpperPin_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.X >= 1 && e.X <= 5 && e.Y >= 1 && e.Y <= 5)
                Upper.Pin = Alignment2D.TopLeft;
            else if (e.X >= 9 && e.X <= 13 && e.Y >= 1 && e.Y <= 5)
                Upper.Pin = Alignment2D.TopCenter;
            else if (e.X >= 17 && e.X <= 21 && e.Y >= 1 && e.Y <= 5)
                Upper.Pin = Alignment2D.TopRight;
            else if (e.X >= 1 && e.X <= 5 && e.Y >= 7 && e.Y <= 11)
                Upper.Pin = Alignment2D.MiddleLeft;
            else if (e.X >= 9 && e.X <= 13 && e.Y >= 7 && e.Y <= 11)
                Upper.Pin = Alignment2D.MiddleCenter;
            else if (e.X >= 17 && e.X <= 21 && e.Y >= 7 && e.Y <= 11)
                Upper.Pin = Alignment2D.MiddleRight;
            else if (e.X >= 1 && e.X <= 5 && e.Y >= 13 && e.Y <= 17)
                Upper.Pin = Alignment2D.BottomLeft;
            else if (e.X >= 9 && e.X <= 13 && e.Y >= 13 && e.Y <= 17)
                Upper.Pin = Alignment2D.BottomCenter;
            else if (e.X >= 17 && e.X <= 21 && e.Y >= 13 && e.Y <= 17)
                Upper.Pin = Alignment2D.BottomRight;

            UpdateUpperPin();

            UpdateUpperX = false;
            numericUpDownUpperX.Value = (decimal)Math.Ceiling(Math.Round(Upper.PinX, 1, MidpointRounding.AwayFromZero));
            UpdateUpperY = false;
            numericUpDownUpperY.Value = (decimal)Math.Ceiling(Math.Round(Upper.PinY, 1, MidpointRounding.AwayFromZero));
        }

        private void UpdateUpperPin()
        {
            Bitmap bmp = new Bitmap(23, 19);

            Graphics g = Graphics.FromImage(bmp);
            g.PixelOffsetMode = PixelOffsetMode.Half;
            //g.InterpolationMode = InterpolationMode.NearestNeighbor;
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.CompositingMode = CompositingMode.SourceOver;
            g.CompositingQuality = CompositingQuality.HighQuality;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

            g.Clear(SystemColors.Control);
            g.DrawImage(Properties.Resources.Pins, 0, 0, 23, 19);

            switch (Upper.Pin)
            {
                case Alignment2D.TopLeft:
                    g.FillRectangle(new SolidBrush(Color.FromArgb(65, 152, 234)), 2, 2, 3, 3);
                    break;
                case Alignment2D.TopCenter:
                    g.FillRectangle(new SolidBrush(Color.FromArgb(65, 152, 234)), 10, 2, 3, 3);
                    break;
                case Alignment2D.TopRight:
                    g.FillRectangle(new SolidBrush(Color.FromArgb(65, 152, 234)), 18, 2, 3, 3);
                    break;
                case Alignment2D.MiddleLeft:
                    g.FillRectangle(new SolidBrush(Color.FromArgb(65, 152, 234)), 2, 8, 3, 3);
                    break;
                case Alignment2D.MiddleCenter:
                    g.FillRectangle(new SolidBrush(Color.FromArgb(65, 152, 234)), 10, 8, 3, 3);
                    break;
                case Alignment2D.MiddleRight:
                    g.FillRectangle(new SolidBrush(Color.FromArgb(65, 152, 234)), 18, 8, 3, 3);
                    break;
                case Alignment2D.BottomLeft:
                    g.FillRectangle(new SolidBrush(Color.FromArgb(65, 152, 234)), 2, 14, 3, 3);
                    break;
                case Alignment2D.BottomCenter:
                    g.FillRectangle(new SolidBrush(Color.FromArgb(65, 152, 234)), 10, 14, 3, 3);
                    break;
                case Alignment2D.BottomRight:
                    g.FillRectangle(new SolidBrush(Color.FromArgb(65, 152, 234)), 18, 14, 3, 3);
                    break;
            }
            panelUpperPin.BackgroundImage = bmp;
        }

        private void numericUpDownUpperX_ValueChanged(object sender, EventArgs e)
        {
            if (!UpdateControls)
            {
                if (UpdateUpperX && !Upper.IsEmpty)
                {
                    PointF upperOffsetLocation = Upper.OffsetLocation;
                    Upper.Translate.X = (float)numericUpDownUpperX.Value - upperOffsetLocation.X;
                    UpdatePreviewFast();
                }
                else
                    UpdateUpperX = true;
            }
        }

        private void numericUpDownUpperY_ValueChanged(object sender, EventArgs e)
        {
            if (!UpdateControls)
            {
                if (UpdateUpperY && !Upper.IsEmpty)
                {
                    PointF upperOffsetLocation = Upper.OffsetLocation;
                    Upper.Translate.Y = (float)numericUpDownUpperY.Value - upperOffsetLocation.Y;
                    UpdatePreviewFast();
                }
                else
                    UpdateUpperY = true;
            }
        }

        private void numericUpDownUpperScale_ValueChanged(object sender, EventArgs e)
        {
            if (!UpdateControls)
            {
                Upper.Scale = (float)numericUpDownUpperScale.Value;
                PointF upperOffsetLocation = Upper.OffsetLocation;
                Upper.Translate.X = (float)numericUpDownUpperX.Value - upperOffsetLocation.X;
                Upper.Translate.Y = (float)numericUpDownUpperY.Value - upperOffsetLocation.Y;
                labelUpperSize.Text = Upper.Width.ToString() + "x" + Upper.Height.ToString();
                UpdatePreviewFast();
            }
        }

        private void panelUpperLeft_Click(object sender, EventArgs e)
        {
            Upper.Angle = 270.0f;
            PointF upperOffsetLocation = Upper.OffsetLocation;
            Upper.Translate.X = (float)numericUpDownUpperX.Value - upperOffsetLocation.X;
            Upper.Translate.Y = (float)numericUpDownUpperY.Value - upperOffsetLocation.Y;
            UpdateUpperRotationButtons();
            UpdatePreviewFast();
        }

        private void panelUpperStand_Click(object sender, EventArgs e)
        {
            Upper.Angle = 0;
            PointF upperOffsetLocation = Upper.OffsetLocation;
            Upper.Translate.X = (float)numericUpDownUpperX.Value - upperOffsetLocation.X;
            Upper.Translate.Y = (float)numericUpDownUpperY.Value - upperOffsetLocation.Y;
            UpdateUpperRotationButtons();
            UpdatePreviewFast();
        }

        private void panelUpperRight_Click(object sender, EventArgs e)
        {
            Upper.Angle = 90.0f;
            PointF upperOffsetLocation = Upper.OffsetLocation;
            Upper.Translate.X = (float)numericUpDownUpperX.Value - upperOffsetLocation.X;
            Upper.Translate.Y = (float)numericUpDownUpperY.Value - upperOffsetLocation.Y;
            UpdateUpperRotationButtons();
            UpdatePreviewFast();
        }

        private void UpdateUpperRotationButtons()
        {
            if (Upper.Angle == 0.0f)
            {
                panelUpperLeft.BackgroundImage = Properties.Resources.Left;
                panelUpperStand.BackgroundImage = Properties.Resources.Stand_light;
                panelUpperRight.BackgroundImage = Properties.Resources.Right;
            }
            else if (Upper.Angle == 270.0f)
            {
                panelUpperLeft.BackgroundImage = Properties.Resources.Left_light;
                panelUpperStand.BackgroundImage = Properties.Resources.Stand;
                panelUpperRight.BackgroundImage = Properties.Resources.Right;
            }
            else if (Upper.Angle == 90.0f)
            {
                panelUpperLeft.BackgroundImage = Properties.Resources.Left;
                panelUpperStand.BackgroundImage = Properties.Resources.Stand;
                panelUpperRight.BackgroundImage = Properties.Resources.Right_light;
            }
        }


        private void panelLowerPin_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.X >= 1 && e.X <= 5 && e.Y >= 1 && e.Y <= 5)
                Lower.Pin = Alignment2D.TopLeft;
            else if (e.X >= 9 && e.X <= 13 && e.Y >= 1 && e.Y <= 5)
                Lower.Pin = Alignment2D.TopCenter;
            else if (e.X >= 17 && e.X <= 21 && e.Y >= 1 && e.Y <= 5)
                Lower.Pin = Alignment2D.TopRight;
            else if (e.X >= 1 && e.X <= 5 && e.Y >= 7 && e.Y <= 11)
                Lower.Pin = Alignment2D.MiddleLeft;
            else if (e.X >= 9 && e.X <= 13 && e.Y >= 7 && e.Y <= 11)
                Lower.Pin = Alignment2D.MiddleCenter;
            else if (e.X >= 17 && e.X <= 21 && e.Y >= 7 && e.Y <= 11)
                Lower.Pin = Alignment2D.MiddleRight;
            else if (e.X >= 1 && e.X <= 5 && e.Y >= 13 && e.Y <= 17)
                Lower.Pin = Alignment2D.BottomLeft;
            else if (e.X >= 9 && e.X <= 13 && e.Y >= 13 && e.Y <= 17)
                Lower.Pin = Alignment2D.BottomCenter;
            else if (e.X >= 17 && e.X <= 21 && e.Y >= 13 && e.Y <= 17)
                Lower.Pin = Alignment2D.BottomRight;

            UpdateLowerPin();

            UpdateLowerX = false;
            numericUpDownLowerX.Value = (decimal)Math.Ceiling(Math.Round(Lower.PinX, 1, MidpointRounding.AwayFromZero));
            UpdateLowerY = false;
            numericUpDownLowerY.Value = (decimal)Math.Ceiling(Math.Round(Lower.PinY, 1, MidpointRounding.AwayFromZero));
        }

        private void UpdateLowerPin()
        {
            Bitmap bmp = new Bitmap(23, 19);

            Graphics g = Graphics.FromImage(bmp);
            g.PixelOffsetMode = PixelOffsetMode.Half;
            //g.InterpolationMode = InterpolationMode.NearestNeighbor;
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.CompositingMode = CompositingMode.SourceOver;
            g.CompositingQuality = CompositingQuality.HighQuality;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

            g.Clear(SystemColors.Control);
            g.DrawImage(Properties.Resources.Pins, 0, 0, 23, 19);

            switch (Lower.Pin)
            {
                case Alignment2D.TopLeft:
                    g.FillRectangle(new SolidBrush(Color.FromArgb(65, 152, 234)), 2, 2, 3, 3);
                    break;
                case Alignment2D.TopCenter:
                    g.FillRectangle(new SolidBrush(Color.FromArgb(65, 152, 234)), 10, 2, 3, 3);
                    break;
                case Alignment2D.TopRight:
                    g.FillRectangle(new SolidBrush(Color.FromArgb(65, 152, 234)), 18, 2, 3, 3);
                    break;
                case Alignment2D.MiddleLeft:
                    g.FillRectangle(new SolidBrush(Color.FromArgb(65, 152, 234)), 2, 8, 3, 3);
                    break;
                case Alignment2D.MiddleCenter:
                    g.FillRectangle(new SolidBrush(Color.FromArgb(65, 152, 234)), 10, 8, 3, 3);
                    break;
                case Alignment2D.MiddleRight:
                    g.FillRectangle(new SolidBrush(Color.FromArgb(65, 152, 234)), 18, 8, 3, 3);
                    break;
                case Alignment2D.BottomLeft:
                    g.FillRectangle(new SolidBrush(Color.FromArgb(65, 152, 234)), 2, 14, 3, 3);
                    break;
                case Alignment2D.BottomCenter:
                    g.FillRectangle(new SolidBrush(Color.FromArgb(65, 152, 234)), 10, 14, 3, 3);
                    break;
                case Alignment2D.BottomRight:
                    g.FillRectangle(new SolidBrush(Color.FromArgb(65, 152, 234)), 18, 14, 3, 3);
                    break;
            }
            panelLowerPin.BackgroundImage = bmp;
        }

        private void numericUpDownLowerX_ValueChanged(object sender, EventArgs e)
        {
            if (!UpdateControls)
            {
                if (UpdateLowerX && !Lower.IsEmpty)
                {
                    PointF lowerOffsetLocation = Lower.OffsetLocation;
                    Lower.Translate.X = (float)numericUpDownLowerX.Value - lowerOffsetLocation.X;
                    UpdatePreviewFast();
                }
                else
                    UpdateLowerX = true;
            }
        }

        private void numericUpDownLowerY_ValueChanged(object sender, EventArgs e)
        {
            if (!UpdateControls)
            {
                if (UpdateLowerY && !Lower.IsEmpty)
                {
                    PointF lowerOffsetLocation = Lower.OffsetLocation;
                    Lower.Translate.Y = (float)numericUpDownLowerY.Value - lowerOffsetLocation.Y;
                    UpdatePreviewFast();
                }
                else
                    UpdateLowerY = true;
            }
        }

        private void numericUpDownLowerScale_ValueChanged(object sender, EventArgs e)
        {
            if (!UpdateControls)
            {
                Lower.Scale = (float)numericUpDownLowerScale.Value;
                PointF lowerOffsetLocation = Lower.OffsetLocation;
                Lower.Translate.X = (float)numericUpDownLowerX.Value - lowerOffsetLocation.X;
                Lower.Translate.Y = (float)numericUpDownLowerY.Value - lowerOffsetLocation.Y;
                labelLowerSize.Text = Lower.Width.ToString() + "x" + Lower.Height.ToString();
                UpdatePreviewFast();
            }
        }

        private void panelLowerLeft_Click(object sender, EventArgs e)
        {
            Lower.Angle = 270.0f;
            PointF lowerOffsetLocation = Lower.OffsetLocation;
            Lower.Translate.X = (float)numericUpDownLowerX.Value - lowerOffsetLocation.X;
            Lower.Translate.Y = (float)numericUpDownLowerY.Value - lowerOffsetLocation.Y;
            UpdateLowerRotationButtons();
            UpdatePreviewFast();
        }

        private void panelLowerStand_Click(object sender, EventArgs e)
        {
            Lower.Angle = 0;
            PointF lowerOffsetLocation = Lower.OffsetLocation;
            Lower.Translate.X = (float)numericUpDownLowerX.Value - lowerOffsetLocation.X;
            Lower.Translate.Y = (float)numericUpDownLowerY.Value - lowerOffsetLocation.Y;
            UpdateLowerRotationButtons();
            UpdatePreviewFast();
        }

        private void panelLowerRight_Click(object sender, EventArgs e)
        {
            Lower.Angle = 90.0f;
            PointF lowerOffsetLocation = Lower.OffsetLocation;
            Lower.Translate.X = (float)numericUpDownLowerX.Value - lowerOffsetLocation.X;
            Lower.Translate.Y = (float)numericUpDownLowerY.Value - lowerOffsetLocation.Y;
            UpdateLowerRotationButtons();
            UpdatePreviewFast();
        }

        private void UpdateLowerRotationButtons()
        {
            if (Lower.Angle == 0.0f)
            {
                panelLowerLeft.BackgroundImage = Properties.Resources.Left;
                panelLowerStand.BackgroundImage = Properties.Resources.Stand_light;
                panelLowerRight.BackgroundImage = Properties.Resources.Right;
            }
            else if (Lower.Angle == 270.0f)
            {
                panelLowerLeft.BackgroundImage = Properties.Resources.Left_light;
                panelLowerStand.BackgroundImage = Properties.Resources.Stand;
                panelLowerRight.BackgroundImage = Properties.Resources.Right;
            }
            else if (Lower.Angle == 90.0f)
            {
                panelLowerLeft.BackgroundImage = Properties.Resources.Left;
                panelLowerStand.BackgroundImage = Properties.Resources.Stand;
                panelLowerRight.BackgroundImage = Properties.Resources.Right_light;
            }
        }

        private void buttonBackground_Click(object sender, EventArgs e)
        {
            openFileDialog.FileName = "";
            openFileDialog.Filter = "Image File|*.jpg;*.png;*.gif;*.bmp";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                if (CurrentTarget == Target.GamePad)
                {
                    if (BackgroundGamePad != null)
                    {
                        BackgroundGamePad.Dispose();
                        BackgroundGamePad = null;
                    }
                    BackgroundGamePadPath = openFileDialog.FileName;
                    BackgroundGamePad = new Bitmap(openFileDialog.FileName);
                    UpdatePreviewFast();
                }
                else
                {
                    if (BackgroundTV != null)
                    {
                        BackgroundTV.Dispose();
                        BackgroundTV = null;
                    }
                    BackgroundTVPath = openFileDialog.FileName;
                    BackgroundTV = new Bitmap(openFileDialog.FileName);
                    UpdatePreviewFast();
                }
            }
        }

        private void buttonUpperBackground_Click(object sender, EventArgs e)
        {
            openFileDialog.FileName = "";
            openFileDialog.Filter = "Image File|*.jpg;*.png;*.gif;*.bmp";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                if (UpperImage != null)
                {
                    UpperImage.Dispose();
                    UpperImage = null;
                }
                UpperImage = new Bitmap(openFileDialog.FileName);
                UpdatePreviewFast();
            }
        }

        private void buttonLowerBackground_Click(object sender, EventArgs e)
        {
            openFileDialog.FileName = "";
            openFileDialog.Filter = "Image File|*.jpg;*.png;*.gif;*.bmp";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                if (LowerImage != null)
                {
                    LowerImage.Dispose();
                    LowerImage = null;
                }
                LowerImage = new Bitmap(openFileDialog.FileName);
                UpdatePreviewFast();
            }
        }

        private void showFullPreviewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SavePreview(Path.Combine(VCNDSLayoutEditorDataPath, "Preview.png"));
            System.Diagnostics.Process.Start(Path.Combine(VCNDSLayoutEditorDataPath, "Preview.png"));
        }

        private void savePreviewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog.FileName = "";
            saveFileDialog.Filter = "PNG File|*.png";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
                SavePreview(Path.Combine(saveFileDialog.FileName));
        }

        private void buttonSwapNDSScreens_Click(object sender, EventArgs e)
        {
            DSScreen tmp = Upper;
            Upper = Lower;
            Lower = tmp;

            UpdateControls = true;
            numericUpDownUpperX.Value = (decimal)Upper.Translate.X;
            numericUpDownUpperY.Value = (decimal)Upper.Translate.Y;
            numericUpDownUpperScale.Value = (decimal)Upper.Scale;
            numericUpDownLowerX.Value = (decimal)Lower.Translate.X;
            numericUpDownLowerY.Value = (decimal)Lower.Translate.Y;
            numericUpDownLowerScale.Value = (decimal)Lower.Scale;
            UpdateControls = false;

            UpdateUpperRotationButtons();
            UpdateLowerRotationButtons();
            labelUpperSize.Text = Upper.Width.ToString() + "x" + Upper.Height.ToString();
            labelLowerSize.Text = Lower.Width.ToString() + "x" + Lower.Height.ToString();

            UpdatePreviewFast();
        }


        private void comboBoxPad_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch ((string)comboBoxPad.SelectedItem)
            {
                case "90":
                    Config.Layouts[CurrentLayout].PadRotation = 90;
                    break;
                case "270":
                    Config.Layouts[CurrentLayout].PadRotation = 270;
                    break;
                default:
                    Config.Layouts[CurrentLayout].PadRotation = 0;
                    break;
            }
        }

        private void comboBoxDRC_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch ((string)comboBoxDRC.SelectedItem)
            {
                case "90":
                    Config.Layouts[CurrentLayout].DRCRotation = 90;
                    break;
                case "270":
                    Config.Layouts[CurrentLayout].DRCRotation = 270;
                    break;
                default:
                    Config.Layouts[CurrentLayout].DRCRotation = 0;
                    break;
            }
        }

        private void comboBoxButtons_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch ((string)comboBoxButtons.SelectedItem)
            {
                case "90":
                    Config.Layouts[CurrentLayout].ButtonsRotation = 90;
                    break;
                case "270":
                    Config.Layouts[CurrentLayout].ButtonsRotation = 270;
                    break;
                default:
                    Config.Layouts[CurrentLayout].ButtonsRotation = 0;
                    break;
            }
        }

        private void textBoxName_TextChanged(object sender, EventArgs e)
        {
            switch ((string)comboBoxLanguage.SelectedItem)
            {
                case "Deutsch":
                    Config.Layouts[CurrentLayout].Name.Deutsch = textBoxName.Text;
                    break;
                case "English (USA)":
                    Config.Layouts[CurrentLayout].Name.EnglishUSA = textBoxName.Text;
                    break;
                case "English (EUR)":
                    Config.Layouts[CurrentLayout].Name.EnglishEUR = textBoxName.Text;
                    break;
                case "French (USA)":
                    Config.Layouts[CurrentLayout].Name.FrenchUSA = textBoxName.Text;
                    break;
                case "French (EUR)":
                    Config.Layouts[CurrentLayout].Name.FrenchEUR = textBoxName.Text;
                    break;
                case "Italian":
                    Config.Layouts[CurrentLayout].Name.Italian = textBoxName.Text;
                    break;
                case "Japanese":
                    Config.Layouts[CurrentLayout].Name.Japanese = textBoxName.Text;
                    break;
                case "Nederlands":
                    Config.Layouts[CurrentLayout].Name.Nederlands = textBoxName.Text;
                    break;
                case "Portuguese (USA)":
                    Config.Layouts[CurrentLayout].Name.PortugueseUSA = textBoxName.Text;
                    break;
                case "Portuguese (EUR)":
                    Config.Layouts[CurrentLayout].Name.PortugueseEUR = textBoxName.Text;
                    break;
                case "Russian":
                    Config.Layouts[CurrentLayout].Name.Russian = textBoxName.Text;
                    break;
                case "Spanish (USA)":
                    Config.Layouts[CurrentLayout].Name.SpanishUSA = textBoxName.Text;
                    break;
                case "Spanish (EUR)":
                    Config.Layouts[CurrentLayout].Name.SpanishEUR = textBoxName.Text;
                    break;
                default:
                    Config.Layouts[CurrentLayout].Name.Default = textBoxName.Text;
                    break;
            }
            Changed();
        }

        private void textBoxDescription_TextChanged(object sender, EventArgs e)
        {
            switch ((string)comboBoxLanguage.SelectedItem)
            {
                case "Deutsch":
                    Config.Layouts[CurrentLayout].Description.Deutsch = textBoxDescription.Text;
                    break;
                case "English (USA)":
                    Config.Layouts[CurrentLayout].Description.EnglishUSA = textBoxDescription.Text;
                    break;
                case "English (EUR)":
                    Config.Layouts[CurrentLayout].Description.EnglishEUR = textBoxDescription.Text;
                    break;
                case "French (USA)":
                    Config.Layouts[CurrentLayout].Description.FrenchUSA = textBoxDescription.Text;
                    break;
                case "French (EUR)":
                    Config.Layouts[CurrentLayout].Description.FrenchEUR = textBoxDescription.Text;
                    break;
                case "Italian":
                    Config.Layouts[CurrentLayout].Description.Italian = textBoxDescription.Text;
                    break;
                case "Japanese":
                    Config.Layouts[CurrentLayout].Description.Japanese = textBoxDescription.Text;
                    break;
                case "Nederlands":
                    Config.Layouts[CurrentLayout].Description.Nederlands = textBoxDescription.Text;
                    break;
                case "Portuguese (USA)":
                    Config.Layouts[CurrentLayout].Description.PortugueseUSA = textBoxDescription.Text;
                    break;
                case "Portuguese (EUR)":
                    Config.Layouts[CurrentLayout].Description.PortugueseEUR = textBoxDescription.Text;
                    break;
                case "Russian":
                    Config.Layouts[CurrentLayout].Description.Russian = textBoxDescription.Text;
                    break;
                case "Spanish (USA)":
                    Config.Layouts[CurrentLayout].Description.SpanishUSA = textBoxDescription.Text;
                    break;
                case "Spanish (EUR)":
                    Config.Layouts[CurrentLayout].Description.SpanishEUR = textBoxDescription.Text;
                    break;
                default:
                    Config.Layouts[CurrentLayout].Description.Default = textBoxDescription.Text;
                    break;
            }
            Changed();
        }

        private void Changed()
        {
            if (!this.Text.EndsWith("*"))
                this.Text += "*";
        }

        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (comboBoxLayout.Items.Count != 0)
            {
                SavePreview(Path.Combine(VCNDSLayoutEditorDataPath, "Preview.png"));
                FormOptions form = new FormOptions();
                form.Bilinear = Config.Bilinear;
                form.RenderScale = Config.RenderScale;
                form.PixelArtUpscaler = Config.PixelArtUpscaler;
                form.Brightness = Config.Brightness;
                form.FoldOnPause = Config.FoldOnPause;
                form.FoldOnResumeFadeFromBlackDuration = Config.FoldOnResumeFadeFromBlackDuration;
                form.FoldOnPauseTimeout = Config.FoldOnPauseTimeout;
                if (form.ShowDialog() == DialogResult.OK)
                {
                    Config.Bilinear = form.Bilinear;
                    Config.RenderScale = form.RenderScale;
                    Config.PixelArtUpscaler = form.PixelArtUpscaler;
                    Config.Brightness = form.Brightness;
                    Config.FoldOnPause = form.FoldOnPause;
                    Config.FoldOnResumeFadeFromBlackDuration = form.FoldOnResumeFadeFromBlackDuration;
                    Config.FoldOnPauseTimeout = form.FoldOnPauseTimeout;
                    UpdatePreviewFast();
                    Changed();
                }
                form.Dispose();
            }
        }
    }
}
