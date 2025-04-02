namespace SCADA.Frontend.ViewModels;

public class PLCViewModel
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string IPAddress { get; set; } = string.Empty;
    public int Port { get; set; }
    public string ProtocolType { get; set; } = string.Empty;
    public bool IsActive { get; set; }

    // Optional: Include related tags if needed
    public List<TagViewModel>? Tags { get; set; }
}