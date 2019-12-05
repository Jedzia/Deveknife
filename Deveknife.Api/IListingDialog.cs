// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IListingDialog.cs" company="EvePanix">
//   Copyright (c) Jedzia 2001-2014, EvePanix. All rights reserved. See the license notes shipped with this source and the GNU GPL.
// </copyright>
//  <author>Jedzia</author>
//  <email>jed69@gmx.de</email>
//  <date>04.03.2014 09:55</date>
// --------------------------------------------------------------------------------------------------------------------
namespace Deveknife.Api
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Forms;

    public interface IListingDialog : IDisposable
    {
        DialogResult ShowDialog(IEnumerable<string> items);

        DialogResult ShowDialog(IWin32Window owner, IEnumerable<string> items);
    }

    public interface IDialogFactory
    {
        IListingDialog Create(string topic);
    }
}