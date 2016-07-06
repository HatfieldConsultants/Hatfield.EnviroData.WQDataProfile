using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NUnit.Framework;

using Hatfield.EnviroData.Core;
using Hatfield.EnviroData.DataProfile.WQ.Models;
using Moq;


namespace Hatfield.EnviroData.DataProfile.WQ.Test
{
    [TestFixture]
    public class WaterQualityProfileTest
    {
        [Test]
        public void TestSiteQuery()
        {
            var siteDb = new InMemoryDatabaseGenerator().CreateTestDbContext();

            var dataProfile = new WaterQualityDataProfile(siteDb);

            var actualSites = dataProfile.GetAllSites();

            Assert.NotNull(actualSites);
            Assert.AreEqual(3, actualSites.Count());
            //Assert.Throws<NotImplementedException>(() => dataProfile.GetAllSites());
        }

        [Test]
        public void TestAnalyteQuery()
        {
            var siteDb = new InMemoryDatabaseGenerator().CreateTestDbContext();

            var dataProfile = new WaterQualityDataProfile(siteDb);

            Assert.Throws<NotImplementedException>(() => dataProfile.GetAllAnalytes());
            //var actualAnalytes = dataProfile.GetAllAnalytes();

            //Assert.NotNull(actualAnalytes);
            //Assert.AreEqual(3, actualAnalytes);
        }

    }
}
