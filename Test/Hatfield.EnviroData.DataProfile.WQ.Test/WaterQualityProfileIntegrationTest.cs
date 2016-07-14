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
        private ODM2Entities _dbContext;
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

            List<Hatfield.EnviroData.DataProfile.WQ.Models.Site> siteList = new List<Hatfield.EnviroData.DataProfile.WQ.Models.Site>
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
                           new Hatfield.EnviroData.DataProfile.WQ.Models.Site{
                                Id = 4,
                                Name = "S01",
                                Latitude = 49.2827,
                                Longitude = 123.1207
                           }
                            
             };

            for (int i = 0; i < arraySites.Length; i++)
            {
                Assert.AreEqual(siteList[i].Name, arraySites[i].Name);
                Assert.AreEqual(siteList[i].Longitude, arraySites[i].Longitude);
                Assert.AreEqual(siteList[i].Latitude, arraySites[i].Latitude);
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

            List<Hatfield.EnviroData.DataProfile.WQ.Models.Analyte> analyteList = new List<Hatfield.EnviroData.DataProfile.WQ.Models.Analyte>
            {
                            new Hatfield.EnviroData.DataProfile.WQ.Models.Analyte
                            {
                                Id = 5,
                                Name = "Analyte1"
                            },

                            new Hatfield.EnviroData.DataProfile.WQ.Models.Analyte
                            {
                                Id = 6,
                                Name = "Analyte2"
                            },
                            new Hatfield.EnviroData.DataProfile.WQ.Models.Analyte
                            {
                                Id = 7,
                                Name = "Analyte3"
                            },
                            new Hatfield.EnviroData.DataProfile.WQ.Models.Analyte
                            {
                                Id = 8,
                                Name = "Analyte4"
                            }
                
             };
       

                for( var i=0; i< arrayAnalytes.Length; i++)
                {
                    Assert.AreEqual(analyteList[i].Name, arrayAnalytes[i].Name);
                }



        }

        [Test]
        public void SamplingActivityQueryTest()
        {
  

            List<DateTime> BeginDates = new List<DateTime>
            {    
                new DateTime(2016, 6, 18, 10, 34, 9),
                new DateTime(2016, 6, 18, 10, 36, 9),
                new DateTime(2016, 6, 18, 10, 38, 9),
                new DateTime(2016, 6, 18, 10, 40, 9)
            };

            List<DateTime> EndDates = new List<DateTime>
            {
                new DateTime(2016, 6, 18, 18, 34, 9),
                new DateTime(2016, 6, 18, 18, 36, 9),
                new DateTime(2016, 6, 18, 18, 38, 9),
                new DateTime(2016, 6, 18, 18, 40, 9)
            };


            List<Hatfield.EnviroData.DataProfile.WQ.Models.Site> siteList = new List<Hatfield.EnviroData.DataProfile.WQ.Models.Site>
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
                           new Hatfield.EnviroData.DataProfile.WQ.Models.Site{
                                Id = 4,
                                Name = "S01",
                                Latitude = 49.2827,
                                Longitude = 123.1207
                           }
                            
             };

            var samplingActivities = _wqDataProfile.GetAllSamplingActivities();
            var arraySamplingActivities = samplingActivities.ToArray();
            Assert.NotNull(samplingActivities);
            Assert.AreEqual(4, samplingActivities.Count());

            for(var i =0; i< arraySamplingActivities.Length; i++)
            {
                
                      Assert.AreEqual(BeginDates[i], arraySamplingActivities[i].StartDateTime);
                      Assert.AreEqual(7, arraySamplingActivities[i].StartDateTimeUTCOffset);
                      Assert.AreEqual(EndDates[i], arraySamplingActivities[i].EndDateTime);
                      Assert.AreEqual(7, arraySamplingActivities[i].EndDateTimeUTCOffset);
                  
                      Assert.AreEqual(siteList[i].Name, arraySamplingActivities[i].SamplingSites.Name);
                      Assert.AreEqual(siteList[i].Longitude, arraySamplingActivities[i].SamplingSites.Longitude);
                      Assert.AreEqual(siteList[i].Latitude, arraySamplingActivities[i].SamplingSites.Latitude);
                  
            }

       
        }

        [Test] 
        public void QuerySamplingActivities1Test()
        {

            List<DateTime> BeginDates = new List<DateTime>
            {
                  new DateTime(2016, 6, 18, 10, 34, 9),
                 new DateTime(2016, 6, 18, 10, 36, 9),
                new DateTime(2016, 6, 18, 10, 38, 9),
                new DateTime(2016, 6, 18, 10, 40, 9)
            };

            List<DateTime> EndDates = new List<DateTime>
            {
                new DateTime(2016, 6, 18, 18, 34, 9),
                new DateTime(2016, 6, 18, 18, 36, 9),
                new DateTime(2016, 6, 18, 18, 38, 9),
                new DateTime(2016, 6, 18, 18, 40, 9)
            };


            List<IQueryable<SamplingActivity>> QueryList = new List<IQueryable<SamplingActivity>>();


            for(int i=0; i<BeginDates.Count(); i++)
            {
                var samplingActivities = _wqDataProfile.QuerySamplingActivities(BeginDates[i], EndDates[i]);
                Assert.NotNull(samplingActivities);
                QueryList.Add(samplingActivities);
            }

            Assert.AreEqual(4, QueryList.Count());

            for (int i = 0; i < QueryList.Count();i++ )
            {
                Assert.AreEqual(1, QueryList[i].Count()); //test for the number of values returned after the query result
                Assert.AreEqual(BeginDates[i], QueryList[i].FirstOrDefault().StartDateTime);
                Assert.AreEqual(EndDates[i], QueryList[i].FirstOrDefault().EndDateTime);
            }
     
		}

        [Test]
        public void QuerySamplingActivities2Test()
        {

            List<DateTime> BeginDates = new List<DateTime>
            {    
                new DateTime(2016, 6, 18, 10, 34, 9),
                new DateTime(2016, 6, 18, 10, 36, 9),
                new DateTime(2016, 6, 18, 10, 38, 9),
                new DateTime(2016, 6, 18, 10, 40, 9)
            };

            List<DateTime> EndDates = new List<DateTime>
            {
                new DateTime(2016, 6, 18, 18, 34, 9),
                new DateTime(2016, 6, 18, 18, 36, 9),
                new DateTime(2016, 6, 18, 18, 38, 9),
                new DateTime(2016, 6, 18, 18, 40, 9)
            };


            List<Hatfield.EnviroData.DataProfile.WQ.Models.Site> siteList = new List<Hatfield.EnviroData.DataProfile.WQ.Models.Site>
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
                           new Hatfield.EnviroData.DataProfile.WQ.Models.Site{
                                Id = 4,
                                Name = "S01",
                                Latitude = 49.2827,
                                Longitude = 123.1207
                           }
                            
             };

            List<IQueryable<SamplingActivity>> QueryList = new List<IQueryable<SamplingActivity>>();


           for(int i=0;i<4;i++)
           {
               var samplingActivities = _wqDataProfile.QuerySamplingActivities(BeginDates[i], EndDates[i], siteList[i]);
               Assert.NotNull(samplingActivities);
               QueryList.Add(samplingActivities);
           }
      
 //           Assert.AreEqual(querySite, arraySamplingActivities[0].SamplingSites); won't work because of the different pointer levels

           for (int i = 0; i < QueryList.Count();i++ )
           {
               Assert.AreEqual(siteList[i].Id, QueryList[i].FirstOrDefault().SamplingSites.Id);
               Assert.AreEqual(siteList[i].Name, QueryList[i].FirstOrDefault().SamplingSites.Name);
               Assert.AreEqual(siteList[i].Latitude, QueryList[i].FirstOrDefault().SamplingSites.Latitude);
               Assert.AreEqual(siteList[i].Longitude, QueryList[i].FirstOrDefault().SamplingSites.Longitude);

           }

        }


        [Test]
        public void QueryWaterQualityDataTestDate()
        {
            DateTime beginDate1 = new DateTime(2016, 6, 18, 10, 34, 9);

            DateTime endDate1 = new DateTime(2016, 6, 18, 18, 34, 9);

            List<DateTime> ResultDates = new List<DateTime>
            {
                new DateTime(2016, 6, 18, 11, 34, 9),
                new DateTime(2016, 6, 18, 12, 34, 9),
                new DateTime(2016, 6, 18, 13, 34, 9),
                new DateTime(2016, 6, 18, 14, 34, 9)
            };


            List<IQueryable<WaterQualityObservation>> QueryList = new List<IQueryable<WaterQualityObservation>>();

            var waterQualityProfile = _wqDataProfile.QueryWaterQualityData(beginDate1, endDate1);
                Assert.NotNull(waterQualityProfile);
                QueryList.Add(waterQualityProfile);
            
            
            Assert.NotNull(waterQualityProfile);
            Assert.AreEqual(4, QueryList.FirstOrDefault().Count()); //test for the number of values returned after the query

            for (int i = 0; i < QueryList.Count(); i++)
            {

                Assert.AreEqual((i + 1), QueryList[i].FirstOrDefault().Value);  //values
                Assert.AreEqual(ResultDates[i], QueryList[i].FirstOrDefault().DateTime); //date time
                Assert.AreEqual(7, QueryList[i].FirstOrDefault().UTCOffset); //offset
            }


        }


        [Test]
        public void QueryWaterQualityDataTestSite()
        {

            List<DateTime> BeginDates = new List<DateTime>
            {    
                new DateTime(2016, 6, 18, 10, 34, 9),
                new DateTime(2016, 6, 18, 10, 36, 9),
                new DateTime(2016, 6, 18, 10, 38, 9),
                new DateTime(2016, 6, 18, 10, 40, 9)
            };

            List<DateTime> EndDates = new List<DateTime>
            {
                new DateTime(2016, 6, 18, 18, 34, 9),
                new DateTime(2016, 6, 18, 18, 36, 9),
                new DateTime(2016, 6, 18, 18, 38, 9),
                new DateTime(2016, 6, 18, 18, 40, 9)
            };

            List<DateTime> ResultDates = new List<DateTime>
            {
                new DateTime(2016, 6, 18, 11, 34, 9),
                new DateTime(2016, 6, 18, 12, 34, 9),
                new DateTime(2016, 6, 18, 13, 34, 9),
                new DateTime(2016, 6, 18, 14, 34, 9)
            };

            List<Hatfield.EnviroData.DataProfile.WQ.Models.Site> siteList = new List<Hatfield.EnviroData.DataProfile.WQ.Models.Site>
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
                           new Hatfield.EnviroData.DataProfile.WQ.Models.Site{
                                Id = 4,
                                Name = "S01",
                                Latitude = 49.2827,
                                Longitude = 123.1207
                           }
                            
             };


            List<IQueryable<WaterQualityObservation>> QueryList = new List<IQueryable<WaterQualityObservation>>();

            for(int i=0; i<4;i++)
            {
                var waterQualityProfile = _wqDataProfile.QueryWaterQualityData(BeginDates[i], EndDates[i], siteList[i]);
                Assert.NotNull(waterQualityProfile);
                QueryList.Add(waterQualityProfile);
            }


            Assert.AreEqual(4, QueryList.Count()); //test for the number of values returned after the query

            for (int i = 0; i < QueryList.Count(); i++)
            {

                Assert.AreEqual((i + 1), QueryList[i].FirstOrDefault().Value);  //values
                Assert.AreEqual(ResultDates[i], QueryList[i].FirstOrDefault().DateTime); //date time
                Assert.AreEqual(7, QueryList[i].FirstOrDefault().UTCOffset); //offset

                Assert.AreEqual(siteList[i].Id, QueryList[i].FirstOrDefault().Site.Id);
                Assert.AreEqual(siteList[i].Name, QueryList[i].FirstOrDefault().Site.Name);
                Assert.AreEqual(siteList[i].Latitude, QueryList[i].FirstOrDefault().Site.Latitude);
                Assert.AreEqual(siteList[i].Longitude, QueryList[i].FirstOrDefault().Site.Longitude);
            }
   

        }


        [Test]
        public void QueryWaterQualityDataTestAnalyte()
        {
            DateTime beginDate1 = new DateTime(2016, 6, 18, 10, 34, 9);
            DateTime endDate1 = new DateTime(2016, 6, 18, 18, 34, 9);

            List<DateTime> ResultDates = new List<DateTime>
            {
                new DateTime(2016, 6, 18, 11, 34, 9),
                new DateTime(2016, 6, 18, 12, 34, 9),
                new DateTime(2016, 6, 18, 13, 34, 9),
                new DateTime(2016, 6, 18, 14, 34, 9)
            };

            List<Hatfield.EnviroData.DataProfile.WQ.Models.Analyte> analyteList = new List<Hatfield.EnviroData.DataProfile.WQ.Models.Analyte>
            {
                            new Hatfield.EnviroData.DataProfile.WQ.Models.Analyte
                            {
                                Id = 5,
                                Name = "Analyte1"
                            },

                            new Hatfield.EnviroData.DataProfile.WQ.Models.Analyte
                            {
                                Id = 6,
                                Name = "Analyte2"
                            },
                            new Hatfield.EnviroData.DataProfile.WQ.Models.Analyte
                            {
                                Id = 7,
                                Name = "Analyte3"
                            },
                            new Hatfield.EnviroData.DataProfile.WQ.Models.Analyte
                            {
                                Id = 8,
                                Name = "Analyte4"
                            }
                
             };

       
            List<IQueryable<WaterQualityObservation>> QueryList = new List<IQueryable<WaterQualityObservation>>();

            for(int i=0;i<analyteList.Count();i++ )
            {
                var waterQualityProfile = _wqDataProfile.QueryWaterQualityData(beginDate1, endDate1, analyteList[i]);
                Assert.NotNull(waterQualityProfile);
                QueryList.Add(waterQualityProfile);
            }


            for (int i = 0; i < QueryList.Count();i++ )
            {
                Assert.AreEqual(1, QueryList[i].Count()); //test for the number of values returned after the query
                Assert.AreEqual((i + 1), QueryList[i].FirstOrDefault().Value);  //values
                Assert.AreEqual(ResultDates[i], QueryList[i].FirstOrDefault().DateTime); //date time
                Assert.AreEqual(7, QueryList[i].FirstOrDefault().UTCOffset); //offset

                Assert.AreEqual(analyteList[i].Id, QueryList[i].FirstOrDefault().Analyte.Id); //analyte id
                Assert.AreEqual(analyteList[i].Name, QueryList[i].FirstOrDefault().Analyte.Name); //analyte name
            }

        }

        [Test]
        public void QueryWaterQualityDataTestAnalyteAndSite()
        {
            DateTime beginDate1 = new DateTime(2016, 6, 18, 10, 34, 9);
            DateTime endDate1 = new DateTime(2016, 6, 18, 18, 34, 9);

       
            List<Hatfield.EnviroData.DataProfile.WQ.Models.Site> siteList = new List<Hatfield.EnviroData.DataProfile.WQ.Models.Site>
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
                           new Hatfield.EnviroData.DataProfile.WQ.Models.Site{
                                Id = 4,
                                Name = "S01",
                                Latitude = 49.2827,
                                Longitude = 123.1207
                           }
                            
             };

            List<DateTime> ResultDates = new List<DateTime>
            {
                new DateTime(2016, 6, 18, 11, 34, 9),
                new DateTime(2016, 6, 18, 12, 34, 9),
                new DateTime(2016, 6, 18, 13, 34, 9),
                new DateTime(2016, 6, 18, 14, 34, 9)
            };

            List<Hatfield.EnviroData.DataProfile.WQ.Models.Analyte> analyteList = new List<Hatfield.EnviroData.DataProfile.WQ.Models.Analyte>
            {
                            new Hatfield.EnviroData.DataProfile.WQ.Models.Analyte
                            {
                                Id = 5,
                                Name = "Analyte1"
                            },

                            new Hatfield.EnviroData.DataProfile.WQ.Models.Analyte
                            {
                                Id = 6,
                                Name = "Analyte2"
                            },
                            new Hatfield.EnviroData.DataProfile.WQ.Models.Analyte
                            {
                                Id = 7,
                                Name = "Analyte3"
                            },
                            new Hatfield.EnviroData.DataProfile.WQ.Models.Analyte
                            {
                                Id = 8,
                                Name = "Analyte4"
                            }

             };


            List<IQueryable<WaterQualityObservation>> QueryList = new List<IQueryable<WaterQualityObservation>>();


            for(int i= 0 ;i<siteList.Count();i++)
            {
                 var waterQualityProfile = _wqDataProfile.QueryWaterQualityData(beginDate1, endDate1, siteList[i], analyteList[i]);
                 Assert.NotNull(waterQualityProfile);
                 QueryList.Add(waterQualityProfile);
            }

            Assert.AreEqual(4, QueryList.Count()); //test for the number of values returned after the query


            for (int i = 0; i < QueryList.Count(); i++)
            {
                Assert.AreEqual(1, QueryList[i].Count()); //test for the number of values returned after the query
                Assert.AreEqual((i + 1), QueryList[i].FirstOrDefault().Value);  //values
                Assert.AreEqual(ResultDates[i], QueryList[i].FirstOrDefault().DateTime); //date time
                Assert.AreEqual(7, QueryList[i].FirstOrDefault().UTCOffset); //offset

                Assert.AreEqual(analyteList[i].Id, QueryList[i].FirstOrDefault().Analyte.Id); //analyte id
                Assert.AreEqual(analyteList[i].Name, QueryList[i].FirstOrDefault().Analyte.Name); //analyte name

                Assert.AreEqual(siteList[i].Id, QueryList[i].FirstOrDefault().Site.Id);
                Assert.AreEqual(siteList[i].Name, QueryList[i].FirstOrDefault().Site.Name);
                Assert.AreEqual(siteList[i].Latitude, QueryList[i].FirstOrDefault().Site.Latitude);
                Assert.AreEqual(siteList[i].Longitude, QueryList[i].FirstOrDefault().Site.Longitude);

            }
 

        }

        [Test]
        public void QueryWaterQualityDataListTest()
        {
            DateTime beginDate1 = new DateTime(2016, 6, 18, 10, 34, 9);
            DateTime endDate1 = new DateTime(2016, 6, 18, 15, 34, 9);

            List<DateTime> ResultDates = new List<DateTime>
            {
                new DateTime(2016, 6, 18, 11, 34, 9),
                new DateTime(2016, 6, 18, 12, 34, 9),
                new DateTime(2016, 6, 18, 13, 34, 9),
                new DateTime(2016, 6, 18, 14, 34, 9)
            };

            List<Hatfield.EnviroData.DataProfile.WQ.Models.Site> siteList = new List<Hatfield.EnviroData.DataProfile.WQ.Models.Site>
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
                           new Hatfield.EnviroData.DataProfile.WQ.Models.Site{
                                Id = 4,
                                Name = "S01",
                                Latitude = 49.2827,
                                Longitude = 123.1207
                           }
                            
             };
         
            List<Hatfield.EnviroData.DataProfile.WQ.Models.Analyte> analyteList = new List<Hatfield.EnviroData.DataProfile.WQ.Models.Analyte>
            {
                            new Hatfield.EnviroData.DataProfile.WQ.Models.Analyte
                            {
                                Id = 5,
                                Name = "Analyte1"
                            },

                            new Hatfield.EnviroData.DataProfile.WQ.Models.Analyte
                            {
                                Id = 6,
                                Name = "Analyte2"
                            },
                            new Hatfield.EnviroData.DataProfile.WQ.Models.Analyte
                            {
                                Id = 7,
                                Name = "Analyte3"
                            },
                            new Hatfield.EnviroData.DataProfile.WQ.Models.Analyte
                            {
                                Id = 8,
                                Name = "Analyte4"
                            }
                
             };

            var waterQualityProfile = _wqDataProfile.QueryWaterQualityData(beginDate1, endDate1, siteList, analyteList);
            var arrayWaterQualityProfile = waterQualityProfile.ToArray();

            Assert.NotNull(waterQualityProfile);
            Assert.AreEqual(4, waterQualityProfile.Count());

            for (int i = 0; i < arrayWaterQualityProfile.Length;i++ )
            {
                Assert.AreEqual((i+1), arrayWaterQualityProfile[i].Value);
                Assert.AreEqual(ResultDates[i], arrayWaterQualityProfile[i].DateTime);
                Assert.AreEqual(analyteList[i].Id, arrayWaterQualityProfile[i].Analyte.Id);
                Assert.AreEqual(analyteList[i].Name, arrayWaterQualityProfile[i].Analyte.Name);

                Assert.AreEqual(siteList[i].Id, arrayWaterQualityProfile[i].Site.Id);
                Assert.AreEqual(siteList[i].Name, arrayWaterQualityProfile[i].Site.Name);
                Assert.AreEqual(siteList[i].Latitude, arrayWaterQualityProfile[i].Site.Latitude);
                Assert.AreEqual(siteList[i].Longitude, arrayWaterQualityProfile[i].Site.Longitude);    
                Assert.AreEqual(7, arrayWaterQualityProfile[i].UTCOffset);
            }

        }

    }
}
