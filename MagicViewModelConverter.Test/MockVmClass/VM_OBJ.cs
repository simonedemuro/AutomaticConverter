using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicViewModelConverter.Test.Mock
{ 
    public class VM_OBJ
    {
        public long Id { get; set; }
        public double Field01 { get; set; }
        public string Timestamp { get; set; }

        private string IWillNotBeConsidered2;
        public void IWillNotBeConsideredBecauseImAMethod() { }
    }
}
