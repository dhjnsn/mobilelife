using System;

namespace TaxManager.Models
{
    public class ScheduledTax
    {
        public decimal tax;
        public DateTime start;
        public TimeSpan duration;
    }
}