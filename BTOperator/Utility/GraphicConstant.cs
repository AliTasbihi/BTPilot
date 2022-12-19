using AutoCreateWithJson.Component.BaseElements;
using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoCreateWithJson.Utility
{
    public static class GraphicConstant
    {
        static public int bluildingBlockWidth = 180;
        static public int arrowLinkWidth = 3;
        static public int connectorWidth = 10;
        static public int connectorOffset = 6;

        static public Color colorBackgroundBuildingBlock = Color.FromArgb(242, 242, 242);
        static public Color colorActiveBuildingBlock = Color.FromArgb(240, 158, 22);
        static public Color colorDeactiveBuildingBlock = Color.FromArgb(161, 159, 162);


        static public SolidBrush brushBackgroundBuildingBlock = new SolidBrush(colorBackgroundBuildingBlock);
        static public SolidBrush brushActive = new SolidBrush(colorActiveBuildingBlock);
        static public SolidBrush brushRed = new SolidBrush(Color.Red);
        static public SolidBrush brushGreen = new SolidBrush(Color.Green);
        static public SolidBrush brushText = new SolidBrush(Color.Aqua);

        static public Pen penActive = new Pen(colorActiveBuildingBlock, arrowLinkWidth);
        static public Pen penDeactive = new Pen(colorDeactiveBuildingBlock, arrowLinkWidth);
        static public Pen penRed1 = new Pen(Color.Red, 1);
        static public Pen penRed2 = new Pen(Color.Red, 2);
        static public Pen penWhite1 = new Pen(Color.White, 1);
        static public Pen penWhite2 = new Pen(Color.White, 2);
        static public Pen penGold1 = new Pen(Color.Gold, 1);
        static public Pen penGold2 = new Pen(Color.Gold, 2);
        static public Pen penBez1 = new Pen(Color.FromArgb(215, 162, 31), 1);

        static public Font textFontSmall = new Font("Microsoft Sans Serif", 8F);

        static public Font textFontMedium = new Font("Microsoft Sans Serif", 10F);
        static public Font textFontLarge = new Font("Microsoft Sans Serif", 12F);
        static public Font textFontExtraLarge = new Font("Microsoft Sans Serif", 14F);
        static public Font textFontDoubleExtraLarge = new Font("Microsoft Sans Serif", 16F);
        static public string textCollapseButton { get; } = "Collapse ▲";
        static public string textExpandButton { get; } = "Expand ▼";
    }
}
