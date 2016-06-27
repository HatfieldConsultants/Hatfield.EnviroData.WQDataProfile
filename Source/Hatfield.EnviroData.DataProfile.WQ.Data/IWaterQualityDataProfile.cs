using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Hatfield.EnviroData.DataProfile.WQ.Models;

namespace Hatfield.EnviroData.DataProfile.WQ
{
    public interface IWaterQualityDataProfile
    {
        /// <summary>
        /// Get all the sites in the databases
        /// </summary>
        /// <returns></returns>
        IQueryable<Site> GetAllSites();

        /// <summary>
        /// Save or Update site data
        /// </summary>
        /// <param name="site">site that need to update or insert</param>
        /// <returns></returns>
        Site SaveOrUpdateSite(Site site);

        /// <summary>
        /// Get all the analytes in the databases
        /// </summary>
        /// <returns></returns>
        IQueryable<Analyte> GetAllAnalytes();

        /// <summary>
        /// Get all sampling activities in the databases
        /// </summary>
        /// <returns></returns>
        IQueryable<SamplingActivity> GetAllSamplingActivities();

        /// <summary>
        /// Get all field work productions in the databases
        /// each field work production contains a sampling activity and the related water quality samples
        /// </summary>
        /// <returns></returns>
        IQueryable<FieldWorkProduction> GetAllFieldWorkProductions();

        /// <summary>
        /// Query sampling activities
        /// </summary>
        /// <param name="startDateTime">sampling activity start date</param>
        /// <param name="endDateTime">sampling activity end date</param>
        /// <returns></returns>
        IQueryable<SamplingActivity> QuerySamplingActivities(DateTime startDateTime, DateTime endDateTime);

        /// <summary>
        /// Query sampling activities
        /// </summary>
        /// <param name="startDateTime">sampling activity start date</param>
        /// <param name="endDateTime">sampling activity end date</param>
        /// <param name="sites">sampling site</param>
        /// <returns></returns>
        IQueryable<SamplingActivity> QuerySamplingActivities(DateTime startDateTime, DateTime endDateTime, IEnumerable<Site> sites);

        /// <summary>
        /// Save or Update sampling activities
        /// </summary>
        /// <param name="samples">sampling activities to save or update</param>
        /// <returns></returns>
        bool SaveOrUpdateSamplingActivities(IEnumerable<SamplingActivity> samples);

        /// <summary>
        /// Query water quality data
        /// </summary>
        /// <param name="startDateTime">query start date</param>
        /// <param name="endDateTime">query end date</param>
        /// <returns>water quality samples within the query time range</returns>
        IQueryable<WaterQualitySample> QueryWaterQualityData(DateTime startDateTime, DateTime endDateTime);

        /// <summary>
        /// Query water quality data
        /// </summary>
        /// <param name="startDateTime">query start date</param>
        /// <param name="endDateTime">query end date</param>
        /// <param name="site">query site</param>
        /// <returns></returns>
        IQueryable<WaterQualitySample> QueryWaterQualityData(DateTime startDateTime, DateTime endDateTime, Site site);

        /// <summary>
        /// Query water quality data
        /// </summary>
        /// <param name="startDateTime">query start date</param>
        /// <param name="endDateTime">query end date</param>
        /// <param name="analyte">query analyte</param>
        /// <returns></returns>
        IQueryable<WaterQualitySample> QueryWaterQualityData(DateTime startDateTime, DateTime endDateTime, Analyte analyte);

        /// <summary>
        /// Query water quality data
        /// </summary>
        /// <param name="startDateTime">query start date</param>
        /// <param name="endDateTime">query end date</param>
        /// <param name="site">query site</param>
        /// <param name="analyte">query analyte</param>
        /// <returns></returns>
        IQueryable<WaterQualitySample> QueryWaterQualityData(DateTime startDateTime, DateTime endDateTime, Site site, Analyte analyte);

        /// <summary>
        /// Query water quality data
        /// </summary>
        /// <param name="startDateTime">query start date</param>
        /// <param name="endDateTime">query end date</param>
        /// <param name="sites">query sites</param>
        /// <param name="analytes">query analytes</param>
        /// <returns></returns>
        IQueryable<WaterQualitySample> QueryWaterQualityData(DateTime startDateTime, DateTime endDateTime, 
                                                                IEnumerable<Site> sites, IEnumerable<Analyte> analytes);

        /// <summary>
        /// Save or update water quality samples
        /// </summary>
        /// <param name="samples">samples to save or update</param>
        /// <returns>save/updated water quality samples</returns>
        bool SaveOrUpdateWaterQualitySamples(IEnumerable<WaterQualitySample> samples);

        
    }
}
