using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

using NUnit.Framework;
using Moq;

using Hatfield.EnviroData.Core;

namespace Hatfield.EnviroData.DataProfile.WQ.Test
{
    [TestFixture]
    public class ProcessingLevelHelperTest
    {
        private ODM2Entities MockDbContext()
        {
            var queryable = (new List<ProcessingLevel> 
            { 
                new ProcessingLevel
                {
                    ProcessingLevelCode = "Draft"
                },
                new ProcessingLevel
                {
                    ProcessingLevelCode = "Provisional"
                },
                new ProcessingLevel
                {
                    ProcessingLevelCode = "Finalized"
                }
            }).AsQueryable();
            var dbSet = new Mock<DbSet<ProcessingLevel>>();
            dbSet.As<IQueryable<ProcessingLevel>>().Setup(m => m.Provider).Returns(queryable.Provider);
            dbSet.As<IQueryable<ProcessingLevel>>().Setup(m => m.Expression).Returns(queryable.Expression);
            dbSet.As<IQueryable<ProcessingLevel>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
            dbSet.As<IQueryable<ProcessingLevel>>().Setup(m => m.GetEnumerator()).Returns(queryable.GetEnumerator());

            var mockContext = new Mock<ODM2Entities>();
            mockContext.Setup(x => x.ProcessingLevels).Returns(dbSet.Object);

            return mockContext.Object;
        
        }

        [Test]
        [TestCase(Hatfield.EnviroData.DataProfile.WQ.Models.ProcessingLevel.Draft, "Draft")]
        [TestCase(Hatfield.EnviroData.DataProfile.WQ.Models.ProcessingLevel.Provisional, "Provisional")]
        [TestCase(Hatfield.EnviroData.DataProfile.WQ.Models.ProcessingLevel.Finalized, "Finalized")]
        public void GetProcessingLevelTest(Hatfield.EnviroData.DataProfile.WQ.Models.ProcessingLevel levelToTest, string actualLevelCode)
        { 
            var context = MockDbContext();
            var foundProcessingLevel = ProcessingLevelHelper.GetOdm2ProcessingLevel(levelToTest, context);

            Assert.NotNull(foundProcessingLevel);
            Assert.AreEqual(actualLevelCode, foundProcessingLevel.ProcessingLevelCode);
        }
    }
}
