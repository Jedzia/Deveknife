// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SettingsProvider.cs" company="EvePanix">
//   Copyright (c) Jedzia 2001-2019, EvePanix. All rights reserved. See the license notes shipped with this source and the GNU GPL.
// </copyright>
//  <author>Jedzia</author>
//  <email>jed69@gmx.de</email>
//  <date>05.12.2019 17:51</date>
// --------------------------------------------------------------------------------------------------------------------

namespace Deveknife
{
    using System;
    using System.Collections;

    using Deveknife.Api;

    public class SettingsProvider : ISettingsProvider
    {
        private readonly Hashtable settings;

        /// <summary>
        /// Initializes a new instance of the <see cref="SettingsProvider"/> class.
        /// </summary>
        public SettingsProvider()
        {
            this.settings = new Hashtable
                            {
                                { "SSH_Host", "vuduo2x" },
                                { "SSH_User", "git" },
                                { "SSH_KeyFile", @"D:\Users\Jedzia.pubsiX\.ssh\vuduo2-id_rsa" }
                            };
        }

        [CanBeNull]
        public string this[string name]
        {
            get
            {
                return this.GetTextSetting(name);
            }
            set
            {
                try
                {
                    this.settings[name] = value;
                }
                catch(ArgumentNullException argumentNullException)
                {
                    // TODO: Handle the System.ArgumentNullException
                    throw;
                }
            }
        }

        /// <inheritdoc />
        [CanBeNull]
        public string GetTextSetting(string name)
        {
            object settingsValue = null;
            if(this.settings.ContainsKey(name))
            {
                settingsValue = this.settings[name];
            }

            if(settingsValue is string)
            {
                return (string)settingsValue;
            }

            return null;
        }
    }
}