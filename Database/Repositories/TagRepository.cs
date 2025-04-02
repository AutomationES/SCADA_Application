using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCADA.Common.Interfaces;
using SCADA.Common.Models;
using SCADA.Common.ViewModels.Tags; // Add this for DTOs
using SCADA.Database.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace SCADA.Database.Repositories
{
    public class TagRepository : ITagRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<TagRepository> _logger;
        public TagRepository(ApplicationDbContext context) => _context = context;
        public async Task<IEnumerable<TagDto>> GetAllTagsAsync()
        {
            try
            {
                return await _context.Tags
                    .Select(t => new TagDto { /* ... */ })
                    .AsNoTracking()
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching tags");
                throw;  // Let global error handler manage it
            }
        }
        //public async Task<IEnumerable<TagDto>> GetAllTagsAsync()
        //{
        //    return await _context.Tags
        //        .Select(t => new TagDto
        //        {
        //            Id = t.Id,
        //            Name = t.Name,
        //            Address = t.Address,
        //            DataType = t.DataType,
        //            PlcId = t.PlcId
        //        })
        //        .AsNoTracking()
        //        .ToListAsync();
        //}

        public async Task<IEnumerable<TagDetailsDto>> GetTagsByPLCIdAsync(int plcId)
        {
            return await _context.Tags
                .Where(t => t.PlcId == plcId)
                .Join(_context.PLCs,
                    tag => tag.PlcId,
                    plc => plc.Id,
                    (tag, plc) => new TagDetailsDto
                    {
                        Id = tag.Id,
                        Name = tag.Name,
                        Address = tag.Address,
                        DataType = tag.DataType,
                        PlcId = tag.PlcId,
                        PLCName = plc.Name,
                        PLCIpAddress = plc.IPAddress,
                        ProtocolType = plc.ProtocolType.ToString()
                    })
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<TagDto?> GetTagByIdAsync(int id)
        {
            return await _context.Tags
                .Where(t => t.Id == id)
                .Select(t => new TagDto
                {
                    Id = t.Id,
                    Name = t.Name,
                    Address = t.Address,
                    DataType = t.DataType,
                    PlcId = t.PlcId
                })
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }

        public async Task AddTagAsync(Tag tag)
        {
            _context.Tags.Add(tag);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateTagAsync(Tag tag)
        {
            _context.Entry(tag).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteTagAsync(int id)
        {
            var tag = await _context.Tags.FindAsync(id);
            if (tag != null)
            {
                _context.Tags.Remove(tag);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> TagExistsAsync(int id)
        {
            return await _context.Tags.AnyAsync(t => t.Id == id);
        }

        // Additional method to get full details when needed
        public async Task<TagDetailsDto?> GetTagDetailsAsync(int id)
        {
            return await _context.Tags
                .Where(t => t.Id == id)
                .Join(_context.PLCs,
                    tag => tag.PlcId,
                    plc => plc.Id,
                    (tag, plc) => new TagDetailsDto
                    {
                        Id = tag.Id,
                        Name = tag.Name,
                        Address = tag.Address,
                        DataType = tag.DataType,
                        PlcId = tag.PlcId,
                        PLCName = plc.Name,
                        PLCIpAddress = plc.IPAddress,
                        ProtocolType = plc.ProtocolType.ToString()
                    })
                .FirstOrDefaultAsync();
        }
    }
}