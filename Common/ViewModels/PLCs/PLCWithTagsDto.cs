using SCADA.Common.ViewModels.Tags;

namespace SCADA.Common.ViewModels.PLCs;

public class PLCWithTagsDto : PLCDto
{
    public List<TagDto> Tags { get; set; } = new();
}