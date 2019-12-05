// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WinRARTests.cs" company="EvePanix">
//   Copyright (c) Jedzia 2001-2015, EvePanix. All rights reserved. See the license notes shipped with this source and the GNU GPL.
// </copyright>
//  <author>Jedzia</author>
//  <email>jed69@gmx.de</email>
//  <date>28.07.2015 20:07</date>
// <summary>
//   Defines the WinRARTests type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Deveknife.Blades.FileManager.Tests.Jobs.Compression
{
    using Deveknife.Api;
    using Deveknife.Blades.FileManager.Jobs.Compression;
    using Deveknife.Blades.Utils.Filters;

    using Castle.Core.Logging;

    using NUnit.Framework;

    using Rhino.Mocks;

    [TestFixture]
    public class WinRARTests
    {
        private ILogger logger;

        private MockRepository mocks;

        private WinRar testObject;

        private string winrarPath;

        [Test]
        public void RemoveTest()
        {
            // TODO: Implement unit test for Remove
            Assert.IsTrue(true);
        }

        [Test]
        public void TempTest()
        {
            string path = @"E:\Shared\!Remove\DAZ\!V4\97429 Modern School Uniform";
            IWinrarParameters parameters = new WinrarParameters();
            ITextFilter filter = new DefaultTextFilter();
            this.testObject.Remove(path, parameters, filter);
        }

        [SetUp]
        public void SetUp()
        {
            // ToDo: Implement SetUp logic here 
            this.mocks = new MockRepository();
            this.logger = this.mocks.StrictMock<ILogger>();
            this.winrarPath = @"C:\Program Files\WinRAR\WinRAR.exe";
            this.testObject = new WinRar(this.logger, this.winrarPath);

            //Assert.Inconclusive("Verify the correctness of this test method.");
        }

        [TearDown]
        public void TearDown()
        {
            // ToDo: Implement TearDown logic here 
            this.testObject = null;

            //Assert.Inconclusive("Verify the correctness of this test method.");
        }
    }
}