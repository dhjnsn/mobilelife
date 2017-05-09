using System;
using System.Linq;
using System.Collections.Generic;

namespace TaxManager.Models
{

    public class Municipality
    {

        public string Name { get; set; }
        public List<ScheduledTax> DailyTaxes { get; set; }
        public List<ScheduledTax> WeeklyTaxes { get; set; }
        public List<ScheduledTax> MonthlyTaxes { get; set; }
        public List<ScheduledTax> YearlyTaxes { get; set; }

        public Municipality()
        {
            DailyTaxes = new List<ScheduledTax>();
            WeeklyTaxes = new List<ScheduledTax>();
            MonthlyTaxes = new List<ScheduledTax>();
            YearlyTaxes = new List<ScheduledTax>();
        }

        public decimal GetTaxOnDate(DateTime date)
        {
            IEnumerable<ScheduledTax> taxes =  DailyTaxes.Concat(WeeklyTaxes)
                .Concat(MonthlyTaxes).Concat(YearlyTaxes);

            foreach (ScheduledTax scheduledTax in taxes)
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

            if (IsDailyTax(scheduledTax))
                UpdateScheduledTaxList(DailyTaxes, scheduledTax);
            else if (IsWeeklyTax(scheduledTax))
                UpdateScheduledTaxList(WeeklyTaxes, scheduledTax);
            else if (IsMonthlyTax(scheduledTax))
                UpdateScheduledTaxList(MonthlyTaxes, scheduledTax);
            else if (IsYearlyTax(scheduledTax))
                UpdateScheduledTaxList(YearlyTaxes, scheduledTax);
            else 
                return false;

            return true;
        }

        private void UpdateScheduledTaxList(List<ScheduledTax> list, ScheduledTax tax)
        {
            int index = list.FindIndex(x => x.Start == tax.Start);
            if (index != -1)
                list[index] = tax;
            else
                list.Add(tax);
        }

        public bool IsDailyTax(ScheduledTax tax)
        {
            return tax.Duration.TotalDays == 1;
        }

        public bool IsWeeklyTax(ScheduledTax tax)
        {
            return tax.Duration.TotalDays == 7 &&
                tax.Start.DayOfWeek == DayOfWeek.Monday;
        }

        public bool IsMonthlyTax(ScheduledTax tax)
        {
            return tax.Start.Day == 1 && tax.Duration.TotalDays ==
                DateTime.DaysInMonth(tax.Start.Year, tax.Start.Month);
        }

        public bool IsYearlyTax(ScheduledTax tax)
        {
            return tax.Start.DayOfYear == 1 && (tax.Duration.TotalDays == 365 ||
                (tax.Duration.TotalDays == 366 &&
                 DateTime.IsLeapYear(tax.Start.Year)));
        }
    }
}
