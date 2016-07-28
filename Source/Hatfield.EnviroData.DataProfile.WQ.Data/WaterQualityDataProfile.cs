using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

using Hatfield.EnviroData.DataProfile.WQ.Models;


namespace Hatfield.EnviroData.DataProfile.WQ
{
    public class WaterQualityDataProfile : IWaterQualityDataProfile, IDisposable
    {
        private Hatfield.EnviroData.Core.ODM2Entities _dbContext;
        public WaterQualityDataProfile(Hatfield.EnviroData.Core.ODM2Entities dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Get all the sites in the databases
        /// </summary>
        /// <returns></returns>
        public IQueryable<Site> GetAllSites()
        {
            var siteModels = from site in _dbContext.SamplingFeatures
                             where (site.SamplingFeatureTypeCV == "Site")
                             select new Site
                             {
                                 Id = site.SamplingFeatureID,
                                 Name = site.SamplingFeatureName,
                                 Latitude = site.Site.Latitude,
                                 Longitude = site.Site.Longitude
                             };

            return siteModels;
        }


        /// <summary>
        /// Save or Update site data
        /// </summary>
        /// <param name="site">site that need to update or insert</param>
        /// <returns></returns>
        public Site SaveOrUpdateSite(Site site) 
        {
            var domainBuildResult = DomainBuilderFactory.Create(site.GetType()).Build(site, _dbContext);
            _dbContext.Entry(domainBuildResult.Data).State = domainBuildResult.State;
            _dbContext.SaveChanges();

            var castedDomain = (Hatfield.EnviroData.Core.Site)domainBuildResult.Data;
            site.Id = castedDomain.SamplingFeatureID;

            return site;
        }

        /// <summary>
        /// Get all the analytes in the databases
        /// </summary>
        /// <returns></returns>
        public IQueryable<Analyte> GetAllAnalytes()
        {
            var analyteModels = from analyte in _dbContext.Variables
                                orderby analyte.VariableID ascending
                                select new Analyte
                                {
                                    Id = analyte.VariableID,
                                    Name = analyte.VariableDefinition,
                                    Category = new AnalyteCategory
                                    {
                                        Id = analyte.VariableID,
                                        Name = analyte.VariableTypeCV,
                                        StandardUnit = new Unit
                                        {
                                            Id = (int?)analyte.Results.FirstOrDefault().UnitsID ?? 0,
                                            Name = analyte.Results.FirstOrDefault().Unit.UnitsName,
                                            Description = analyte.Results.FirstOrDefault().Unit.UnitsTypeCV
                                        }
                                    }
                                    
                                };

            return analyteModels;
        }

        /// <summary>
        /// Get all sampling activities in the databases
        /// </summary>
        /// <returns></returns>
        public IQueryable<SamplingActivity> GetAllSamplingActivities()
        {
            var samplingActivityModels = from samplingActivity in _dbContext.Actions
                                      
                                         where (samplingActivity.ActionTypeCV == "Specimen collection"
                                         && samplingActivity.RelatedActions1.FirstOrDefault().RelationshipTypeCV == "Is child of")
                                         orderby samplingActivity.BeginDateTime ascending
                                         select new SamplingActivity
                                         {
                                             Id = samplingActivity.ActionID,
                                             StartDateTime = samplingActivity.BeginDateTime,
                                             StartDateTimeUTCOffset = samplingActivity.BeginDateTimeUTCOffset,
                                             EndDateTime = samplingActivity.EndDateTime,
                                             EndDateTimeUTCOffset = (long?)samplingActivity.EndDateTimeUTCOffset ?? 0,
                                             Site = samplingActivity.FeatureActions.Select(x => new Site
                                             {
                                                 Id = x.SamplingFeature.SamplingFeatureID,
                                                 Name = x.SamplingFeature.SamplingFeatureName,
                                                 Latitude = x.SamplingFeature.Site.Latitude,
                                                 Longitude = x.SamplingFeature.Site.Longitude
                                             }).FirstOrDefault()
                                         };
            return samplingActivityModels;
        }

        /// <summary>
        /// Get all field work productions in the databases
        /// each field work production contains a sampling activity and the related water quality samples
        /// </summary>
        /// <returns></returns>
        public IQueryable<FieldWorkProduction> GetAllFieldWorkProductions()
        {
 

            throw new NotImplementedException();
        }

        /// <summary>
        /// Query sampling activities
        /// </summary>
        /// <param name="startDateTime">sampling activity start date</param>
        /// <param name="endDateTime">sampling activity end date</param>
        /// <returns></returns>
        public IQueryable<SamplingActivity> QuerySamplingActivities(DateTime startDateTime, DateTime endDateTime)
        {
            var samplingActivityModels = from samplingActivity in _dbContext.Actions
                                         where (samplingActivity.BeginDateTime >= startDateTime
                                         && samplingActivity.BeginDateTime <= endDateTime       //need to account for nullable cases
                                         && samplingActivity.RelatedActions1.FirstOrDefault().RelationshipTypeCV == "Is child of"
                                         && samplingActivity.ActionTypeCV == "Specimen collection")
                                         orderby samplingActivity.BeginDateTime ascending
                                         select new SamplingActivity
                                         {
                                             Id = samplingActivity.ActionID,
                                             StartDateTime = samplingActivity.BeginDateTime,
                                             StartDateTimeUTCOffset = samplingActivity.BeginDateTimeUTCOffset,
                                           
                                             EndDateTimeUTCOffset = (long?)samplingActivity.EndDateTimeUTCOffset ?? 0,
                                             Site = samplingActivity.FeatureActions.Select(x => new Site
                                             {      
                                                 Id = x.SamplingFeature.SamplingFeatureID,
                                                 Name = x.SamplingFeature.SamplingFeatureName,
                                                 Latitude = x.SamplingFeature.Site.Latitude,
                                                 Longitude = x.SamplingFeature.Site.Longitude
                                             }).FirstOrDefault(),
                                             EndDateTime = samplingActivity.EndDateTime
                                         };
            return samplingActivityModels;
        
        
        }

        /// <summary>
        /// Query sampling activities
        /// </summary>
        /// <param name="startDateTime">sampling activity start date</param>
        /// <param name="endDateTime">sampling activity end date</param>
        /// <param name="sites">sampling site</param>
        /// <returns></returns>
        public IQueryable<SamplingActivity> QuerySamplingActivities(DateTime startDateTime, DateTime endDateTime, Site sites)
        {

            var samplingActivityModels = from samplingActivity in _dbContext.Actions
                                         where (samplingActivity.BeginDateTime >= startDateTime && 
                                         samplingActivity.BeginDateTime <= endDateTime 
                                         && samplingActivity.ActionTypeCV == "Specimen collection"
                                         && samplingActivity.RelatedActions1.FirstOrDefault().RelationshipTypeCV == "Is child of"
                                         && samplingActivity.FeatureActions.FirstOrDefault().SamplingFeatureID == sites.Id)
                                         orderby samplingActivity.BeginDateTime ascending
                                         select new SamplingActivity
                                         {
                                             Id = samplingActivity.ActionID,
                                             StartDateTime = samplingActivity.BeginDateTime,
                                             StartDateTimeUTCOffset = samplingActivity.BeginDateTimeUTCOffset,
                                             EndDateTime = samplingActivity.EndDateTime,
                                             EndDateTimeUTCOffset = (long?)samplingActivity.EndDateTimeUTCOffset ?? 0,
                                             Site = samplingActivity.FeatureActions.Select(x => new Site
                                             {
                                                 Id = x.SamplingFeature.SamplingFeatureID,
                                                 Name = x.SamplingFeature.SamplingFeatureName,
                                                 Latitude = x.SamplingFeature.Site.Latitude,
                                                 Longitude = x.SamplingFeature.Site.Longitude
                                             }).FirstOrDefault()
                                         };
            return samplingActivityModels;
        }

        /// <summary>
        /// Save or Update sampling activities
        /// </summary>
        /// <param name="samples">sampling activities to save or update</param>
        /// <returns></returns>
        public bool SaveOrUpdateSamplingActivities(IEnumerable<SamplingActivity> samples)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Query water quality data
        /// </summary>
        /// <param name="startDateTime">query start date</param>
        /// <param name="endDateTime">query end date</param>
        /// <returns>water quality samples within the query time range</returns>
        public IQueryable<WaterQualityObservation> QueryWaterQualityData(DateTime startDateTime, DateTime endDateTime)
        {

            var waterQualityObervationModel = from observationModel in _dbContext.RelatedActions
                                              where (observationModel.RelationshipTypeCV == "Is related to"
                                              && observationModel.Action1.BeginDateTime >= startDateTime
                                              && observationModel.Action1.BeginDateTime <= endDateTime)
                                              orderby observationModel.Action1.BeginDateTime ascending
                                              select new WaterQualityObservation
                                              {
                                                  Id = observationModel.Action1.FeatureActions.FirstOrDefault().Results.FirstOrDefault().ResultID,
                                                  DateTime = observationModel.Action1.BeginDateTime,
                                                  UTCOffset = (long)observationModel.Action1.BeginDateTimeUTCOffset,
                                                  Value = (float?)observationModel.Action1.FeatureActions.FirstOrDefault().Results.FirstOrDefault().MeasurementResult.MeasurementResultValues.FirstOrDefault().DataValue ?? 0,
                                                  Unit = new Unit
                                                  {
                                                      Id = observationModel.Action1.FeatureActions.FirstOrDefault().Results.FirstOrDefault().UnitsID,
                                                      Name = observationModel.Action1.FeatureActions.FirstOrDefault().Results.FirstOrDefault().Unit.UnitsName,
                                                      Description = observationModel.Action1.FeatureActions.FirstOrDefault().Results.FirstOrDefault().Unit.UnitsTypeCV
                                                  },
                                                  Analyte = new Analyte
                                                  {
                                                      Id = observationModel.Action1.FeatureActions.FirstOrDefault().Results.FirstOrDefault().VariableID,
                                                      Name = observationModel.Action1.FeatureActions.FirstOrDefault().Results.FirstOrDefault().Variable.VariableDefinition,
                                                      Category = new AnalyteCategory
                                                      {
                                                          Id = observationModel.Action1.FeatureActions.FirstOrDefault().Results.FirstOrDefault().Variable.VariableID,
                                                          Name = observationModel.Action1.FeatureActions.FirstOrDefault().Results.FirstOrDefault().Variable.VariableTypeCV,
                                                          StandardUnit = new Unit
                                                          {
                                                              Id = observationModel.Action1.FeatureActions.FirstOrDefault().Results.FirstOrDefault().UnitsID,
                                                              Name = observationModel.Action1.FeatureActions.FirstOrDefault().Results.FirstOrDefault().Unit.UnitsName,
                                                              Description = observationModel.Action1.FeatureActions.FirstOrDefault().Results.FirstOrDefault().Unit.UnitsTypeCV
                                                          }
                                                      }
                                                  },
                                                  Site = new Site
                                                  {
                                                      Id = observationModel.Action1.FeatureActions.FirstOrDefault().SamplingFeatureID,
                                                      Name = observationModel.Action1.FeatureActions.FirstOrDefault().SamplingFeature.SamplingFeatureName,
                                                      Latitude = observationModel.Action1.FeatureActions.FirstOrDefault().SamplingFeature.Site.Latitude,
                                                      Longitude = observationModel.Action1.FeatureActions.FirstOrDefault().SamplingFeature.Site.Longitude
                                                  }

                                              };




            return waterQualityObervationModel;
        }

        /// <summary>
        /// Query water quality data
        /// </summary>
        /// <param name="startDateTime">query start date</param>
        /// <param name="endDateTime">query end date</param>
        /// <param name="site">query site</param>
        /// <returns></returns>
        public IQueryable<WaterQualityObservation> QueryWaterQualityData(DateTime startDateTime, DateTime endDateTime, Site site)
        {

            var waterQualityObervationModel = from observationModel in _dbContext.RelatedActions
                                              where (observationModel.RelationshipTypeCV == "Is related to"
                                              && observationModel.Action1.BeginDateTime >= startDateTime
                                              && observationModel.Action1.EndDateTime <= endDateTime
                                              && observationModel.Action1.FeatureActions.FirstOrDefault().SamplingFeatureID == site.Id)
                                              orderby observationModel.Action1.FeatureActions.FirstOrDefault().SamplingFeatureID ascending
                                              select new WaterQualityObservation
                                              {
                                                  Id = observationModel.Action1.FeatureActions.FirstOrDefault().Results.FirstOrDefault().ResultID,
                                                  DateTime = observationModel.Action1.BeginDateTime,
                                                  UTCOffset = (long)observationModel.Action1.BeginDateTimeUTCOffset,
                                                  Value = (float?)observationModel.Action1.FeatureActions.FirstOrDefault().Results.FirstOrDefault().MeasurementResult.MeasurementResultValues.FirstOrDefault().DataValue ?? 0,
                                                  Unit = new Unit
                                                  {
                                                      Id = observationModel.Action1.FeatureActions.FirstOrDefault().Results.FirstOrDefault().UnitsID,
                                                      Name = observationModel.Action1.FeatureActions.FirstOrDefault().Results.FirstOrDefault().Unit.UnitsName,
                                                      Description = observationModel.Action1.FeatureActions.FirstOrDefault().Results.FirstOrDefault().Unit.UnitsTypeCV
                                                  },
                                                  Analyte = new Analyte
                                                  {
                                                      Id = observationModel.Action1.FeatureActions.FirstOrDefault().Results.FirstOrDefault().VariableID,
                                                      Name = observationModel.Action1.FeatureActions.FirstOrDefault().Results.FirstOrDefault().Variable.VariableDefinition,
                                                      Category = new AnalyteCategory
                                                      {
                                                          Id = observationModel.Action1.FeatureActions.FirstOrDefault().Results.FirstOrDefault().Variable.VariableID,
                                                          Name = observationModel.Action1.FeatureActions.FirstOrDefault().Results.FirstOrDefault().Variable.VariableTypeCV,
                                                          StandardUnit = new Unit
                                                          {
                                                              Id = observationModel.Action1.FeatureActions.FirstOrDefault().Results.FirstOrDefault().UnitsID,
                                                              Name = observationModel.Action1.FeatureActions.FirstOrDefault().Results.FirstOrDefault().Unit.UnitsName,
                                                              Description = observationModel.Action1.FeatureActions.FirstOrDefault().Results.FirstOrDefault().Unit.UnitsTypeCV
                                                          }
                                                      }
                                                  },
                                                  Site = new Site
                                                  {
                                                      Id = observationModel.Action1.FeatureActions.FirstOrDefault().SamplingFeatureID,
                                                      Name = observationModel.Action1.FeatureActions.FirstOrDefault().SamplingFeature.SamplingFeatureName,
                                                      Latitude = observationModel.Action1.FeatureActions.FirstOrDefault().SamplingFeature.Site.Latitude,
                                                      Longitude = observationModel.Action1.FeatureActions.FirstOrDefault().SamplingFeature.Site.Longitude
                                                  }

                                              };

            return waterQualityObervationModel;

        }

        /// <summary>
        /// Query water quality data
        /// </summary>
        /// <param name="startDateTime">query start date</param>
        /// <param name="endDateTime">query end date</param>
        /// <param name="analyte">query analyte</param>
        /// <returns></returns>
        public IQueryable<WaterQualityObservation> QueryWaterQualityData(DateTime startDateTime, DateTime endDateTime, Analyte analyte)
        {
            var waterQualityObervationModel = from observationModel in _dbContext.RelatedActions
                                              where (observationModel.RelationshipTypeCV == "Is related to"
                                              && observationModel.Action1.BeginDateTime >= startDateTime
                                              && observationModel.Action1.EndDateTime <= endDateTime
                                              && observationModel.Action1.FeatureActions.FirstOrDefault().Results.FirstOrDefault().VariableID == analyte.Id)
                                              orderby observationModel.Action1.FeatureActions.FirstOrDefault().Results.FirstOrDefault().VariableID ascending 
                                              select new WaterQualityObservation
                                              {
                                                  Id = observationModel.Action1.FeatureActions.FirstOrDefault().Results.FirstOrDefault().ResultID,
                                                  DateTime = observationModel.Action1.BeginDateTime,
                                                  UTCOffset = (long)observationModel.Action1.BeginDateTimeUTCOffset,
                                                  Value = (float?)observationModel.Action1.FeatureActions.FirstOrDefault().Results.FirstOrDefault().MeasurementResult.MeasurementResultValues.FirstOrDefault().DataValue ?? 0,
                                                  Unit = new Unit
                                                  {
                                                      Id = observationModel.Action1.FeatureActions.FirstOrDefault().Results.FirstOrDefault().UnitsID,
                                                      Name = observationModel.Action1.FeatureActions.FirstOrDefault().Results.FirstOrDefault().Unit.UnitsName,
                                                      Description = observationModel.Action1.FeatureActions.FirstOrDefault().Results.FirstOrDefault().Unit.UnitsTypeCV
                                                  },
                                                  Analyte = new Analyte
                                                  {
                                                      Id = observationModel.Action1.FeatureActions.FirstOrDefault().Results.FirstOrDefault().VariableID,
                                                      Name = observationModel.Action1.FeatureActions.FirstOrDefault().Results.FirstOrDefault().Variable.VariableDefinition,
                                                      Category = new AnalyteCategory
                                                      {
                                                          Id = observationModel.Action1.FeatureActions.FirstOrDefault().Results.FirstOrDefault().Variable.VariableID,
                                                          Name = observationModel.Action1.FeatureActions.FirstOrDefault().Results.FirstOrDefault().Variable.VariableTypeCV,
                                                          StandardUnit = new Unit
                                                          {
                                                              Id = observationModel.Action1.FeatureActions.FirstOrDefault().Results.FirstOrDefault().UnitsID,
                                                              Name = observationModel.Action1.FeatureActions.FirstOrDefault().Results.FirstOrDefault().Unit.UnitsName,
                                                              Description = observationModel.Action1.FeatureActions.FirstOrDefault().Results.FirstOrDefault().Unit.UnitsTypeCV
                                                          }
                                                      }
                                                  },
                                                  Site = new Site
                                                  {
                                                      Id = observationModel.Action1.FeatureActions.FirstOrDefault().SamplingFeatureID,
                                                      Name = observationModel.Action1.FeatureActions.FirstOrDefault().SamplingFeature.SamplingFeatureName,
                                                      Latitude = observationModel.Action1.FeatureActions.FirstOrDefault().SamplingFeature.Site.Latitude,
                                                      Longitude = observationModel.Action1.FeatureActions.FirstOrDefault().SamplingFeature.Site.Longitude
                                                  }

                                              };

            return waterQualityObervationModel;


        }

        /// <summary>
        /// Query water quality data
        /// </summary>
        /// <param name="startDateTime">query start date</param>
        /// <param name="endDateTime">query end date</param>
        /// <param name="site">query site</param>
        /// <param name="analyte">query analyte</param>
        /// <returns></returns>
        public IQueryable<WaterQualityObservation> QueryWaterQualityData(DateTime startDateTime, DateTime endDateTime, Site site, Analyte analyte)
        {
            var waterQualityObervationModel = from observationModel in _dbContext.RelatedActions
                                              where (observationModel.RelationshipTypeCV == "Is related to"
                                              && observationModel.Action1.BeginDateTime >= startDateTime
                                              && observationModel.Action1.EndDateTime <= endDateTime
                                              && observationModel.Action1.FeatureActions.FirstOrDefault().Results.FirstOrDefault().VariableID == analyte.Id
                                              && observationModel.Action1.FeatureActions.FirstOrDefault().SamplingFeatureID == site.Id)
                                              orderby observationModel.Action1.FeatureActions.FirstOrDefault().Results.FirstOrDefault().VariableID ascending 
                                              select new WaterQualityObservation
                                              {
                                                  Id = observationModel.Action1.FeatureActions.FirstOrDefault().Results.FirstOrDefault().ResultID,
                                                  DateTime = observationModel.Action1.BeginDateTime,
                                                  UTCOffset = (long)observationModel.Action1.BeginDateTimeUTCOffset,
                                                  Value = (float?)observationModel.Action1.FeatureActions.FirstOrDefault().Results.FirstOrDefault().MeasurementResult.MeasurementResultValues.FirstOrDefault().DataValue ?? 0,
                                                  Unit = new Unit
                                                  {
                                                      Id = observationModel.Action1.FeatureActions.FirstOrDefault().Results.FirstOrDefault().UnitsID,
                                                      Name = observationModel.Action1.FeatureActions.FirstOrDefault().Results.FirstOrDefault().Unit.UnitsName,
                                                      Description = observationModel.Action1.FeatureActions.FirstOrDefault().Results.FirstOrDefault().Unit.UnitsTypeCV
                                                  },
                                                  Analyte = new Analyte
                                                  {
                                                      Id = observationModel.Action1.FeatureActions.FirstOrDefault().Results.FirstOrDefault().VariableID,
                                                      Name = observationModel.Action1.FeatureActions.FirstOrDefault().Results.FirstOrDefault().Variable.VariableDefinition,
                                                      Category = new AnalyteCategory
                                                      {
                                                          Id = observationModel.Action1.FeatureActions.FirstOrDefault().Results.FirstOrDefault().Variable.VariableID,
                                                          Name = observationModel.Action1.FeatureActions.FirstOrDefault().Results.FirstOrDefault().Variable.VariableTypeCV,
                                                          StandardUnit = new Unit
                                                          {
                                                              Id = observationModel.Action1.FeatureActions.FirstOrDefault().Results.FirstOrDefault().UnitsID,
                                                              Name = observationModel.Action1.FeatureActions.FirstOrDefault().Results.FirstOrDefault().Unit.UnitsName,
                                                              Description = observationModel.Action1.FeatureActions.FirstOrDefault().Results.FirstOrDefault().Unit.UnitsTypeCV
                                                          }
                                                      }
                                                  },
                                                  Site = new Site
                                                  {
                                                      Id = observationModel.Action1.FeatureActions.FirstOrDefault().SamplingFeatureID,
                                                      Name = observationModel.Action1.FeatureActions.FirstOrDefault().SamplingFeature.SamplingFeatureName,
                                                      Latitude = observationModel.Action1.FeatureActions.FirstOrDefault().SamplingFeature.Site.Latitude,
                                                      Longitude = observationModel.Action1.FeatureActions.FirstOrDefault().SamplingFeature.Site.Longitude
                                                  }

                                              };

            return waterQualityObervationModel;
                                               
        }

        /// <summary>
        /// Query water quality data
        /// </summary>
        /// <param name="startDateTime">query start date</param>
        /// <param name="endDateTime">query end date</param>
        /// <param name="sites">query sites</param>
        /// <param name="analytes">query analytes</param>
        /// <returns></returns>
        public IQueryable<WaterQualityObservation> QueryWaterQualityData(DateTime startDateTime, DateTime endDateTime,
                                                          IEnumerable<Site> sites, IEnumerable<Analyte> analytes)
        {

            var siteIDList = sites.Select(x => x.Id);
            var analyteIDList = analytes.Select(x => x.Id); //returns all the values and adds it to list


            var waterQualityObervationModel = from observationModel in _dbContext.RelatedActions
                                              where (observationModel.RelationshipTypeCV == "Is related to"
                                              && observationModel.Action1.BeginDateTime >= startDateTime
                                              && observationModel.Action1.EndDateTime <= endDateTime
                                              && siteIDList.Contains(observationModel.Action1.FeatureActions.FirstOrDefault().SamplingFeatureID)
                                              && analyteIDList.Contains(observationModel.Action1.FeatureActions.FirstOrDefault().Results.FirstOrDefault().VariableID))
                                              select new WaterQualityObservation
                                              {
                                                  Id = observationModel.Action1.FeatureActions.FirstOrDefault().Results.FirstOrDefault().ResultID,
                                                  DateTime = observationModel.Action1.BeginDateTime,
                                                  UTCOffset = (long)observationModel.Action1.BeginDateTimeUTCOffset,
                                                  Value = (float?)observationModel.Action1.FeatureActions.FirstOrDefault().Results.FirstOrDefault().MeasurementResult.MeasurementResultValues.FirstOrDefault().DataValue ?? 0,
                                                  Unit = new Unit
                                                  {
                                                      Id = observationModel.Action1.FeatureActions.FirstOrDefault().Results.FirstOrDefault().UnitsID,
                                                      Name = observationModel.Action1.FeatureActions.FirstOrDefault().Results.FirstOrDefault().Unit.UnitsName,
                                                      Description = observationModel.Action1.FeatureActions.FirstOrDefault().Results.FirstOrDefault().Unit.UnitsTypeCV
                                                  },
                                                  Analyte = new Analyte
                                                  {
                                                      Id = observationModel.Action1.FeatureActions.FirstOrDefault().Results.FirstOrDefault().VariableID,
                                                      Name = observationModel.Action1.FeatureActions.FirstOrDefault().Results.FirstOrDefault().Variable.VariableDefinition,
                                                      Category = new AnalyteCategory
                                                      {
                                                          Id = observationModel.Action1.FeatureActions.FirstOrDefault().Results.FirstOrDefault().Variable.VariableID,
                                                          Name = observationModel.Action1.FeatureActions.FirstOrDefault().Results.FirstOrDefault().Variable.VariableTypeCV,
                                                          StandardUnit = new Unit
                                                          {
                                                              Id = observationModel.Action1.FeatureActions.FirstOrDefault().Results.FirstOrDefault().UnitsID,
                                                              Name = observationModel.Action1.FeatureActions.FirstOrDefault().Results.FirstOrDefault().Unit.UnitsName,
                                                              Description = observationModel.Action1.FeatureActions.FirstOrDefault().Results.FirstOrDefault().Unit.UnitsTypeCV
                                                          }
                                                      }
                                                  },
                                                  Site = new Site
                                                  {
                                                      Id = observationModel.Action1.FeatureActions.FirstOrDefault().SamplingFeatureID,
                                                      Name = observationModel.Action1.FeatureActions.FirstOrDefault().SamplingFeature.SamplingFeatureName,
                                                      Latitude = observationModel.Action1.FeatureActions.FirstOrDefault().SamplingFeature.Site.Latitude,
                                                      Longitude = observationModel.Action1.FeatureActions.FirstOrDefault().SamplingFeature.Site.Longitude
                                                  }

                                              };





            return waterQualityObervationModel;

           
        }

        /// <summary>
        /// Save or update water quality samples
        /// </summary>
        /// <param name="samples">samples to save or update</param>
        /// <returns>save/updated water quality samples</returns>
        public bool SaveOrUpdateWaterQualityObservations(IEnumerable<WaterQualityObservation> samples)
        {
            throw new NotImplementedException();
        }
    
        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
