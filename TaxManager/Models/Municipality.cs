using System;
using System.Collections.Generic;

namespace TaxManager.Models
{
    public class Municipality
    {
        public Municipality()
        {
            taxes = new List<ScheduledTax>();
        }

        public string name;
        public List<ScheduledTax> taxes;
    }
}