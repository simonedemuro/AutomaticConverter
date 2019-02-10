using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicViewModelConverter
{
    public class MagicClone 
    {
        public static MagicCloneBuilder<TSource, TTarget> New<TSource, TTarget>() where TTarget : new()
        {
            return new MagicCloneBuilder<TSource, TTarget>();
        }
    }
}
