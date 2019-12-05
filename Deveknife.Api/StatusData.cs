//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="StatusData.cs" company="EvePanix">Copyright (c) Jedzia 2001-2013, EvePanix. All rights reserved. See the license notes shipped with this source and the GNU GPL.</copyright>
//  <author>Jedzia</author>
//  <email>jed69@gmx.de</email>
//  <date>27.12.2013 21:45</date>
//  --------------------------------------------------------------------------------------------------------------------

namespace Deveknife.Api
{
    using System;

    public class StatusData : EventArgs
    {
        private int progress;

        public StatusData(string text)
        {
            this.Text = text;
        }

        public bool HasProgress { get; private set; }

        public int MaxProgress { get; private set; }

        public int Progress
        {
            get
            {
                return this.progress;
            }
        }

        public string Text { get; private set; }

        public void SetProgress(int currentProgress, int maxProgress)
        {
            this.HasProgress = true;
            this.progress = currentProgress;
            this.MaxProgress = maxProgress;
        }
    }
}