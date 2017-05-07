using System;
using System.Collections.Generic;

namespace TaxManager.Models
{

    public class Municipality
    {

        public string Name { get; set; }
        public List<ScheduledTax> Taxes { get; set; }

        public Municipality()
        {
            Taxes = new List<ScheduledTax>();
        }
        public decimal GetTaxOnDate(DateTime date)
        {
            foreach (ScheduledTax scheduledTax in Taxes)
                if (scheduledTax.IncludesDate(date))
                    return scheduledTax.Tax;

            // No tax scheduled for date => no tax is levied.
            return 0M;
        }

        public void AddScheduledTax(decimal tax, string start, string duration)
        {

            ScheduledTax scheduledTax = new ScheduledTax()
            {
                Tax = tax,
                Start = DateTime.Parse(start),
                Duration = TimeSpan.Parse(duration)
            };

            int index = Taxes.BinarySearch(scheduledTax,
                new ScheduledTaxComparer());
            if (index < 0)
                index = ~index;
            Taxes.Insert(index, scheduledTax);
        }

        class ScheduledTaxComparer : IComparer<ScheduledTax>
        {
            public int Compare(ScheduledTax a, ScheduledTax b)
            {
                return a.Duration.CompareTo(b.Duration);
            }
        }
    }
}
