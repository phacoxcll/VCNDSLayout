using System;
using System.Drawing;
using System.Text;

namespace VCNDSLayout
{
    public class Layout
    {
        public struct ScreenStruct
        {
            public string Source;
            public int Rotation;
            public Size Size;
            public string Target;
            public Point Position;

            public ScreenStruct(string source, string target, int x, int y, int width, int height, int rotation = 0)
            {
                if (source != "upper" && source != "lower")
                    throw new ArgumentException("Source must be \"upper\" or \"lower\".");
                if (target != "tv" && target != "drc")
                    throw new ArgumentException("Target must be \"tv\" or \"drc\".");

                Source = source;
                Rotation = rotation;
                Size = new Size(width, height);
                Target = target;
                Position = new Point(x, y);
            }

            public ScreenStruct(Cll.JSON.Object screen)
            {
                string[] size = ((Cll.JSON.String)screen.GetFirstValue("size")).Value.Split(new char[] { ' ' });
                string[] position = ((Cll.JSON.String)screen.GetFirstValue("position")).Value.Split(new char[] { ' ' });

                Source = ((Cll.JSON.String)screen.GetFirstValue("source")).Value;
                Rotation = Convert.ToInt32(((Cll.JSON.Number)screen.GetFirstValue("rotation")).Value);
                Size = new Size(Convert.ToInt32(size[0]), Convert.ToInt32(size[1]));
                Target = ((Cll.JSON.String)screen.GetFirstValue("target")).Value;
                Position = new Point(Convert.ToInt32(position[0]), Convert.ToInt32(position[1]));
            }

            public override string ToString()
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("Source: " + Source);
                sb.AppendLine("Rotation: " + Rotation.ToString());
                sb.AppendLine("Size: " + Size.Width.ToString() + "x" + Size.Height.ToString());
                sb.AppendLine("Target: " + Target);
                sb.Append("Position: " + Position.X.ToString() + ", " + Position.Y.ToString());
                return sb.ToString();
            }

            public Cll.JSON.Value GetJSON()
            {
                Cll.JSON.Object jsonObj = new Cll.JSON.Object();
                jsonObj.AddMember("source", Source);
                jsonObj.AddMember("rotation", Rotation);
                jsonObj.AddMember("size", Size.Width.ToString() + " " + Size.Height.ToString());
                jsonObj.AddMember("target", Target);
                jsonObj.AddMember("position", Position.X.ToString() + " " + Position.Y.ToString());
                return jsonObj;
            }
        }

        public struct BackgroundStruct
        {
            public Point Position;
            public int Rotation;
            public string Resource;
            public string Target;
            public Size Size;

            public BackgroundStruct(string target, string resource = "")
            {
                if (target != "tv" && target != "drc")
                    throw new ArgumentException("Target must be \"tv\" or \"drc\".");

                Position = new Point(0, 0);
                Rotation = 0;
                Resource = resource;
                Target = target;
                if (target == "drc")
                    Size = new Size(854, 480);
                else
                    Size = new Size(1280, 720);
            }

            public BackgroundStruct(Cll.JSON.Object background)
            {
                string[] size = ((Cll.JSON.String)background.GetFirstValue("size")).Value.Split(new char[] { ' ' });
                string[] position = ((Cll.JSON.String)background.GetFirstValue("position")).Value.Split(new char[] { ' ' });

                Position = new Point(Convert.ToInt32(position[0]), Convert.ToInt32(position[1]));
                Rotation = Convert.ToInt32(((Cll.JSON.Number)background.GetFirstValue("rotation")).Value);
                Resource = ((Cll.JSON.String)background.GetFirstValue("resource")).Value;
                Target = ((Cll.JSON.String)background.GetFirstValue("target")).Value;
                Size = new Size(Convert.ToInt32(size[0]), Convert.ToInt32(size[1]));
            }

            public override string ToString()
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("Position: " + Position.X.ToString() + ", " + Position.Y.ToString());
                sb.AppendLine("Rotation: " + Rotation.ToString());
                sb.AppendLine("Resource: " + Resource);
                sb.AppendLine("Target: " + Target);
                sb.Append("Size: " + Size.Width.ToString() + "x" + Size.Height.ToString());
                return sb.ToString();
            }

            public Cll.JSON.Value GetJSON(int index)
            {
                Cll.JSON.Object jsonObj = new Cll.JSON.Object();
                jsonObj.AddMember("position", Position.X.ToString() + " " + Position.Y.ToString());
                jsonObj.AddMember("rotation", Rotation);
                jsonObj.AddMember("resource", "//content_dir/assets/textures/" + Target + index.ToString("00") + ".png");
                jsonObj.AddMember("target", Target);
                jsonObj.AddMember("size", Size.Width.ToString() + " " + Size.Height.ToString());
                return jsonObj;
            }
        }

        public struct StringLanguages
        {
            public string Default;
            public string Deutsch;
            public string EnglishUSA;
            public string EnglishEUR;
            public string FrenchUSA;
            public string FrenchEUR;
            public string Italian;
            public string Japanese;
            public string Nederlands;
            public string PortugueseUSA;
            public string PortugueseEUR;
            public string Russian;
            public string SpanishUSA;
            public string SpanishEUR;

            public static StringLanguages Empty;
        }

        public int PadRotation;
        public int DRCRotation;
        public int ButtonsRotation;
        public string NameID;
        public string DescriptionID;
        public StringLanguages Name;
        public StringLanguages Description;
        public ScreenStruct ScreenUpperTV;
        public ScreenStruct ScreenLowerTV;
        public ScreenStruct ScreenUpperGamePad;
        public ScreenStruct ScreenLowerGamePad;
        public BackgroundStruct BackgroundTV;
        public BackgroundStruct BackgroundGamePad;

        public Layout(int id, int padRotation = 0, int drcRotation = 0, int buttonsRotation = 0)
        {
            if (id <= 0)
                throw new ArgumentException("ID must be greater than zero.");

            PadRotation = padRotation;
            DRCRotation = drcRotation;
            ButtonsRotation = buttonsRotation;

            NameID = "VCM_LAYOUT_" + id.ToString() + "_NAME";
            DescriptionID = "VCM_LAYOUT_" + id.ToString() + "_EXPLANATION";

            Name = StringLanguages.Empty;
            Description = StringLanguages.Empty;
        }

        public Layout(Cll.JSON.Object layout)
        {
            Cll.JSON.Array screen = (Cll.JSON.Array)layout.GetValue("screen");
            Cll.JSON.Array background = (Cll.JSON.Array)layout.GetValue("background");

            if (screen.Count < 1 || screen.Count > 4)
                throw new Exception("Screens count must be greater than 0 and less than 5.");

            if (background.Count != 2)
                throw new Exception("Backgrounds count must be equal to 2.");

            ScreenStruct[] screens = new ScreenStruct[screen.Count];
            BackgroundStruct[] backgrounds = new BackgroundStruct[background.Count];

            for (int i = 0; i < screens.Length; i++)
                screens[i] = new ScreenStruct((Cll.JSON.Object)screen.GetValue(i));

            if (screens[0].Source == "upper" && screens[0].Target == "tv")
                ScreenUpperTV = screens[0];
            else if (screens.Length > 1 && screens[1].Source == "upper" && screens[1].Target == "tv")
                ScreenUpperTV = screens[1];
            else if (screens.Length > 2 && screens[2].Source == "upper" && screens[2].Target == "tv")
                ScreenUpperTV = screens[2];
            else if (screens.Length > 3 && screens[3].Source == "upper" && screens[3].Target == "tv")
                ScreenUpperTV = screens[3];
            else
                ScreenUpperTV = new ScreenStruct("upper", "tv", 0, 0, 0, 0);

            if (screens[0].Source == "lower" && screens[0].Target == "tv")
                ScreenLowerTV = screens[0];
            else if (screens.Length > 1 && screens[1].Source == "lower" && screens[1].Target == "tv")
                ScreenLowerTV = screens[1];
            else if (screens.Length > 2 && screens[2].Source == "lower" && screens[2].Target == "tv")
                ScreenLowerTV = screens[2];
            else if (screens.Length > 3 && screens[3].Source == "lower" && screens[3].Target == "tv")
                ScreenLowerTV = screens[3];
            else
                ScreenLowerTV = new ScreenStruct("lower", "tv", 0, 0, 0, 0);

            if (screens[0].Source == "upper" && screens[0].Target == "drc")
                ScreenUpperGamePad = screens[0];
            else if (screens.Length > 1 && screens[1].Source == "upper" && screens[1].Target == "drc")
                ScreenUpperGamePad = screens[1];
            else if (screens.Length > 2 && screens[2].Source == "upper" && screens[2].Target == "drc")
                ScreenUpperGamePad = screens[2];
            else if (screens.Length > 3 && screens[3].Source == "upper" && screens[3].Target == "drc")
                ScreenUpperGamePad = screens[3];
            else
                ScreenUpperGamePad = new ScreenStruct("upper", "drc", 0, 0, 0, 0);

            if (screens[0].Source == "lower" && screens[0].Target == "drc")
                ScreenLowerGamePad = screens[0];
            else if (screens.Length > 1 && screens[1].Source == "lower" && screens[1].Target == "drc")
                ScreenLowerGamePad = screens[1];
            else if (screens.Length > 2 && screens[2].Source == "lower" && screens[2].Target == "drc")
                ScreenLowerGamePad = screens[2];
            else if (screens.Length > 3 && screens[3].Source == "lower" && screens[3].Target == "drc")
                ScreenLowerGamePad = screens[3];
            else
                ScreenLowerGamePad = new ScreenStruct("lower", "drc", 0, 0, 0, 0);

            backgrounds[0] = new BackgroundStruct((Cll.JSON.Object)background.GetValue(0));
            backgrounds[1] = new BackgroundStruct((Cll.JSON.Object)background.GetValue(1));

            if (backgrounds[0].Target == "tv")
                BackgroundTV = backgrounds[0];
            else if (backgrounds[1].Target == "tv")
                BackgroundTV = backgrounds[1];
            else
                BackgroundTV = new BackgroundStruct("tv");

            if (backgrounds[0].Target == "drc")
                BackgroundGamePad = backgrounds[0];
            else if (backgrounds[1].Target == "drc")
                BackgroundGamePad = backgrounds[1];
            else
                BackgroundGamePad = new BackgroundStruct("drc");

            if (layout.Contains("pad_rotation"))
                PadRotation = Convert.ToInt32(((Cll.JSON.Number)layout.GetValue("pad_rotation")).Value);
            else
                PadRotation = 0;
            if (layout.Contains("drc_rotation"))
                DRCRotation = Convert.ToInt32(((Cll.JSON.Number)layout.GetValue("drc_rotation")).Value);
            else
                DRCRotation = 0;
            if (layout.Contains("buttons_rotation"))
                ButtonsRotation = Convert.ToInt32(((Cll.JSON.Number)layout.GetValue("buttons_rotation")).Value);
            else
                ButtonsRotation = 0;

            if (layout.Contains("name_string_id"))
                NameID = ((Cll.JSON.String)layout.GetValue("name_string_id")).Value;
            else
                NameID = "";
            if (layout.Contains("desc_string_id"))
                DescriptionID = ((Cll.JSON.String)layout.GetValue("desc_string_id")).Value;
            else
                DescriptionID = "";

            Name = StringLanguages.Empty;
            Description = StringLanguages.Empty;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("PadRotation: " + PadRotation.ToString());
            sb.AppendLine("DRCRotation: " + DRCRotation.ToString());
            sb.AppendLine("Screens: [");
            sb.AppendLine("{" + ScreenUpperTV.ToString() + "},");
            sb.AppendLine("{" + ScreenLowerTV.ToString() + "},");
            sb.AppendLine("{" + ScreenUpperGamePad.ToString() + "},");
            sb.AppendLine("{" + ScreenLowerGamePad.ToString() + "}]");
            sb.AppendLine("NameID: " + NameID);
            sb.AppendLine("Backgrounds: [");
            sb.AppendLine("{" + BackgroundTV.ToString() + "},");
            sb.AppendLine("{" + BackgroundGamePad.ToString() + "}]");
            sb.AppendLine("ButtonsRotation: " + ButtonsRotation.ToString());
            sb.Append("DescriptionID : " + DescriptionID);
            return sb.ToString();
        }

        public Cll.JSON.Value GetJSON(int index)
        {
            Cll.JSON.Object jsonObj = new Cll.JSON.Object();
            Cll.JSON.Array arrayScreens = new Cll.JSON.Array();
            Cll.JSON.Array arrayBackgrounds = new Cll.JSON.Array();

            if (ScreenUpperTV.Size.Width > 0 && ScreenUpperTV.Size.Height > 0)
                arrayScreens.AddValue(ScreenUpperTV.GetJSON());
            if (ScreenLowerTV.Size.Width > 0 && ScreenLowerTV.Size.Height > 0)
                arrayScreens.AddValue(ScreenLowerTV.GetJSON());
            if (ScreenUpperGamePad.Size.Width > 0 && ScreenUpperGamePad.Size.Height > 0)
                arrayScreens.AddValue(ScreenUpperGamePad.GetJSON());
            if (ScreenLowerGamePad.Size.Width > 0 && ScreenLowerGamePad.Size.Height > 0)
                arrayScreens.AddValue(ScreenLowerGamePad.GetJSON());

            if (arrayScreens.Count == 0)
                throw new Exception("There must be at least one screen.");

            arrayBackgrounds.AddValue(BackgroundTV.GetJSON(index));
            arrayBackgrounds.AddValue(BackgroundGamePad.GetJSON(index));

            jsonObj.AddMember("pad_rotation", PadRotation);
            jsonObj.AddMember("drc_rotation", DRCRotation);
            jsonObj.AddMember("screen", arrayScreens);
            jsonObj.AddMember("name_string_id", NameID);
            jsonObj.AddMember("background", arrayBackgrounds);
            jsonObj.AddMember("buttons_rotation", ButtonsRotation);
            jsonObj.AddMember("desc_string_id", DescriptionID);

            return jsonObj;
        }
    }
}
