using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Hatfield.EnviroData.Core;
using Hatfield.EnviroData.Core.Repositories;

namespace Hatfield.EnviroData.WQDataProfile.Repositories
{
    public class WQDataRepository : ActionRepository, IWQDataRepository
    {
        private static readonly string ISChildOfRelationshipCV = "Is child of";
        private static readonly string IsRelatedToRelationshipCV = "Is related to";

        public WQDataRepository(IDbContext dbContext)
            : base(dbContext)
        {

        }

        //"Is related to" in this case means "Is parent of". Change later.
        public IEnumerable<Core.Action> GetAllWQAnalyteDataActions()
        {
            var dbContext = (ODM2Entities)_dbContext;
            var sampleAnalysisActions = (from action in dbContext.Actions
                                           join relatedAction in dbContext.RelatedActions
                                           on action.ActionID equals relatedAction.RelatedActionID
                                           where relatedAction.RelationshipTypeCV == IsRelatedToRelationshipCV
                                           select action)
                                             .Distinct()
                                             .OrderBy(x => x.BeginDateTime);

            return sampleAnalysisActions.ToList();
        }

        public IEnumerable<Core.Action> GetAllWQSampleDataActions()
        {
            var dbContext = (ODM2Entities)_dbContext;
            var sampleCollectionActions = (from action in dbContext.Actions
                                           join relatedAction in dbContext.RelatedActions
                                           on action.ActionID equals relatedAction.RelatedActionID
                                           where relatedAction.RelationshipTypeCV == ISChildOfRelationshipCV
                                           select action)
                                          .Distinct()
                                          .OrderBy(x => x.BeginDateTime);

            return sampleCollectionActions.ToList();
        }


        public int SaveChanges()
        {
            return _dbContext.SaveChanges();
        }
    }
}
