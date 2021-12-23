using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TreasuryShadowSystem.Model
{
    public class WeeklyDay
    {
        public string WeekDay { get; set; }
        public DateTime WeekDate { get; set; }
        public int Day { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public string ColumnDay { get; set; }
        public string ColumnMonth { get; set; }
        public string ColumnYear { get; set; }
        public int Sheet { get; set; }
        public Decimal Capital { get; set; }
    }
}
