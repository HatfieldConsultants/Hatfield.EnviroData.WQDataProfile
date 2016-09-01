using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Hatfield.EnviroData.DataProfile.WQ.Builders;
using Hatfield.EnviroData.DataProfile.WQ.Models;

namespace Hatfield.EnviroData.DataProfile.WQ
{
    public class DomainBuilderFactory
    {
        private DomainBuilderFactory() { }

        static readonly Dictionary<Type, Func<IDomainBuilder>> _dict
             = new Dictionary<Type, Func<IDomainBuilder>>();

        public static IDomainBuilder Create(Type type)
        {
            Func<IDomainBuilder> constructor = null;
            if (_dict.TryGetValue(type, out constructor))
                return constructor();

            throw new ArgumentException("No type registered for this type");
        }

        public static void Register(Type type, Func<IDomainBuilder> ctor)
        {
            _dict.Add(type, ctor);
        }


        public static void DefaultSetUp()
        { 
            DomainBuilderFactory.Register(typeof(Site), () => new SiteDomainBuilder());
            DomainBuilderFactory.Register(typeof(LabReportSample), () => new WaterQualityObservationBuilder());
        }
    }
}
