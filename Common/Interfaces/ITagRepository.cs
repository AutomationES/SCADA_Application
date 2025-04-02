using SCADA.Common.Models;
using SCADA.Common.ViewModels.Tags;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCADA.Common.Interfaces
{
    public interface ITagRepository
    {
        Task<IEnumerable<TagDto>> GetAllTagsAsync();
        Task<IEnumerable<TagDetailsDto>> GetTagsByPLCIdAsync(int plcId);
        Task<TagDto?> GetTagByIdAsync(int id);
        Task<TagDetailsDto?> GetTagDetailsAsync(int id);
        Task AddTagAsync(Tag tag);
        Task UpdateTagAsync(Tag tag);
        Task DeleteTagAsync(int id);
        // In SCADA.Common/Interfaces/ITagRepository.cs
        Task<bool> TagExistsAsync(int id);
    }
}
