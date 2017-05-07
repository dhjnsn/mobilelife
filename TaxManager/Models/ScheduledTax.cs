using System;

namespace TaxManager.Models
{
    public class ScheduledTax
    {
        public decimal Tax { get; set; }
        public DateTime Start { get; set; }
        public TimeSpan Duration { get; set; }

        internal bool IncludesDate(DateTime date)
        {
            return (Start <= date) && (date < Start + Duration);
        }
    }
}
