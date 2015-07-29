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

        public WQDataRepository(IDbContext dbContext)
            : base(dbContext)
        {

        }

        public IEnumerable<Core.Action> GetAllWQAnalyteDataActions()
        {
            throw new NotImplementedException("Get all WQ analyte data actions function is not implemented");
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
