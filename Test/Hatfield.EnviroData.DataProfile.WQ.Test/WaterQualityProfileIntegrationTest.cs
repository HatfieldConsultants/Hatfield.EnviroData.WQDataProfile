﻿using System;
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
<<<<<<< HEAD
        private ODM2Entities _dbContext;
=======
        private DbContext _dbContext;
>>>>>>> upstream/master
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

<<<<<<< HEAD
        [Test]
=======
          [Test]
>>>>>>> upstream/master
        public void AnalyteQueryTest()
        {
            var analytes = _wqDataProfile.GetAllAnalytes();
            var arrayAnalytes = analytes.ToArray();

<<<<<<< HEAD
            string[] Names = { "Analyte1", "Analyte2", "Analyte3", "Analyte4" };

                for( var i=0; i< arrayAnalytes.Length; i++)
                {
                    Assert.AreEqual(Names[i], arrayAnalytes[i].Name);
                }
=======


>>>>>>> upstream/master

            Assert.NotNull(analytes);
            Assert.AreEqual(4, analytes.Count());

<<<<<<< HEAD
        }

        [Test]
        public void SamplingActivityQueryTest()
        {
            var samplingActivities = _wqDataProfile.GetAllSamplingActivities();
            var arraySamplingActivities = samplingActivities.ToArray();
          

            DateTime beginDate1 = new DateTime(2016, 6, 18, 10, 34, 9);
            DateTime beginDate2 = new DateTime(2016, 6, 18, 10, 36, 9);
            DateTime beginDate3 = new DateTime(2016, 6, 18, 10, 38, 9);
            DateTime beginDate4 = new DateTime(2016, 6, 18, 10, 40, 9);

            DateTime[] arrayBeginDates = { beginDate1, beginDate2, beginDate3, beginDate4 };

            DateTime endDate1 = new DateTime(2016, 6, 18, 18, 34, 9);
            DateTime endDate2 = new DateTime(2016, 6, 18, 18, 36, 9);
            DateTime endDate3 = new DateTime(2016, 6, 18, 18, 38, 9);
            DateTime endDate4 = new DateTime(2016, 6, 18, 18, 40, 9);

            DateTime[] arrayEndDates = { endDate1, endDate2, endDate3, endDate4 };


            long offsetDate = 7;

            double[] Longitudes = { 123.1207, 123.1207, 111.1111, 123.1207 };

            string[] Names = { "C1", "C2", null, "S01" };

            for(var i =0; i< arraySamplingActivities.Length; i++)
            {
                
                  Assert.AreEqual(arrayBeginDates[i], arraySamplingActivities[i].StartDateTime);
                  Assert.AreEqual(offsetDate, arraySamplingActivities[i].StartDateTimeUTCOffset);
                  Assert.AreEqual(arrayEndDates[i], arraySamplingActivities[i].EndDateTime);
                  Assert.AreEqual(offsetDate, arraySamplingActivities[i].EndDateTimeUTCOffset);
         
                  
                      Assert.AreEqual(Names[i], arraySamplingActivities[i].SamplingSites.Name);
                      Assert.AreEqual(Longitudes[i], arraySamplingActivities[i].SamplingSites.Longitude);
                      Assert.AreEqual(49.2827, arraySamplingActivities[i].SamplingSites.Latitude);
                  
            }

            Assert.NotNull(samplingActivities);
            Assert.AreEqual(4, samplingActivities.Count());
        }

        [Test] 
        public void QuerySamplingActivities1Test()
        {
            DateTime beginDate1 = new DateTime(2016, 6, 18, 10, 34, 9);
            DateTime beginDate2 = new DateTime(2016, 6, 18, 10, 36, 9);
            DateTime beginDate3 = new DateTime(2016, 6, 18, 10, 38, 9);
            DateTime beginDate4 = new DateTime(2016, 6, 18, 10, 40, 9);

            DateTime[] arrayBeginDates = { beginDate1, beginDate2, beginDate3, beginDate4 };

            DateTime endDate1 = new DateTime(2016, 6, 18, 18, 34, 9);
            DateTime endDate2 = new DateTime(2016, 6, 18, 18, 36, 9);
            DateTime endDate3 = new DateTime(2016, 6, 18, 18, 38, 9);
            DateTime endDate4 = new DateTime(2016, 6, 18, 18, 40, 9);

            DateTime[] arrayEndDates = { endDate1, endDate2, endDate3, endDate4 };

            var samplingActivities = _wqDataProfile.QuerySamplingActivities(beginDate1, endDate1);
            var samplingActivities2 = _wqDataProfile.QuerySamplingActivities(beginDate2, endDate2);
            var samplingActivities3 = _wqDataProfile.QuerySamplingActivities(beginDate3, endDate3);
            var samplingActivities4 = _wqDataProfile.QuerySamplingActivities(beginDate4, endDate4);


           var arraySamplingActivities = samplingActivities.ToArray();
            var arraySamplingActivities2 = samplingActivities2.ToArray();
              var arraySamplingActivitie3 = samplingActivities3.ToArray();
              var arraySamplingActivities4 = samplingActivities4.ToArray();


                Assert.AreEqual(arrayBeginDates[0], arraySamplingActivities[0].StartDateTime);
                Assert.AreEqual(arrayEndDates[0], arraySamplingActivities[0].EndDateTime);


            Assert.NotNull(samplingActivities);
            Assert.AreEqual(1, samplingActivities.Count()); //test for the number of values returned after the query

    

              
=======

>>>>>>> upstream/master

        }

        [Test]
<<<<<<< HEAD
        public void QuerySamplingActivities2Test()
        {
            DateTime beginDate1 = new DateTime(2016, 6, 18, 10, 34, 9);
            DateTime beginDate2 = new DateTime(2016, 6, 18, 10, 36, 9);
            DateTime beginDate3 = new DateTime(2016, 6, 18, 10, 38, 9);
            DateTime beginDate4 = new DateTime(2016, 6, 18, 10, 40, 9);

            DateTime[] arrayBeginDates = { beginDate1, beginDate2, beginDate3, beginDate4 };

            DateTime endDate1 = new DateTime(2016, 6, 18, 18, 34, 9);
            DateTime endDate2 = new DateTime(2016, 6, 18, 18, 36, 9);
            DateTime endDate3 = new DateTime(2016, 6, 18, 18, 38, 9);
            DateTime endDate4 = new DateTime(2016, 6, 18, 18, 40, 9);

           var querySite = new Hatfield.EnviroData.DataProfile.WQ.Models.Site
                                             {
                                                 Id = 1,
                                                 Name = "C1",
                                                 Latitude = 123.1207,
                                                 Longitude = 49.2827
                                             };


           var querySite2 = new Hatfield.EnviroData.DataProfile.WQ.Models.Site
                                   {
                                                    Id = 2,
                                                Name = "C2",
                                       Latitude = 123.1207,
                                       Longitude = 49.2827
                                   };

           List<Hatfield.EnviroData.DataProfile.WQ.Models.Site> querySite1 = new List<Models.Site>();

           querySite1.Add(querySite);

            DateTime[] arrayEndDates = { endDate1, endDate2, endDate3, endDate4 };

            var samplingActivities = _wqDataProfile.QuerySamplingActivities(beginDate1, endDate1, querySite1);
            var arraySamplingActivities = samplingActivities.ToArray();

            Assert.AreEqual(querySite, arraySamplingActivities[0].SamplingSites);

            Assert.NotNull(samplingActivities);
            Assert.AreEqual(1, samplingActivities.Count()); //test for the number of values returned after the query

        }
=======
        public void SamplingActivityQueryTest()
          {
              var samplingActivities = _wqDataProfile.GetAllSamplingActivities();
              var arraySamplingActivities = samplingActivities.ToArray();

              Assert.NotNull(samplingActivities);
          }
>>>>>>> upstream/master
    }
}
