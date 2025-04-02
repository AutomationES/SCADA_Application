using SCADA.Common.Models;
using SCADA.Common.Enums;  // Add this using directive
using SCADA.Database.Data;

namespace SCADA.Database.Seeders;

public static class DatabaseSeeder
{
    public static async Task SeedAsync(ApplicationDbContext context)
    {
        if (!context.PLCs.Any())
        {
            var plcs = new List<PLC>
            {
                new PLC {
                    Name = "Main PLC",
                    IPAddress = "192.168.1.10",
                    Port = 502,
                    ProtocolType = ProtocolType.ModbusTCP,  // Now properly recognized
                    PollingInterval = 1000,
                    IsActive = true
                },
                new PLC {
                    Name = "Backup PLC",
                    IPAddress = "192.168.1.20",
                    Port = 102,
                    ProtocolType = ProtocolType.SiemensS7,  // Now properly recognized
                    PollingInterval = 1000,
                    IsActive = true
                }
            };

            await context.PLCs.AddRangeAsync(plcs);
            await context.SaveChangesAsync();
        }
    }
}