using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SCADA.Common.Enums;

namespace SCADA.Common.Models
{
    public class PLC
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string IPAddress { get; set; } = string.Empty;
        public int Port { get; set; }
        //public string ProtocolType { get; set; } = "Modbus"; // Default
        public ProtocolType ProtocolType { get; set; } = ProtocolType.ModbusTCP;
        public int PollingInterval { get; set; } = 1000; // ms
        public bool IsActive { get; set; } = true;

        public ICollection<Tag> Tags { get; set; } = new List<Tag>();

    }
}
