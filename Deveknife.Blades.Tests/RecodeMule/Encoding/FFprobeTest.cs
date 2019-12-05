namespace Deveknife.Blades.RecodeMule.Encoding.Tests.RecodeMule.Encoding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using MbUnit.Framework;
    using Deveknife.Blades.RecodeMule.Encoding;
    using Rhino.Mocks;
    
    
    [TestFixture()]
    public partial class FFprobeTest
    {
        
        private string ffprobePath;
        
        private Castle.Core.Logging.ILogger logger;
        
        private MockRepository mocks;
        
        private FFprobe testObject;

        [SetUp()]
        public void SetUp()
        {
            // ToDo: Implement SetUp logic here 
            this.mocks = new MockRepository();
            this.logger = this.mocks.Stub<Castle.Core.Logging.ILogger>();
            this.ffprobePath = "Value of ffprobePath";
            this.testObject = new FFprobe(this.logger, this.ffprobePath);

            Assert.Inconclusive("Verify the correctness of this test method.");
        }
        
        [TearDown()]
        public void TearDown()
        {
            // ToDo: Implement TearDown logic here 
            this.testObject = null;

            Assert.Inconclusive("Verify the correctness of this test method.");
        }
        
        [Test()]
        public void PropertyStreamMappingNormalBehavior()
        {
            // TODO: Implement unit test for PropertyStreamMapping

            // Test read access of 'StreamMapping' Property.
            var expected = "Value of StreamMapping";
            var actual = testObject.StreamMapping;
            Assert.AreEqual(expected, actual);

            Assert.Inconclusive("Verify the correctness of this test method.");
        }
        
        [Test()]
        public void DetectTest()
        {
            // TODO: Implement unit test for Detect

            Assert.Inconclusive("Verify the correctness of this test method.");
        }
        
        [Test()]
        public void ProbeLogTest()
        {
            // TODO: Implement unit test for ProbeLog

            Assert.Inconclusive("Verify the correctness of this test method.");
        }
    }
}
