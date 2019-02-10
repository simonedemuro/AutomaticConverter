using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicViewModelConverter.ConversionMethods
{
    public class ConversionBehaviour<TSource, TTarget> : IConversionBehaviour
    {
        private readonly Func<TSource, TTarget> conversion;

        public ConversionBehaviour(Func<TSource, TTarget> conversion)
        {
            this.conversion = conversion;
        }
        public Type SourceType
        {
            get
            {
                return typeof(TSource);
            }
        }

        public Type TargetType
        {
            get
            {
                return typeof(TTarget);
            }
        }


        public object ConvertObjects(object source)
        {
            return this.conversion((TSource)source);
        }
    }
}
