using MagicViewModelConverter.ConversionMethods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MagicViewModelConverter
{
    public class MagicCloneConverter<Tsource, TTarget> where TTarget : new()
    {
        private readonly List<IConversionBehaviour> behaviours = new List<IConversionBehaviour>();
        
        internal MagicCloneConverter(List<IConversionBehaviour> behaviours)
        {
            this.behaviours = behaviours;
        }

        public TTarget DoTheMagic(Tsource srcObj)
        {
            var a = new TTarget();
            Type SrcType = srcObj.GetType();
            PropertyInfo[] SrcProperties = SrcType.GetProperties();
            var tgtObj = new TTarget();
            Type TrgType = tgtObj.GetType();
            PropertyInfo[] TrgProps = TrgType.GetProperties();

            foreach (var srcProp in SrcProperties)
            {
                var SourceVal = srcProp.GetValue(srcObj);
                Type SourceType = srcProp.PropertyType;

                Type TargetType = TrgProps.FirstOrDefault(x => x.Name == srcProp.Name).PropertyType;
                PropertyInfo Trgprop = TrgProps.FirstOrDefault(x => x.Name == srcProp.Name);

                var customBehaviour = this.behaviours.Where(
                    b => b.SourceType == SourceType && b.TargetType == TargetType).SingleOrDefault();
                bool hasCustomBehaviour = customBehaviour != null;
                if (SourceType == TargetType)
                {
                    Trgprop.SetValue(tgtObj, SourceVal);
                }
                else if (hasCustomBehaviour)
                {
                    object convertedProperty = customBehaviour.ConvertObjects(SourceVal);
                    Trgprop.SetValue(tgtObj, convertedProperty);
                }
            }
            return tgtObj;
        }
    }
}
