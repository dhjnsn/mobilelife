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
            vilnius = new Municipality() { Name = "Vilnius" };
            vilnius.AddScheduledTax(0.2M, DateTime.Parse("2016-01-01"),
                TimeSpan.Parse("366"));
            vilnius.AddScheduledTax(0.1M, DateTime.Parse("2016-05-09"),
                TimeSpan.Parse("7"));
            vilnius.AddScheduledTax(0.1M, DateTime.Parse("2016-01-01"),
                TimeSpan.Parse("1"));
            vilnius.AddScheduledTax(0.4M, DateTime.Parse("2016-05-01"),
                TimeSpan.Parse("31"));
            vilnius.AddScheduledTax(0.1M, DateTime.Parse("2016-12-25"),
                TimeSpan.Parse("1"));
        }

        [Fact]
        public void AddScheduledTax_DailyTaxIsFirst()
        {
            Assert.Equal(1, vilnius.Taxes[0].Duration.TotalDays);
            Assert.Equal(1, vilnius.Taxes[1].Duration.TotalDays);
        }

        [Fact]
        public void AddScheduledTax_WeeklyTaxIsAfterDailyTax()
        {
            Assert.Equal(7, vilnius.Taxes[2].Duration.TotalDays);
        }

        [Fact]
        public void AddScheduledTax_MonthlyTaxIsAfterWeeklyTax()
        {
            Assert.Equal(31, vilnius.Taxes[3].Duration.TotalDays);
        }

        [Fact]
        public void AddScheduledTax_YearlyTaxIsAfterMonthlyTax()
        {
            Assert.Equal(366, vilnius.Taxes[4].Duration.TotalDays);
        }

        [Fact]
        public void AddScheduledTax_ReturnsFalse_OnInvalidStartAndDuration()
        {
            Assert.False(new Municipality()
                .AddScheduledTax(0M, DateTime.Now, TimeSpan.FromDays(20)));
            Assert.False(new Municipality()
                .AddScheduledTax(0M, DateTime.Now, new TimeSpan(25,0,0)));
        }

        [Fact]
        public void AddScheduledTax_ReturnsTrue_OnValidStartAndDuration()
        {
            Assert.True(new Municipality()
                .AddScheduledTax(0M, DateTime.Now, TimeSpan.FromDays(1)));
            Assert.True(new Municipality().AddScheduledTax(0M,
                new DateTime(2017,5,8), TimeSpan.FromDays(7)));
            Assert.True(new Municipality().AddScheduledTax(0M,
                new DateTime(2017,2,1), TimeSpan.FromDays(28)));
            Assert.True(new Municipality().AddScheduledTax(0M,
                new DateTime(2016,2,1), TimeSpan.FromDays(29)));
            Assert.True(new Municipality().AddScheduledTax(0M,
                new DateTime(2015,1,1), TimeSpan.FromDays(365)));
            Assert.True(new Municipality().AddScheduledTax(0M,
                new DateTime(2000,1,1), TimeSpan.FromDays(366)));
        }

        [Fact]
        public void GetTaxOnDate_VilniusOn20160101()
        {
            decimal result = vilnius.GetTaxOnDate(new DateTime(2016,1,1));
            Assert.Equal(0.1, Convert.ToDouble(result), 2);
        }

        [Fact]
        public void GetTaxOnDate_VilniusOn20160102()
        {
            decimal result = vilnius.GetTaxOnDate(new DateTime(2016,1,2));
            Assert.Equal(0.2, Convert.ToDouble(result), 2);
        }

        [Fact]
        public void GetTaxOnDate_VilniusOn20160502()
        {
            decimal result = vilnius.GetTaxOnDate(new DateTime(2016,5,02));
            Assert.Equal(0.4, Convert.ToDouble(result), 2);
        }

        [Fact]
        public void GetTaxOnDate_VilniusOn20160710()
        {
            decimal result = vilnius.GetTaxOnDate(new DateTime(2016,7,10));
            Assert.Equal(0.2, Convert.ToDouble(result), 2);
        }

        [Fact]
        public void GetTaxOnDate_VilniusOn20160316()
        {
            decimal result = vilnius.GetTaxOnDate(new DateTime(2016,3,16));
            Assert.Equal(0.2, Convert.ToDouble(result), 2);
        }
    }
}
