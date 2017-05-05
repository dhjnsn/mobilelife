using System;
using System.Collections.Generic;

namespace TaxManager.Models
{

    public class Municipality
    {

        public string name;
        public List<ScheduledTax> taxes;

        public Municipality()
        {
            taxes = new List<ScheduledTax>();
        }

        public decimal GetTaxOnDate(DateTime date)
        {
            foreach (ScheduledTax scheduledTax in taxes)
                if (scheduledTax.IncludesDate(date))
                    return scheduledTax.tax;

            // No tax scheduled for date => no tax is levied.
            return 0M;
        }

        public void AddScheduledTax(decimal tax, string start, string duration)
        {

            ScheduledTax scheduledTax = new ScheduledTax()
            {
                tax = tax,
                start = DateTime.Parse(start),
                duration = TimeSpan.Parse(duration)
            };

            int index = taxes.BinarySearch(scheduledTax, new ScheduledTaxComparer());
            if (index < 0)
                index = ~index;
            taxes.Insert(index, scheduledTax);
        }

        class ScheduledTaxComparer : IComparer<ScheduledTax>
        {
            public int Compare(ScheduledTax a, ScheduledTax b)
            {
                return a.duration.CompareTo(b.duration);
            }
        }
    }
}
