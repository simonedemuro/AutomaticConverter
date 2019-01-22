using MagicViewModelConverter.ConversionMethods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MagicViewModelConverter
{
    public class MagicClone<S, T>
    {
        private S srcObj;
        private T tgtObj;

        private Dictionary<string, Func<Object, Object>> ConvertionBehaviors;

        public MagicClone(S srcObj, T trgObj)
        {
            ConvertionBehaviors = new Dictionary<string, Func<object, object>>();
            this.srcObj = srcObj;
            this.tgtObj = trgObj;
        }

        public void AddConvertionBehavior(string key, Func<Object, Object> func) =>
            this.ConvertionBehaviors.Add(key, func);

        public void AddConvertionBehaviors(Dictionary<string, Func<Object, Object>> ConvertionBehaviors) =>
            this.ConvertionBehaviors = ConvertionBehaviors;


        public T DoTheMagic()
        {
            Type SrcType = srcObj.GetType();
            PropertyInfo[] SrcProperties = SrcType.GetProperties();

            Type TrgType = tgtObj.GetType();
            PropertyInfo[] TrgProps = TrgType.GetProperties();

            foreach (var srcProp in SrcProperties)
            {
                var SourceVal = srcProp.GetValue(srcObj);
                Type SourceType = srcProp.PropertyType;

                Type TargetType = TrgProps.FirstOrDefault(x => x.Name == srcProp.Name).PropertyType;
                PropertyInfo Trgprop = TrgProps.FirstOrDefault(x => x.Name == srcProp.Name);

                string ConvBehaviorKey = GetConvBehaviorKey(SourceType, TargetType);
                if (SourceType == TargetType)
                {
                    Trgprop.SetValue(tgtObj, SourceVal);
                }
                else if (ConvertionBehaviors.TryGetValue(ConvBehaviorKey, out Func<object, object> Matchingfunction))
                {
                    object convertedProperty = Matchingfunction(SourceVal);
                    Trgprop.SetValue(tgtObj, convertedProperty);
                }
                else
                {
                    LifebuoyRing(SourceVal);
                }
            }
            return tgtObj;
        }



        private void LifebuoyRing(object SourceVal)
        {
            try
            {
                object c = GenericCast.Cast(tgtObj.GetType(), SourceVal);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static string GetConvBehaviorKey(Type SourceType, Type TargetType) =>
            SourceType.ToString() + "->" + TargetType.ToString();

    }
}
