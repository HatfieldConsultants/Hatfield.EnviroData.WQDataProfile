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
            var arraySites = sites.ToArray();   //lazy loading 


            Assert.NotNull(sites);
            Assert.AreEqual(4, sites.Count());

        

                for (int i = 0; i < arraySites.Length; i++)
                {
                    Assert.AreEqual(siteList[i].Name, arraySites[i].Name);
                    Assert.AreEqual(siteList[i].Longitude, arraySites[i].Longitude);
                    Assert.AreEqual(siteList[i].Latitude, arraySites[i].Latitude);
                }

        }


          [Test] //need to rewrite tests

        public void AnalyteQueryTest()
        {
            var analytes = _wqDataProfile.GetAllAnalytes();
            var arrayAnalytes = analytes.ToArray();


            Assert.NotNull(analytes);
            Assert.AreEqual(21, analytes.Count());
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

        [Test]
        public void SamplingActivityQueryTest()
        {

            List<DateTime> beginDates = new List<DateTime>
            {    
                new DateTime(2016, 6, 02, 10, 34, 9),
                new DateTime(2016, 6, 03, 10, 34, 9),
                new DateTime(2016, 6, 04, 10, 34, 9),
                new DateTime(2016, 6, 05, 10, 34, 9)
            };

            List<DateTime> endDates = new List<DateTime>
            {
                new DateTime(2016, 7, 18, 18, 34, 9),
                new DateTime(2016, 7, 18, 18, 34, 9),
                new DateTime(2016, 7, 18, 18, 34, 9),
                new DateTime(2016, 7, 18, 18, 34, 9)
            };


      

            var samplingActivities = _wqDataProfile.GetAllSamplingActivities();
            var arraySamplingActivities = samplingActivities.ToArray();
            Assert.NotNull(samplingActivities);
            Assert.AreEqual(4, samplingActivities.Count());

            for(var i =0; i< arraySamplingActivities.Length; i++)
            {
                
                      Assert.AreEqual(beginDates[i], arraySamplingActivities[i].StartDateTime);
                      Assert.AreEqual(7, arraySamplingActivities[i].StartDateTimeUTCOffset);
                      Assert.AreEqual(endDates[i], arraySamplingActivities[i].EndDateTime);
                      Assert.AreEqual(7, arraySamplingActivities[i].EndDateTimeUTCOffset);

                      Assert.AreEqual(siteList[i].Name, arraySamplingActivities[i].Site.Name);
                      Assert.AreEqual(siteList[i].Longitude, arraySamplingActivities[i].Site.Longitude);
                      Assert.AreEqual(siteList[i].Latitude, arraySamplingActivities[i].Site.Latitude);
                  
            }

       
        }

        [Test] 
        public void QuerySamplingActivities1Test()
        {

            List<DateTime> beginDates = new List<DateTime>
            {    
                new DateTime(2016, 6, 02, 10, 34, 9),
                new DateTime(2016, 6, 03, 10, 34, 9),
                new DateTime(2016, 6, 04, 10, 34, 9),
                new DateTime(2016, 6, 05, 10, 34, 9)
            };

            List<DateTime> endDates = new List<DateTime>
            {
                new DateTime(2016, 7, 18, 18, 34, 9),
                new DateTime(2016, 7, 18, 18, 34, 9),
                new DateTime(2016, 7, 18, 18, 34, 9),
                new DateTime(2016, 7, 18, 18, 34, 9)
            };

            int[] queryCount = {4, 3, 2, 1};

            List<IQueryable<SamplingActivity>> QueryList = new List<IQueryable<SamplingActivity>>();


            for(int i=0; i<beginDates.Count(); i++)
            {
                var samplingActivities = _wqDataProfile.QuerySamplingActivities(beginDates[i], endDates[i]);
                Assert.NotNull(samplingActivities);
                QueryList.Add(samplingActivities);
            }

            Assert.AreEqual(4, QueryList.Count());

            for (int i = 0; i < QueryList.Count();i++ )
            {
                Assert.AreEqual(queryCount[i], QueryList[i].Count()); //test for the number of values returned after the query result
                Assert.AreEqual(beginDates[i], QueryList[i].FirstOrDefault().StartDateTime);
                Assert.AreEqual(endDates[i], QueryList[i].FirstOrDefault().EndDateTime);
            }
     
		}

        [Test]
        public void QuerySamplingActivities2Test()
        {
            List<DateTime> beginDates = new List<DateTime>
            {    
                new DateTime(2016, 6, 02, 10, 34, 9),
                new DateTime(2016, 6, 03, 10, 34, 9),
                new DateTime(2016, 6, 04, 10, 34, 9),
                new DateTime(2016, 6, 05, 10, 34, 9)
            };

            List<DateTime> endDates = new List<DateTime>
            {
                new DateTime(2016, 7, 18, 18, 34, 9),
                new DateTime(2016, 7, 18, 18, 34, 9),
                new DateTime(2016, 7, 18, 18, 34, 9),
                new DateTime(2016, 7, 18, 18, 34, 9)
            };

     

            List<IQueryable<SamplingActivity>> QueryList = new List<IQueryable<SamplingActivity>>();


           for(int i=0;i<4;i++)
           {
               var samplingActivities = _wqDataProfile.QuerySamplingActivities(beginDates[i], endDates[i], siteList[i]);
               Assert.NotNull(samplingActivities);
               QueryList.Add(samplingActivities);
           }
      
 //           Assert.AreEqual(querySite, arraySamplingActivities[0].SamplingSites); won't work because of the different pointer levels

           for (int i = 0; i < QueryList.Count();i++ )
           {
               Assert.AreEqual(siteList[i].Id, QueryList[i].FirstOrDefault().Site.Id);
               Assert.AreEqual(siteList[i].Name, QueryList[i].FirstOrDefault().Site.Name);
               Assert.AreEqual(siteList[i].Latitude, QueryList[i].FirstOrDefault().Site.Latitude);
               Assert.AreEqual(siteList[i].Longitude, QueryList[i].FirstOrDefault().Site.Longitude);
           }
        }

        /// <summary>
        /// Test for Water Quality Profile generation with input dates
        /// </summary>

        [Test]
        public void QueryWaterQualityDataTestDate()
        {
            DateTime beginDate1 = new DateTime(2016, 6, 01, 10, 34, 9);

            DateTime endDate1 = new DateTime(2016, 7, 18, 18, 34, 9);

            DateTime resultDate = new DateTime(2016, 6, 15, 10, 34, 9);

            List<IQueryable<WaterQualityObservation>> QueryList = new List<IQueryable<WaterQualityObservation>>();

            var waterQualityProfile = _wqDataProfile.QueryWaterQualityData(beginDate1, endDate1);
           
            QueryList.Add(waterQualityProfile);
            var arrayWaterQualityProfile = QueryList[0].ToArray();

            Assert.NotNull(waterQualityProfile);
            Assert.AreEqual(12, QueryList.FirstOrDefault().Count()); //test for the number of values returned after the query

            

            for (int i = 0; i < arrayWaterQualityProfile.Length; i++)
            {
                Assert.AreEqual((i + 1), arrayWaterQualityProfile[i].Value);  //values
                Assert.AreEqual(resultDate, arrayWaterQualityProfile[i].DateTime); //date time
                Assert.AreEqual(7, arrayWaterQualityProfile[i].UTCOffset); //offset
                Assert.AreEqual((i + 1), arrayWaterQualityProfile[i].Value);

                var siteName = arrayWaterQualityProfile[i].Site.Name;
            }

        }
        /// <summary>
        /// Test for Water Profile Generation with Dates and Site query
        /// </summary>


        [Test]
        public void QueryWaterQualityDataTestSite()
        {

            DateTime beginDate1 = new DateTime(2016, 6, 01, 10, 34, 9);

            DateTime endDate1 = new DateTime(2016, 6, 28, 18, 34, 9);

            DateTime resultDate = new DateTime(2016, 6, 15, 10, 34, 9);


            List<IQueryable<WaterQualityObservation>> QueryList = new List<IQueryable<WaterQualityObservation>>();

            for(int i=0; i<siteList.Count();i++)
            {
                var waterQualityProfile = _wqDataProfile.QueryWaterQualityData(beginDate1, endDate1, siteList[i]);
                Assert.NotNull(waterQualityProfile);
                QueryList.Add(waterQualityProfile);
            }
            //four queries


            Assert.AreEqual(4, QueryList.Count()); //test for the number of values returned after the query

            for (int i = 0; i < QueryList.Count(); i++)
            {
                var arrayWaterQualityProfile = QueryList[i].ToArray();

                for (int j = 0; j < arrayWaterQualityProfile.Length; j++)
                {
                    Assert.AreEqual((i+1+4*j), arrayWaterQualityProfile[j].Value);  //values
                    Assert.AreEqual(resultDate, arrayWaterQualityProfile[j].DateTime); //date time
                    Assert.AreEqual(7, arrayWaterQualityProfile[j].UTCOffset); //offset

                    Assert.AreEqual(siteList[i].Id, arrayWaterQualityProfile[j].Site.Id);
                    Assert.AreEqual(siteList[i].Name, arrayWaterQualityProfile[j].Site.Name);
                    Assert.AreEqual(siteList[i].Latitude, arrayWaterQualityProfile[j].Site.Latitude);
                    Assert.AreEqual(siteList[i].Longitude, arrayWaterQualityProfile[j].Site.Longitude);   
                }
              
            }

        }

        /// <summary>
        /// Test of Water Profile Generation with Analyte and Dates in query
        /// </summary>

        [Test]
        public void QueryWaterQualityDataTestAnalyte()
        {
            DateTime beginDate1 = new DateTime(2016, 6, 01, 10, 34, 9);

            DateTime endDate1 = new DateTime(2016, 7, 18, 18, 34, 9);

            DateTime resultDate = new DateTime(2016, 6, 15, 10, 34, 9);

            List<Hatfield.EnviroData.DataProfile.WQ.Models.Analyte> analyteList = new List<Hatfield.EnviroData.DataProfile.WQ.Models.Analyte>();
            
            String[] arrayNames = {"Toluene", "Ethylbenzene", "Xylenes", "Naphthenic Acid", "Toluene-d8 (BTEX)", "Benzene", "o-Terphenyl (F2-F4)", "C>10 - C16", "Phenolphthalein Alkalinity", "Alkalinity (Total as CaCO3)", "Alkalinity (Carbonate as CaCO3)", "True Colour"};

            for (int i = 0; i < 12; i++)
            {
                Analyte analyte = new Analyte
                {
                    Id = (14 + i),
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


            for (int i = 0; i < QueryList.Count();i++ )
            {
                Assert.AreEqual(1,QueryList[i].Count());
                Assert.AreEqual((i + 1), QueryList[i].FirstOrDefault().Value);  //values
                Assert.AreEqual(resultDate, QueryList[i].FirstOrDefault().DateTime); //date time
                Assert.AreEqual(7, QueryList[i].FirstOrDefault().UTCOffset); //offset

                Assert.AreEqual(analyteList[i].Id, QueryList[i].FirstOrDefault().Analyte.Id); //analyte id
                Assert.AreEqual(analyteList[i].Name, QueryList[i].FirstOrDefault().Analyte.Name); //analyte name
            }

        }

        [Test]
        public void QueryWaterQualityDataTestAnalyteAndSite()
        {
            DateTime beginDate1 = new DateTime(2016, 6, 01, 10, 34, 9);

            DateTime endDate1 = new DateTime(2016, 7, 18, 18, 34, 9);

            DateTime resultDate = new DateTime(2016, 6, 15, 10, 34, 9);

            List<Hatfield.EnviroData.DataProfile.WQ.Models.Analyte> analyteList = new List<Hatfield.EnviroData.DataProfile.WQ.Models.Analyte>();

            String[] arrayNames = { "Toluene", "Ethylbenzene", "Xylenes", "Naphthenic Acid", "Toluene-d8 (BTEX)", "Benzene", "o-Terphenyl (F2-F4)", "C>10 - C16", "Phenolphthalein Alkalinity", "Alkalinity (Total as CaCO3)", "Alkalinity (Carbonate as CaCO3)", "True Colour" };

            for (int i = 0; i < 12; i++)
            {
                Analyte analyte = new Analyte
                {
                    Id = (14 + i),
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

            for(int i= 0 ; i<analyteList.Count();i++)
            {
           
                 var waterQualityProfile = _wqDataProfile.QueryWaterQualityData(beginDate1, endDate1, siteList[k], analyteList[i]);
                 Assert.NotNull(waterQualityProfile);
                 QueryList.Add(waterQualityProfile);
        
                k++;
                if ((i + 1) % 4 == 0)
                {
                    k = 0;
                }

            }

            Assert.AreEqual(12, QueryList.Count()); //test for the number of values returned after the query


            for (int i = 0; i < QueryList.Count(); i++)
            {
                Assert.AreEqual(1, QueryList[i].Count()); //test for the number of values returned after the query
                Assert.AreEqual((i + 1), QueryList[i].FirstOrDefault().Value);  //values
                Assert.AreEqual(resultDate, QueryList[i].FirstOrDefault().DateTime); //date time
                Assert.AreEqual(7, QueryList[i].FirstOrDefault().UTCOffset); //offset

                Assert.AreEqual(analyteList[i].Id, QueryList[i].FirstOrDefault().Analyte.Id); //analyte id
                Assert.AreEqual(analyteList[i].Name, QueryList[i].FirstOrDefault().Analyte.Name); //analyte name

                Assert.AreEqual(siteList[k].Id, QueryList[i].FirstOrDefault().Site.Id);
                Assert.AreEqual(siteList[k].Name, QueryList[i].FirstOrDefault().Site.Name);
                Assert.AreEqual(siteList[k].Latitude, QueryList[i].FirstOrDefault().Site.Latitude);
                Assert.AreEqual(siteList[k].Longitude, QueryList[i].FirstOrDefault().Site.Longitude);
                    k++;
                    if ((i + 1) % 4 == 0)
                    {
                        k = 0;
                    }

            }
 

        }

        [Test]
        public void QueryWaterQualityDataListTest()
        {
            DateTime beginDate1 = new DateTime(2016, 6, 01, 10, 34, 9);

            DateTime endDate1 = new DateTime(2016, 7, 18, 18, 34, 9);

            DateTime resultDate = new DateTime(2016, 6, 15, 10, 34, 9);

            List<Hatfield.EnviroData.DataProfile.WQ.Models.Analyte> analyteList = new List<Hatfield.EnviroData.DataProfile.WQ.Models.Analyte>();

            String[] arrayNames = { "Toluene", "Ethylbenzene", "Xylenes", "Naphthenic Acid", "Toluene-d8 (BTEX)", "Benzene", "o-Terphenyl (F2-F4)", "C>10 - C16", "Phenolphthalein Alkalinity", "Alkalinity (Total as CaCO3)", "Alkalinity (Carbonate as CaCO3)", "True Colour" };

            for (int i = 0; i < 12; i++)
            {
                Analyte analyte = new Analyte
                {
                    Id = (14 + i),
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

            Assert.AreEqual(12, waterQualityProfile.Count());

            for (int i = 0; i < arrayWaterQualityProfile.Length; i++)
            {
                Assert.AreEqual((i + 1), arrayWaterQualityProfile[i].Value);
                Assert.AreEqual(resultDate, arrayWaterQualityProfile[i].DateTime);
                Assert.AreEqual(analyteList[i].Id, arrayWaterQualityProfile[i].Analyte.Id);
                Assert.AreEqual(analyteList[i].Name, arrayWaterQualityProfile[i].Analyte.Name);

                Assert.AreEqual(siteList[i].Id, arrayWaterQualityProfile[i].Site.Id);
                Assert.AreEqual(siteList[i].Name, arrayWaterQualityProfile[i].Site.Name);
                Assert.AreEqual(siteList[i].Latitude, arrayWaterQualityProfile[i].Site.Latitude);
                Assert.AreEqual(siteList[i].Longitude, arrayWaterQualityProfile[i].Site.Longitude);
                Assert.AreEqual(7, arrayWaterQualityProfile[i].UTCOffset);
            }

        }

        [Test]
        public void FieldWorkProductionTest()
        {
            var fieldWorkProduction = _wqDataProfile.GetAllFieldWorkProductions();

            Assert.NotNull(fieldWorkProduction);


        }

    }
}
