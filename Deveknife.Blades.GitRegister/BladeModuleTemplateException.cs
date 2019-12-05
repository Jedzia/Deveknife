// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BladeModuleTemplateException.cs" company="EvePanix">
//   Copyright (c) Jedzia 2001-2016, EvePanix. All rights reserved. See the license notes shipped with this source and the GNU GPL.
// </copyright>
//  <author>Jedzia</author>
//  <email>jed69@gmx.de</email>
//  <date>22.10.2016 09:03</date>
// --------------------------------------------------------------------------------------------------------------------
namespace Deveknife.Blades.GitRegister
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// Default Exception thrown by this module.
    /// </summary>
    [Serializable]
    public class BladeModuleTemplateException : Exception
    {
        // For guidelines regarding the creation of new exception types, see
        // http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpgenref/html/cpconerrorraisinghandlingguidelines.asp
        // and
        // http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dncscol/html/csharp07192001.asp

        /// <summary>
        /// Initializes a new instance of the <see cref="BladeModuleTemplateException"/> class.
        /// </summary>
        public BladeModuleTemplateException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BladeModuleTemplateException" /> class with a specified error message.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public BladeModuleTemplateException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BladeModuleTemplateException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="inner">The inner.</param>
        public BladeModuleTemplateException(string message, Exception inner)
            : base(message, inner)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BladeModuleTemplateException" /> class with serialized data.
        /// </summary>
        /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination.</param>
        /// <exception cref="SerializationException">The class name is null or <see cref="P:System.Exception.HResult" /> is zero (0). </exception>
        protected BladeModuleTemplateException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}