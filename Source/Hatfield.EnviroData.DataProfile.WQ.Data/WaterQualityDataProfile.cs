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
                                select new Analyte
                                {
                                    Id = analyte.VariableID,
                                    Name = analyte.VariableDefinition,
                                    Category = new AnalyteCategory
                                    {
                                        Id = analyte.VariableID,
                                        Name = analyte.VariableTypeCV,
                                        //StandardUnit = new Unit
                                        //{
                                        //    Id = analyte.Results.FirstOrDefault().UnitsID,
                                        //    Name = analyte.Results.FirstOrDefault().Unit.UnitsName,
                                        //    Description = analyte.Results.FirstOrDefault().Unit.UnitsTypeCV
                                        //}
                                    }
                                    
                                };

            return analyteModels;
        }

        /// <summary>
        /// Get all sampling activities in the databases
        /// </summary>
        /// <returns></returns>
        public IQueryable<FieldVisit> GetAllSamplingActivities()
        {
            var samplingActivityModels = from samplingActivity in _dbContext.RelatedActions
                                         select new FieldVisit
                                         {
                                             Id = samplingActivity.RelatedActionID,
                                             StartDateTime = samplingActivity.Action1.BeginDateTime,
                                             StartDateTimeUTCOffset = samplingActivity.Action1.BeginDateTimeUTCOffset,
                                             EndDateTime = samplingActivity.Action1.EndDateTime,
                                             EndDateTimeUTCOffset = (long)samplingActivity.Action1.EndDateTimeUTCOffset,
                                             Site = samplingActivity.Action1.FeatureActions.Select(x => new Site
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
        public IQueryable<Observation> GetAllFieldWorkProductions()
        {
            var fieldWorkProductionModel = from fieldWork in _dbContext.RelatedResults
                                           
                                           select new Observation
                                           {
                                               SamplingActivity = new FieldVisit
                                               {
                                                   Id = fieldWork.Result.FeatureAction.Action.ActionID,
                                                   StartDateTime = fieldWork.Result.FeatureAction.Action.BeginDateTime,
                                                   StartDateTimeUTCOffset = fieldWork.Result.FeatureAction.Action.BeginDateTimeUTCOffset,
                                                   EndDateTime = fieldWork.Result.FeatureAction.Action.EndDateTime,
                                                   EndDateTimeUTCOffset = (int)fieldWork.Result.FeatureAction.Action.EndDateTimeUTCOffset,
                                                   Site = fieldWork.Result.FeatureAction.Action.FeatureActions.Select(x => new Site
                                                   {
                                                       Id = x.SamplingFeature.SamplingFeatureID,
                                                       Name = x.SamplingFeature.SamplingFeatureName,
                                                       Latitude = x.SamplingFeature.Site.Latitude,
                                                       Longitude = x.SamplingFeature.Site.Longitude
                                                   }).FirstOrDefault()

                                               },
                                               Samples = new List<LabReportSample>
                                               {
                                                   new LabReportSample{

                                                          Id = fieldWork.RelationID,
                                                          DateTime = fieldWork.Result.ResultDateTime,
                                                          UTCOffset = (long)fieldWork.Result.ResultDateTimeUTCOffset,

                                                          Value = (int)fieldWork.Result.MeasurementResult.MeasurementResultValues.FirstOrDefault().DataValue, 
                                                          Analyte = new Analyte()
                                                          {
                                                                Id = fieldWork.Result1.FeatureAction.SamplingFeature.Specimen.SamplingFeatureID,
                                                                Name = fieldWork.Result1.FeatureAction.SamplingFeature.Specimen.SamplingFeature.SamplingFeatureName
                                                          },
                                                          Site = new Site()
                                                          {
                                                                  Id = fieldWork.Result.FeatureAction.SamplingFeature.Site.SamplingFeatureID,
                                                                  Name = fieldWork.Result.FeatureAction.SamplingFeature.Site.SamplingFeature.SamplingFeatureName,
                                                                  Latitude = fieldWork.Result.FeatureAction.SamplingFeature.Site.Latitude,
                                                                  Longitude = fieldWork.Result.FeatureAction.SamplingFeature.Site.Longitude
                                                          }


                                                    }
                                               }

                                           };

            return fieldWorkProductionModel;
        }

        /// <summary>
        /// Query sampling activities
        /// </summary>
        /// <param name="startDateTime">sampling activity start date</param>
        /// <param name="endDateTime">sampling activity end date</param>
        /// <returns></returns>
        public IQueryable<FieldVisit> QuerySamplingActivities(DateTime startDateTime, DateTime endDateTime)
        {
            var samplingActivityModels = from samplingActivity in _dbContext.RelatedActions
                                         where samplingActivity.Action1.BeginDateTime >= startDateTime && samplingActivity.Action1.EndDateTime <= endDateTime
                                         select new FieldVisit
                                         {
                                             Id = samplingActivity.ActionID,
                                             StartDateTime = samplingActivity.Action1.BeginDateTime,
                                             StartDateTimeUTCOffset = samplingActivity.Action1.BeginDateTimeUTCOffset,
                                             EndDateTime = samplingActivity.Action1.EndDateTime,
                                             EndDateTimeUTCOffset = (long)samplingActivity.Action1.EndDateTimeUTCOffset,
                                             Site = samplingActivity.Action1.FeatureActions.Select(x => new Site
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
        /// Query sampling activities
        /// </summary>
        /// <param name="startDateTime">sampling activity start date</param>
        /// <param name="endDateTime">sampling activity end date</param>
        /// <param name="sites">sampling site</param>
        /// <returns></returns>
        public IQueryable<FieldVisit> QuerySamplingActivities(DateTime startDateTime, DateTime endDateTime, Site sites)
        {

            var samplingActivityModels = from samplingActivity in _dbContext.RelatedActions
                                         where (samplingActivity.Action1.BeginDateTime >= startDateTime && samplingActivity.Action1.EndDateTime <= endDateTime 
                                         && (samplingActivity.Action1.FeatureActions.FirstOrDefault().SamplingFeatureID == sites.Id))         
                                         select new FieldVisit
                                         {
                                             Id = samplingActivity.ActionID,
                                             StartDateTime = samplingActivity.Action1.BeginDateTime,
                                             StartDateTimeUTCOffset = samplingActivity.Action1.BeginDateTimeUTCOffset,
                                             EndDateTime = samplingActivity.Action1.EndDateTime,
                                             EndDateTimeUTCOffset = (long)samplingActivity.Action1.EndDateTimeUTCOffset,
                                             Site = samplingActivity.Action1.FeatureActions.Select(x => new Site
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
        public bool SaveOrUpdateSamplingActivities(IEnumerable<FieldVisit> samples)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Query water quality data
        /// </summary>
        /// <param name="startDateTime">query start date</param>
        /// <param name="endDateTime">query end date</param>
        /// <returns>water quality samples within the query time range</returns>
        public IQueryable<LabReportSample> QueryWaterQualityData(DateTime startDateTime, DateTime endDateTime)
        {
            var waterQualityObervationModel = from observationModel in _dbContext.Results
                                              where (observationModel.MeasurementResult.MeasurementResultValues.FirstOrDefault().ValueDateTime >= startDateTime &&
                                             observationModel.MeasurementResult.MeasurementResultValues.FirstOrDefault().ValueDateTime <= endDateTime)
      
                                              select new LabReportSample
                                              {
                                                 Id = observationModel.ResultID,
                                                 DateTime = observationModel.MeasurementResult.MeasurementResultValues.FirstOrDefault().ValueDateTime,
                                                 UTCOffset = (long)observationModel.MeasurementResult.MeasurementResultValues.FirstOrDefault().ValueDateTimeUTCOffset,
                                                 Value = (float)observationModel.MeasurementResult.MeasurementResultValues.FirstOrDefault().DataValue,    //needs to be changed
                                                 Unit = new Unit
                                                 {
                                                     Id = observationModel.UnitsID,
                                                     Name = observationModel.Unit.UnitsName,
                                                     Description = observationModel.Unit.UnitsTypeCV
                                                 },
                                                 Analyte = new Analyte
                                                 {
                                                     Id = observationModel.VariableID,
                                                     Name = observationModel.Variable.VariableDefinition,
                                                     Category = new AnalyteCategory
                                                     {
                                                         Id = observationModel.Variable.VariableID,
                                                         Name = observationModel.Variable.VariableTypeCV,
                                                         StandardUnit = new Unit
                                                         {
                                                             Id = observationModel.UnitsID,
                                                             Name = observationModel.Unit.UnitsName,
                                                             Description = observationModel.Unit.UnitsTypeCV
                                                         }
                                                     }
                                                 },
                                                 Site = new Site
                                                 {
                                                     Id = observationModel.FeatureAction.SamplingFeatureID,
                                                     Name = observationModel.FeatureAction.SamplingFeature.SamplingFeatureName,
                                                     Latitude = observationModel.FeatureAction.SamplingFeature.Site.Latitude,
                                                     Longitude = observationModel.FeatureAction.SamplingFeature.Site.Longitude
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
        public IQueryable<LabReportSample> QueryWaterQualityData(DateTime startDateTime, DateTime endDateTime, Site site)
        {

            var waterQualityObervationModel = from observationModel in _dbContext.Results
                                              where (observationModel.MeasurementResult.MeasurementResultValues.FirstOrDefault().ValueDateTime >= startDateTime &&
                                             observationModel.MeasurementResult.MeasurementResultValues.FirstOrDefault().ValueDateTime <= endDateTime
                                              && observationModel.FeatureAction.SamplingFeature.SamplingFeatureID == site.Id)
                                              select new LabReportSample
                                              {
                                                  Id = observationModel.ResultID,
                                                  DateTime = observationModel.MeasurementResult.MeasurementResultValues.FirstOrDefault().ValueDateTime,
                                                  UTCOffset = (long)observationModel.MeasurementResult.MeasurementResultValues.FirstOrDefault().ValueDateTimeUTCOffset,

                                                  Value = (int)observationModel.MeasurementResult.MeasurementResultValues.FirstOrDefault().DataValue,    //needs to be changed
                                                  Site = new Site()
                                                  {
                                                      Id = observationModel.FeatureAction.SamplingFeature.Site.SamplingFeatureID,
                                                      Name = observationModel.FeatureAction.SamplingFeature.Site.SamplingFeature.SamplingFeatureName,
                                                      Latitude = observationModel.FeatureAction.SamplingFeature.Site.Latitude,
                                                      Longitude = observationModel.FeatureAction.SamplingFeature.Site.Longitude
                                                  },
                                                  Unit = new Unit
                                                  {
                                                      Id = observationModel.UnitsID,
                                                      Name = observationModel.Unit.UnitsName,
                                                      Description = observationModel.Unit.UnitsTypeCV
                                                  },
                                                  Analyte = new Analyte
                                                  {
                                                      Id = observationModel.VariableID,
                                                      Name = observationModel.Variable.VariableDefinition,
                                                      Category = new AnalyteCategory
                                                      {
                                                          Id = observationModel.Variable.VariableID,
                                                          Name = observationModel.Variable.VariableTypeCV,
                                                          StandardUnit = new Unit
                                                          {
                                                              Id = observationModel.UnitsID,
                                                              Name = observationModel.Unit.UnitsName,
                                                              Description = observationModel.Unit.UnitsTypeCV
                                                          }
                                                      }
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
        public IQueryable<LabReportSample> QueryWaterQualityData(DateTime startDateTime, DateTime endDateTime, Analyte analyte)
        {
            var waterQualityObervationModel = from observationModel in _dbContext.Results
                                              where (observationModel.MeasurementResult.MeasurementResultValues.FirstOrDefault().ValueDateTime >= startDateTime &&
                                             observationModel.MeasurementResult.MeasurementResultValues.FirstOrDefault().ValueDateTime <= endDateTime
                                              && observationModel.VariableID == analyte.Id)
                                              select new LabReportSample
                                              {
                                                  Id = observationModel.ResultID,
                                                  DateTime = observationModel.MeasurementResult.MeasurementResultValues.FirstOrDefault().ValueDateTime,
                                                  UTCOffset = (long)observationModel.MeasurementResult.MeasurementResultValues.FirstOrDefault().ValueDateTimeUTCOffset,

                                                  Value = (int)observationModel.MeasurementResult.MeasurementResultValues.FirstOrDefault().DataValue,    //needs to be changed
                                                  Analyte = new Analyte()
                                                  {
                                                      Id = observationModel.VariableID,
                                                      Name = observationModel.Variable.VariableDefinition,

                                                      Category = new AnalyteCategory
                                                      {
                                                          Id = observationModel.VariableID,
                                                          Name = observationModel.Variable.VariableTypeCV
                                                      }   

                                                  },
                                                  Unit = new Unit
                                                  {
                                                      Id = observationModel.UnitsID,
                                                      Name = observationModel.Unit.UnitsName,
                                                      Description = observationModel.Unit.UnitsTypeCV
                                                  },
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
        public IQueryable<LabReportSample> QueryWaterQualityData(DateTime startDateTime, DateTime endDateTime, Site site, Analyte analyte)
        {


            var waterQualityObervationModel = from observationModel in _dbContext.Results

                                              where (observationModel.MeasurementResult.MeasurementResultValues.FirstOrDefault().ValueDateTime >= startDateTime &&
                                              observationModel.MeasurementResult.MeasurementResultValues.FirstOrDefault().ValueDateTime <= endDateTime
                                              && observationModel.FeatureAction.SamplingFeature.SamplingFeatureID == site.Id
                                              && observationModel.VariableID == analyte.Id) //use contains to see if identical value is present 
                                              select new LabReportSample
                                              {
                                                  Id = observationModel.ResultID,
                                                  DateTime = observationModel.MeasurementResult.MeasurementResultValues.FirstOrDefault().ValueDateTime,
                                                  UTCOffset = (long)observationModel.MeasurementResult.MeasurementResultValues.FirstOrDefault().ValueDateTimeUTCOffset,
                                                  Value = (int)observationModel.MeasurementResult.MeasurementResultValues.FirstOrDefault().DataValue,
                                                  Analyte = new Analyte()
                                                  {
                                                      Id = observationModel.VariableID,
                                                      Name = observationModel.Variable.VariableDefinition,

                                                      Category = new AnalyteCategory
                                                      {
                                                          Id = observationModel.VariableID,
                                                          Name = observationModel.Variable.VariableTypeCV
                                                      }
                                                  },
                                                  Site = new Site()
                                                  {
                                                      Id = observationModel.FeatureAction.SamplingFeature.Site.SamplingFeatureID,
                                                      Name = observationModel.FeatureAction.SamplingFeature.Site.SamplingFeature.SamplingFeatureName,
                                                      Latitude = observationModel.FeatureAction.SamplingFeature.Site.Latitude,
                                                      Longitude = observationModel.FeatureAction.SamplingFeature.Site.Longitude
                                                  },
                                                  Unit = new Unit
                                                  {
                                                      Id = observationModel.UnitsID,
                                                      Name = observationModel.Unit.UnitsName,
                                                      Description = observationModel.Unit.UnitsTypeCV
                                                  },
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
        public IQueryable<LabReportSample> QueryWaterQualityData(DateTime startDateTime, DateTime endDateTime,
                                                          IEnumerable<Site> sites, IEnumerable<Analyte> analytes)
        {

            var siteIDList = sites.Select(x => x.Id);
            var analyteIDList = analytes.Select(x => x.Id); //returns all the values and adds it to list


            var waterQualityObervationModel = from observationModel in _dbContext.Results

                                              where (observationModel.MeasurementResult.MeasurementResultValues.FirstOrDefault().ValueDateTime >= startDateTime &&
                                              observationModel.MeasurementResult.MeasurementResultValues.FirstOrDefault().ValueDateTime <= endDateTime
                                              && siteIDList.Contains(observationModel.FeatureAction.SamplingFeature.SamplingFeatureID)
                                              && analyteIDList.Contains(observationModel.VariableID)) //use contains to see if identical value is present 
                                              select new LabReportSample
                                              {
                                                  Id = observationModel.ResultID,
                                                  DateTime = observationModel.MeasurementResult.MeasurementResultValues.FirstOrDefault().ValueDateTime,
                                                  UTCOffset = (long)observationModel.MeasurementResult.MeasurementResultValues.FirstOrDefault().ValueDateTimeUTCOffset,
                                                  Value = (int)observationModel.MeasurementResult.MeasurementResultValues.FirstOrDefault().DataValue,
                                                  Unit = new Unit
                                                  {
                                                      Id = observationModel.UnitsID,
                                                      Name = observationModel.Unit.UnitsName,
                                                      Description = observationModel.Unit.UnitsTypeCV
                                                  },
                                                  Analyte = new Analyte()
                                                  {
                                                      Id = observationModel.VariableID,
                                                      Name = observationModel.Variable.VariableDefinition,

                                                      Category = new AnalyteCategory
                                                      {
                                                          Id = observationModel.VariableID,
                                                          Name = observationModel.Variable.VariableTypeCV
                                                      }
                                                  },
                                                  Site = new Site()
                                                  {
                                                      Id = observationModel.FeatureAction.SamplingFeature.Site.SamplingFeatureID,
                                                      Name = observationModel.FeatureAction.SamplingFeature.Site.SamplingFeature.SamplingFeatureName,
                                                      Latitude = observationModel.FeatureAction.SamplingFeature.Site.Latitude,
                                                      Longitude = observationModel.FeatureAction.SamplingFeature.Site.Longitude
                                                  },
                                                
                                              };

            return waterQualityObervationModel;

           
        }

        /// <summary>
        /// Save or update water quality samples
        /// </summary>
        /// <param name="samples">samples to save or update</param>
        /// <returns>save/updated water quality samples</returns>
        public bool SaveOrUpdateWaterQualityObservations(IEnumerable<LabReportSample> samples)
        {
            throw new NotImplementedException();
        }
    
        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
