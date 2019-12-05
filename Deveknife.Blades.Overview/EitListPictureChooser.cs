// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EitListPictureChooser.cs" company="EvePanix">
//   Copyright (c) Jedzia 2001-2015, EvePanix. All rights reserved. See the license notes shipped with this source and the GNU GPL.
// </copyright>
//  <author>Jedzia</author>
//  <email>jed69@gmx.de</email>
//  <date>07.11.2015 23:31</date>
// --------------------------------------------------------------------------------------------------------------------

namespace Deveknife.Blades.Overview
{
    using System;
    using System.Collections.Generic;

    using Castle.Core.Logging;

    /// <summary>
    /// Set a picture on a list of EITFormatDisplay items.
    /// </summary>
    internal class EitListPictureChooser : IEitListProcessor
    {
        private readonly ILogger logger;

        private readonly Func<EITFormatDisplay, bool> skipEitForColoringFunction;

        /// <summary>
        /// Initializes a new instance of the <see cref="EitListPictureChooser" /> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="skipEitForColoringFunction">Determines, when to skip coloring.</param>
        public EitListPictureChooser(ILogger logger, Func<EITFormatDisplay, bool> skipEitForColoringFunction)
        {
            Guard.NotNull(() => logger, logger);
            this.logger = logger;

            this.skipEitForColoringFunction = skipEitForColoringFunction;
        }

        /// <summary>
        /// Colors the specified list of EITFormatDisplay's.
        /// </summary>
        /// <param name="eits">The list of EITFormatDisplay.</param>
        public void Apply(IEnumerable<EITFormatDisplay> eits)
        {
            try
            {
                foreach (var display in eits)
                {
                    var func = this.skipEitForColoringFunction;
                    if (func != null && func(display))
                    {
                        display.Icon = Icons64.documents;
                        ////display.Icon = null;
                        ////display.IsFixed = false;
                        display.DisplayState = EitDisplayState.Unfixed;
                    }
                    else
                    {
                        display.Icon = Icons64.exec;
                        ////display.IsFixed = true;
                        display.DisplayState = EitDisplayState.Fixed;
                    }
                }
            }
            catch (Exception ex)
            {
                this.logger.Error(ex.ToString());
            }
        }
    }
}