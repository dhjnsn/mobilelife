using System;
using System.Collections.Generic;
using Xunit;
using TaxManager.Models;

namespace TaxManager.Tests
{

    public class MunicipalityTests
    {
        Municipality vilnius;

        public MunicipalityTests()
        {
            vilnius = new Municipality() { name = "Vilnius" };
            vilnius.AddScheduledTax(0.2M, "2016-01-01", "366");
            vilnius.AddScheduledTax(0.1M, "2016-05-09", "7");
            vilnius.AddScheduledTax(0.1M, "2016-01-01", "1");
            vilnius.AddScheduledTax(0.4M, "2016-05-01", "31");
            vilnius.AddScheduledTax(0.1M, "2016-12-25", "1");
        }

        [Fact]
        public void AddScheduledTax_DailyTaxIsFirst()
        {
            Assert.Equal(1, vilnius.taxes[0].duration.TotalDays);
            Assert.Equal(1, vilnius.taxes[1].duration.TotalDays);
        }

        [Fact]
        public void AddScheduledTax_WeeklyTaxIsAfterDailyTax()
        {
            Assert.Equal(7, vilnius.taxes[2].duration.TotalDays);
        }

        [Fact]
        public void AddScheduledTax_MonthlyTaxIsAfterWeeklyTax()
        {
            Assert.Equal(31, vilnius.taxes[3].duration.TotalDays);
        }

        [Fact]
        public void AddScheduledTax_YearlyTaxIsAfterMonthlyTax()
        {
            Assert.Equal(366, vilnius.taxes[4].duration.TotalDays);
        }

    }
}
