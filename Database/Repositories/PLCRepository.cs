using Microsoft.EntityFrameworkCore;
using SCADA.Common.Interfaces;
using SCADA.Common.Models;
using SCADA.Database.Data;

namespace SCADA.Database.Repositories;

public class PLCRepository : IPLCRepository
{
    private readonly ApplicationDbContext _context;

    public PLCRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<PLC>> GetAllPLCsAsync(bool includeTags = false)
    {
        IQueryable<PLC> query = _context.PLCs;

        if (includeTags)
        {
            query = query.Include(p => p.Tags);
        }

        return await query
            .OrderBy(p => p.Name)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<PLC?> GetPLCByIdAsync(int id, bool includeTags = false)
    {
        IQueryable<PLC> query = _context.PLCs;

        if (includeTags)
        {
            query = query.Include(p => p.Tags);
        }

        return await query
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<PLC?> GetPLCByIpAsync(string ipAddress)
    {
        return await _context.PLCs
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.IPAddress == ipAddress);
    }

    public async Task AddPLCAsync(PLC plc)
    {
        _context.PLCs.Add(plc);
        await _context.SaveChangesAsync();
    }

    public async Task UpdatePLCAsync(PLC plc)
    {
        _context.PLCs.Update(plc);
        await _context.SaveChangesAsync();
    }

    public async Task DeletePLCAsync(int id)
    {
        var plc = await _context.PLCs.FindAsync(id);
        if (plc != null)
        {
            _context.PLCs.Remove(plc);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<bool> PLCExistsAsync(int id)
    {
        return await _context.PLCs.AnyAsync(p => p.Id == id);
    }

    public async Task<bool> IpAddressExistsAsync(string ipAddress)
    {
        return await _context.PLCs.AnyAsync(p => p.IPAddress == ipAddress);
    }
}