// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EitListColorer.cs" company="EvePanix">
//   Copyright (c) Jedzia 2001-2014, EvePanix. All rights reserved. See the license notes shipped with this source and the GNU GPL.
// </copyright>
//  <author>Jedzia</author>
//  <email>jed69@gmx.de</email>
//  <date>23.02.2014 22:42</date>
// --------------------------------------------------------------------------------------------------------------------
namespace Deveknife.Blades.Overview
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Castle.Core.Logging;

    using Guard = Deveknife.Guard;

    /// <summary>
    /// Color a list of EITFormatDisplay items with the specified coloring function.
    /// </summary>
    internal class EitListColorer : IEitListProcessor
    {
        private readonly ColorCarousel colorCarousel = new ColorCarousel();

        private readonly Func<EITFormatDisplay, string> groupEitForColoringFunc;

        private readonly ILogger logger;

        private readonly Func<EITFormatDisplay, bool> skipEitForColouringFunc;

        /// <summary>
        /// Initializes a new instance of the <see cref="EitListColorer" /> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="groupEitForColoringFunc">The grouping function for colouring.</param>
        /// <param name="skipEitForColouringFunc">Determines, when to skip coloring.</param>
        public EitListColorer(
            ILogger logger, 
            Func<EITFormatDisplay, string> groupEitForColoringFunc, 
            Func<EITFormatDisplay, bool> skipEitForColouringFunc)
        {
            Guard.NotNull(() => logger, logger);
            this.logger = logger;
            this.groupEitForColoringFunc = groupEitForColoringFunc;
            this.skipEitForColouringFunc = skipEitForColouringFunc;
        }

        /// <summary>
        /// Colors the specified list of EITFormatDisplay's.
        /// </summary>
        /// <param name="eits">The list of EITFormatDisplay.</param>
        public void Apply(IEnumerable<EITFormatDisplay> eits)
        {
            try
            {
                // here goes the doublets detection
                var groups = eits.GroupBy(itm => this.groupEitForColoringFunc(itm)).ToList();
                foreach (var eitformatGroup in groups)
                {
                    var cnt = eitformatGroup.Count(eit => this.skipEitForColouringFunc(eit));

                    if (cnt < 2)
                    {
                        continue;
                    }

                    var eitColor = this.colorCarousel.GetColor().ToArgb();
                    foreach (var eitFormat in eitformatGroup)
                    {
                        eitFormat.Color = eitColor;
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