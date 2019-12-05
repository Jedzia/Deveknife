// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WordcountingTooltipController.cs" company="EvePanix">
//   Copyright (c) Jedzia 2001-2018, EvePanix. All rights reserved. See the license notes shipped with this source and the GNU GPL.
// </copyright>
//  <author>Jedzia</author>
//  <email>jed69@gmx.de</email>
//  <date>13.01.2019 11:39</date>
// --------------------------------------------------------------------------------------------------------------------

namespace Deveknife.Blades.Overview
{
    using System;
    using System.ComponentModel;

    using DevExpress.Utils;

    public class WordcountingTooltipController : ToolTipController
    {
        /// <inheritdoc />
        public WordcountingTooltipController(IContainer container)
            : base(container)
        {
        }

        /// <inheritdoc />
        protected override void OnBeforeShow(ToolTipControllerShowEventArgs e)
        {
            // **Calculation is based on the average reading speed that around 200 words per minute (wpm)
            const float WordsPerSecond = 60f / 200f;
            const int MinDelay = 10 * 1000;

            var words = this.countWords(e.ToolTip);
            var delay = (int)(words * WordsPerSecond * 1000);
            this.AutoPopDelay = Math.Max(MinDelay, delay);

            base.OnBeforeShow(e);
        }

        private int countWords(string text)
        {
            int wordCount = 0, index = 0;

            while(index < text.Length)
            {
                // check if current char is part of a word
                while(index < text.Length && !char.IsWhiteSpace(text[index]))
                {
                    index++;
                }

                wordCount++;

                // skip whitespace until next word
                while(index < text.Length && char.IsWhiteSpace(text[index]))
                {
                    index++;
                }
            }

            return wordCount;
        }
    }
}