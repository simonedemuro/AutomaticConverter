using MagicViewModelConverter.ConversionMethods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicViewModelConverter
{
    public class MagicCloneBuilder<TSource, TTarget> where TTarget : new()
    {
        private readonly List<IConversionBehaviour> behaviours = new List<IConversionBehaviour>();

        public MagicCloneBuilder<TSource, TTarget> AddBehaviour<T1, T2>(Func<T1, T2> conversionFunction)
        {
            this.behaviours.Add(new ConversionBehaviour<T1, T2>(conversionFunction));
            return this;
        }

        public MagicCloneConverter<TSource, TTarget> Create()
        {
            return new MagicCloneConverter<TSource, TTarget>(this.behaviours);
        }
    }
}
