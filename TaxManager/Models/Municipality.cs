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

        public bool AddScheduledTax(decimal tax, DateTime start, TimeSpan duration)
        {

            ScheduledTax scheduledTax = new ScheduledTax()
            {
                Tax = tax,
                Start = start,
                Duration = duration
            };

            if (!IsValidStartAndDuration(start, duration))
                return false;

            int index = Taxes.BinarySearch(scheduledTax,
                new ScheduledTaxComparer());
            if (index < 0)
                index = ~index;
            Taxes.Insert(index, scheduledTax);

            return true;
        }

        class ScheduledTaxComparer : IComparer<ScheduledTax>
        {
            public int Compare(ScheduledTax a, ScheduledTax b)
            {
                return a.Duration.CompareTo(b.Duration);
            }
        }

        private bool IsValidStartAndDuration(DateTime start, TimeSpan duration)
        {
            double days = duration.TotalDays;

            if (days % 1 != 0)
                return false;

            if (days == 1)
                return true;

            if (days == 7)
                return start.DayOfWeek == DayOfWeek.Monday;

            if (DateTime.DaysInMonth(start.Year, start.Month) == days)
                return start.Day == 1;

            if (days == 365)
                return start.DayOfYear == 1;

            if (days == 366 && DateTime.IsLeapYear(start.Year))
                return start.DayOfYear == 1;

            return false;
        }
    }
}
