using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Hatfield.EnviroData.Core;

namespace Hatfield.EnviroData.WQDataProfile
{
    public class DataVersioningHelper : IDataVersioningHelper
    {
        private IWQDefaultValueProvider _wqDefaultValueProvider;

        public DataVersioningHelper(IWQDefaultValueProvider wqDefaultValueProvider)
        {
            _wqDefaultValueProvider = wqDefaultValueProvider;
        }

        public Hatfield.EnviroData.Core.Action CloneActionData(Hatfield.EnviroData.Core.Action previousVersionAction)
        {
            var clonedAction = new Hatfield.EnviroData.Core.Action();

            //clone value in the root action object
            clonedAction.ActionTypeCV = previousVersionAction.ActionTypeCV;
            clonedAction.CV_ActionType = previousVersionAction.CV_ActionType;
            clonedAction.MethodID = previousVersionAction.MethodID;
            clonedAction.Method = previousVersionAction.Method;
            clonedAction.BeginDateTime = previousVersionAction.BeginDateTime;
            clonedAction.BeginDateTimeUTCOffset = previousVersionAction.BeginDateTimeUTCOffset;
            clonedAction.EndDateTime = previousVersionAction.EndDateTime;
            clonedAction.EndDateTimeUTCOffset = previousVersionAction.EndDateTimeUTCOffset;
            clonedAction.ActionDescription = previousVersionAction.ActionDescription;
            clonedAction.ActionFileLink = previousVersionAction.ActionFileLink;
            clonedAction.ActionBies = CloneActionBies(previousVersionAction, clonedAction);

            clonedAction.FeatureActions = CloneFeatureActions(previousVersionAction, clonedAction);

            return clonedAction;
        }

        public Core.Action GetNextVersionActionData(Core.Action originVersionActionData)
        {
            if(originVersionActionData.RelatedActions == null || !originVersionActionData.RelatedActions.Any())
            {
                return null;
            }
            else
            {
                try
                {
                    var subVersionRelateAction = originVersionActionData.RelatedActions
                                                                        .Where(x => x.CV_RelationshipType.Name == _wqDefaultValueProvider.ActionRelationshipTypeSubVersion)
                                                                        .SingleOrDefault();

                    if(subVersionRelateAction == null)
                    {
                        return originVersionActionData;
                    }

                    var childVersion = subVersionRelateAction.Action1;
                    return childVersion;
                }
                catch(Exception)
                {
                    throw new ArgumentException("Action data is not allowed to have multiple direct children verion");
                }
                
            }
            
        }

        public Core.Action GetLatestVersionActionData(Core.Action originVersionActionData)
        {
            var childVersion = GetNextVersionActionData(originVersionActionData);
            if (childVersion == null)
            {
                return originVersionActionData;
            }
            else
            {
                return GetLatestVersionActionData(childVersion);
            }
        }


        private ICollection<ActionBy> CloneActionBies(Hatfield.EnviroData.Core.Action previousVersionAction,
                                                    Hatfield.EnviroData.Core.Action newVersionAction)
        {
            if (previousVersionAction.ActionBies == null)
            {
                return null;
            }
            else
            {
                var clonedNewActionBies = new List<ActionBy>();
                foreach(var actionByInPreviousVersion in previousVersionAction.ActionBies)
                {
                    var newActionBy = new ActionBy();

                    newActionBy.Action = newVersionAction;
                    newActionBy.Affiliation = actionByInPreviousVersion.Affiliation;
                    newActionBy.AffiliationID = actionByInPreviousVersion.AffiliationID;
                    newActionBy.IsActionLead = actionByInPreviousVersion.IsActionLead;
                    newActionBy.RoleDescription = actionByInPreviousVersion.RoleDescription;

                    clonedNewActionBies.Add(newActionBy);
                }

                return clonedNewActionBies;
            }
        }

        private ICollection<FeatureAction> CloneFeatureActions(Hatfield.EnviroData.Core.Action previousVersionAction, 
                                                               Hatfield.EnviroData.Core.Action newVersionAction)
        {
            if (previousVersionAction.FeatureActions == null)
            {
                return null;
            }
            else
            {
                var newVersionFeatureActions = new List<FeatureAction>();

                foreach(var featureAction in previousVersionAction.FeatureActions)
                {
                    var newFeatureAction = new FeatureAction();
                    newFeatureAction.Action = newVersionAction;
                    newFeatureAction.SamplingFeatureID = featureAction.SamplingFeatureID;
                    newFeatureAction.SamplingFeature = featureAction.SamplingFeature;
                    newFeatureAction.Results = CloneResults(featureAction, newFeatureAction);

                    newVersionFeatureActions.Add(newFeatureAction);
                }

                return newVersionFeatureActions;
            }
        }

        private ICollection<Result> CloneResults(FeatureAction previousVersionFeatureAction, FeatureAction newVersionFeatureAction)
        {
            if (previousVersionFeatureAction.Results == null)
            {
                return null;
            }
            else
            {
                var newVersionResults = new List<Result>();

                foreach(var previousVersionResult in previousVersionFeatureAction.Results)
                {
                    var newResult = new Result();
                    newResult.FeatureAction = newVersionFeatureAction;
                    newResult.ResultTypeCV = previousVersionResult.ResultTypeCV;
                    newResult.CV_ResultType = previousVersionResult.CV_ResultType;
                    newResult.VariableID = previousVersionResult.VariableID;
                    newResult.Variable = previousVersionResult.Variable;
                    newResult.UnitsID = previousVersionResult.UnitsID;
                    newResult.Unit = previousVersionResult.Unit;
                    newResult.TaxonomicClassifierID = previousVersionResult.TaxonomicClassifierID;
                    newResult.TaxonomicClassifier = previousVersionResult.TaxonomicClassifier;
                    newResult.ProcessingLevelID = previousVersionResult.ProcessingLevelID;
                    newResult.ProcessingLevel = previousVersionResult.ProcessingLevel;
                    newResult.ResultDateTime = previousVersionResult.ResultDateTime;
                    newResult.ResultDateTimeUTCOffset = previousVersionResult.ResultDateTimeUTCOffset;
                    newResult.ValidDateTime = previousVersionResult.ValidDateTime;
                    newResult.StatusCV = previousVersionResult.StatusCV;
                    newResult.CV_Status = previousVersionResult.CV_Status;
                    newResult.SampledMediumCV = previousVersionResult.SampledMediumCV;
                    newResult.CV_SampledMedium = previousVersionResult.CV_SampledMedium;
                    newResult.ValueCount = previousVersionResult.ValueCount;
                    //clone measurement result
                    newResult.MeasurementResult = CloneMeasurementResult(previousVersionResult.MeasurementResult, newResult);
                    newResult.ResultExtensionPropertyValues = CloneResultExtensionPropertyValues(newResult, previousVersionResult.ResultExtensionPropertyValues);

                    newVersionResults.Add(newResult);
                }

                return newVersionResults;
            }
        }

        private MeasurementResult CloneMeasurementResult(MeasurementResult previousVersionMeasurementResult, Result newVersionResult)
        {
            if (previousVersionMeasurementResult == null)
            {
                return null;
            }
            else
            {
                var newVersionMeasurementResult = new MeasurementResult();
                newVersionMeasurementResult.Result = newVersionResult;
                newVersionMeasurementResult.AggregationStatisticCV = previousVersionMeasurementResult.AggregationStatisticCV;
                newVersionMeasurementResult.CV_AggregationStatistic = previousVersionMeasurementResult.CV_AggregationStatistic;
                newVersionMeasurementResult.CensorCodeCV = previousVersionMeasurementResult.CensorCodeCV;
                newVersionMeasurementResult.CV_CensorCode = previousVersionMeasurementResult.CV_CensorCode;
                newVersionMeasurementResult.QualityCodeCV = previousVersionMeasurementResult.QualityCodeCV;
                newVersionMeasurementResult.CV_QualityCode = previousVersionMeasurementResult.CV_QualityCode;
                
                newVersionMeasurementResult.SpatialReference = previousVersionMeasurementResult.SpatialReference;
                newVersionMeasurementResult.SpatialReferenceID = previousVersionMeasurementResult.SpatialReferenceID;
                newVersionMeasurementResult.TimeAggregationInterval = previousVersionMeasurementResult.TimeAggregationInterval;
                newVersionMeasurementResult.TimeAggregationIntervalUnitsID = previousVersionMeasurementResult.TimeAggregationIntervalUnitsID;
                newVersionMeasurementResult.Unit = previousVersionMeasurementResult.Unit;
                newVersionMeasurementResult.Unit1 = previousVersionMeasurementResult.Unit1;
                newVersionMeasurementResult.Unit2 = previousVersionMeasurementResult.Unit2;
                newVersionMeasurementResult.Unit3 = previousVersionMeasurementResult.Unit3;
                newVersionMeasurementResult.XLocation = previousVersionMeasurementResult.XLocation;
                newVersionMeasurementResult.XLocationUnitsID = previousVersionMeasurementResult.XLocationUnitsID;
                newVersionMeasurementResult.YLocation = previousVersionMeasurementResult.YLocation;
                newVersionMeasurementResult.YLocationUnitsID = previousVersionMeasurementResult.YLocationUnitsID;
                newVersionMeasurementResult.ZLocation = previousVersionMeasurementResult.ZLocation;
                newVersionMeasurementResult.ZLocationUnitsID = previousVersionMeasurementResult.ZLocationUnitsID;
                
                if(previousVersionMeasurementResult.MeasurementResultValues != null)
                {
                    newVersionMeasurementResult.MeasurementResultValues = new List<MeasurementResultValue>();
                    foreach(var previousMeasurementResultValue in previousVersionMeasurementResult.MeasurementResultValues)
                    {
                        var newMeasurementResultValue = new MeasurementResultValue();
                        newMeasurementResultValue.MeasurementResult = newVersionMeasurementResult;
                        newMeasurementResultValue.DataValue = previousMeasurementResultValue.DataValue;
                        newMeasurementResultValue.ValueDateTime = previousMeasurementResultValue.ValueDateTime;
                        newMeasurementResultValue.ValueDateTimeUTCOffset = previousMeasurementResultValue.ValueDateTimeUTCOffset;

                        newVersionMeasurementResult.MeasurementResultValues.Add(newMeasurementResultValue);
                    }
                }

                return newVersionMeasurementResult;
            }
        }

        private ICollection<ResultExtensionPropertyValue> CloneResultExtensionPropertyValues(Result newVersionResult, 
                                                                                             ICollection<ResultExtensionPropertyValue> previousExtensionValues)
        {
            if (previousExtensionValues == null)
            {
                return null;
            }
            else
            {
                var newVersionExtensionPropertyValules = new List<ResultExtensionPropertyValue>();

                foreach(var previousExtension in previousExtensionValues)
                {
                    var newVersionExtensionValue = new ResultExtensionPropertyValue();

                    newVersionExtensionValue.Result = newVersionResult;
                    newVersionExtensionValue.BridgeID = previousExtension.BridgeID;
                    newVersionExtensionValue.ExtensionProperty = previousExtension.ExtensionProperty;
                    newVersionExtensionValue.PropertyID = previousExtension.PropertyID;
                    newVersionExtensionValue.PropertyValue = previousExtension.PropertyValue;

                    newVersionExtensionPropertyValules.Add(newVersionExtensionValue);
                }

                return newVersionExtensionPropertyValules;
            }
        }


        
    }
}
