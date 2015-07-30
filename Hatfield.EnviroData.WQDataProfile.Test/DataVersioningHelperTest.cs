using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;
using Moq;

using Hatfield.EnviroData.Core;
using Hatfield.EnviroData.WQDataProfile;

namespace Hatfield.EnviroData.WQDataProfile.Test
{
    [TestFixture]
    public class DataVersioningHelperTest
    {
        public Hatfield.EnviroData.Core.Action rootActionData;
        public Hatfield.EnviroData.Core.Action childActionData;
        public Hatfield.EnviroData.Core.Action grandActionData;
        
        [Test]
        public void GetNextVersionDataTest()
        {
            var mockDefaultValueProvider = new Mock<IWQDefaultValueProvider>();
            mockDefaultValueProvider.Setup(x => x.ActionRelationshipTypeSubVersion).Returns("is new version of");

            var versionHelper = new DataVersioningHelper(mockDefaultValueProvider.Object);

            CreateTestAction(mockDefaultValueProvider.Object);

            var foundNextVersionData = versionHelper.GetNextVersionActionData(rootActionData);

            Assert.NotNull(foundNextVersionData);
            Assert.AreEqual(foundNextVersionData, childActionData);
            Assert.AreEqual(2, foundNextVersionData.ActionID);
        }

        [Test]
        public void GetLatestVersionDataTest()
        {
            var mockDefaultValueProvider = new Mock<IWQDefaultValueProvider>();
            mockDefaultValueProvider.Setup(x => x.ActionRelationshipTypeSubVersion).Returns("is new version of");

            var versionHelper = new DataVersioningHelper(mockDefaultValueProvider.Object);

            CreateTestAction(mockDefaultValueProvider.Object);

            var foundLatestVersionData = versionHelper.GetLatestVersionActionData(rootActionData);

            Assert.NotNull(foundLatestVersionData);
            Assert.AreEqual(foundLatestVersionData, grandActionData);
            Assert.AreEqual(3, foundLatestVersionData.ActionID);
        }
                
        public void CreateTestAction(IWQDefaultValueProvider wqDefaultValueProvider)
        {

            grandActionData = new Hatfield.EnviroData.Core.Action();
            grandActionData.ActionID = 3;

            childActionData = new Hatfield.EnviroData.Core.Action();
            childActionData.RelatedActions = new List<RelatedAction>();
            childActionData.ActionID = 2;
                        
            rootActionData = new Hatfield.EnviroData.Core.Action();
            rootActionData.RelatedActions = new List<RelatedAction>();
            rootActionData.ActionID = 1;

            var grandChildRelation = new RelatedAction();
            grandChildRelation.CV_RelationshipType = new CV_RelationshipType { 
                Name = wqDefaultValueProvider.ActionRelationshipTypeSubVersion
            };
            grandChildRelation.Action = grandActionData;
            childActionData.RelatedActions.Add(grandChildRelation);

            var childRelation = new RelatedAction();
            childRelation.CV_RelationshipType = new CV_RelationshipType
            {
                Name = wqDefaultValueProvider.ActionRelationshipTypeSubVersion
            };
            childRelation.Action = childActionData;
            rootActionData.RelatedActions.Add(childRelation);            
        }
    }
}
