using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicViewModelConverter.Test.Mock
{
    public class DB_OBJ
    {
        public long Id { get; set; }
        public int Field01 { get; set; }
        public DateTime Timestamp { get; set; }

        private int IWillNotBeConsidered;
        public void IWillNotBeConsideredBecauseImAMethodAAA() { }
    }
}
