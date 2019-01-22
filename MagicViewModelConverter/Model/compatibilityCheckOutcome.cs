using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicViewModelConverter.Model
{
    public class CompatibilityCheckOutcome
    {
        public bool outcome;
        public string message;

        public CompatibilityCheckOutcome(bool outcome, string message)
        {
            this.outcome = outcome;
            this.message = message;
        }
    }
}
