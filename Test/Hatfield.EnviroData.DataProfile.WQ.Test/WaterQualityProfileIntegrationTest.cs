using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Hatfield.EnviroData.Core;
using Hatfield.EnviroData.DataProfile.WQ.Models;

namespace Hatfield.EnviroData.DataProfile.WQ.Test
{
    using NUnit.Framework;

    [TestFixture]
    public class WaterQualityProfileIntegrationTest
    {
        private DbContext _dbContext;
        private IWaterQualityDataProfile _wqDataProfile;

        [OneTimeSetUp]
        public void Init()
        {
            _dbContext = new ODM2Entities();
            _wqDataProfile = new WaterQualityDataProfile(_dbContext);    
        }

        [OneTimeTearDown]
        public void Cleanup()
        {
            _wqDataProfile.Dispose();
        }

        [Test]
        public void SiteQueryTest()
        {
            var sites = _wqDataProfile.GetAllSites();
            var arraySites = sites.ToArray();   //lazy loading 

            double[] Longitudes = { 123.1207, 123.1207, 111.1111, 123.1207 };

            string[] Names = { "C1", "C2", null, "S01" };

            for (int i = 0; i < arraySites.Length; i++)
            {
                Assert.AreEqual(Names[i], arraySites[i].Name);
                Assert.AreEqual(Longitudes[i], arraySites[i].Longitude);
                Assert.AreEqual(49.2827, arraySites[i].Latitude);
            }

            Assert.NotNull(sites);
            Assert.AreEqual(4, sites.Count());
        }

          [Test]
        public void AnalyteQueryTest()
        {
            var analytes = _wqDataProfile.GetAllAnalytes();
            var arrayAnalytes = analytes.ToArray();

            Assert.NotNull(analytes);
            Assert.AreEqual(4, analytes.Count());



        }

        [Test]
        public void SamplingActivityQueryTest()
          {
              var samplingActivities = _wqDataProfile.GetAllSamplingActivities();
              var arraySamplingActivities = samplingActivities.ToArray();

              Assert.NotNull(samplingActivities);
          }
    }
}
