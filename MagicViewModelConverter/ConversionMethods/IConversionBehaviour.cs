using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicViewModelConverter.ConversionMethods
{
    public interface IConversionBehaviour
    {
        Type SourceType { get; }
        Type TargetType { get; }
        object ConvertObjects(object source);
    }
}
