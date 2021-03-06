﻿using MagicViewModelConverter.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MagicViewModelConverter
{
    public class MagicConverter
    {
        public string DateFormat { get; set; }
        public string DateTimeFormat { get; set; }

        public MagicConverter(string dateFormat, string dateTimeFormat)
        {
            DateFormat = dateFormat;
            DateTimeFormat = dateTimeFormat;
        }

        public MagicConverter(string dateFormat)
        {
            DateFormat = dateFormat;
        }
        public MagicConverter()
        {
        }

        private static CompatibilityCheckOutcome CompatibilityCheck(PropertyInfo[] SrcProp, PropertyInfo[] TrgProp)
        {
            if (!(SrcProp.Length == TrgProp.Length))
                return new CompatibilityCheckOutcome(false, "The object provided have a different number of public properties");

            int NumOfIdenticalFields = SrcProp.Where(x => TrgProp.Select(y => y.Name).Contains(x.Name)).Count();

            if (!(SrcProp.Length == NumOfIdenticalFields))
                return new CompatibilityCheckOutcome(false, "Seems that the Target object miss some public property of the source");

            return new CompatibilityCheckOutcome(true, "");
        }

        //https://stackoverflow.com/questions/6884653/how-to-make-a-generic-type-cast-function
        public static T ConvertValue<T, U>(U value) where U : IConvertible
        {
            return (T)System.Convert.ChangeType(value, typeof(T));
        }

        public void Convert(object source, object target)
        {
            Type SrcType = source.GetType();
            PropertyInfo[] SrcProperties = SrcType.GetProperties();

            Type TrgType = target.GetType();
            PropertyInfo[] TrgProps = TrgType.GetProperties();

            if (!CompatibilityCheck(SrcProperties, TrgProps).outcome)
            {
                //adesso ci penso...
            }

            foreach (var srcProp in SrcProperties)
            {
                var SourceVal = srcProp.GetValue(source);
                Type SourceType = srcProp.PropertyType;

                Type TargetType = TrgProps.Where(x => x.Name == srcProp.Name).FirstOrDefault().PropertyType;
                PropertyInfo Trgprop = TrgProps.FirstOrDefault(x => x.Name == srcProp.Name);
                ExcplicitConversion(target, SourceVal, SourceType, TargetType, Trgprop);
            }
        }

        private void ExcplicitConversion(object target, object SourceVal, Type SourceType, Type TargetType, PropertyInfo Trgprop)
        {
            if (SourceType == TargetType)
            {
                Trgprop.SetValue(target, SourceVal);
            }
            else if (SourceType == Type.GetType("System.DateTime") && TargetType == Type.GetType("System.String"))
            {
                DateTime dt = (DateTime)SourceVal;
                Trgprop.SetValue(target, dt.ToString(DateFormat));
            }
            else if (SourceType == Type.GetType("System.String") && TargetType == Type.GetType("System.DateTime"))
            {
                string dt = (string)SourceVal;
                DateTime res = new DateTime();

                if (DateTime.TryParseExact(dt.Trim(), DateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out res))
                    Trgprop.SetValue(target, res);
                else
                {
                    string error = "ERROR - parsing string to DateTime (" + dt + " with format: " + DateFormat + " )";
                    throw new FormatException(error);
                }
            }
            else if (SourceType == Type.GetType("System.Int32") && TargetType == Type.GetType("System.DateTime"))
            {
            }
            else
            {
                
                //Trgprop.SetValue(target, (typeof(target))SourceVal);
                throw new UnhandledConversion(SourceType.Name, TargetType.Name);
            }
        }
    }
}
