namespace SCADA.Common.ViewModels.Tags;

public class TagDetailsDto : TagDto
{
    public string PLCName { get; set; } = string.Empty;
    public string PLCIpAddress { get; set; } = string.Empty;
    public string ProtocolType { get; set; } = string.Empty;
}