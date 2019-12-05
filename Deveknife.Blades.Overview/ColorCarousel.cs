//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="ColorCarousel.cs" company="EvePanix">Copyright (c) Jedzia 2001-2014, EvePanix. All rights reserved. See the license notes shipped with this source and the GNU GPL.</copyright>
//  <author>Jedzia</author>
//  <email>jed69@gmx.de</email>
//  <date>14.02.2014 13:01</date>
//  --------------------------------------------------------------------------------------------------------------------

namespace Deveknife.Blades.Overview
{
    using System.Drawing;

    public class ColorCarousel
    {
        private static readonly Color[] CarouselColors =
            {
                Color.ForestGreen, Color.IndianRed, Color.HotPink,
                Color.Khaki, Color.Lavender, Color.Lavender, Color.LawnGreen,
                Color.LightBlue, Color.GreenYellow, Color.PaleTurquoise,
                Color.Orange, Color.Navy, Color.BlueViolet, Color.Turquoise,
                Color.Tan, Color.Plum
            };

        private int colorswitch;

        private Color startcolor = Color.Salmon;

        public Color GetColor()
        {
            const int i = 1;
            if ((this.colorswitch % i) == 0)
            {
                //startcolor = Color.FromArgb((startcolor.R + 30) % 255, (startcolor.G + 30) % 255, (startcolor.B + 30) % 255);
                this.startcolor = CarouselColors[(this.colorswitch / i) % CarouselColors.Length];
            }

            this.colorswitch++;
            return this.startcolor;
        }
    }
}