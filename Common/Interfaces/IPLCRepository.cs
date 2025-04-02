using SCADA.Common.Models;

namespace SCADA.Common.Interfaces;

public interface IPLCRepository
{
    Task<IEnumerable<PLC>> GetAllPLCsAsync(bool includeTags = false);
    Task<PLC?> GetPLCByIdAsync(int id, bool includeTags = false);
    Task<PLC?> GetPLCByIpAsync(string ipAddress);
    Task AddPLCAsync(PLC plc);
    Task UpdatePLCAsync(PLC plc);
    Task DeletePLCAsync(int id);
    Task<bool> PLCExistsAsync(int id);
    Task<bool> IpAddressExistsAsync(string ipAddress);
}