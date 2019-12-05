namespace Deveknife.Blades.FileMoveTool.Tests.Filesystem
{
    using Deveknife.Blades.FileMoveTool.Filesystem;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass()]
    public partial class FileCheckerTests
    {
        
        private SystemInterface.IO.IDirectoryInfoFactory directoryInfoFactory;
        
        private Castle.Core.Logging.ILogger logger;
        
        //private MockRepository mocks;
        
        private FileChecker testObject;
        
        public FileCheckerTests()
        {}
        
        [TestInitialize()]public void SetUp(){
            // ToDo: Implement SetUp logic here 
            //this.mocks = new MockRepository(MockBehavior.Default);
            //this.logger = MockRepository.Mock<Castle.Core.Logging.ILogger>();
            this.logger = new Mock<Castle.Core.Logging.ILogger>().Object;
            //this.directoryInfoFactory = MockRepository.Mock<SystemInterface.IO.IDirectoryInfoFactory>();
            this.directoryInfoFactory = new Mock<SystemInterface.IO.IDirectoryInfoFactory>().Object;
            var fileInfoFactory = new Mock<SystemInterface.IO.IFileInfoFactory>().Object;
            this.testObject = new FileChecker(this.logger, this.directoryInfoFactory, fileInfoFactory);
        }
        
        [TestCleanup()]
        public void TearDown()
        {
            // ToDo: Implement TearDown logic here 
            this.testObject = null;
        }
        
        [TestMethod()]
        public void PropertyDirectoryInfoFactoryNormalBehavior()
        {
            // TODO: Implement unit test for PropertyDirectoryInfoFactory

            // Test read access of 'DirectoryInfoFactory' Property.
            var expected = this.directoryInfoFactory;
            var actual = this.testObject.DirectoryInfoFactory;
            Assert.AreSame(expected, actual);
        }
        
        [TestMethod()]
        public void PropertyLoggerNormalBehavior()
        {
            // TODO: Implement unit test for PropertyLogger

            // Test read access of 'Logger' Property.
            var expected = this.logger;
            var actual = this.testObject.Logger;
            Assert.AreSame(expected, actual);

            Assert.Inconclusive("Verify the correctness of this test method.");
        }
        
        [TestMethod()]
        public void CompareFoldersTest()
        {
            // TODO: Implement unit test for CompareFolders

            Assert.Inconclusive("Verify the correctness of this test method.");
        }
        
        [TestMethod()]
        public void CreateSymbolicLinkedContentTest()
        {
            // TODO: Implement unit test for CreateSymbolicLinkedContent

            Assert.Inconclusive("Verify the correctness of this test method.");
        }
        
        [TestMethod()]
        public void MoveSymbolicLinkedContentTest()
        {
            // TODO: Implement unit test for MoveSymbolicLinkedContent

            Assert.Inconclusive("Verify the correctness of this test method.");
        }
    }
}
