// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RichtextBoxFormat.cs" company="EvePanix">
//   Copyright (c) Jedzia 2001-2014, EvePanix. All rights reserved. See the license notes shipped with this source and the GNU GPL.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace EitView
{
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Documents;
    using System.Windows.Media;

    /// <summary>
    ///     Extension class for Rich Text Color Formatting.
    /// </summary>
    public static class RichtextBoxFormat
    {
        /// <summary>
        /// Appends the bold black <c>text</c> .
        /// </summary>
        /// <param name="richTextBox">The rich <paramref name="text" /> box.</param>
        /// <param name="text">The text.</param>
        /// <exception cref="System.InvalidOperationException">This property is set while a change block has been activated.</exception>
        /// <exception cref="System.ArgumentNullException">An attempt is made to set this property to null.</exception>
        public static void AppendBoldBlackText(this RichTextBox richTextBox, string text)
        {
            richTextBox.AppendText(text, FontWeights.Bold, Brushes.Black);
        }

        /// <summary>
        /// Appends the bold <c>text</c> .
        /// </summary>
        /// <param name="richTextBox">The rich <paramref name="text" /> box.</param>
        /// <param name="text">The text.</param>
        /// <param name="solidColorBrush">The solid color brush.</param>
        /// <exception cref="System.InvalidOperationException">This property is set while a change block has been activated.</exception>
        /// <exception cref="System.ArgumentNullException">An attempt is made to set this property to null.</exception>
        public static void AppendBoldText(this RichTextBox richTextBox, string text, SolidColorBrush solidColorBrush = null)
        {
            richTextBox.AppendText(text, FontWeights.Bold, solidColorBrush);
        }

        /// <summary>
        /// Appends the black <c>text</c> .
        /// </summary>
        /// <param name="richTextBox">
        /// The rich <paramref name="text"/> box.
        /// </param>
        /// <param name="text">
        /// The text.
        /// </param>
        /// <exception cref="System.InvalidOperationException">
        /// This property is set while a change block has been activated.
        /// </exception>
        /// <exception cref="System.ArgumentNullException">
        /// An attempt is made to set this property to null.
        /// </exception>
        public static void AppendBlackText(this RichTextBox richTextBox, string text)
        {
            richTextBox.AppendText(text, FontWeights.Normal, Brushes.Black);
        }

        /// <summary>
        /// Appends the normal <c>text</c> .
        /// </summary>
        /// <param name="richTextBox">
        /// The rich <paramref name="text"/> box.
        /// </param>
        /// <param name="text">
        /// The text.
        /// </param>
        /// <param name="solidColorBrush">
        /// The solid color brush.
        /// </param>
        /// <exception cref="System.InvalidOperationException">
        /// This property is set while a change block has been activated.
        /// </exception>
        /// <exception cref="System.ArgumentNullException">
        /// An attempt is made to set this property to null.
        /// </exception>
        public static void AppendNormalText(
            this RichTextBox richTextBox, 
            string text, 
            SolidColorBrush solidColorBrush = null)
        {
            if (solidColorBrush == null)
            {
                solidColorBrush = Brushes.Black;
            }

            richTextBox.AppendText(text, FontWeights.Normal, solidColorBrush);
        }

        /// <summary>
        /// Appends the normal <c>text</c> .
        /// </summary>
        /// <param name="richTextBox">The rich <paramref name="text" /> box.</param>
        /// <param name="text">The text.</param>
        /// <param name="fontWeight">The font weight.</param>
        /// <param name="solidColorBrush">The solid color brush.</param>
        /// <exception cref="System.InvalidOperationException">This property is set while a change block has been activated.</exception>
        /// <exception cref="System.ArgumentNullException">An attempt is made to set this property to null.</exception>
        public static void AppendText(
            this RichTextBox richTextBox, 
            string text, 
            FontWeight fontWeight = default(FontWeight), 
            SolidColorBrush solidColorBrush = null)
        {
            var tr = new TextRange(richTextBox.Document.ContentEnd, richTextBox.Document.ContentEnd) { Text = text };
            if (fontWeight == default(FontWeight))
            {
                fontWeight = FontWeights.Normal;
            }

            if (solidColorBrush == null)
            {
                solidColorBrush = Brushes.Black;
            }

            SetProperties(tr, solidColorBrush, fontWeight);
        }

        private static void SetProperties(TextRange tr, SolidColorBrush solidColorBrush, FontWeight fontWeight)
        {
            tr.ApplyPropertyValue(TextElement.ForegroundProperty, solidColorBrush);
            tr.ApplyPropertyValue(TextElement.FontWeightProperty, fontWeight);
        }
    }
}