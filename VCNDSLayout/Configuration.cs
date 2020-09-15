using System;
using System.Text;

namespace VCNDSLayout
{
    public class Configuration
    {
        public Layout[] Layouts;
        public int[] Groups;
        public int Bilinear;
        public int RenderScale;
        public int PixelArtUpscaler;
        public int Brightness;
        public bool FoldOnPause;
        public int FoldOnResumeFadeFromBlackDuration;
        public int FoldOnPauseTimeout;

        public Configuration()
        {
            Layouts = new Layout[10];
            Groups = new int[6];
            Groups[0] = 1;
            Groups[1] = 2;
            Groups[2] = 2;
            Groups[3] = 2;
            Groups[4] = 2;
            Groups[5] = 1;
            Bilinear = 0;
            RenderScale = 1;
            PixelArtUpscaler = 0;
            Brightness = 100;
            FoldOnPause = false;
            FoldOnResumeFadeFromBlackDuration = 1000;
            FoldOnPauseTimeout = 3000;

            for (int i = 0; i < Layouts.Length; i++)
                Layouts[i] = new Layout(i + 1);

            Layouts[0].Name.Default = "Standard Nintendo DS";
            Layouts[0].Description.Default = "Use the Wii U GamePad like a Nintendo DS.";
            Layouts[1].Name.Default = "Upper Screen Focus (Right-Handed)";
            Layouts[1].Description.Default = "Use the Wii U GamePad, focusing on the Upper Screen.";
            Layouts[2].Name.Default = "Upper Screen Focus (Left-Handed)";
            Layouts[2].Description.Default = "Use the Wii U GamePad, focusing on the Upper Screen.";
            Layouts[3].Name.Default = "Touch Screen Focus (Right-Handed)";
            Layouts[3].Description.Default = "Use the Wii U GamePad, focusing on the Touch Screen.";
            Layouts[4].Name.Default = "Touch Screen Focus (Left-Handed)";
            Layouts[4].Description.Default = "Use the Wii U GamePad, focusing on the Touch Screen.";
            Layouts[5].Name.Default = "Vertical Screens (Right-Handed)";
            Layouts[5].Description.Default = "For games that use stylus controls only.";
            Layouts[6].Name.Default = "Vertical Screens (Left-Handed)";
            Layouts[6].Description.Default = "For games that use stylus controls only.";
            Layouts[7].Name.Default = "Side-by-Side Screens (Right-Handed)";
            Layouts[7].Description.Default = "For games where you hold the Nintendo DS sideways.";
            Layouts[8].Name.Default = "Side-by-Side Screens (Left-Handed)";
            Layouts[8].Description.Default = "For games where you hold the Nintendo DS sideways.";
            Layouts[9].Name.Default = "Upper Screen on TV";
            Layouts[9].Description.Default = "Use both the TV and the Wii U GamePad to play.";

            for (int i = 0; i < Layouts.Length; i++)
            {
                Layouts[i].BackgroundTV = new Layout.BackgroundStruct("tv");
                Layouts[i].BackgroundGamePad = new Layout.BackgroundStruct("drc");
                Layouts[i].Name.Deutsch = Layouts[i].Name.Default;
                Layouts[i].Name.EnglishUSA = Layouts[i].Name.Default;
                Layouts[i].Name.EnglishEUR = Layouts[i].Name.Default;
                Layouts[i].Name.FrenchUSA = Layouts[i].Name.Default;
                Layouts[i].Name.FrenchEUR = Layouts[i].Name.Default;
                Layouts[i].Name.Italian = Layouts[i].Name.Default;
                Layouts[i].Name.Japanese = Layouts[i].Name.Default;
                Layouts[i].Name.Nederlands = Layouts[i].Name.Default;
                Layouts[i].Name.PortugueseUSA = Layouts[i].Name.Default;
                Layouts[i].Name.PortugueseEUR = Layouts[i].Name.Default;
                Layouts[i].Name.Russian = Layouts[i].Name.Default;
                Layouts[i].Name.SpanishUSA = Layouts[i].Name.Default;
                Layouts[i].Name.SpanishEUR = Layouts[i].Name.Default;
                Layouts[i].Description.Deutsch = Layouts[i].Description.Default;
                Layouts[i].Description.EnglishUSA = Layouts[i].Description.Default;
                Layouts[i].Description.EnglishEUR = Layouts[i].Description.Default;
                Layouts[i].Description.FrenchUSA = Layouts[i].Description.Default;
                Layouts[i].Description.FrenchEUR = Layouts[i].Description.Default;
                Layouts[i].Description.Italian = Layouts[i].Description.Default;
                Layouts[i].Description.Japanese = Layouts[i].Description.Default;
                Layouts[i].Description.Nederlands = Layouts[i].Description.Default;
                Layouts[i].Description.PortugueseUSA = Layouts[i].Description.Default;
                Layouts[i].Description.PortugueseEUR = Layouts[i].Description.Default;
                Layouts[i].Description.Russian = Layouts[i].Description.Default;
                Layouts[i].Description.SpanishUSA = Layouts[i].Description.Default;
                Layouts[i].Description.SpanishEUR = Layouts[i].Description.Default;
            }

            Layouts[0].ScreenUpperTV = new Layout.ScreenStruct("upper", "tv", 0, 120, 640, 480);
            Layouts[0].ScreenLowerTV = new Layout.ScreenStruct("lower", "tv", 640, 120, 640, 480);
            Layouts[0].ScreenUpperGamePad = new Layout.ScreenStruct("upper", "drc", 299, 24, 256, 192);
            Layouts[0].ScreenLowerGamePad = new Layout.ScreenStruct("lower", "drc", 299, 264, 256, 192);

            Layouts[1].ScreenUpperTV = new Layout.ScreenStruct("upper", "tv", 0, 72, 768, 576);
            Layouts[1].ScreenLowerTV = new Layout.ScreenStruct("lower", "tv", 768, 168, 512, 384);
            Layouts[1].ScreenUpperGamePad = new Layout.ScreenStruct("upper", "drc", 30, 48, 512, 384);
            Layouts[1].ScreenLowerGamePad = new Layout.ScreenStruct("lower", "drc", 568, 240, 256, 192);

            Layouts[2].ScreenUpperTV = new Layout.ScreenStruct("upper", "tv", 512, 72, 768, 576);
            Layouts[2].ScreenLowerTV = new Layout.ScreenStruct("lower", "tv", 0, 168, 512, 384);
            Layouts[2].ScreenUpperGamePad = new Layout.ScreenStruct("upper", "drc", 312, 48, 512, 384);
            Layouts[2].ScreenLowerGamePad = new Layout.ScreenStruct("lower", "drc", 30, 240, 256, 192);

            Layouts[3].ScreenUpperTV = new Layout.ScreenStruct("upper", "tv", 0, 168, 512, 384);
            Layouts[3].ScreenLowerTV = new Layout.ScreenStruct("lower", "tv", 512, 72, 768, 576);
            Layouts[3].ScreenUpperGamePad = new Layout.ScreenStruct("upper", "drc", 30, 48, 256, 192);
            Layouts[3].ScreenLowerGamePad = new Layout.ScreenStruct("lower", "drc", 312, 48, 512, 384);

            Layouts[4].ScreenUpperTV = new Layout.ScreenStruct("upper", "tv", 768, 168, 512, 384);
            Layouts[4].ScreenLowerTV = new Layout.ScreenStruct("lower", "tv", 0, 72, 768, 576);
            Layouts[4].ScreenUpperGamePad = new Layout.ScreenStruct("upper", "drc", 568, 48, 256, 192);
            Layouts[4].ScreenLowerGamePad = new Layout.ScreenStruct("lower", "drc", 30, 48, 512, 384);

            Layouts[5].PadRotation = 90;
            Layouts[5].DRCRotation = 90;
            Layouts[5].ScreenUpperTV = new Layout.ScreenStruct("upper", "tv", 400, 0, 480, 360);
            Layouts[5].ScreenLowerTV = new Layout.ScreenStruct("lower", "tv", 400, 360, 480, 360);
            Layouts[5].ScreenUpperGamePad = new Layout.ScreenStruct("upper", "drc", 466, 0, 360, 480, 270);
            Layouts[5].ScreenLowerGamePad = new Layout.ScreenStruct("lower", "drc", 28, 0, 360, 480, 270);

            Layouts[6].PadRotation = 270;
            Layouts[6].DRCRotation = 270;
            Layouts[6].ScreenUpperTV = new Layout.ScreenStruct("upper", "tv", 400, 0, 480, 360);
            Layouts[6].ScreenLowerTV = new Layout.ScreenStruct("lower", "tv", 400, 360, 480, 360);
            Layouts[6].ScreenUpperGamePad = new Layout.ScreenStruct("upper", "drc", 28, 0, 360, 480, 90);
            Layouts[6].ScreenLowerGamePad = new Layout.ScreenStruct("lower", "drc", 466, 0, 360, 480, 90);

            Layouts[7].PadRotation = 270;
            Layouts[7].ScreenUpperTV = new Layout.ScreenStruct("upper", "tv", 55, 0, 540, 720, 90);
            Layouts[7].ScreenLowerTV = new Layout.ScreenStruct("lower", "tv", 685, 0, 540, 720, 90);
            Layouts[7].ScreenUpperGamePad = new Layout.ScreenStruct("upper", "drc", 28, 0, 360, 480, 90);
            Layouts[7].ScreenLowerGamePad = new Layout.ScreenStruct("lower", "drc", 466, 0, 360, 480, 90);

            Layouts[8].PadRotation = 90;
            Layouts[8].ScreenUpperTV = new Layout.ScreenStruct("upper", "tv", 685, 0, 540, 720, 270);
            Layouts[8].ScreenLowerTV = new Layout.ScreenStruct("lower", "tv", 55, 0, 540, 720, 270);
            Layouts[8].ScreenUpperGamePad = new Layout.ScreenStruct("upper", "drc", 466, 0, 360, 480, 270);
            Layouts[8].ScreenLowerGamePad = new Layout.ScreenStruct("lower", "drc", 28, 0, 360, 480, 270);

            Layouts[9].ScreenUpperTV = new Layout.ScreenStruct("upper", "tv", 160, 0, 960, 720);
            Layouts[9].ScreenLowerTV = new Layout.ScreenStruct("lower", "tv", 0, 0, 0, 0);
            Layouts[9].ScreenUpperGamePad = new Layout.ScreenStruct("upper", "drc", 0, 0, 0, 0);
            Layouts[9].ScreenLowerGamePad = new Layout.ScreenStruct("lower", "drc", 106, 0, 640, 480);
        }

        public Configuration(Cll.JSON.Element json)
        {
            Cll.JSON.Object config = (Cll.JSON.Object)json.Value.GetValue("configuration");
            Cll.JSON.Array layout = (Cll.JSON.Array)config.GetValue("layouts").GetValue("layout");
            Cll.JSON.Array groups = (Cll.JSON.Array)config.GetValue("layouts").GetValue("groups");

            Layouts = new Layout[layout.Count];
            Groups = new int[groups.Count];
            Bilinear = Convert.ToInt32(((Cll.JSON.Number)config.GetValue("3DRendering").GetValue("Bilinear")).Value);
            RenderScale = Convert.ToInt32(((Cll.JSON.Number)config.GetValue("3DRendering").GetValue("RenderScale")).Value);
            PixelArtUpscaler = Convert.ToInt32(((Cll.JSON.Number)config.GetValue("Display").GetValue("PixelArtUpscaler")).Value);
            Brightness = Convert.ToInt32(((Cll.JSON.Number)config.GetValue("Display").GetValue("Brightness")).Value);

            if (config.Contains("arguments"))
            {
                FoldOnPause = ((Cll.JSON.Boolean)config.GetValue("arguments").GetValue("fold_on_pause")).Value;
                FoldOnResumeFadeFromBlackDuration = Convert.ToInt32(((Cll.JSON.Number)config.GetValue("arguments").GetValue("fold_on_resume_fade_from_black_duration")).Value);
                FoldOnPauseTimeout = Convert.ToInt32(((Cll.JSON.Number)config.GetValue("arguments").GetValue("fold_on_pause_timeout")).Value);
            }
            else
            {
                FoldOnPause = false;
                FoldOnResumeFadeFromBlackDuration = 1000;
                FoldOnPauseTimeout = 3000;
            }

            for (int i = 0; i < Layouts.Length; i++)
                Layouts[i] = new Layout((Cll.JSON.Object)layout.GetValue(i));

            for (int i = 0; i < Groups.Length; i++)
                Groups[i] = Convert.ToInt32(((Cll.JSON.Number)groups.GetValue(i)).Value);
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Layouts: [");
            int i = 0;
            for (; i < Layouts.Length - 1; i++)
                sb.AppendLine("{" + Layouts[i].ToString() + "},");
            sb.AppendLine("{" + Layouts[i].ToString() + "}]");
            sb.AppendLine("Groups: [");
            for (i = 0; i < Groups.Length - 1; i++)
                sb.AppendLine("" + Groups[i].ToString() + ",");
            sb.AppendLine("" + Groups[i].ToString() + "]");
            sb.AppendLine("Bilinear: " + Bilinear.ToString());
            sb.AppendLine("RenderScale: " + RenderScale.ToString());
            sb.AppendLine("PixelArtUpscaler: " + PixelArtUpscaler.ToString());
            sb.AppendLine("Brightness: " + Brightness.ToString());
            sb.AppendLine("FoldOnPause: " + FoldOnPause.ToString());
            sb.AppendLine("FoldOnResumeFadeFromBlackDuration: " + FoldOnResumeFadeFromBlackDuration.ToString());
            sb.Append("FoldOnPauseTimeout: " + FoldOnPauseTimeout.ToString());
            return sb.ToString();
        }

        public Cll.JSON.Value GetJSON()
        {
            Cll.JSON.Object jsonObj = new Cll.JSON.Object();
            Cll.JSON.Object jsonConfiguration = new Cll.JSON.Object();
            Cll.JSON.Object jsonLayouts = new Cll.JSON.Object();
            Cll.JSON.Array arrayLayout = new Cll.JSON.Array();
            Cll.JSON.Array arrayGroups = new Cll.JSON.Array();
            Cll.JSON.Object json3DRendering = new Cll.JSON.Object();
            Cll.JSON.Object jsonDisplay = new Cll.JSON.Object();
            Cll.JSON.Object jsonArguments = new Cll.JSON.Object();

            for (int i = 0; i < Layouts.Length; i++)
                arrayLayout.AddValue(Layouts[i].GetJSON(i + 1));

            for (int i = 0; i < Groups.Length; i++)
                arrayGroups.AddValue(Groups[i]);

            jsonLayouts.AddMember("layout", arrayLayout);
            jsonLayouts.AddMember("groups", arrayGroups);
            json3DRendering.AddMember("Bilinear", Bilinear);
            json3DRendering.AddMember("RenderScale", RenderScale);
            jsonDisplay.AddMember("PixelArtUpscaler", PixelArtUpscaler);
            jsonDisplay.AddMember("Brightness", Brightness);
            jsonArguments.AddMember("fold_on_pause", FoldOnPause);
            jsonArguments.AddMember("fold_on_resume_fade_from_black_duration", FoldOnResumeFadeFromBlackDuration);
            jsonArguments.AddMember("fold_on_pause_timeout", FoldOnPauseTimeout);

            jsonConfiguration.AddMember("layouts", jsonLayouts);
            jsonConfiguration.AddMember("3DRendering", json3DRendering);
            jsonConfiguration.AddMember("Display", jsonDisplay);
            jsonConfiguration.AddMember("arguments", jsonArguments);

            jsonObj.AddMember("configuration", jsonConfiguration);

            return jsonObj;
        }

        public Cll.JSON.Value GetExtended()
        {
            Cll.JSON.Object jsonObj = (Cll.JSON.Object)GetJSON();
            Cll.JSON.Object Strings = new Cll.JSON.Object();
            Cll.JSON.Object Default = new Cll.JSON.Object();
            Cll.JSON.Object Deutsch = new Cll.JSON.Object();
            Cll.JSON.Object EnglishUSA = new Cll.JSON.Object();
            Cll.JSON.Object EnglishEUR = new Cll.JSON.Object();
            Cll.JSON.Object FrenchUSA = new Cll.JSON.Object();
            Cll.JSON.Object FrenchEUR = new Cll.JSON.Object();
            Cll.JSON.Object Italian = new Cll.JSON.Object();
            Cll.JSON.Object Japanese = new Cll.JSON.Object();
            Cll.JSON.Object Nederlands = new Cll.JSON.Object();
            Cll.JSON.Object PortugueseUSA = new Cll.JSON.Object();
            Cll.JSON.Object PortugueseEUR = new Cll.JSON.Object();
            Cll.JSON.Object Russian = new Cll.JSON.Object();
            Cll.JSON.Object SpanishUSA = new Cll.JSON.Object();
            Cll.JSON.Object SpanishEUR = new Cll.JSON.Object();

            foreach(Layout layout in Layouts)
            {
                Default.AddMember(layout.NameID, layout.Name.Default != null ? layout.Name.Default : "");
                Default.AddMember(layout.DescriptionID, layout.Description.Default != null ? layout.Description.Default : "");
                Deutsch.AddMember(layout.NameID, layout.Name.Deutsch != null ? layout.Name.Deutsch : "");
                Deutsch.AddMember(layout.DescriptionID, layout.Description.Deutsch != null ? layout.Description.Deutsch : "");
                EnglishUSA.AddMember(layout.NameID, layout.Name.EnglishUSA != null ? layout.Name.EnglishUSA : "");
                EnglishUSA.AddMember(layout.DescriptionID, layout.Description.EnglishUSA != null ? layout.Description.EnglishUSA : "");
                EnglishEUR.AddMember(layout.NameID, layout.Name.EnglishEUR != null ? layout.Name.EnglishEUR : "");
                EnglishEUR.AddMember(layout.DescriptionID, layout.Description.EnglishEUR != null ? layout.Description.EnglishEUR : "");
                FrenchUSA.AddMember(layout.NameID, layout.Name.FrenchUSA != null ? layout.Name.FrenchUSA : "");
                FrenchUSA.AddMember(layout.DescriptionID, layout.Description.FrenchUSA != null ? layout.Description.FrenchUSA : "");
                FrenchEUR.AddMember(layout.NameID, layout.Name.FrenchEUR != null ? layout.Name.FrenchEUR : "");
                FrenchEUR.AddMember(layout.DescriptionID, layout.Description.FrenchEUR != null ? layout.Description.FrenchEUR : "");
                Italian.AddMember(layout.NameID, layout.Name.Italian != null ? layout.Name.Italian : "");
                Italian.AddMember(layout.DescriptionID, layout.Description.Italian != null ? layout.Description.Italian : "");
                Japanese.AddMember(layout.NameID, layout.Name.Japanese != null ? layout.Name.Japanese : "");
                Japanese.AddMember(layout.DescriptionID, layout.Description.Japanese != null ? layout.Description.Japanese : "");
                Nederlands.AddMember(layout.NameID, layout.Name.Nederlands != null ? layout.Name.Nederlands : "");
                Nederlands.AddMember(layout.DescriptionID, layout.Description.Nederlands != null ? layout.Description.Nederlands : "");
                PortugueseUSA.AddMember(layout.NameID, layout.Name.PortugueseUSA != null ? layout.Name.PortugueseUSA : "");
                PortugueseUSA.AddMember(layout.DescriptionID, layout.Description.PortugueseUSA != null ? layout.Description.PortugueseUSA : "");
                PortugueseEUR.AddMember(layout.NameID, layout.Name.PortugueseEUR != null ? layout.Name.PortugueseEUR : "");
                PortugueseEUR.AddMember(layout.DescriptionID, layout.Description.PortugueseEUR != null ? layout.Description.PortugueseEUR : "");
                Russian.AddMember(layout.NameID, layout.Name.Russian != null ? layout.Name.Russian : "");
                Russian.AddMember(layout.DescriptionID, layout.Description.Russian != null ? layout.Description.Russian : "");
                SpanishUSA.AddMember(layout.NameID, layout.Name.SpanishUSA != null ? layout.Name.SpanishUSA : "");
                SpanishUSA.AddMember(layout.DescriptionID, layout.Description.SpanishUSA != null ? layout.Description.SpanishUSA : "");
                SpanishEUR.AddMember(layout.NameID, layout.Name.SpanishEUR != null ? layout.Name.SpanishEUR : "");
                SpanishEUR.AddMember(layout.DescriptionID, layout.Description.SpanishEUR != null ? layout.Description.SpanishEUR : "");
            }

            Strings.AddMember("Default", Default);
            Strings.AddMember("Deutsch", Deutsch);
            Strings.AddMember("EnglishUSA", EnglishUSA);
            Strings.AddMember("EnglishEUR", EnglishEUR);
            Strings.AddMember("FrenchUSA", FrenchUSA);
            Strings.AddMember("FrenchEUR", FrenchEUR);
            Strings.AddMember("Italian", Italian);
            Strings.AddMember("Japanese", Japanese);
            Strings.AddMember("Nederlands", Nederlands);
            Strings.AddMember("PortugueseUSA", PortugueseUSA);
            Strings.AddMember("PortugueseEUR", PortugueseEUR);
            Strings.AddMember("Russian", Russian);
            Strings.AddMember("SpanishUSA", SpanishUSA);
            Strings.AddMember("SpanishEUR", SpanishEUR);

            jsonObj.AddMember("strings", Strings);

            return jsonObj;
        }
    }
}
