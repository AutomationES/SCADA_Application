using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SCADA.Common.Enums;

namespace SCADA.Common.Models
{
    public class Tag
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        //public string DataType { get; set; } = "Int32";
        public DataType DataType { get; set; } = DataType.Int32;
        public int PlcId { get; set; }
        // Navigation property
        //public PLC PLC { get; set; } = null!;
        public bool LogHistory { get; set; }
        public int HistoryInterval { get; set; } = 1000; // ms
        public bool HasAlarm { get; set; }
        public string? AlarmCondition { get; set; }
        public double? AlarmThreshold { get; set; }
        public string? AlarmMessage { get; set; }
    }
}
