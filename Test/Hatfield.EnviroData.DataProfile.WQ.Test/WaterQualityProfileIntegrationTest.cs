using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Globalization;
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
        private ODM2Entities _dbContext;
        private IWaterQualityDataProfile _wqDataProfile;
        private List<Hatfield.EnviroData.DataProfile.WQ.Models.Site> siteList;

        [OneTimeSetUp]
        public void Init()
        {
            _dbContext = new ODM2Entities();
            _wqDataProfile = new WaterQualityDataProfile(_dbContext);

            siteList = new List<Hatfield.EnviroData.DataProfile.WQ.Models.Site>
            {
                            new Hatfield.EnviroData.DataProfile.WQ.Models.Site
                            {
                                Id = 1,
                                Name = "C1",
                                Latitude = 49.2827,
                                Longitude = 123.1207
                             },
                            new Hatfield.EnviroData.DataProfile.WQ.Models.Site
                            {
                                Id = 2,
                                Name = "C2",
                                Latitude = 49.2827,
                                Longitude = 123.1207
                            },
                            new Hatfield.EnviroData.DataProfile.WQ.Models.Site
                            {
                                 Id = 3,
                                Name = null,
                                Latitude = 49.2827,
                                Longitude = 111.1111
                            },
                            new Hatfield.EnviroData.DataProfile.WQ.Models.Site
                            {
                                Id = 4,
                                Name = "S01",
                                Latitude = 49.2827,
                                Longitude = 123.1207
                            }
                            
             };
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
            var arraySites = sites.ToArray();  

            Assert.NotNull(sites);
            Assert.AreEqual(4, sites.Count());

                for (int i = 0; i < arraySites.Length; i++)
                {
                    Assert.AreEqual(siteList[i].Name, arraySites[i].Name);
                    Assert.AreEqual(siteList[i].Longitude, arraySites[i].Longitude);
                    Assert.AreEqual(siteList[i].Latitude, arraySites[i].Latitude);
                }

        }


        [Test] 
        public void AnalyteQueryTest()
        {
            var analytes = _wqDataProfile.GetAllAnalytes();
            var arrayAnalytes = analytes.ToArray();

            Assert.NotNull(analytes);
            Assert.AreEqual(21, analytes.Count());  //total number of returned values


            String[] arrayNames = { null, null, "Toluene", "Ethylbenzene", "Xylenes", "Naphthenic Acid", "Toluene-d8 (BTEX)", "Benzene", "o-Terphenyl (F2-F4)", "C>10 - C16", "Phenolphthalein Alkalinity", "Alkalinity (Total as CaCO3)", "Alkalinity (Carbonate as CaCO3)", "True Colour", "Conductivity", "pH", "Calc Total Kjeldahl Nitrogen", "nitrate and nitrite (as N)", "Ortho Phosphate (P)", "Phosphorus (P)", "Nitrogen (N)" };


            for (int i = 0; i < 21; i++)
            {
                Assert.AreEqual(arrayNames[i], arrayAnalytes[i].Name);
                Assert.AreEqual((i + 12), arrayAnalytes[i].Id);
                if(i==1)
                {
                    Assert.AreEqual("Unknown", arrayAnalytes[i].Category.Name);
                }
                else
                {
                    Assert.AreEqual("Chemistry", arrayAnalytes[i].Category.Name);
                }
            }

        }

        /// <summary>
        /// Test for query that generates sampling activities
        /// </summary>

        [Test]
        public void SamplingActivityQueryTest()
        {
            List<DateTime> startDates = new List<DateTime>
            {
                new DateTime(2011, 05, 16), 
                new DateTime(2012, 05, 16),
                new DateTime(2016, 06, 01)
            };

            var samplingActivities = _wqDataProfile.GetAllSamplingActivities();
            var arraySamplingActivities = samplingActivities.ToArray();
            Assert.NotNull(samplingActivities);
            Assert.AreEqual(8, samplingActivities.Count());

            for (int i = 0; i < 8; i++)
            {

                if (i < 2)
                {
                    Assert.AreEqual(startDates[0], arraySamplingActivities[i].StartDateTime);
                }
                else if (i >= 2 && i < 4)
                {
                    Assert.AreEqual(startDates[1], arraySamplingActivities[i].StartDateTime);
                }
                else
                {
                    Assert.AreEqual(startDates[2], arraySamplingActivities[i].StartDateTime);
                }
            }

        }
        /// <summary>
        /// Test for Sampling activities within Date parameters
        /// </summary>
        [Test] 
        public void QuerySamplingActivitiesDateTest()
        {

            List<DateTime> beginDates = new List<DateTime>
            {
                new DateTime(2011, 01, 01),
                new DateTime(2012, 01, 01),
                new DateTime(2016, 01, 01)
            };

            List<DateTime> endDates = new List<DateTime>
            {
                new DateTime(2011, 12, 31),
                new DateTime(2012, 12, 31),
                new DateTime(2016, 12, 31)
            };

            List<DateTime> resultDates = new List<DateTime>
            {
                new DateTime(2011, 05, 16),
                new DateTime(2012, 05, 16),
                new DateTime(2016, 06, 01)
            };

            int[] queryCount = {2, 2, 4};   // number of sample actions per year

            List<IQueryable<SamplingActivity>> QueryList = new List<IQueryable<SamplingActivity>>();

            for(int i=0; i<beginDates.Count(); i++)
            {
                var samplingActivities = _wqDataProfile.QuerySamplingActivities(beginDates[i], endDates[i]);
                Assert.NotNull(samplingActivities);
                QueryList.Add(samplingActivities);
            }

            Assert.AreEqual(3, QueryList.Count());  //number of registered queries
            
            
            int queryCounter = 0;
            int iCheck = 0;
            foreach (var resultQuery in QueryList)
            {
                

                foreach (var arrayResult in resultQuery)
                {
                    Assert.AreEqual(resultDates[iCheck], arrayResult.StartDateTime);
                    queryCounter++;
                }
                iCheck++;
            }

            Assert.AreEqual(8, queryCounter);    //total number of values returned from all queries

            
		}

        [Test]
        public void QuerySamplingActivitiesDateandSiteTest()
        {
            List<DateTime> beginDates = new List<DateTime>
            {
                new DateTime(2011, 01, 01),
                new DateTime(2012, 01, 01),
                new DateTime(2016, 01, 01)
            };

            List<DateTime> endDates = new List<DateTime>
            {
                new DateTime(2011, 12, 31),
                new DateTime(2012, 12, 31),
                new DateTime(2016, 12, 31)
            };

            List<DateTime> resultDates = new List<DateTime>
            {
                new DateTime(2011, 05, 16),
                new DateTime(2012, 05, 16),
                new DateTime(2016, 06, 01)
            };

            List<IQueryable<SamplingActivity>> QueryList = new List<IQueryable<SamplingActivity>>();


            for (int i = 0; i < beginDates.Count(); i++)
            {
                for (int j = 0; j < siteList.Count(); j++)
                {
                    var waterQualityProfile = _wqDataProfile.QuerySamplingActivities(beginDates[i], endDates[i], siteList[j]);
                    Assert.NotNull(waterQualityProfile);
                    QueryList.Add(waterQualityProfile);
                }

           }
  
            int queryCounter = 0;
         

            foreach (var resultQuery in QueryList)
            {
                var arraySamplingFeatures = resultQuery.ToArray();

                foreach (var arrayResult in arraySamplingFeatures)
                {
                    queryCounter++;
                }
                
            }

            Assert.AreEqual(8, queryCounter);

         
        }

        /// <summary>
        /// Test for Water Quality Profile generation with input dates
        /// </summary>

        [Test]
        public void QueryWaterQualityDataTestDate()
        {
            List<DateTime> beginDates = new List<DateTime>
            {
                new DateTime(2014, 01, 01),
                new DateTime(2015, 01, 01),
                new DateTime(2016, 01, 01)
            };

            List<DateTime> endDates = new List<DateTime>
            {
                new DateTime(2014, 12, 31),
                new DateTime(2015, 12, 31),
                new DateTime(2016, 12, 31)
            };

            DateTime beginDate1 = new DateTime(2010, 6, 01);

            DateTime endDate1 = new DateTime(2016, 12, 31);

            int[] resultCount = { 27, 143, 4 }; //query results for each year
            
            List<IQueryable<WaterQualityObservation>> QueryList = new List<IQueryable<WaterQualityObservation>>();

            var waterQualityProfile1 = _wqDataProfile.QueryWaterQualityData(beginDate1, endDate1);
            var arrayWaterQualityProfile = waterQualityProfile1.ToArray();
         
            Assert.NotNull(arrayWaterQualityProfile);
            Assert.AreEqual(174, arrayWaterQualityProfile.Count()); //total number of results

            for(int i=0; i<beginDates.Count();i++)
            {
                var waterQualityProfile = _wqDataProfile.QueryWaterQualityData(beginDates[i], endDates[i]);
                Assert.NotNull(waterQualityProfile);
                QueryList.Add(waterQualityProfile);
                Assert.AreEqual(resultCount[i], QueryList[i].Count());
            }
   
        }

        /// <summary>
        /// Test for Water Profile Generation with Dates and Site query
        /// </summary>


        [Test]
        public void QueryWaterQualityDataTestSite()
        {
            List<DateTime> beginDates = new List<DateTime>
            {
                new DateTime(2014, 01, 01),
                new DateTime(2015, 01, 01),
                new DateTime(2016, 01, 01)
            };

            List<DateTime> endDates = new List<DateTime>
            {
                new DateTime(2014, 12, 31),
                new DateTime(2015, 12, 31),
                new DateTime(2016, 12, 31)
            };

            List<Hatfield.EnviroData.DataProfile.WQ.Models.Site> siteList1 = new List<Hatfield.EnviroData.DataProfile.WQ.Models.Site>
            {
                            new Hatfield.EnviroData.DataProfile.WQ.Models.Site
                            {
                                Id = 1,
                                Name = "C1",
                                Latitude = 49.2827,
                                Longitude = 123.1207
                             },
                            new Hatfield.EnviroData.DataProfile.WQ.Models.Site
                            {
                                Id = 2,
                                Name = "C2",
                                Latitude = 49.2827,
                                Longitude = 123.1207
                            },
                            new Hatfield.EnviroData.DataProfile.WQ.Models.Site
                            {
                                 Id = 3,
                                Name = null,
                                Latitude = 49.2827,
                                Longitude = 111.1111
                            },
                            new Hatfield.EnviroData.DataProfile.WQ.Models.Site
                            {
                                Id = 4,
                                Name = "S01",
                                Latitude = 49.2827,
                                Longitude = 123.1207
                            }
                            
             };

            //number of sites returned after each query
            int[] queryCount = { 18, 0, 0, 9, 143, 0, 0, 0, 1, 1, 1, 1 };


            List<IQueryable<WaterQualityObservation>> QueryList = new List<IQueryable<WaterQualityObservation>>();

            for (int i = 0; i < beginDates.Count(); i++)
            {
                for(int j=0; j< siteList.Count();j++)
                {
                    var waterQualityProfile = _wqDataProfile.QueryWaterQualityData(beginDates[i], endDates[i], siteList1[j]);
                    Assert.NotNull(waterQualityProfile);
                    QueryList.Add(waterQualityProfile);
                }
          
            }
            
            int k = 0;

            Assert.AreEqual(12, QueryList.Count()); //test for the number of values returned after the query
            //four queries


            for (int i = 0; i < QueryList.Count(); i++)
            {
                Assert.AreEqual(queryCount[i], QueryList[i].Count());
                var tempArray = QueryList[i].ToArray();

                foreach (var queryResult in tempArray)
                {
                    Assert.AreEqual(siteList[k].Id, queryResult.Site.Id);
                    Assert.AreEqual(siteList[k].Name, queryResult.Site.Name);
                }

                k++;
                if (k%4 == 0)
                {
                    k = 0;
                }
            }


        }

        /// <summary>
        /// Test of Water Profile Generation with Analyte and Dates in query
        /// </summary>

        [Test]
        public void QueryWaterQualityDataTestAnalyte()
        {
            DateTime beginDate1 = new DateTime(2010, 1, 01, 10, 34, 9);

            DateTime endDate1 = new DateTime(2016, 7, 18, 18, 34, 9);

            DateTime resultDate = new DateTime(2016, 6, 15, 10, 34, 9);


            List<Hatfield.EnviroData.DataProfile.WQ.Models.Analyte> analyteList = new List<Hatfield.EnviroData.DataProfile.WQ.Models.Analyte>();

            String[] arrayNames = { null, null, "Toluene", "Ethylbenzene", "Xylenes", "Naphthenic Acid", "Toluene-d8 (BTEX)", "Benzene", "o-Terphenyl (F2-F4)", "C>10 - C16", "Phenolphthalein Alkalinity", "Alkalinity (Total as CaCO3)", "Alkalinity (Carbonate as CaCO3)", "True Colour", "Conductivity", "pH", "Calc Total Kjeldahl Nitrogen", "nitrate and nitrite (as N)", "Ortho Phosphate (P)", "Phosphorus (P)", "Nitrogen (N)" };

            for (int i = 0; i < 21; i++)
            {
                Analyte analyte = new Analyte
                {
                    Id = (12 + i),
                    Name = arrayNames[i],
                    Category = new AnalyteCategory
                    {
                        Id = 14,
                        Name = "Chemistry"
                    }
                    
                };
                analyteList.Add(analyte);

            }

            List<IQueryable<WaterQualityObservation>> QueryList = new List<IQueryable<WaterQualityObservation>>();

            for(int i=0; i<analyteList.Count(); i++ )
            {
                var waterQualityProfile = _wqDataProfile.QueryWaterQualityData(beginDate1, endDate1, analyteList[i]);
                Assert.NotNull(waterQualityProfile);
                QueryList.Add(waterQualityProfile);
            }

            int queryCounter = 0;

            for(int i=0; i<QueryList.Count();i++)
            {
                for(int j=0; j<QueryList[i].Count(); j++)
                {
                    queryCounter++;
                }
            }

            Assert.AreEqual(21, QueryList.Count()); //test for the number of queries

            Assert.AreEqual(174, queryCounter); //number of result values returned

            for (int i = 0; i < analyteList.Count(); i++)
            {
                var tempArray = QueryList[i].ToArray();

                foreach (var resultValue in tempArray)
                {
                       Assert.AreEqual(analyteList[i].Id, resultValue.Analyte.Id);
                }
            
            }

        }

        [Test]
        public void QueryWaterQualityDataTestAnalyteAndSite()
        {
            DateTime beginDate1 = new DateTime(2010, 1, 01, 10, 34, 9);

            DateTime endDate1 = new DateTime(2016, 7, 18, 18, 34, 9);

            List<Hatfield.EnviroData.DataProfile.WQ.Models.Analyte> analyteList = new List<Hatfield.EnviroData.DataProfile.WQ.Models.Analyte>();
            String[] arrayNames = { null, null, "Toluene", "Ethylbenzene", "Xylenes", "Naphthenic Acid", "Toluene-d8 (BTEX)", "Benzene", "o-Terphenyl (F2-F4)", "C>10 - C16", "Phenolphthalein Alkalinity", "Alkalinity (Total as CaCO3)", "Alkalinity (Carbonate as CaCO3)", "True Colour", "Conductivity", "pH", "Calc Total Kjeldahl Nitrogen", "nitrate and nitrite (as N)", "Ortho Phosphate (P)", "Phosphorus (P)", "Nitrogen (N)" };
            for (int i = 0; i < 21; i++)
            {
                Analyte analyte = new Analyte
                {
                    Id = (12 + i),
                    Name = arrayNames[i],
                    Category = new AnalyteCategory
                    {
                        Id = 14,
                        Name = "Chemistry"
                    }

                };
                analyteList.Add(analyte);
            }


            List<IQueryable<WaterQualityObservation>> QueryList = new List<IQueryable<WaterQualityObservation>>();
            int k = 0;

            for(int i= 0 ; i<siteList.Count();i++)
            {

                for (int j = 0; j < analyteList.Count(); j++)
                {
                    var waterQualityProfile = _wqDataProfile.QueryWaterQualityData(beginDate1, endDate1, siteList[i], analyteList[j]);
                    Assert.NotNull(waterQualityProfile);
                    QueryList.Add(waterQualityProfile);
                }
              
            }

            Assert.AreEqual(84, QueryList.Count()); //test for the number of values returned after the query

            for (int i = 0; i < siteList.Count(); i++)
            {

                for (int j = 0; j < analyteList.Count(); j++)
                {
                    var tempArray = QueryList[k].ToArray();

                        foreach (var result in tempArray)
                        {
                            Assert.AreEqual(siteList[i].Id, result.Site.Id);
                            Assert.AreEqual(analyteList[j].Id, result.Analyte.Id);
                        }                    
                  
                    k++;
                }

            }

        }

        [Test]
        public void QueryWaterQualityDataListTest()
        {
            DateTime beginDate1 = new DateTime(2010, 6, 01);

            DateTime endDate1 = new DateTime(2016, 12, 31);

            DateTime resultDate = new DateTime(2016, 6, 15, 10, 34, 9);

            List<Hatfield.EnviroData.DataProfile.WQ.Models.Analyte> analyteList = new List<Hatfield.EnviroData.DataProfile.WQ.Models.Analyte>();

            String[] arrayNames = { null, null, "Toluene", "Ethylbenzene", "Xylenes", "Naphthenic Acid", "Toluene-d8 (BTEX)", "Benzene", "o-Terphenyl (F2-F4)", "C>10 - C16", "Phenolphthalein Alkalinity", "Alkalinity (Total as CaCO3)", "Alkalinity (Carbonate as CaCO3)", "True Colour", "Conductivity", "pH", "Calc Total Kjeldahl Nitrogen", "nitrate and nitrite (as N)", "Ortho Phosphate (P)", "Phosphorus (P)", "Nitrogen (N)" };

            for (int i = 0; i < 21; i++)
            {
                Analyte analyte = new Analyte
                {
                    Id = (12 + i),
                    Name = arrayNames[i],
                    Category = new AnalyteCategory
                    {
                        Id = 14,
                        Name = "Chemistry"
                    }

                };
                analyteList.Add(analyte);
            }
               
            
            List<IQueryable<WaterQualityObservation>> queryList = new List<IQueryable<WaterQualityObservation>>();

            var waterQualityProfile = _wqDataProfile.QueryWaterQualityData(beginDate1, endDate1, siteList, analyteList);
          
            queryList.Add(waterQualityProfile);

            var arrayWaterQualityProfile = queryList[0].ToArray();
            Assert.NotNull(waterQualityProfile);

            Assert.AreEqual(174, waterQualityProfile.Count());  //total number of analyte results

            Assert.AreEqual(siteList[0].Id, arrayWaterQualityProfile[0].Site.Id);
            Assert.AreEqual(siteList[0].Name, arrayWaterQualityProfile[0].Site.Name);
            Assert.AreEqual(analyteList[2].Id, arrayWaterQualityProfile[0].Analyte.Id);
            Assert.AreEqual(analyteList[2].Name, arrayWaterQualityProfile[0].Analyte.Name);

            Assert.AreEqual(siteList[3].Id, arrayWaterQualityProfile[173].Site.Id);
            Assert.AreEqual(siteList[3].Name, arrayWaterQualityProfile[173].Site.Name);

        }


    }
}
