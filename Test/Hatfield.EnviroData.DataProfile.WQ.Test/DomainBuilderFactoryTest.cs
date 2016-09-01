using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NUnit.Framework;
using Moq;

using Hatfield.EnviroData.DataProfile.WQ.Builders;
using Hatfield.EnviroData.DataProfile.WQ.Models;


namespace Hatfield.EnviroData.DataProfile.WQ.Test
{
    [TestFixture]
    public class DomainBuilderFactoryTest
    {
        [OneTimeSetUp]
        public void SetUp()
        {
            DomainBuilderFactory.DefaultSetUp();
        }

        [Test]
        [TestCase(typeof(Site), typeof(SiteDomainBuilder))]
        [TestCase(typeof(LabReportSample), typeof(WaterQualityObservationBuilder))]
        public void DefaultSetUpTest(Type testType, Type expectedBuilderType)
        {
            var builder = DomainBuilderFactory.Create(testType);
            Assert.NotNull(builder);
            Assert.IsInstanceOf(expectedBuilderType, builder);
        }

        [Test]
        public void RegisterTest()
        {
            var mockBuilder = new Mock<IDomainBuilder>();            

            DomainBuilderFactory.Register(typeof(int), () => mockBuilder.Object);

            var actualBuilder = DomainBuilderFactory.Create(typeof(int));
            Assert.NotNull(actualBuilder);
        }
    }
}
