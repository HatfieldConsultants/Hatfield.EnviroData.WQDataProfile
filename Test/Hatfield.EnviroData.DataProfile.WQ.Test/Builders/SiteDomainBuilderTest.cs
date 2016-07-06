using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

using NUnit.Framework;

using Hatfield.EnviroData.DataProfile.WQ.Models;
using Hatfield.EnviroData.Core;
using Hatfield.EnviroData.DataProfile.WQ.Builders;

namespace Hatfield.EnviroData.DataProfile.WQ.Test.Builders
{
    [TestFixture]
    public class SiteDomainBuilderTest
    {
        [Test]
        [TestCaseSource("isDataDirtyTestCases")]
        public void IsDataDirtyTest(WQProfileEntity entity, object domain, bool expectedIsDirty)
        {
            var builder = new SiteDomainBuilder();
            var actualDirty = builder.IsDataDirty(entity, domain);

            Assert.AreEqual(expectedIsDirty, actualDirty);
        }

        private static object[] isDataDirtyTestCases = {
                                                            new object[]{
                                                                null, null, true
                                                            },
                                                            new object[]{
                                                                new Hatfield.EnviroData.DataProfile.WQ.Models.Site
                                                                {
                                                                    Latitude = 123.4,
                                                                    Longitude = 23.5,
                                                                    Name = "C1"
                                                                }, 
                                                                new Hatfield.EnviroData.Core.Site
                                                                {
                                                                    Latitude = 123.4,
                                                                    Longitude = 23.5,
                                                                    SamplingFeature = new SamplingFeature
                                                                    {
                                                                        SamplingFeatureName = "C1"
                                                                    }
                                                                },
                                                                false
                                                            },
                                                            new object[]{
                                                                new Hatfield.EnviroData.DataProfile.WQ.Models.Site
                                                                {
                                                                    Latitude = 123.4,
                                                                    Longitude = 23.5,
                                                                    Name = "C1"
                                                                }, 
                                                                new Hatfield.EnviroData.Core.Site
                                                                {
                                                                    Latitude = 125.4,
                                                                    Longitude = 23.5,
                                                                    SamplingFeature = new SamplingFeature
                                                                    {
                                                                        SamplingFeatureName = "C1"
                                                                    }
                                                                },
                                                                true
                                                            },
                                                            new object[]{
                                                                new Hatfield.EnviroData.DataProfile.WQ.Models.Site
                                                                {
                                                                    Latitude = 123.4,
                                                                    Longitude = 23.5,
                                                                    Name = "C1"
                                                                }, 
                                                                new Hatfield.EnviroData.Core.Site
                                                                {
                                                                    Latitude = 123.4,
                                                                    Longitude = 23.5
                                                                },
                                                                true
                                                            }
                                                       };
    }
}
