﻿using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Moq;

using Hatfield.EnviroData.Core;

namespace Hatfield.EnviroData.DataProfile.WQ.Test
{
    public class InMemoryDatabaseGenerator
    {
        private Mock<DbSet<T>> ToDbSet<T>(List<T> sourceList) where T : class
        {
            var queryable = sourceList.AsQueryable();

            var dbSet = new Mock<DbSet<T>>();
            dbSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryable.Provider);
            dbSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryable.Expression);
            dbSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
            dbSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(queryable.GetEnumerator());
            dbSet.Setup(d => d.Add(It.IsAny<T>())).Callback<T>(sourceList.Add);
            return dbSet;
        }


        private DbSet<Site> CreateMockSiteDb()
        {
            var data = new List<Site> { 
                new Site{
                    Latitude = 123.4,
                    Longitude = 234.5
                }
            };

            var mockSiteDbSet = ToDbSet<Site>(data);

            //add more custom query mock here

            return mockSiteDbSet.Object;
        
        }

        private DbSet<Variable> CreateMockVariableDb()
        {
            var data = new List<Variable> { 
                new Variable{
                    VariableCode = "pH"
                },
                new Variable{
                    VariableCode = "Aluminum (Al)-Dissolved"
                },
                new Variable{
                    VariableCode = "Zinc (Zn)-Dissolved"
                }
            };

            var mockSiteDbSet = ToDbSet<Variable>(data);

            //add more custom query mock here

            return mockSiteDbSet.Object;
        }



        public ODM2Entities CreateTestDbContext()
        {
            var mockDbContext = new Mock<ODM2Entities>();

            mockDbContext.Setup(x => x.Sites).Returns(CreateMockSiteDb());
            //mockDbContext.Setup(x => x.Variables).Returns(CreateMockVariableDb());
            return mockDbContext.Object;
        }
    }
}
